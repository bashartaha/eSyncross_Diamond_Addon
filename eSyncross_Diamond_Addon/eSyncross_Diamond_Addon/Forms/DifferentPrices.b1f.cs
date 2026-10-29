using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;
using Diamond_Addon.Models;
using System.Globalization;

namespace DiamondAddon.Forms
{
    [FormAttribute("DifferentPrices", "Forms/DifferentPrices.b1f")]
    class DifferentPrices : UserFormBase
    {
        public DifferentPrices(List<PriceDifference> list)
        {
            UIAPIRawForm.Freeze(true);

            try
            {
                SAPbouiCOM.DataTable pricesDataTable =
                    UIAPIRawForm.DataSources.DataTables.Item("DT_0");

                pricesDataTable.Rows.Clear();

                if (list != null)
                {
                    foreach (PriceDifference priceDifference in list)
                    {
                        pricesDataTable.Rows.Add(1);
                        int rowIndex = pricesDataTable.Rows.Count - 1;

                        pricesDataTable.SetValue("ItemCode", rowIndex, priceDifference.ItemCode);
                        pricesDataTable.SetValue("ItemName", rowIndex, priceDifference.ItemName);
                        pricesDataTable.SetValue(
                            "CurrPrice",
                            rowIndex,
                            priceDifference.CurrentPrice.ToString("0.######", CultureInfo.CurrentCulture));
                        pricesDataTable.SetValue(
                            "TagPrice",
                            rowIndex,
                            priceDifference.NewTagPrice.ToString("0.######", CultureInfo.CurrentCulture));
                    }
                }

                ((SAPbouiCOM.EditTextColumn)Grid0.Columns.Item("ItemCode")).LinkedObjectType = "4";
                Grid0.Columns.Item("ItemCode").Editable = false;
                Grid0.Columns.Item("ItemName").Editable = false;
                Grid0.Columns.Item("CurrPrice").Editable = false;
                Grid0.Columns.Item("TagPrice").Editable = false;
                Grid0.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message);
            }
            finally
            {
                UIAPIRawForm.Freeze(false);
            }
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Folder0 = ((SAPbouiCOM.Folder)(this.GetItem("Item_1").Specific));
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("2").Specific));
            this.Grid0 = ((SAPbouiCOM.Grid)(this.GetItem("Item_3").Specific));
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

        private SAPbouiCOM.Button Button0;
        private SAPbouiCOM.Grid Grid0;
    }
}
