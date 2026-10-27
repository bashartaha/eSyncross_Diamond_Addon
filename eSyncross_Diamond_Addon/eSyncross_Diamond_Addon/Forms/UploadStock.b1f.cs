using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;

namespace DiamondAddon.Forms
{
    [FormAttribute("DiamondAddon.Forms.UploadStock", "Forms/UploadStock.b1f")]
    class UploadStock : UserFormBase
    {
        public UploadStock()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_0").Specific));
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_1").Specific));
            this.Button0.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.Button0_ClickAfter);
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

        private SAPbouiCOM.Button Button0;

        private void Button0_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            // this button will call AWS_Provider and trigger the fucitns 
            // call SQL query to fetch all serial numbers
            // create CSV file 
            //Upload to a bucket in AWS

        }
    }
}
