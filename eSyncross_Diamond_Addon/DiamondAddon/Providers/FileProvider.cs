 
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace EvoAddon.Providers
{
    class FileProvider
    {
        // download fro : https://www.microsoft.com/en-in/download/details.aspx?id=13255
        public static DataTable ReadExcel(string filePath)
        {
            DataTable dt = new DataTable();
            string connectionString = "";
            if (filePath.EndsWith(".xls"))
            {
                connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            }
            else if (filePath.EndsWith(".xlsx"))
            {
                connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            }
            else if (filePath.EndsWith(".xlsm"))
            {
                connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0 Macro;HDR=Yes;IMEX=2\"";
            }
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("SELECT * FROM [Sheet1$]", connection);

                using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(command))
                { 
                    dataAdapter.Fill(dt);
                    // use the data in the DataTable here
                }
            }
            return dt;
        }
    }
}
