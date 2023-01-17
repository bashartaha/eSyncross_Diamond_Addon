using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;
using Diamond_Addon.Providers;
using System.IO;

using CsvHelper;
using System.Globalization;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using EvoAddon.Providers;

namespace Diamond_Addon.Forms
{
    [FormAttribute("ESY_BRS", "Forms/BrowseImportFile.b1f")]
    class BrowseImportFile : UserFormBase
    {

        public BrowseImportFile(SAPbouiCOM.Form form)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message);
            }
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_0").Specific));
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("Item_1").Specific));
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_2").Specific));
            this.Button0.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.Button0_ClickAfter);
            this.Folder0 = ((SAPbouiCOM.Folder)(this.GetItem("Item_4").Specific));
            this.Grid0 = ((SAPbouiCOM.Grid)(this.GetItem("Item_5").Specific));
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("Item_6").Specific));
            this.Button1.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.Button1_ClickAfter);
            this.Button2 = ((SAPbouiCOM.Button)(this.GetItem("2").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
        }

        private SAPbouiCOM.StaticText StaticText0;

        private void OnCustomInitialize()
        {

        }

        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.Button Button0;
        private SAPbouiCOM.Folder Folder0;
        private SAPbouiCOM.Grid Grid0;
        private SAPbouiCOM.Button Button1;
        private SAPbouiCOM.Button Button2;

        private void Button0_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            UIAPIRawForm.Freeze(true);
            try
            {
                SelectFileDialog dialog = new SelectFileDialog("C:\\", "",
                   "|*.csv;*.xls;*.xlsx;*.xlsm", DialogType.OPEN);
                dialog.Open();
                if (!string.IsNullOrEmpty(dialog.SelectedFile))
                {
                    EditText0.Value = dialog.SelectedFile;
                }
                else
                {
                    return;
                }


                // Clean Matrix 

                // add new row as the matrix is empty


                //read csv
                System.Data.DataTable dt = new System.Data.DataTable();

                if (Path.GetExtension(dialog.SelectedFile).ToLower() == "csv")
                {
                    using (var reader = new StreamReader(dialog.SelectedFile))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        using (var dr = new CsvDataReader(csv))
                        {
                            dt.Load(dr);
                        }
                    }
                }
                else
                {
                    dt = FileProvider.ReadExcel(dialog.SelectedFile);
                }
               

                // dt.Columns.Add(new DataColumn("DocNum", typeof(String)));
                //dt.Rows.Cast<DataRow>().ToList().ForEach(x => x["DocNum"] = docNum);

                B1Provider.oRecordset("Delete from \"ESY_GRPO\" ");

                dt.TableName = "ESY_GRPO";

                var json = JsonConvert.SerializeObject(dt);
                List<JObject> jsonObjects = JsonConvert.DeserializeObject<List<JObject>>(json);


                using (SqlConnection connection = new SqlConnection(string.Format("Data Source={0};User ID={1};Password={2};Integrated security=False;database={3}", B1Provider.oCompany.Server, "sa", "1234", B1Provider.oCompany.CompanyDB)))
                {
                    // open transection
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand())
                            {
                                command.Connection = connection;
                                command.Transaction = transaction;
                                command.CommandType = CommandType.Text;
                                foreach (JObject obj in jsonObjects)
                                {

                                    // Get the column names and values
                                    var columns = obj.Properties().Select(p => new
                                    {
                                        Name = p.Name,
                                        Value = ConvertValue(p.Value)
                                    });
                                    var columnNames = string.Join(",", columns.Where(w => w.Value != null && !string.IsNullOrEmpty(w.Value.ToString())).Select(c => c.Name));
                                    var parameterNames = string.Join(",", columns.Where(w => w.Value != null && !string.IsNullOrEmpty(w.Value.ToString())).Select(c => c.Value));

                                    string insertCommand = $"INSERT INTO \"ESY_GRPO\" ({columnNames}) VALUES ({parameterNames})";

                                    command.CommandText = insertCommand;
                                    command.ExecuteNonQuery();

                                }
                                //commit transection
                                transaction.Commit();
                            }
                        }
                        catch (Exception ex)
                        {
                            //rollback transection
                            transaction.Rollback();

                            throw;

                        }
                    }

                }

                SAPbouiCOM.DataTable b1DataTable = (SAPbouiCOM.DataTable)UIAPIRawForm.DataSources.DataTables.Item("DT_0");

                b1DataTable.ExecuteQuery($" Select * from \"ESY_VW_GRPOLines\"   ");
            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message);
            }

            UIAPIRawForm.Freeze(false);

        }


        static object ConvertValue(JToken value)
        {
            switch (value.Type)
            {
                case JTokenType.Integer:
                    return value.Value<int>();
                case JTokenType.Float:
                    return value.Value<double>();
                case JTokenType.Date:
                    return value.Value<DateTime>();
                case JTokenType.String:
                    if (value.Value<string>() == null || value.Value<string>() == "")
                    {
                        return null;
                    }
                    else
                    {
                        return "'" + value.Value<string>() + "'";
                    }

                case JTokenType.Boolean:
                    return value.Value<bool>();
                default:
                    return null;
            }
        }

        private void Button1_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            UIAPIRawForm.Freeze(true);
            try
            {

            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message);
            }
            UIAPIRawForm.Freeze(false);
        }
    }
}
