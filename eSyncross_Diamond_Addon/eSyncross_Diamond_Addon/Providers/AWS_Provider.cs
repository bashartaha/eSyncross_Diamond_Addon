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
        private const string AwsConfigViewName = "ESY_VW_AWSConfig";
        private const string FileNamePrefix = "stock";
        private const string CountersFolderPrefix =
            "ApiPushData/c2024ecd-791f-4861-a70d-b6367aed967f_tenant/bc393926-be05-455e-becc-39cfe5526544_connection/counters";
        private const string StoresFolderPrefix =
            "ApiPushData/c2024ecd-791f-4861-a70d-b6367aed967f_tenant/bc393926-be05-455e-becc-39cfe5526544_connection/erp_stores";

        private const string AvailableStockQuery =
            "SELECT * FROM [dbo].[ESY_RIDF_AvailableStockQuery]";
        private const string CountersQuery =
            "SELECT * FROM [dbo].[ESY_RIDF_CountersQuery]";
        private const string StoresQuery =
            "SELECT * FROM [dbo].[ESY_RIDF_StoresQuery]";

        internal static CsvUploadResult UploadCounters()
        {
            return UploadTwoColumnCsv(
                CountersQuery,
                "counters.csv",
                CountersFolderPrefix,
                "counter_code",
                "counter_name");
        }

        internal static CsvUploadResult UploadStores()
        {
            return UploadTwoColumnCsv(
                StoresQuery,
                "stores.csv",
                StoresFolderPrefix,
                "code",
                "name");
        }

        internal static StockUploadResult UploadAvailableStock()
        {
            AwsConfiguration configuration = LoadConfiguration();
            string fileName = string.Format(
                CultureInfo.InvariantCulture,
                "{0}.csv",
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

        private static CsvUploadResult UploadTwoColumnCsv(
            string query,
            string fileName,
            string folderPrefix,
            string firstColumn,
            string secondColumn)
        {
            AwsConfiguration configuration = LoadConfiguration();
            string temporaryFilePath = Path.Combine(Path.GetTempPath(), fileName);

            try
            {
                Recordset recordset = null;
                int rowCount = 0;

                try
                {
                    recordset = B1Provider.oRecordset(query);

                    using (StreamWriter writer = new StreamWriter(
                        temporaryFilePath,
                        false,
                        new UTF8Encoding(false)))
                    using (CsvWriter csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteField(firstColumn);
                        csv.WriteField(secondColumn);
                        csv.NextRecord();

                        while (!recordset.EoF)
                        {
                            csv.WriteField(GetText(recordset, firstColumn));
                            csv.WriteField(GetText(recordset, secondColumn));
                            csv.NextRecord();

                            rowCount++;
                            recordset.MoveNext();
                        }
                    }
                }
                finally
                {
                    ReleaseComObject(recordset);
                }

                string objectKey = BuildObjectKey(folderPrefix, fileName);
                UploadFile(configuration, temporaryFilePath, objectKey);
                return new CsvUploadResult(fileName, objectKey, rowCount);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "Unable to create or upload " + fileName + ". " + ex.Message,
                    ex);
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

    internal sealed class CsvUploadResult
    {
        internal CsvUploadResult(string fileName, string objectKey, int rowCount)
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
