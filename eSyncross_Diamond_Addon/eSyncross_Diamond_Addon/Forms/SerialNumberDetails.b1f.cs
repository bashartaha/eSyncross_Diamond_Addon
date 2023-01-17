using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;

namespace EvoAddon.Forms
{
    [FormAttribute("65051", "Forms/SerialNumberDetails.b1f")]
    class SerialNumberDetails : SystemFormBase
    {
        public SerialNumberDetails()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("Item_0").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
        }

        private SAPbouiCOM.EditText EditText0;

        private void OnCustomInitialize()
        {

        }
    }
}
