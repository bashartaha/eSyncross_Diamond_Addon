using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondAddon
{
   public static class LogProvider
    { 

        public static StringBuilder setupProcessLoggerBuilder = new StringBuilder();
        public static StringBuilder setupErrorProcessLoggerBuilder = new StringBuilder();

        public static int TablesCount = 0, TablesSuccessfulCount = 0;
        public static int FieldsCount = 0, FieldsSuccessfulCount = 0;
        public static int ObjectsCount = 0,ObjectsSuccessfulCount = 0;
        public static int CategoryCount = 0, CategorySuccessfulCount = 0;
        public static int QueryCount = 0, QuerySuccessfulCount = 0;
        public static int FMSCount = 0, FMSSuccessfulCount = 0;
        public static int InitialValuesCount = 0, InitialValuesSuccessfulCount = 0;

        public static void LogTableCreation(string ProcessType,string tableName, bool isCreated,string errorMessage)
        {
            if (isCreated == false)
            { 
                setupProcessLoggerBuilder.AppendLine($"Time: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} , Process Type: {ProcessType} ,Table Name : {tableName} , Status : Failed, Error Message : {errorMessage}");
                setupErrorProcessLoggerBuilder.AppendLine($"Time: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} , Process Type: {ProcessType} ,Table Name : {tableName} , Status : Failed, Error Message : {errorMessage}");
            }
            else
            {
                string status = "Successful";
                if (ProcessType == "Update")
                {
                    status = "Successful - Exists";
                }
                setupProcessLoggerBuilder.AppendLine($"Time: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} , Process Type: {ProcessType} ,Table Name : {tableName} , Status : {status}");
            }
            TablesCount += 1;

            if(isCreated == true)
            {
                TablesSuccessfulCount += 1;
            }
        }

        public static void LogFieldsCreation(string ProcessType, string tableName,string fieldName, bool isCreated, string errorMessage)
        {
            if(isCreated == false)
            {
                setupProcessLoggerBuilder.AppendLine($"Time: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} , Process Type: {ProcessType} ,Field Name : {tableName} , Field Name: {fieldName} , Status : Failed, Error Message : {errorMessage}");
                setupErrorProcessLoggerBuilder.AppendLine($"Time: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} , Process Type: {ProcessType} ,Field Name : {tableName} , Field Name: {fieldName} , Status : Failed, Error Message : {errorMessage}");
            }
            else
            {
                string status = "Successful";
                if (ProcessType == "Update")
                {
                    status = "Successful - Exists";
                }
                setupProcessLoggerBuilder.AppendLine($"Time: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} , Process Type: {ProcessType} ,Field Name : {tableName} , Field Name: {fieldName} , Status : {status}");
            }
           
            FieldsCount += 1;
            if (isCreated == true)
            {
                FieldsSuccessfulCount += 1;
            }
        }


        public static void LogObjectsCreation(string ProcessType, string objName, bool isCreated, string errorMessage)
        {
            if(isCreated == false)
            {
                setupProcessLoggerBuilder.AppendLine($"Time: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} , Process Type: {ProcessType} ,Object Name : {objName} , Status : Failed, Error Message : {errorMessage}");
                setupErrorProcessLoggerBuilder.AppendLine($"Time: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} , Process Type: {ProcessType} ,Object Name : {objName} , Status : Failed, Error Message : {errorMessage}");
            }
            else
            {
                string status = "Successful";
                if (ProcessType == "Update")
                {
                    status = "Successful - Exists";
                }
                setupProcessLoggerBuilder.AppendLine($"Time: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} , Process Type: {ProcessType} ,Object Name : {objName} , Status : {status}");
            }
           
            ObjectsCount += 1;
            if (isCreated == true)
            {
                ObjectsSuccessfulCount += 1;
            }
        }

        public static void LogCategoryCreation(string ProcessType, string catName, bool isCreated, string errorMessage)
        {
            if (isCreated == false)
            {
                setupProcessLoggerBuilder.AppendLine($"Time: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} , Process Type: {ProcessType} ,Query Category Name : {catName} , Status : Failed, Error Message : {errorMessage}");
                setupErrorProcessLoggerBuilder.AppendLine($"Time: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} , Process Type: {ProcessType} ,Query Category Name : {catName} , Status : Failed, Error Message : {errorMessage}");
            }
            else
            {
                string status = "Successful";
                if (ProcessType == "Update")
                {
                    status = "Successful - Exists";
                }
                setupProcessLoggerBuilder.AppendLine($"Time: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} , Process Type: {ProcessType} ,Query Category Name: {catName} , Status : {status}");
            }

            CategoryCount += 1;
            if (isCreated == true)
            {
                CategorySuccessfulCount += 1;
            }
        }


        public static void LogReportUpload(string ProcessType, string catName, bool isCreated, string errorMessage)
        {
            if (isCreated == false)
            {
                setupProcessLoggerBuilder.AppendLine($"Time: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} , Process Type: {ProcessType} ,Report Name : {catName} , Status : Failed, Error Message : {errorMessage}");
                setupErrorProcessLoggerBuilder.AppendLine($"Time: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} , Process Type: {ProcessType} ,Report Name : {catName} , Status : Failed, Error Message : {errorMessage}");
            }
            else
            {
                string status = "Successful";
                if (ProcessType == "Update")
                {
                    status = "Successful - Exists";
                }
                setupProcessLoggerBuilder.AppendLine($"Time: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} , Process Type: {ProcessType} ,Report Name: {catName} , Status : {status}");
            }

            CategoryCount += 1;
            if (isCreated == true)
            {
                CategorySuccessfulCount += 1;
            }
        }

        public static void LogQueryCreation(string ProcessType, string queryName, bool isCreated, string errorMessage)
        {
            if (isCreated == false)
            {
                setupProcessLoggerBuilder.AppendLine($"Time: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} , Process Type: {ProcessType} ,Query Name : {queryName} , Status : Failed, Error Message : {errorMessage}");
                setupErrorProcessLoggerBuilder.AppendLine($"Time: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} , Process Type: {ProcessType} ,Query Name : {queryName} , Status : Failed, Error Message : {errorMessage}");
            }
            else
            {
                string status = "Successful";
                if (ProcessType == "Update")
                {
                    status = "Successful - Exists";
                }
                setupProcessLoggerBuilder.AppendLine($"Time: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} , Process Type: {ProcessType} ,Query Name: {queryName} , Status : {status}");
            }

            QueryCount += 1;
            if (isCreated == true)
            {
                QuerySuccessfulCount += 1;
            }
        }


        public static void LogFMSCreation(string ProcessType, string fmsName, bool isCreated, string errorMessage)
        {
            if (isCreated == false)
            {
                setupProcessLoggerBuilder.AppendLine($"Time: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} , Process Type: {ProcessType} ,FMS Name : {fmsName} , Status : Failed, Error Message : {errorMessage}");
                setupErrorProcessLoggerBuilder.AppendLine($"Time: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} , Process Type: {ProcessType} ,FMS Name : {fmsName} , Status : Failed, Error Message : {errorMessage}");
            }
            else
            {
                string status = "Successful";
                if (ProcessType == "Update")
                {
                    status = "Successful - Exists";
                }
                setupProcessLoggerBuilder.AppendLine($"Time: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} , Process Type: {ProcessType} ,FMS Name: {fmsName} , Status : {status}");
            }

            FMSCount += 1;
            if (isCreated == true)
            {
                FMSSuccessfulCount += 1;
            }
        }


        public static void LogInitialValuesCreation(string tableName,string fieldName, bool isCreated, string errorMessage)
        { 

            if (isCreated == false)
            {
                setupProcessLoggerBuilder.AppendLine($"Time: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} ,Table Name : {tableName},Value Name: {fieldName} , Status : Failed, Error Message : {errorMessage}");
                setupErrorProcessLoggerBuilder.AppendLine($"Time: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} ,Table Name : {tableName},Value Name: {fieldName} , Status : Failed, Error Message : {errorMessage}");
            }
            else
            {
                setupProcessLoggerBuilder.AppendLine($"Time: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} ,Table Name : {tableName},Value Name: {fieldName} , Status : Successful");
            }
            
            InitialValuesCount += 1;
            if (isCreated == true)
            {
                InitialValuesSuccessfulCount += 1;
            }
        }


        public static void LogSetupProcess(string message, bool isCreated)
        {
            if (isCreated == false)
            {
                setupProcessLoggerBuilder.AppendLine($"Time: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} , Message: {message}");
                setupErrorProcessLoggerBuilder.AppendLine($"Time: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} , Message: {message}");
            }
            else
            {
                setupProcessLoggerBuilder.AppendLine($"Time: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} , Message: {message}");
            }
        }

        public static void LogSetup_AddSeparator(string message)
        { 
          setupProcessLoggerBuilder.AppendLine($"\n\n******* {message} **************************************************************");
             
        }


        public static void SaveSetupsLogs(string path )
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
            {
                file.WriteLine(setupProcessLoggerBuilder.ToString()); // "sb" is the StringBuilder
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(Path.GetDirectoryName(path)+"\\"+ Path.GetFileNameWithoutExtension(path)+"_Error.txt"))
            {
                file.WriteLine(setupErrorProcessLoggerBuilder.ToString()); // "sb" is the StringBuilder
            }
        }

        public static void SaveUninistallLogs(string path)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
            {
                file.WriteLine(setupProcessLoggerBuilder.ToString()); // "sb" is the StringBuilder
            }

            
        }


        public static void LogJsonStrings(string path, string message)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
            using (StreamWriter outputFile = new StreamWriter(path , true))
            {
                outputFile.WriteLine($"Time:{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}, String :\n   {message}");
            }
        }



        //public static string CreatePDFByDocEntry(string reportPath, int docEntry)
        //{

        //    string path = Path.GetTempPath() + "\\" + DateTime.Now.ToString("yyyyMMddhhmmss_") + docEntry.ToString() + ".pdf";
           

        //    ReportDocument cryRpt = new ReportDocument();
        //    cryRpt.Load(reportPath);


        //    var logonProps2 = cryRpt.DataSourceConnections[0].LogonProperties;

        //    logonProps2.Set("Connection String", Config.connectionString);


        //    cryRpt.DataSourceConnections[0].SetLogonProperties(logonProps2);
        //    cryRpt.DataSourceConnections[0].SetConnection(Config.Server + ":30015", Config.CompanyDB, Config.DbUserName, Config.DbPassword);

        //    ConnectionInfo connectInfo = new ConnectionInfo()
        //    {
        //        ServerName = Config.Server,
        //        DatabaseName = Config.CompanyDB,
        //        UserID = Config.DbUserName,
        //        Password = Config.DbPassword
        //    };

        //    // cryRpt.SetDatabaseLogon("SYSTEM", "PmgP_SaP!-1202");

        //    foreach (CrystalDecisions.CrystalReports.Engine.Table tbl in cryRpt.Database.Tables)
        //    {
        //        tbl.LogOnInfo.ConnectionInfo = connectInfo;
        //        tbl.ApplyLogOnInfo(tbl.LogOnInfo);
        //    }


        //    cryRpt.SetParameterValue("DocKey@", docEntry);


        //    ExportOptions CrExportOptions;
        //    DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
        //    PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
        //    CrDiskFileDestinationOptions.DiskFileName = path;
        //    CrExportOptions = cryRpt.ExportOptions;

        //    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
        //    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
        //    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
        //    CrExportOptions.FormatOptions = CrFormatTypeOptions;


        //    cryRpt.Export();
        //    cryRpt.Close();
        //    cryRpt.Dispose();

        //    return path;

        //}

    }
}
