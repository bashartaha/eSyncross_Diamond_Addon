using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;

namespace EvoAddon.Forms
{
    [FormAttribute("EvoAddon.Forms.SerialNumberDetails", "Forms/SerialNumberDetails.b1f")]
    class SerialNumberDetails : UserFormBase
    {
        public SerialNumberDetails()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Folder0 = ((SAPbouiCOM.Folder)(this.GetItem("Item_1").Specific));
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_2").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_3").Specific));
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_4").Specific));
            this.StaticText4 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_6").Specific));
            this.StaticText5 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_7").Specific));
            this.StaticText6 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_8").Specific));
            this.StaticText7 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_9").Specific));
            this.Folder1 = ((SAPbouiCOM.Folder)(this.GetItem("Item_10").Specific));
            this.Folder2 = ((SAPbouiCOM.Folder)(this.GetItem("Item_11").Specific));
            this.Folder3 = ((SAPbouiCOM.Folder)(this.GetItem("Item_13").Specific));
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("1").Specific));
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("2").Specific));
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("Item_16").Specific));
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("Item_17").Specific));
            this.EditText2 = ((SAPbouiCOM.EditText)(this.GetItem("Item_18").Specific));
            this.EditText3 = ((SAPbouiCOM.EditText)(this.GetItem("Item_19").Specific));
            this.EditText4 = ((SAPbouiCOM.EditText)(this.GetItem("Item_20").Specific));
            this.StaticText8 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_21").Specific));
            this.LinkedButton0 = ((SAPbouiCOM.LinkedButton)(this.GetItem("Item_22").Specific));
            this.EditText5 = ((SAPbouiCOM.EditText)(this.GetItem("Item_23").Specific));
            this.EditText6 = ((SAPbouiCOM.EditText)(this.GetItem("Item_24").Specific));
            this.EditText7 = ((SAPbouiCOM.EditText)(this.GetItem("Item_25").Specific));
            this.EditText8 = ((SAPbouiCOM.EditText)(this.GetItem("Item_26").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
        }

        private SAPbouiCOM.Folder Folder0;

        private void OnCustomInitialize()
        {

        }

        private SAPbouiCOM.StaticText StaticText0;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.StaticText StaticText2;
        private SAPbouiCOM.StaticText StaticText4;
        private SAPbouiCOM.StaticText StaticText5;
        private SAPbouiCOM.StaticText StaticText6;
        private SAPbouiCOM.StaticText StaticText7;
        private SAPbouiCOM.Folder Folder1;
        private SAPbouiCOM.Folder Folder2;
        private SAPbouiCOM.Folder Folder3;
        private SAPbouiCOM.Button Button0;
        private SAPbouiCOM.Button Button1;
        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.EditText EditText1;
        private SAPbouiCOM.EditText EditText2;
        private SAPbouiCOM.EditText EditText3;
        private SAPbouiCOM.EditText EditText4;
        private SAPbouiCOM.StaticText StaticText8;
        private SAPbouiCOM.LinkedButton LinkedButton0;
        private SAPbouiCOM.EditText EditText5;
        private SAPbouiCOM.EditText EditText6;
        private SAPbouiCOM.EditText EditText7;
        private SAPbouiCOM.EditText EditText8;
    }
}
