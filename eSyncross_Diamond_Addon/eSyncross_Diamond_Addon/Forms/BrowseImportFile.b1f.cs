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
using Diamond_Addon.Providers;

namespace Diamond_Addon.Forms
{
    [FormAttribute("ESY_BRS", "Forms/BrowseImportFile.b1f")]
    class BrowseImportFile : UserFormBase
    {
        System.Data.DataTable dt = null;
        SAPbouiCOM.Form oForm;
        public BrowseImportFile(SAPbouiCOM.Form form)
        {
            try
            {
                oForm = form;
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
                 dt = new System.Data.DataTable();

                if (Path.GetExtension(dialog.SelectedFile).ToLower() == ".csv")
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

                b1DataTable.ExecuteQuery($" Select * from \"ESY_GRPO\" ");
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
         
            try
            {
                oForm.Select();
               
                //loop through DataTable dt and fill matrix in oForm
                SAPbouiCOM.Matrix matrix = (SAPbouiCOM.Matrix)oForm.Items.Item("38").Specific;
                foreach (DataRow row in dt.Rows)
                {
                    // fill matrix
                    oForm.Freeze(true);
                    ((SAPbouiCOM.EditText)matrix.GetCellSpecific("1", matrix.RowCount)).Value = row["Alias"].ToString();
                    ((SAPbouiCOM.EditText)matrix.GetCellSpecific("11", matrix.RowCount-1)).Value = "1";
                    ((SAPbouiCOM.EditText)matrix.GetCellSpecific("14", matrix.RowCount-1)).Value = row["Costprice"].ToString();





                    #region Fill Serial Numbers

                    oForm.Freeze(false);

                    matrix.Columns.Item("11").Cells.Item(matrix.RowCount-1).Click(SAPbouiCOM.BoCellClickType.ct_Regular);

                 

                    Application.SBO_Application.SendKeys("^{TAB}");
                     

                    SAPbouiCOM.Form serialnumberform = null;

                    foreach (SAPbouiCOM.Form form in Application.SBO_Application.Forms)
                    {
                        if (form.TypeEx == "21")
                        {
                            serialnumberform = form;
                            break;
                        }

                    }
                    

                     
                    
                    serialnumberform.Freeze(true);
                    var matrix_serial = serialnumberform.Items.Item("3").Specific as SAPbouiCOM.Matrix;



 
                     
                        ((SAPbouiCOM.EditText)matrix_serial.Columns.Item("54").Cells.Item(matrix_serial.RowCount ).Specific).Value = "SB3434343";
                        ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("1", matrix_serial.RowCount )).Value = "SB3434343";
                     

       
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_GoldWeight", matrix_serial.RowCount )).Value = row["GoldWeight"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_DiamodWt", matrix_serial.RowCount )).Value = row["DiamondWt"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_RubyWt", matrix_serial.RowCount )).Value = row["RubyWt"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_EmraldWt", matrix_serial.RowCount )).Value = row["EmraldWt"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_SaphireWt", matrix_serial.RowCount )).Value = row["SaphireWt"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_OtherStoneWt", matrix_serial.RowCount )).Value = row["OtherStoneWt"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_TagCurrency", matrix_serial.RowCount )).Value = row["TagCurrency"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_TagCurrencyExRate", matrix_serial.RowCount )).Value = row["TagCurrencyExRate"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_CostPrice", matrix_serial.RowCount )).Value = row["CostPrice"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_AddCharges", matrix_serial.RowCount )).Value = row["AddCharges"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_MarkUpPercent", matrix_serial.RowCount )).Value = row["MarkUpPercent"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_TagPrice", matrix_serial.RowCount )).Value = row["TagPrice"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_MaxDiscountPer", matrix_serial.RowCount )).Value = row["MaxDiscountPer"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_PearlWeight", matrix_serial.RowCount )).Value = row["PearlWeight"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_SupplierRefNo", matrix_serial.RowCount )).Value = row["SupplierRefNo"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_MColor", matrix_serial.RowCount )).Value = row["MetalColor"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_TaggingLine1", matrix_serial.RowCount )).Value = row["TaggingLine1"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_TaggingLine2", matrix_serial.RowCount )).Value = row["TaggingLine2"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_TaggingLine3", matrix_serial.RowCount )).Value = row["TaggingLine3"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_TaggingLine4", matrix_serial.RowCount )).Value = row["TaggingLine4"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_ProfitMargin", matrix_serial.RowCount )).Value = row["ProfitMargin"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_DWeightSub1", matrix_serial.RowCount )).Value = row["DiamondWeightSub1"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_DWeightSub2", matrix_serial.RowCount )).Value = row["DiamondWeightSub2"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_DWeightSub3", matrix_serial.RowCount )).Value = row["DiamondWeightSub3"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_DQuantity", matrix_serial.RowCount )).Value = row["DiamondQuantity"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_DQuantitySub1", matrix_serial.RowCount )).Value = row["DiamondQuantitySub1"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_DQuantitySub2", matrix_serial.RowCount )).Value = row["DiamondQuantitySub2"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_DQuantitySub3", matrix_serial.RowCount )).Value = row["DiamondQuantitySub3"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_DShapeCode", matrix_serial.RowCount )).Value = row["DiamondShapeCode"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_DShapeCodeSub1", matrix_serial.RowCount )).Value = row["DiamondShapeCodeSub1"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_DShapeCodeSub2", matrix_serial.RowCount )).Value = row["DiamondShapeCodeSub2"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_DShapeCodeSub3", matrix_serial.RowCount )).Value = row["DiamondShapeCodeSub3"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_DColorCode", matrix_serial.RowCount )).Value = row["DiamondColorCode"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_DColorCodeSub1", matrix_serial.RowCount )).Value = row["DiamondColorCodeSub1"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_DColorCodeSub2", matrix_serial.RowCount )).Value = row["DiamondColorCodeSub2"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_DColorCodeSub3", matrix_serial.RowCount )).Value = row["DiamondColorCodeSub3"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_DItem", matrix_serial.RowCount )).Value = row["DiamondItem"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_DItemSub1", matrix_serial.RowCount )).Value = row["DiamondItemSub1"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_DItemSub2", matrix_serial.RowCount )).Value = row["DiamondItemSub2"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_DItemSub3", matrix_serial.RowCount )).Value = row["DiamondItemSub3"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_MWeightType", matrix_serial.RowCount )).Value = row["MetalWeightType"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_StockPoint", matrix_serial.RowCount )).Value = row["StockPoint"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_Size", matrix_serial.RowCount )).Value = row["Size"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_MWeightSub1Type", matrix_serial.RowCount )).Value = row["MetalWeightSub1Type"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_MWeightSub1", matrix_serial.RowCount )).Value = row["MetalWeightSub1"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_MWeightSub2Type", matrix_serial.RowCount )).Value = row["MetalWeightSub2Type"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_MWeightSub2", matrix_serial.RowCount )).Value = row["MetalWeightSub2"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_MLoss", matrix_serial.RowCount )).Value = row["MetalLoss"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_SWeightSub1Type", matrix_serial.RowCount )).Value = row["StoneWeightSub1Type"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_SWeightSub1", matrix_serial.RowCount )).Value = row["StoneWeightSub1"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_SWeightSub2Type", matrix_serial.RowCount )).Value = row["StoneWeightSub2Type"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_SWeightSub2", matrix_serial.RowCount )).Value = row["StoneWeightSub2"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_SWeightSub3Type", matrix_serial.RowCount )).Value = row["StoneWeightSub3Type"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_SWeightSub3", matrix_serial.RowCount )).Value = row["StoneWeightSub3"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_Notes", matrix_serial.RowCount )).Value = row["Notes"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_BufferValueBC", matrix_serial.RowCount )).Value = row["BufferValueBC"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_TotalWeight", matrix_serial.RowCount )).Value = row["TotalWeight"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_ReplacementCost", matrix_serial.RowCount )).Value = row["ReplacementCost"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_UOMCode", matrix_serial.RowCount )).Value = row["UOMCode"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_ClarityCode", matrix_serial.RowCount )).Value = row["DiamondClarityCode"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_ClarityCodeSub1", matrix_serial.RowCount )).Value = row["DiamondClarityCodeSub1"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_ClarityCodeSub2", matrix_serial.RowCount )).Value = row["DiamondClarityCodeSub2"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_ClarityCodeSub3", matrix_serial.RowCount )).Value = row["DiamondClarityCodeSub3"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_CertificateNo", matrix_serial.RowCount )).Value = row["DiamondCertificateNo"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_CertificateNoSub1", matrix_serial.RowCount )).Value = row["DiamondCertificateNoSub1"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_CertificateNoSub2", matrix_serial.RowCount )).Value = row["DiamondCertificateNoSub2"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_CertificateNoSub3", matrix_serial.RowCount )).Value = row["DiamondCertificateNoSub3"].ToString();
                    ((SAPbouiCOM.EditText)matrix_serial.GetCellSpecific("U_BufferType", matrix_serial.RowCount )).Value = row["BufferConsiderationType"].ToString();

                    serialnumberform.Freeze(false);
                    serialnumberform.Items.Item("1").Click();
                    serialnumberform.Items.Item("1").Click();




                    #endregion
                }



            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message);
            }
            oForm.Freeze(false);
        }
    }
}
