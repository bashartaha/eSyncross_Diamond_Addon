using Diamond_Addon.Models;
using SAPbobsCOM;
using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diamond_Addon.Providers
{
    class AddonProvider
    {

        public static void CreateDatabase()
        {
            string[,] YesNOValidValues = new string[2, 2] { { "Y", "Yes" }, { "N", "No" } };
            string[,] ConsignmentArray = new string[2, 2] { { "1", "Normal" }, { "2", "Consignment" } };


            #region Serial Numbers
            B1Provider.CreateUDF("OSRN", "GoldWeight", "Gold Weight", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 0, null, null, null);
            B1Provider.CreateUDF("OSRN", "DiamondWt", "Diamond Weight", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 0, null, null, null);
            B1Provider.CreateUDF("OSRN", "RubyWt", "Ruby Weight", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 0, null, null, null);
            B1Provider.CreateUDF("OSRN", "EmraldWt", "Emrald Weight", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 0, null, null, null);
            B1Provider.CreateUDF("OSRN", "SaphireWt", "Saphire Weight", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 0, null, null, null);
            B1Provider.CreateUDF("OSRN", "OtherStoneWt", "Other Weight", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 0, null, null, null);
            B1Provider.CreateUDF("OSRN", "TagCurrency", "Tag Currency", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 20, null, null, null);
            B1Provider.CreateUDF("OSRN", "TagCurrencyExRate", "Tag Currency Rate", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 0, null, null, null);
            B1Provider.CreateUDF("OSRN", "CostPrice", "Cost Price", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 0, null, null, null);
            B1Provider.CreateUDF("OSRN", "AddCharges", "Add Charges", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 0, null, null, null);
            B1Provider.CreateUDF("OSRN", "MarkUpPercent", "Mark Up Percent", BoFieldTypes.db_Float, BoFldSubTypes.st_Percentage, 0, null, null, null);
            B1Provider.CreateUDF("OSRN", "TagPrice", "Tag Price", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 0, null, null, null);
            B1Provider.CreateUDF("OSRN", "MaxDiscountPer", "Max Discount ", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 0, null, null, null);
            B1Provider.CreateUDF("OSRN", "PearlWeight", "Pearl Weight", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 0, null, null, null);
            B1Provider.CreateUDF("OSRN", "SupplierRefNo", "Supplier Reference No.", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "MColor", "Metal Color", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "TaggingLine1", "Tagging Line 1", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "TaggingLine2", "Tagging Line 2", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "TaggingLine3", "Tagging Line 3", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "TaggingLine4", "Tagging Line 4", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "ProfitMargin", "Profit Margin", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 0, null, null, null);
            B1Provider.CreateUDF("OSRN", "DWeightSub1", "Diamond Weight Sub 1", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 0, null, null, null);
            B1Provider.CreateUDF("OSRN", "DWeightSub2", "Diamond Weight Sub 2", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 0, null, null, null);
            B1Provider.CreateUDF("OSRN", "DWeightSub3", "Diamond Weight Sub 3", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 0, null, null, null);
            B1Provider.CreateUDF("OSRN", "DQuantity", "Diamond Quantity", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 0, null, null, null);
            B1Provider.CreateUDF("OSRN", "DQuantitySub1", "Diamond Quantity Sub 1", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 0, null, null, null);
            B1Provider.CreateUDF("OSRN", "DQuantitySub2", "Diamond QuantitySub 2", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 0, null, null, null);
            B1Provider.CreateUDF("OSRN", "DQuantitySub3", "Diamond Quantity Sub 3", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 0, null, null, null);
            B1Provider.CreateUDF("OSRN", "ClarityCode", "Diamond Clarity Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "ClarityCodeSub1", "Clarity Code Sub1", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "ClarityCodeSub2", "Clarity Code Sub2", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "ClarityCodeSub3", "Clarity Code Sub3", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "DShapeCode", "Diamond Shape Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "DShapeCodeSub1", "Diamond Shape Code Sub1", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "DShapeCodeSub2", "Diamond Shape Code Sub2", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "DShapeCodeSub3", "Diamond Shape Code Sub3", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "DColorCode", "Diamond Color Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "DColorCodeSub1", "Diamond Color Code Sub1", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "DColorCodeSub2", "Diamond Color Code Sub2", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "DColorCodeSub3", "Diamond Color Code Sub3", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "CertificateNo", "Diamond Certificate No", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "CertificateNoSub1", "Diamond Certificate No Sub1", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "CertificateNoSub2", "Diamond Certificate No Sub2", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "CertificateNoSub3", "Diamond Certificate No Sub3", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "DItem", "Diamond Item", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "DItemSub1", "Diamond Item Sub1", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "DItemSub2", "Diamond Item Sub2", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "DItemSub3", "Diamond Item Sub3", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "MWeightType", "Metal Weight Type", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "StockPoint", "Stock Point", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "Size", "Size", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 0, null, null, null);
            B1Provider.CreateUDF("OSRN", "MWeightSub1Type", "Metal Weight Sub1 Type", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "MWeightSub1", "Metal Weight Sub1", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 0, null, null, null);
            B1Provider.CreateUDF("OSRN", "MWeightSub2Type", "Metal Weight Sub2 Type", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "MWeightSub2", "Metal Weight Sub2", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 0, null, null, null);
            B1Provider.CreateUDF("OSRN", "MLoss", "Metal Loss", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "SWeightSub1Type", "Stone Weight Sub1 Type", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "SWeightSub1", "Stone Weight Sub1", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 0, null, null, null);
            B1Provider.CreateUDF("OSRN", "SWeightSub2Type", "Stone Weight Sub2 Type", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "SWeightSub2", "Stone Weight Sub2", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 0, null, null, null);
            B1Provider.CreateUDF("OSRN", "SWeightSub3Type", "Stone Weight Sub3 Type", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "SWeightSub3", "Stone Weight Sub3", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 0, null, null, null);
            B1Provider.CreateUDF("OSRN", "Notes", "Notes", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 250, null, null, null);
            B1Provider.CreateUDF("OSRN", "BufferValueBC", "Buffer Value BC", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 0, null, null, null);
            B1Provider.CreateUDF("OSRN", "BufferType", "Buffer Consideration Type", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "TotalWeight", "Total Weight", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 0, null, null, null);
            B1Provider.CreateUDF("OSRN", "ReplacementCost", "Replacement Cost", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 0, null, null, null);
            B1Provider.CreateUDF("OSRN", "UOMCode", "UOM Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
            B1Provider.CreateUDF("OSRN", "SalesPrice", "Sales Price", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 50, null, null, null);



            #endregion



            B1Provider.CreateUDF("OPDN", "ESY_StockType", "Stock Type", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 20, ConsignmentArray, "1", null);
            B1Provider.CreateUDF("OWHS", "ESY_StockType", "Stock Type", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 20, ConsignmentArray, "1", null);
        }


    }
}