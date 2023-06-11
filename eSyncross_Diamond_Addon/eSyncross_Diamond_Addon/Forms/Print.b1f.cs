using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;

namespace DiamondAddon.Forms
{
    [FormAttribute("DiamondAddon.Forms.Print", "Forms/Print.b1f")]
    class Print : UserFormBase
    {
        public Print()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.ComboBox0 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_0").Specific));
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_1").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_2").Specific));
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("Item_3").Specific));
            this.EditText0.ChooseFromListAfter += new SAPbouiCOM._IEditTextEvents_ChooseFromListAfterEventHandler(this.EditText0_ChooseFromListAfter);
            this.Folder0 = ((SAPbouiCOM.Folder)(this.GetItem("Item_5").Specific));
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_6").Specific));
            this.Grid0 = ((SAPbouiCOM.Grid)(this.GetItem("Item_7").Specific));
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("Item_8").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
        }

        private SAPbouiCOM.ComboBox ComboBox0;

        private void OnCustomInitialize()
        {

        }

        private SAPbouiCOM.StaticText StaticText0;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.Folder Folder0;
        private SAPbouiCOM.Button Button0;
        private SAPbouiCOM.Grid Grid0;
        private SAPbouiCOM.Button Button1;

        private void EditText0_ChooseFromListAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {

            try
            {
                SAPbouiCOM.ISBOChooseFromListEventArg cfle = (SAPbouiCOM.ISBOChooseFromListEventArg)pVal;
                string uid = cfle.ChooseFromListUID;
                SAPbouiCOM.DataTable dt = cfle.SelectedObjects;


                try { EditText0.Value = dt.GetValue("DocNum", 0).ToString(); } catch { }

                try
                {
                    Grid0.DataTable.ExecuteQuery($"Select * from \"GetSerialNumbersByDocNum\" Where \"DocNum\" = '{ dt.GetValue("DocNum", 0).ToString()}'  ");

                } catch
                {
                }


            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message);
            }


        }
    }
}
