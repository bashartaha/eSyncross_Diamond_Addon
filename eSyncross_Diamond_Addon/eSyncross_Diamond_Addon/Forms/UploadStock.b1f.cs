using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diamond_Addon.Providers;
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
            try
            {
                Button0.Item.Enabled = false;
                Application.SBO_Application.SetStatusBarMessage(
                    "Creating and uploading the available-stock CSV file...",
                    SAPbouiCOM.BoMessageTime.bmt_Short,
                    false);

                StockUploadResult result = AWS_Provider.UploadAvailableStock();

                Application.SBO_Application.StatusBar.SetSystemMessage(
                    string.Format(
                        "Uploaded {0} available serial number(s) to AWS S3 as {1}.",
                        result.RowCount,
                        result.ObjectKey),
                    SAPbouiCOM.BoMessageTime.bmt_Medium,
                    SAPbouiCOM.BoStatusBarMessageType.smt_Success);
            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(
                    "Stock upload failed: " + ex.Message,
                    SAPbouiCOM.BoMessageTime.bmt_Medium,
                    true);
            }
            finally
            {
                Button0.Item.Enabled = true;
            }
        }
    }
}
