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
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("1").Specific));
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("2").Specific));
            this.Matrix0 = ((SAPbouiCOM.Matrix)(this.GetItem("Item_9").Specific));
            this.Matrix1 = ((SAPbouiCOM.Matrix)(this.GetItem("Item_10").Specific));
            this.StaticText5 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_13").Specific));
            this.Button2 = ((SAPbouiCOM.Button)(this.GetItem("Item_14").Specific));
            this.EditText4 = ((SAPbouiCOM.EditText)(this.GetItem("Item_16").Specific));
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
    }
}
