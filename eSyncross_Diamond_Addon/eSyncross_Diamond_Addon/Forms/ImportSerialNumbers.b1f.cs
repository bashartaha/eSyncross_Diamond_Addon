using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;
using Diamond_Addon.Providers;
using System.IO;
using CsvHelper;
using System.Globalization;
using Diamond_Addon.Providers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;

namespace Diamond_Addon.Forms
{
    [FormAttribute("ESY_DIO_IMT", "Forms/ImportSerialNumbers.b1f")]
    class ImportSerialNumbers : UserFormBase
    {
        System.Data.DataTable dt = null;
        Dictionary<string, int> NextSerialNumbers = new Dictionary<string, int>();
        public ImportSerialNumbers()
        {
            try
            {
                ComboBox0.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                ComboBox1.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                EditText4.Value = DateTime.Now.ToString("yyyyMMdd");

                #region Tax codes

                SAPbobsCOM.Recordset oRecordset = B1Provider.oRecordset(" Select \"Code\",\"Name\"  from OVTG Where  \"Inactive\" = 'N' and \"Category\" ='I'  ");

                while (!oRecordset.EoF)
                {
                    ComboBox2.ValidValues.Add(oRecordset.Fields.Item(0).Value.ToString(), oRecordset.Fields.Item(1).Value.ToString());
                    oRecordset.MoveNext();
                }
                #endregion

                EditText12.Value = "SR";

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
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_3").Specific));
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_4").Specific));
            this.StaticText3 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_5").Specific));
            this.StaticText4 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_6").Specific));
            this.StaticText5 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_7").Specific));
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("Item_8").Specific));
            this.EditText1.ChooseFromListBefore += new SAPbouiCOM._IEditTextEvents_ChooseFromListBeforeEventHandler(this.EditText1_ChooseFromListBefore);
            this.EditText1.ChooseFromListAfter += new SAPbouiCOM._IEditTextEvents_ChooseFromListAfterEventHandler(this.EditText1_ChooseFromListAfter);
            this.EditText2 = ((SAPbouiCOM.EditText)(this.GetItem("Item_9").Specific));
            this.EditText2.ChooseFromListBefore += new SAPbouiCOM._IEditTextEvents_ChooseFromListBeforeEventHandler(this.EditText2_ChooseFromListBefore);
            this.EditText2.ChooseFromListAfter += new SAPbouiCOM._IEditTextEvents_ChooseFromListAfterEventHandler(this.EditText2_ChooseFromListAfter);
            this.EditText3 = ((SAPbouiCOM.EditText)(this.GetItem("Item_10").Specific));
            this.StaticText6 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_11").Specific));
            this.StaticText7 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_12").Specific));
            this.StaticText8 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_13").Specific));
            this.EditText4 = ((SAPbouiCOM.EditText)(this.GetItem("Item_14").Specific));
            this.ComboBox0 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_15").Specific));
            this.EditText5 = ((SAPbouiCOM.EditText)(this.GetItem("Item_16").Specific));
            this.Grid0 = ((SAPbouiCOM.Grid)(this.GetItem("Item_17").Specific));
            this.Folder0 = ((SAPbouiCOM.Folder)(this.GetItem("Item_19").Specific));
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("Item_21").Specific));
            this.Button1.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.Button1_ClickAfter);
            this.Button2 = ((SAPbouiCOM.Button)(this.GetItem("2").Specific));
            this.StaticText9 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_23").Specific));
            this.StaticText10 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_24").Specific));
            this.StaticText11 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_25").Specific));
            this.StaticText12 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_26").Specific));
            this.StaticText13 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_27").Specific));
            this.EditText6 = ((SAPbouiCOM.EditText)(this.GetItem("Item_29").Specific));
            this.EditText7 = ((SAPbouiCOM.EditText)(this.GetItem("Item_30").Specific));
            this.EditText7.ValidateAfter += new SAPbouiCOM._IEditTextEvents_ValidateAfterEventHandler(this.EditText7_ValidateAfter);
            this.EditText8 = ((SAPbouiCOM.EditText)(this.GetItem("Item_31").Specific));
            this.EditText8.ValidateAfter += new SAPbouiCOM._IEditTextEvents_ValidateAfterEventHandler(this.EditText8_ValidateAfter);
            this.EditText9 = ((SAPbouiCOM.EditText)(this.GetItem("Item_32").Specific));
            this.EditText9.ValidateAfter += new SAPbouiCOM._IEditTextEvents_ValidateAfterEventHandler(this.EditText9_ValidateAfter);
            this.EditText10 = ((SAPbouiCOM.EditText)(this.GetItem("Item_33").Specific));
            this.StaticText15 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_34").Specific));
            this.EditText11 = ((SAPbouiCOM.EditText)(this.GetItem("Item_35").Specific));
            this.ComboBox1 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_36").Specific));
            this.LinkedButton0 = ((SAPbouiCOM.LinkedButton)(this.GetItem("Item_37").Specific));
            this.StaticText14 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_20").Specific));
            this.ComboBox2 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_22").Specific));
            this.EditText12 = ((SAPbouiCOM.EditText)(this.GetItem("Item_28").Specific));
            this.EditText12.ChooseFromListAfter += new SAPbouiCOM._IEditTextEvents_ChooseFromListAfterEventHandler(this.EditText12_ChooseFromListAfter);
            this.EditText12.ChooseFromListBefore += new SAPbouiCOM._IEditTextEvents_ChooseFromListBeforeEventHandler(this.EditText12_ChooseFromListBefore);
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
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.StaticText StaticText2;
        private SAPbouiCOM.StaticText StaticText3;
        private SAPbouiCOM.StaticText StaticText4;
        private SAPbouiCOM.StaticText StaticText5;
        private SAPbouiCOM.EditText EditText1;
        private SAPbouiCOM.EditText EditText2;
        private SAPbouiCOM.EditText EditText3;
        private SAPbouiCOM.StaticText StaticText6;
        private SAPbouiCOM.StaticText StaticText7;
        private SAPbouiCOM.StaticText StaticText8;
        private SAPbouiCOM.EditText EditText4;
        private SAPbouiCOM.ComboBox ComboBox0;
        private SAPbouiCOM.EditText EditText5;
        private SAPbouiCOM.Grid Grid0;
        private SAPbouiCOM.Folder Folder0;
        private SAPbouiCOM.Folder Folder1;
        private SAPbouiCOM.Button Button1;
        private SAPbouiCOM.Button Button2;
        private SAPbouiCOM.StaticText StaticText9;
        private SAPbouiCOM.StaticText StaticText10;
        private SAPbouiCOM.StaticText StaticText11;
        private SAPbouiCOM.StaticText StaticText12;
        private SAPbouiCOM.StaticText StaticText13;
        private SAPbouiCOM.EditText EditText6;
        private SAPbouiCOM.EditText EditText7;
        private SAPbouiCOM.EditText EditText8;
        private SAPbouiCOM.EditText EditText9;
        private SAPbouiCOM.EditText EditText10;
        private SAPbouiCOM.StaticText StaticText15;
        private SAPbouiCOM.EditText EditText11;
        private SAPbouiCOM.ComboBox ComboBox1;

        private void EditText1_ChooseFromListAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {

            try
            {
                SAPbouiCOM.ISBOChooseFromListEventArg cfle = (SAPbouiCOM.ISBOChooseFromListEventArg)pVal;
                string uid = cfle.ChooseFromListUID;
                SAPbouiCOM.DataTable dt = cfle.SelectedObjects;


                try { EditText2.Value = dt.GetValue("CardName", 0).ToString(); } catch { }
                try { EditText1.Value = dt.GetValue("CardCode", 0).ToString(); } catch { }

                try { ComboBox2.Select(dt.GetValue("CardCode", 0).ToString(),SAPbouiCOM.BoSearchKey.psk_ByValue); } catch { }

            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message);
            }

        }

        private void EditText2_ChooseFromListAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {

            try
            {
                SAPbouiCOM.ISBOChooseFromListEventArg cfle = (SAPbouiCOM.ISBOChooseFromListEventArg)pVal;
                string uid = cfle.ChooseFromListUID;
                SAPbouiCOM.DataTable dt = cfle.SelectedObjects;

                try { EditText1.Value = dt.GetValue("CardCode", 0).ToString(); } catch { }
                try { EditText2.Value = dt.GetValue("CardName", 0).ToString(); } catch { }


            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message);
            }

        }

        private SAPbouiCOM.LinkedButton LinkedButton0;

        private void EditText1_ChooseFromListBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
                #region add conditions to CFL

                SAPbouiCOM.ChooseFromListCollection oCFLs = UIAPIRawForm.ChooseFromLists; ;
                SAPbouiCOM.Conditions oCons = null;
                SAPbouiCOM.Condition oCon = null;
                SAPbouiCOM.ChooseFromList oCFL = oCFLs.Item("CardCode");
                oCons = oCFL.GetConditions();
                if (oCons.Count == 0)
                {
                    oCon = oCons.Add();
                    oCon.Alias = "CardType";
                    oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                    oCon.CondVal = "S";
                    oCFL.SetConditions(oCons);
                }

                #endregion
            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, true);
            }

        }

        private void EditText2_ChooseFromListBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
                #region add conditions to CFL

                SAPbouiCOM.ChooseFromListCollection oCFLs = UIAPIRawForm.ChooseFromLists; ;
                SAPbouiCOM.Conditions oCons = null;
                SAPbouiCOM.Condition oCon = null;
                SAPbouiCOM.ChooseFromList oCFL = oCFLs.Item("CardName");
                oCons = oCFL.GetConditions();
                if (oCons.Count == 0)
                {
                    oCon = oCons.Add();
                    oCon.Alias = "CardType";
                    oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                    oCon.CondVal = "S";
                    oCFL.SetConditions(oCons);
                }

                #endregion
            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, true);
            }

        }

        private void Button0_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                if (string.IsNullOrEmpty(ComboBox0.Value))
                {
                    Application.SBO_Application.SetStatusBarMessage("Stock type must be selected", SAPbouiCOM.BoMessageTime.bmt_Medium, true);
                    return;
                }
                if (string.IsNullOrEmpty(EditText1.Value))
                {
                    Application.SBO_Application.SetStatusBarMessage("Supplier code must be selected", SAPbouiCOM.BoMessageTime.bmt_Medium, true);
                    return;
                }



                UIAPIRawForm.Freeze(true);
                try
                {
                    SelectFileDialog dialog = new SelectFileDialog("C:\\", "",
                       "|*.csv;*.xls;*.xlsx;*.xlsm", DialogType.OPEN);
                    dialog.Open();
                    if (!string.IsNullOrEmpty(dialog.SelectedFile))
                    {
                        EditText0.Value = dialog.SelectedFile;



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

                        string dbuserName = "sa";
                        if(B1Provider.oCompany.DbServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                        {
                            dbuserName = "SYSTEM";
                        }
                        using (SqlConnection connection = new SqlConnection(string.Format("Data Source={0};User ID={1};Password={2};Integrated security=False;database={3}", B1Provider.oCompany.Server, dbuserName, B1Provider.getServerPassword(), B1Provider.oCompany.CompanyDB)))
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

                        b1DataTable.ExecuteQuery($" Select * from \"ESY_VW_GRPO_LINES\" ");



                        ((SAPbouiCOM.EditTextColumn)Grid0.Columns.Item("Alias")).LinkedObjectType = "4";
                        ((SAPbouiCOM.EditTextColumn)Grid0.Columns.Item("CostPrice")).ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto;
                        ((SAPbouiCOM.EditTextColumn)Grid0.Columns.Item("DiamondWt")).ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto;
                        ((SAPbouiCOM.EditTextColumn)Grid0.Columns.Item("DiamondWeightSub1")).ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto;
                        ((SAPbouiCOM.EditTextColumn)Grid0.Columns.Item("DiamondWeightSub2")).ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto;
                        ((SAPbouiCOM.EditTextColumn)Grid0.Columns.Item("GoldWeight")).ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto;


                        SAPbobsCOM.Recordset result = B1Provider.oRecordset("Select SUM(\"CostPrice\") from \"ESY_VW_GRPO_LINES\"");

                        EditText6.Value = result.Fields.Item(0).Value.ToString();
                        EditText7.Value = "0";
                        EditText8.Value = "0";
                        EditText9.Value = "0";
                        EditText10.Value = result.Fields.Item(0).Value.ToString();


                        System.Runtime.InteropServices.Marshal.ReleaseComObject(result);
                        GC.Collect(); // Release the handle to the User Fields

                    }
                }
                catch (Exception ex)
                {
                    Application.SBO_Application.SetStatusBarMessage(ex.Message);
                }




                UIAPIRawForm.Freeze(false);




            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, true);
            }

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

        private void EditText7_ValidateAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            var currentEventFilters = Application.SBO_Application.GetFilter();
            Application.SBO_Application.SetFilter(null);
            try
            {

                double gross = double.Parse(EditText6.Value);
                double round = double.Parse(EditText7.Value);
                double discount = double.Parse(EditText8.Value);
                double charges = double.Parse(EditText9.Value);
                double net = gross - round - discount + charges;



                EditText10.Value = net.ToString();


            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message);
            }

            Application.SBO_Application.SetFilter(currentEventFilters);

        }

        private void EditText8_ValidateAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            var currentEventFilters = Application.SBO_Application.GetFilter();
            Application.SBO_Application.SetFilter(null);
            try
            {
                double gross = double.Parse(EditText6.Value);
                double round = double.Parse(EditText7.Value);
                double discount = double.Parse(EditText8.Value);
                double charges = double.Parse(EditText9.Value);
                double net = gross - round - discount + charges;


                EditText10.Value = net.ToString();


            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message);
            }

            Application.SBO_Application.SetFilter(currentEventFilters);

        }

        private void EditText9_ValidateAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            var currentEventFilters = Application.SBO_Application.GetFilter();
            Application.SBO_Application.SetFilter(null);
            try
            {

                double gross = double.Parse(EditText6.Value);
                double round = double.Parse(EditText7.Value);
                double discount = double.Parse(EditText8.Value);
                double charges = double.Parse(EditText9.Value);
                double net = gross - round - discount + charges;

                EditText10.Value = net.ToString();


            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message);
            }

            Application.SBO_Application.SetFilter(currentEventFilters);

        }

        private void Button1_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            SAPbouiCOM.ProgressBar oBar = null;
            try
            {
                string whseCode = EditText12.Value;
                if (string.IsNullOrEmpty(whseCode))
                {
                    Application.SBO_Application.SetStatusBarMessage("Select a warehouse");
                    return;
                }

                if (string.IsNullOrEmpty(ComboBox2.Value))
                {
                    Application.SBO_Application.SetStatusBarMessage("Select a tax code");
                    return;
                }

                if (string.IsNullOrEmpty(ComboBox0.Value))
                {
                    Application.SBO_Application.SetStatusBarMessage("Select Stock Type");
                    return;
                }

                if (string.IsNullOrEmpty(EditText3.Value))
                {
                    Application.SBO_Application.SetStatusBarMessage("Supplier reference number is missing");
                    return;
                }
                else
                {
                    SAPbobsCOM.Recordset rs = B1Provider.oRecordset($"Select DocNum from OPDN where NumAtCard = '{EditText3.Value}'");
                    if (rs.RecordCount > 0)
                    {
                        Application.SBO_Application.SetStatusBarMessage($"Supplier reference: {EditText3.Value} is already processed, check Goods Receipt PO number : {rs.Fields.Item(0).Value.ToString()}");
                        return;
                    }
                }


                oBar = (SAPbouiCOM.ProgressBar)Application.SBO_Application.StatusBar.CreateProgressBar("Please wait", 100, false);
                try
                {
                    oBar.Value = 1;
                    oBar.Text = "Please wait";
                }
                catch
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oBar);
                    GC.Collect(); // Release the handle to the User Fields

                    oBar = (SAPbouiCOM.ProgressBar)Application.SBO_Application.StatusBar.CreateProgressBar("Please wait", 100, false);
                }
                 
                #region Posting

                SAPbobsCOM.Recordset records = B1Provider.oRecordset("Select * from \"ESY_GRPO\"");

                SAPbobsCOM.Documents oDocument =(SAPbobsCOM.Documents)B1Provider.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseDeliveryNotes);

                //if (ComboBox0.Value == "1")
                //{
                //    oDocument = (SAPbobsCOM.Documents)B1Provider.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseInvoices);
                //}
                //else
                //{
                 //   oDocument = (SAPbobsCOM.Documents)B1Provider.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseDeliveryNotes);
                //}

                #region Header
                oDocument.CardCode = EditText1.Value;
                oDocument.DocDate = DateTime.Today;
                oDocument.NumAtCard = EditText3.Value;
                oDocument.UserFields.Fields.Item("U_ESY_StockType").Value = ComboBox0.Value;
                oDocument.Comments = EditText11.Value;
                oDocument.DocCurrency = "AED";
                //oDocument.Rounding = SAPbobsCOM.BoYesNoEnum.tYES;
                //oDocument.RoundingDiffAmount = EditText8.Value;

                if (double.Parse(EditText9.Value) > 0)
                {
                    oDocument.Expenses.ExpenseCode = 1;
                    oDocument.Expenses.LineTotal = double.Parse(EditText9.Value);
                }

                #endregion

                #region Lines

                NextSerialNumbers.Clear();

                while (!records.EoF)
                {
                    oDocument.Lines.ItemCode = GenerateItemCode(records);



                    oDocument.Lines.Quantity = 1;
                    oDocument.Lines.WarehouseCode = whseCode;

                    oDocument.Lines.UnitPrice = (double)records.Fields.Item("CostPrice").Value;
                   
                    oDocument.Lines.VatGroup= ComboBox2.Selected.Value;

                  

                    string serialNumber = GenerateNextSerialNumber(oDocument.Lines.ItemCode, records.Fields.Item("TaggingDefinition").Value.ToString());

                    oDocument.Lines.SerialNum = serialNumber;
                    oDocument.Lines.UserFields.Fields.Item("U_ESY_Status").Value = "Availble";
                    oDocument.Lines.SerialNumbers.InternalSerialNumber = serialNumber;
                    oDocument.Lines.SerialNumbers.ManufacturerSerialNumber = serialNumber;
                    oDocument.Lines.SerialNumbers.Quantity = 1;


                    #region UDFs
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_GoldWeight").Value = records.Fields.Item("GoldWeight").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_DiamondWt").Value = records.Fields.Item("DiamondWt").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_RubyWt").Value = records.Fields.Item("RubyWt").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_EmraldWt").Value = records.Fields.Item("EmraldWt").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_SaphireWt").Value = records.Fields.Item("SaphireWt").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_OtherStoneWt").Value = records.Fields.Item("OtherStoneWt").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_TagCurrency").Value = records.Fields.Item("TagCurrency").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_TagCurrencyExRate").Value = records.Fields.Item("TagCurrencyExRate").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_CostPrice").Value = records.Fields.Item("CostPrice").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_AddCharges").Value = records.Fields.Item("AddCharges").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_MarkUpPercent").Value = records.Fields.Item("MarkUpPercent").Value;

                    //  oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_TagPrice").Value = records.Fields.Item("TagPrice").Value;

                    if (records.Fields.Item("TagPrice").Value != null && !string.IsNullOrEmpty(records.Fields.Item("TagPrice").Value.ToString()) && double.Parse(records.Fields.Item("TagPrice").Value.ToString()) > 0)
                    {
                        oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_TagPrice").Value = records.Fields.Item("TagPrice").Value;
                    }
                    else
                    {
                        if (records.Fields.Item("MarkUpPercent").Value != null && !string.IsNullOrEmpty(records.Fields.Item("MarkUpPercent").Value.ToString()) && double.Parse(records.Fields.Item("MarkUpPercent").Value.ToString()) > 0)
                        {
                            oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_TagPrice").Value = (
                                double.Parse(records.Fields.Item("CostPrice").Value.ToString())  * (double.Parse(records.Fields.Item("MarkUpPercent").Value.ToString())/100.00)
                                ) + double.Parse(records.Fields.Item("CostPrice").Value.ToString()) ;
                        }
                      
                    }

                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_MaxDiscountPer").Value = records.Fields.Item("MaxDiscountPer").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_PearlWeight").Value = records.Fields.Item("PearlWeight").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_SupplierRefNo").Value = records.Fields.Item("SupplierRefNo").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_MColor").Value = records.Fields.Item("MetalColor").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_TaggingLine1").Value = records.Fields.Item("TaggingLine1").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_TaggingLine2").Value = records.Fields.Item("TaggingLine2").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_TaggingLine3").Value = records.Fields.Item("TaggingLine3").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_TaggingLine4").Value = records.Fields.Item("TaggingLine4").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_ProfitMargin").Value = records.Fields.Item("ProfitMargin").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_DWeightSub1").Value = records.Fields.Item("DiamondWeightSub1").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_DWeightSub2").Value = records.Fields.Item("DiamondWeightSub2").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_DWeightSub3").Value = records.Fields.Item("DiamondWeightSub3").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_DQuantity").Value = records.Fields.Item("DiamondQuantity").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_DQuantitySub1").Value = records.Fields.Item("DiamondQuantitySub1").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_DQuantitySub2").Value = records.Fields.Item("DiamondQuantitySub2").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_DQuantitySub3").Value = records.Fields.Item("DiamondQuantitySub3").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_DShapeCode").Value = records.Fields.Item("DiamondShapeCode").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_DShapeCodeSub1").Value = records.Fields.Item("DiamondShapeCodeSub1").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_DShapeCodeSub2").Value = records.Fields.Item("DiamondShapeCodeSub2").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_DShapeCodeSub3").Value = records.Fields.Item("DiamondShapeCodeSub3").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_DColorCode").Value = records.Fields.Item("DiamondColorCode").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_DColorCodeSub1").Value = records.Fields.Item("DiamondColorCodeSub1").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_DColorCodeSub2").Value = records.Fields.Item("DiamondColorCodeSub2").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_DColorCodeSub3").Value = records.Fields.Item("DiamondColorCodeSub3").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_DItem").Value = records.Fields.Item("DiamondItem").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_DItemSub1").Value = records.Fields.Item("DiamondItemSub1").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_DItemSub2").Value = records.Fields.Item("DiamondItemSub2").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_DItemSub3").Value = records.Fields.Item("DiamondItemSub3").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_MWeightType").Value = records.Fields.Item("MetalWeightType").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_StockPoint").Value = records.Fields.Item("StockPoint").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_Size").Value = records.Fields.Item("Size").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_MWeightSub1Type").Value = records.Fields.Item("MetalWeightSub1Type").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_MWeightSub1").Value = records.Fields.Item("MetalWeightSub1").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_MWeightSub2Type").Value = records.Fields.Item("MetalWeightSub2Type").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_MWeightSub2").Value = records.Fields.Item("MetalWeightSub2").Value;
                //    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_MLoss").Value = records.Fields.Item("MetalLoss").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_SWeightSub1Type").Value = records.Fields.Item("StoneWeightSub1Type").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_SWeightSub1").Value = records.Fields.Item("StoneWeightSub1").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_SWeightSub2Type").Value = records.Fields.Item("StoneWeightSub2Type").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_SWeightSub2").Value = records.Fields.Item("StoneWeightSub2").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_SWeightSub3Type").Value = records.Fields.Item("StoneWeightSub3Type").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_SWeightSub3").Value = records.Fields.Item("StoneWeightSub3").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_Notes").Value = records.Fields.Item("Notes").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_BufferValueBC").Value = records.Fields.Item("BufferValueBC").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_TotalWeight").Value = records.Fields.Item("TotalWeight").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_ReplacementCost").Value = records.Fields.Item("ReplacementCost").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_UOMCode").Value = records.Fields.Item("UOMCode").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_ClarityCode").Value = records.Fields.Item("DiamondClarityCode").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_ClarityCodeSub1").Value = records.Fields.Item("DiamondClarityCodeSub1").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_ClarityCodeSub2").Value = records.Fields.Item("DiamondClarityCodeSub2").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_ClarityCodeSub3").Value = records.Fields.Item("DiamondClarityCodeSub3").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_CertificateNo").Value = records.Fields.Item("DiamondCertificateNo").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_CertificateNoSub1").Value = records.Fields.Item("DiamondCertificateNoSub1").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_CertificateNoSub2").Value = records.Fields.Item("DiamondCertificateNoSub2").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_CertificateNoSub3").Value = records.Fields.Item("DiamondCertificateNoSub3").Value;
                    oDocument.Lines.SerialNumbers.UserFields.Fields.Item("U_BufferType").Value = records.Fields.Item("BufferConsiderationType").Value;

                    #endregion


                    oDocument.Lines.SerialNumbers.Add();
                    oDocument.Lines.Add();

                    records.MoveNext();
                }

                #endregion

                #region Attachment

                SAPbobsCOM.Attachments2 oAtt = (SAPbobsCOM.Attachments2)B1Provider.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oAttachments2);

                string path = EditText0.Value;

                oAtt.Lines.SourcePath = Path.GetDirectoryName(path);
                oAtt.Lines.FileName = Path.GetFileNameWithoutExtension(path);
                oAtt.Lines.FileExtension = Path.GetExtension(path).Replace(".", "");               
                oAtt.Lines.Add();


                int oAttDocEntry = oAtt.Add();

                if (oAttDocEntry == 0)
                {
                    oDocument.AttachmentEntry = oAttDocEntry;
                }


                #endregion

                #region Exportto XML

                //if (Config.ExportXML == "Y")
                //{
                //    string xml = oDocument.GetAsXML();


                //}

                #endregion


                //Add the docuemnt
                int j = oDocument.Add();

                oBar.Stop();

                if (j != 0)
                {
                    Application.SBO_Application.SetStatusBarMessage(B1Provider.oCompany.GetLastErrorDescription());
                }
                else
                {
                    Application.SBO_Application.StatusBar.SetSystemMessage("Document has been successfully posted", SAPbouiCOM.BoMessageTime.bmt_Medium,SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                    Button1.Item.Visible = false;
                }
                #endregion



            }
            catch (Exception ex)
            {
                oBar.Stop();
                Application.SBO_Application.SetStatusBarMessage(ex.Message);
            }

           
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oBar);
            GC.Collect(); // Release the handle to the User Fields
        }

        private string GenerateItemCode(SAPbobsCOM.Recordset records)
        {
            string itemCode = "";

            SAPbobsCOM.Items oItem = (SAPbobsCOM.Items)B1Provider.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems);

            if (oItem.GetByKey(records.Fields.Item("Alias").Value.ToString()))
            {
                itemCode = records.Fields.Item("Alias").Value.ToString();
            }
            else
            {
                oItem.ItemCode = records.Fields.Item("Alias").Value.ToString();
                oItem.ItemName = records.Fields.Item("Description").Value.ToString();

                oItem.ItemsGroupCode = 101;

                oItem.ManageSerialNumbers = SAPbobsCOM.BoYesNoEnum.tYES;
                oItem.CostAccountingMethod = SAPbobsCOM.BoInventorySystem.bis_SNB;


                oItem.UserFields.Fields.Item("U_ESY_Category").Value = records.Fields.Item("Category").Value.ToString();
                oItem.UserFields.Fields.Item("U_ESY_Design").Value = records.Fields.Item("Design").Value.ToString();
                oItem.UserFields.Fields.Item("U_ESY_Style").Value = records.Fields.Item("Style").Value.ToString();
                oItem.UserFields.Fields.Item("U_ESY_SubCategory").Value = records.Fields.Item("SubCategory").Value.ToString();
                oItem.UserFields.Fields.Item("U_ESY_Brand").Value = records.Fields.Item("Brand").Value.ToString();
                oItem.UserFields.Fields.Item("U_ESY_Occasion").Value = records.Fields.Item("Occasion").Value.ToString();

                int retCode = oItem.Add();

                if (retCode == 0)
                {
                    itemCode = oItem.ItemCode;
                }
                else
                {
                    throw new Exception(B1Provider.oCompany.GetLastErrorDescription());
                }
            }






            return itemCode;
        }

        private string GenerateNextSerialNumber(string itemCode, string taggingDefinition)
        {
            string nextSN = "";
            int current = 1;
            int next = 1;
            if (NextSerialNumbers.Where(w => w.Key == itemCode).Count() == 0)
            {
                current = B1Provider.GetNextSerialNumber(taggingDefinition);

                 next = current + 1;

                NextSerialNumbers.Add(itemCode, next);

                
            }
            else
            {
                current = NextSerialNumbers.Where(w => w.Key == itemCode).First().Value;

                 next = current + 1;

                NextSerialNumbers[itemCode] = next;
             
            }



            nextSN = taggingDefinition + next.ToString().PadLeft(5, '0');

            return nextSN;

        }

        private SAPbouiCOM.StaticText StaticText14;
        private SAPbouiCOM.ComboBox ComboBox2;
        private SAPbouiCOM.EditText EditText12;

        private void EditText12_ChooseFromListBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
                if (!string.IsNullOrEmpty(ComboBox0.Value))
                {
                    #region add conditions to CFL

                    SAPbouiCOM.ChooseFromListCollection oCFLs = UIAPIRawForm.ChooseFromLists; ;
                    SAPbouiCOM.Conditions oCons = null;
                    SAPbouiCOM.Condition oCon = null;
                    SAPbouiCOM.ChooseFromList oCFL = oCFLs.Item("OWHS");
                    oCons = oCFL.GetConditions();
                    if (oCons.Count == 0)
                    {
                        oCon = oCons.Add();
                        oCon.Alias = "U_ESY_StockType";
                        oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                        oCon.CondVal = ComboBox0.Value;
                        oCFL.SetConditions(oCons);
                    }

                    #endregion
                }
                else
                {
                    Application.SBO_Application.SetStatusBarMessage("Stock type must be selected", SAPbouiCOM.BoMessageTime.bmt_Medium, true);
                    BubbleEvent = false;
                }
            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, true);
            }

        }

        private void EditText12_ChooseFromListAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {

            try
            {
                SAPbouiCOM.ISBOChooseFromListEventArg cfle = (SAPbouiCOM.ISBOChooseFromListEventArg)pVal;
                string uid = cfle.ChooseFromListUID;
                SAPbouiCOM.DataTable dt = cfle.SelectedObjects;

                try { EditText12.Value = dt.GetValue("WhsCode", 0).ToString(); } catch { }
                
            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message);
            }
        }
    }
}
