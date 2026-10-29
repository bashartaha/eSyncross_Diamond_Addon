using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using CsvHelper;
using SAPbobsCOM;
using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace Diamond_Addon.Providers
{
    internal static class AWS_Provider
    {
        private const string AwsConfigViewName = "ESY_AWSConfig";
        private const string FileNamePrefix = "stocks_template";

        private const string AvailableStockQuery = @"
SELECT
    T0.[DistNumber] AS [SRNo],
    ISNULL(T2.[ItemName], '') AS [ItemDescription],
    ISNULL(T2.[U_ESY_Category], '') AS [CategoryName],
    ISNULL(T2.[U_ESY_JewelleryType], '') AS [JewelryTypeName],
    ISNULL(T0.[U_StockPoint], '') AS [StockPointName],
    ISNULL(T3.[WhsName], '') AS [LocationName],
    ISNULL(T0.[U_TotalWeight], 0) AS [GrossWeight],
    ISNULL(T0.[U_RubyWt], 0) AS [RubyWeight],
    ISNULL(T0.[U_SaphireWt], 0) AS [SaphireWeight],
    ISNULL(T0.[U_EmraldWt], 0) AS [EmaraldWeight],
    ISNULL(T0.[U_OtherStoneWt], 0) AS [OtherStoneWeight],
    ISNULL(T0.[U_DiamondWt], 0) AS [Carat],
    ISNULL(T0.[U_TagPrice], 0) AS [TagPrice],
    T0.[InDate] AS [InwardDate],
    ISNULL(T2.[U_ESY_Design], '') AS [DesignName],
    ISNULL(T2.[U_ESY_Brand], '') AS [BrandName],
    T2.[ItemCode] AS [Alias]
FROM [OSRN] T0
INNER JOIN [OSRQ] T1
    ON T1.[ItemCode] = T0.[ItemCode]
   AND T1.[SysNumber] = T0.[SysNumber]
INNER JOIN [OITM] T2
    ON T2.[ItemCode] = T0.[ItemCode]
LEFT JOIN [OWHS] T3
    ON T3.[WhsCode] = T1.[WhsCode]
WHERE T1.[Quantity] > 0
ORDER BY T0.[DistNumber], T1.[WhsCode]";

        internal static StockUploadResult UploadAvailableStock()
        {
            AwsConfiguration configuration = LoadConfiguration();
            string fileName = string.Format(
                CultureInfo.InvariantCulture,
                "{0}_{1:yyyyMMddHHmmss}.csv",
                FileNamePrefix,
                DateTime.Now);
            string temporaryFilePath = Path.Combine(Path.GetTempPath(), fileName);

            try
            {
                int rowCount = CreateAvailableStockCsv(temporaryFilePath);
                string objectKey = BuildObjectKey(configuration.FolderPrefix, fileName);
                UploadFile(configuration, temporaryFilePath, objectKey);

                return new StockUploadResult(fileName, objectKey, rowCount);
            }
            finally
            {
                TryDeleteTemporaryFile(temporaryFilePath);
            }
        }

        private static AwsConfiguration LoadConfiguration()
        {
            Recordset recordset = null;

            try
            {
                recordset = B1Provider.oRecordset(string.Format(
                    CultureInfo.InvariantCulture,
                    "SELECT TOP 1 [BucketName], [RegionName], [AccessKeyId], [SecretAccessKey], " +
                    "[SessionToken], [FolderPrefix] FROM [dbo].[{0}]",
                    AwsConfigViewName));

                if (recordset.EoF)
                {
                    throw new InvalidOperationException(
                        "AWS configuration view " + AwsConfigViewName + " returned no rows.");
                }

                AwsConfiguration configuration = new AwsConfiguration
                {
                    BucketName = GetText(recordset, "BucketName"),
                    RegionName = GetText(recordset, "RegionName"),
                    AccessKeyId = GetText(recordset, "AccessKeyId"),
                    SecretAccessKey = GetText(recordset, "SecretAccessKey"),
                    SessionToken = GetText(recordset, "SessionToken"),
                    FolderPrefix = GetText(recordset, "FolderPrefix")
                };

                ValidateConfiguration(configuration);
                return configuration;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "Unable to read AWS settings from " + AwsConfigViewName + ". " + ex.Message,
                    ex);
            }
            finally
            {
                ReleaseComObject(recordset);
            }
        }

        private static int CreateAvailableStockCsv(string filePath)
        {
            Recordset recordset = null;
            int rowCount = 0;

            try
            {
                recordset = B1Provider.oRecordset(AvailableStockQuery);

                using (StreamWriter writer = new StreamWriter(
                    filePath,
                    false,
                    new UTF8Encoding(false)))
                using (CsvWriter csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    WriteHeader(csv);

                    while (!recordset.EoF)
                    {
                        csv.WriteField(GetText(recordset, "SRNo"));
                        csv.WriteField(GetText(recordset, "ItemDescription"));
                        csv.WriteField(GetText(recordset, "CategoryName"));
                        csv.WriteField(GetText(recordset, "JewelryTypeName"));
                        csv.WriteField(GetText(recordset, "StockPointName"));
                        csv.WriteField(GetText(recordset, "LocationName"));
                        csv.WriteField(GetNumber(recordset, "GrossWeight"));
                        csv.WriteField(GetNumber(recordset, "RubyWeight"));
                        csv.WriteField(GetNumber(recordset, "SaphireWeight"));
                        csv.WriteField(GetNumber(recordset, "EmaraldWeight"));
                        csv.WriteField(GetNumber(recordset, "OtherStoneWeight"));
                        csv.WriteField(GetNumber(recordset, "Carat"));
                        csv.WriteField(GetNumber(recordset, "TagPrice"));
                        csv.WriteField(GetDate(recordset, "InwardDate"));
                        csv.WriteField(GetText(recordset, "DesignName"));
                        csv.WriteField(GetText(recordset, "BrandName"));
                        csv.WriteField(GetText(recordset, "Alias"));
                        csv.NextRecord();

                        rowCount++;
                        recordset.MoveNext();
                    }
                }

                return rowCount;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "Unable to create the available-stock CSV file. " + ex.Message,
                    ex);
            }
            finally
            {
                ReleaseComObject(recordset);
            }
        }

        private static void WriteHeader(CsvWriter csv)
        {
            csv.WriteField("SRNo");
            csv.WriteField("ItemDescription");
            csv.WriteField("CategoryName");
            csv.WriteField("JewelryTypeName");
            csv.WriteField("StockPointName");
            csv.WriteField("LocationName");
            csv.WriteField("GrossWeight");
            csv.WriteField("RubyWeight");
            csv.WriteField("SaphireWeight");
            csv.WriteField("EmaraldWeight");
            csv.WriteField("OtherStoneWeight");
            csv.WriteField("Carat");
            csv.WriteField("TagPrice");
            csv.WriteField("InwardDate");
            csv.WriteField("DesignName");
            csv.WriteField("BrandName");
            csv.WriteField("Alias");
            csv.NextRecord();
        }

        private static void UploadFile(
            AwsConfiguration configuration,
            string filePath,
            string objectKey)
        {
            using (IAmazonS3 client = CreateS3Client(configuration))
            {
                PutObjectRequest request = new PutObjectRequest
                {
                    BucketName = configuration.BucketName,
                    Key = objectKey,
                    FilePath = filePath,
                    ContentType = "text/csv"
                };

                PutObjectResponse response = client.PutObject(request);
                if (response.HttpStatusCode != HttpStatusCode.OK)
                {
                    throw new InvalidOperationException(
                        "AWS S3 returned HTTP status " + (int)response.HttpStatusCode + ".");
                }
            }
        }

        private static IAmazonS3 CreateS3Client(AwsConfiguration configuration)
        {
            RegionEndpoint region = RegionEndpoint.GetBySystemName(configuration.RegionName);

            if (string.IsNullOrWhiteSpace(configuration.AccessKeyId))
            {
                return new AmazonS3Client(region);
            }

            AWSCredentials credentials = string.IsNullOrWhiteSpace(configuration.SessionToken)
                ? (AWSCredentials)new BasicAWSCredentials(
                    configuration.AccessKeyId,
                    configuration.SecretAccessKey)
                : new SessionAWSCredentials(
                    configuration.AccessKeyId,
                    configuration.SecretAccessKey,
                    configuration.SessionToken);

            return new AmazonS3Client(credentials, region);
        }

        private static void ValidateConfiguration(AwsConfiguration configuration)
        {
            if (string.IsNullOrWhiteSpace(configuration.BucketName))
            {
                throw new InvalidOperationException("BucketName is empty.");
            }

            if (string.IsNullOrWhiteSpace(configuration.RegionName))
            {
                throw new InvalidOperationException("RegionName is empty.");
            }

            bool hasAccessKey = !string.IsNullOrWhiteSpace(configuration.AccessKeyId);
            bool hasSecretKey = !string.IsNullOrWhiteSpace(configuration.SecretAccessKey);

            if (hasAccessKey != hasSecretKey)
            {
                throw new InvalidOperationException(
                    "AccessKeyId and SecretAccessKey must either both be filled or both be empty.");
            }
        }

        private static string BuildObjectKey(string folderPrefix, string fileName)
        {
            string normalizedPrefix = (folderPrefix ?? string.Empty)
                .Trim()
                .Trim('/', '\\');

            return string.IsNullOrEmpty(normalizedPrefix)
                ? fileName
                : normalizedPrefix + "/" + fileName;
        }

        private static string GetText(Recordset recordset, string fieldName)
        {
            object value = recordset.Fields.Item(fieldName).Value;
            return value == null || value == DBNull.Value
                ? string.Empty
                : Convert.ToString(value, CultureInfo.InvariantCulture).Trim();
        }

        private static string GetNumber(Recordset recordset, string fieldName)
        {
            object value = recordset.Fields.Item(fieldName).Value;

            if (value == null || value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return "0";
            }

            return Convert.ToDecimal(value, CultureInfo.InvariantCulture)
                .ToString("0.################", CultureInfo.InvariantCulture);
        }

        private static string GetDate(Recordset recordset, string fieldName)
        {
            object value = recordset.Fields.Item(fieldName).Value;

            if (value == null || value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return string.Empty;
            }

            return Convert.ToDateTime(value, CultureInfo.InvariantCulture)
                .ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        private static void TryDeleteTemporaryFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch
            {
                // The upload result should not be changed by a temporary-file cleanup failure.
            }
        }

        private static void ReleaseComObject(object value)
        {
            if (value != null && Marshal.IsComObject(value))
            {
                Marshal.FinalReleaseComObject(value);
            }
        }

        private sealed class AwsConfiguration
        {
            internal string BucketName { get; set; }
            internal string RegionName { get; set; }
            internal string AccessKeyId { get; set; }
            internal string SecretAccessKey { get; set; }
            internal string SessionToken { get; set; }
            internal string FolderPrefix { get; set; }
        }
    }

    internal sealed class StockUploadResult
    {
        internal StockUploadResult(string fileName, string objectKey, int rowCount)
        {
            FileName = fileName;
            ObjectKey = objectKey;
            RowCount = rowCount;
        }

        internal string FileName { get; private set; }
        internal string ObjectKey { get; private set; }
        internal int RowCount { get; private set; }
    }
}
