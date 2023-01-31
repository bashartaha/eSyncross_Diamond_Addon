using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;

namespace EvoAddon.Forms
{
    [FormAttribute("EvoAddon.Forms.ProductionScreen", "Forms/ProductionScreen.b1f")]
    class ProductionScreen : UserFormBase
    {
        public ProductionScreen()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_0").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_1").Specific));
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("Item_2").Specific));
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_3").Specific));
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("Item_4").Specific));
            this.StaticText3 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_5").Specific));
            this.EditText2 = ((SAPbouiCOM.EditText)(this.GetItem("Item_6").Specific));
            this.EditText2.ChooseFromListBefore += new SAPbouiCOM._IEditTextEvents_ChooseFromListBeforeEventHandler(this.EditText2_ChooseFromListBefore);
            this.EditText2.ChooseFromListAfter += new SAPbouiCOM._IEditTextEvents_ChooseFromListAfterEventHandler(this.EditText2_ChooseFromListAfter);
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("1").Specific));
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("2").Specific));
            this.Matrix0 = ((SAPbouiCOM.Matrix)(this.GetItem("Item_9").Specific));
            this.Matrix0.ChooseFromListBefore += new SAPbouiCOM._IMatrixEvents_ChooseFromListBeforeEventHandler(this.Matrix0_ChooseFromListBefore);
            this.Matrix0.ChooseFromListAfter += new SAPbouiCOM._IMatrixEvents_ChooseFromListAfterEventHandler(this.Matrix0_ChooseFromListAfter);
            this.Matrix1 = ((SAPbouiCOM.Matrix)(this.GetItem("Item_10").Specific));
            this.StaticText5 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_13").Specific));
            this.Button2 = ((SAPbouiCOM.Button)(this.GetItem("Item_14").Specific));
            this.EditText4 = ((SAPbouiCOM.EditText)(this.GetItem("Item_16").Specific));
            this.EditText4.ChooseFromListBefore += new SAPbouiCOM._IEditTextEvents_ChooseFromListBeforeEventHandler(this.EditText4_ChooseFromListBefore);
            this.EditText4.ChooseFromListAfter += new SAPbouiCOM._IEditTextEvents_ChooseFromListAfterEventHandler(this.EditText4_ChooseFromListAfter);
            this.StaticText6 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_17").Specific));
            this.EditText6 = ((SAPbouiCOM.EditText)(this.GetItem("Item_23").Specific));
            this.EditText7 = ((SAPbouiCOM.EditText)(this.GetItem("Item_24").Specific));
            this.StaticText8 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_25").Specific));
            this.StaticText9 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_26").Specific));
            this.LinkedButton0 = ((SAPbouiCOM.LinkedButton)(this.GetItem("Item_27").Specific));
            this.LinkedButton1 = ((SAPbouiCOM.LinkedButton)(this.GetItem("Item_28").Specific));
            this.Button7 = ((SAPbouiCOM.Button)(this.GetItem("Item_29").Specific));
            this.StaticText10 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_30").Specific));
            this.EditText8 = ((SAPbouiCOM.EditText)(this.GetItem("Item_31").Specific));
            this.Button4 = ((SAPbouiCOM.Button)(this.GetItem("Item_18").Specific));
            this.Button6 = ((SAPbouiCOM.Button)(this.GetItem("Item_20").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            this.ResizeAfter += new ResizeAfterHandler(this.Form_ResizeAfter);

        }

        private SAPbouiCOM.StaticText StaticText0;

        private void OnCustomInitialize()
        {
            EditText8.Item.SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, (int)SAPbouiCOM.BoAutoFormMode.afm_Find, SAPbouiCOM.BoModeVisualBehavior.mvb_True);
            EditText2.Item.SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, (int)SAPbouiCOM.BoAutoFormMode.afm_Find, SAPbouiCOM.BoModeVisualBehavior.mvb_True);
            EditText4.Item.SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, (int)SAPbouiCOM.BoAutoFormMode.afm_Find, SAPbouiCOM.BoModeVisualBehavior.mvb_True);
           

            Matrix0.AutoResizeColumns();
            Matrix1.AutoResizeColumns();

            if(UIAPIRawForm.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE)
            {
                Matrix0.AddRow();
                ((SAPbouiCOM.EditText)Matrix0.Columns.Item("#").Cells.Item(Matrix0.RowCount).Specific).Value = Matrix0.RowCount.ToString();
                Matrix1.AddRow();
                ((SAPbouiCOM.EditText)Matrix1.Columns.Item("#").Cells.Item(Matrix1.RowCount).Specific).Value = Matrix1.RowCount.ToString();
            }
        }

        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.StaticText StaticText2;
        private SAPbouiCOM.EditText EditText1;
        private SAPbouiCOM.StaticText StaticText3;
        private SAPbouiCOM.EditText EditText2;
        private SAPbouiCOM.Button Button0;
        private SAPbouiCOM.Button Button1;
        private SAPbouiCOM.Matrix Matrix0;
        private SAPbouiCOM.Matrix Matrix1;
        private SAPbouiCOM.StaticText StaticText5;
        private SAPbouiCOM.Button Button2;
        private SAPbouiCOM.EditText EditText4;
        private SAPbouiCOM.StaticText StaticText6;
        private SAPbouiCOM.EditText EditText6;
        private SAPbouiCOM.EditText EditText7;
        private SAPbouiCOM.StaticText StaticText8;
        private SAPbouiCOM.StaticText StaticText9;
        private SAPbouiCOM.LinkedButton LinkedButton0;
        private SAPbouiCOM.LinkedButton LinkedButton1;
        private SAPbouiCOM.Button Button7;
        private SAPbouiCOM.StaticText StaticText10;
        private SAPbouiCOM.EditText EditText8;
        private SAPbouiCOM.Button Button4;
        private SAPbouiCOM.Button Button6;

        private void Form_ResizeAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {
            Matrix0 = ((SAPbouiCOM.Matrix)(this.GetItem("Item_9").Specific));
            Matrix1 = ((SAPbouiCOM.Matrix)(this.GetItem("Item_10").Specific));
            Button4 = ((SAPbouiCOM.Button)(this.GetItem("Item_18").Specific));
            Button6 = ((SAPbouiCOM.Button)(this.GetItem("Item_20").Specific));

            int fullWidth = Matrix0.Item.Left + Button4.Item.Left + Button4.Item.Width;

            Matrix0.Item.Width = (fullWidth / 2) - 30;

            Matrix1.Item.Left = Matrix0.Item.Left + Matrix0.Item.Width+10;
            Matrix1.Item.Width = Matrix0.Item.Width;

            Button6.Item.Left = Matrix0.Item.Left + Matrix0.Item.Width - Button6.Item.Width;
            Button4.Item.Left = Matrix1.Item.Left + Matrix1.Item.Width - Button4.Item.Width;

        }

        private void EditText2_ChooseFromListAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {

            try
            {
                SAPbouiCOM.ISBOChooseFromListEventArg cfle = (SAPbouiCOM.ISBOChooseFromListEventArg)pVal;
                string uid = cfle.ChooseFromListUID;
                SAPbouiCOM.DataTable dt = cfle.SelectedObjects;


                try { EditText4.Value = dt.GetValue("CardName", 0).ToString(); } catch { }
                try { EditText2.Value = dt.GetValue("CardCode", 0).ToString(); } catch { }

            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message);
            }

        }

        private void EditText4_ChooseFromListAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {

            try
            {
                SAPbouiCOM.ISBOChooseFromListEventArg cfle = (SAPbouiCOM.ISBOChooseFromListEventArg)pVal;
                string uid = cfle.ChooseFromListUID;
                SAPbouiCOM.DataTable dt = cfle.SelectedObjects;
                 
               
                try { EditText2.Value = dt.GetValue("CardCode", 0).ToString(); } catch { }
                try { EditText4.Value = dt.GetValue("CardName", 0).ToString(); } catch { }

            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message);
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
                SAPbouiCOM.ChooseFromList oCFL = oCFLs.Item("OCRD_1");
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

        private void EditText4_ChooseFromListBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
                #region add conditions to CFL

                SAPbouiCOM.ChooseFromListCollection oCFLs = UIAPIRawForm.ChooseFromLists; ;
                SAPbouiCOM.Conditions oCons = null;
                SAPbouiCOM.Condition oCon = null;
                SAPbouiCOM.ChooseFromList oCFL = oCFLs.Item("OCRD_2");
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

        private void Matrix0_ChooseFromListAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {
                if (pVal.ColUID == "Serial")
                {
                    SAPbouiCOM.ISBOChooseFromListEventArg cfle = (SAPbouiCOM.ISBOChooseFromListEventArg)pVal;
                    string uid = cfle.ChooseFromListUID;
                    SAPbouiCOM.DataTable dt = cfle.SelectedObjects;


                  
                    try { Matrix0.SetCellWithoutValidation(pVal.Row, "ItemCode", dt.GetValue("ItemCode", 0).ToString()); } catch { }
                    try { Matrix0.SetCellWithoutValidation(pVal.Row, "ItemName", dt.GetValue("ItemName", 0).ToString()); } catch { }
                    try { Matrix0.SetCellWithoutValidation(pVal.Row, "Qty", "1"); } catch { }
                    try { Matrix0.SetCellWithoutValidation(pVal.Row, "Cost", dt.GetValue("U_CostPrice", 0).ToString()); } catch { }
                    try { Matrix0.SetCellWithoutValidation(pVal.Row, "Serial", dt.GetValue("IntrSerial", 0).ToString()); } catch { }
                }

               


                if (pVal.ColUID == "ItemName")
                {
                    SAPbouiCOM.ISBOChooseFromListEventArg cfle = (SAPbouiCOM.ISBOChooseFromListEventArg)pVal;
                    string uid = cfle.ChooseFromListUID;
                    SAPbouiCOM.DataTable dt = cfle.SelectedObjects;

                    try { Matrix0.SetCellWithoutValidation(pVal.Row, "Serial", ""); } catch { }
                    try { Matrix0.SetCellWithoutValidation(pVal.Row, "Qty", "1"); } catch { }
                    try { Matrix0.SetCellWithoutValidation(pVal.Row, "ItemCode", dt.GetValue("ItemCode", 0).ToString()); } catch { }
                    try { Matrix0.SetCellWithoutValidation(pVal.Row, "ItemName", dt.GetValue("ItemName", 0).ToString()); } catch { }
                }


                if (pVal.ColUID == "ItemCode")
                {
                    SAPbouiCOM.ISBOChooseFromListEventArg cfle = (SAPbouiCOM.ISBOChooseFromListEventArg)pVal;
                    string uid = cfle.ChooseFromListUID;
                    SAPbouiCOM.DataTable dt = cfle.SelectedObjects;

                    try { Matrix0.SetCellWithoutValidation(pVal.Row, "Qty", "1"); } catch { }
                    try { Matrix0.SetCellWithoutValidation(pVal.Row, "Serial", ""); } catch { }
                    try { Matrix0.SetCellWithoutValidation(pVal.Row, "ItemName", dt.GetValue("ItemName", 0).ToString()); } catch { }
                    try { Matrix0.SetCellWithoutValidation(pVal.Row, "ItemCode", dt.GetValue("ItemCode", 0).ToString()); } catch { }
                }


                 ((SAPbouiCOM.EditText)Matrix0.Columns.Item("#").Cells.Item(Matrix0.RowCount).Specific).Value = Matrix0.RowCount.ToString();

                if (!string.IsNullOrEmpty(((SAPbouiCOM.EditText)Matrix0.Columns.Item("ItemCode").Cells.Item(Matrix0.RowCount).Specific).Value))
                {
                    Matrix0.AddRow();
                    ((SAPbouiCOM.EditText)Matrix0.Columns.Item("#").Cells.Item(Matrix0.RowCount).Specific).Value = Matrix0.RowCount.ToString();
                    Matrix0.ClearRowData(Matrix0.RowCount);

                }
            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message);
            }
        }

        private void Matrix0_ChooseFromListBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            BubbleEvent = true;
            try
            {
                #region add conditions to CFL - Serial
                if (pVal.ColUID == "Serial")
                {
                    SAPbouiCOM.ChooseFromListCollection oCFLs = UIAPIRawForm.ChooseFromLists; ;
                    SAPbouiCOM.Conditions oCons = null;
                    SAPbouiCOM.Condition oCon = null;
                    SAPbouiCOM.ChooseFromList oCFL = oCFLs.Item("OSRN");
                    oCons = oCFL.GetConditions();
                    if (oCons.Count == 0)
                    {
                        oCon = oCons.Add();
                        oCon.Alias = "Status";
                        oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                        oCon.CondVal = "0";
                        oCFL.SetConditions(oCons);
                    }

                }

                #endregion
            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, true);
            }
 


        }
    }
}
