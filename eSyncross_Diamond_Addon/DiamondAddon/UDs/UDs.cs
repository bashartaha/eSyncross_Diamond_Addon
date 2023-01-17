
using System;
using SAPbobsCOM;
using SAPbouiCOM;
using DiamondAddon;
using DiamondAddon.Providers;

public class UDs
{
    private Recordset oRecordSet;

    private int lRetCode = 0;
    private string sErrMsg = "";


    public void AddTables()
    {
       B1Provider.CreateTables("VO_Notifications", "EVO Notifications", BoUTBTableType.bott_NoObject);
        
    }

    public void AddUserUdfs()
    {
        GC.Collect();

        #region SMS Log
        //B1Provider.CreateUDF("@VO_Notifications", "ID", "ID", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
        //B1Provider.CreateUDF("@VO_Notifications", "DocEntry", "DocEntry", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
        //B1Provider.CreateUDF("@VO_Notifications", "DocNum", "DocEntry", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
        //B1Provider.CreateUDF("@VO_Notifications", "ObjType", "ObjType", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
        //B1Provider.CreateUDF("@VO_Notifications", "DateTime", "DateTime", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
        //B1Provider.CreateUDF("@VO_Notifications", "Date", "Date", BoFieldTypes.db_Date, BoFldSubTypes.st_None, 0, null, null, null);
        //B1Provider.CreateUDF("@VO_Notifications", "Title", "Title", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
        //B1Provider.CreateUDF("@VO_Notifications", "Message", "Message", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 254, null, null, null);
        //B1Provider.CreateUDF("@VO_Notifications", "IsOpen", "DocEntry", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
        //B1Provider.CreateUDF("@VO_Notifications", "CreatedBy", "DocEntry", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
        //B1Provider.CreateUDF("@VO_Notifications", "UserId", "DocEntry", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null);
        //B1Provider.CreateUDF("@VO_Notifications", "CompanyId", "DocEntry", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, null, null, null); 

        #endregion

        #region Serial Numbers
        B1Provider.CreateUDF("OSRN", "GoldWeight", "Gold Weight", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "DiamondWt", "Diamond Weight", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "RubyWt", "Ruby Weight", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "EmraldWt", "Emrald Weight", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "SaphireWt", "Saphire Weight", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "OtherStoneWt", "Other Weight", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "TagCurrency", "Tag Currency", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "TagCurrencyExRate", "Tag Currency Rate", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "CostPrice", "Cost Price", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "AddCharges", "Add Charges", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "MarkUpPercent", "Mark Up Percent", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "TagPrice", "Tag Price", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "MaxDiscountPer", "Max Discount ", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "PearlWeight", "Pearl Weight", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "SupplierRefNo", "Supplier Reference No.", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "MetalColor", "Metal Color", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "TaggingLine1", "Tagging Line 1", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "TaggingLine2", "Tagging Line 2", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "TaggingLine3", "Tagging Line 3", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "TaggingLine4", "Tagging Line 4", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "ProfitMargin", "Profit Margin", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "DiamondWeightSub1", "Diamond Weight Sub 1", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "DiamondWeightSub2", "Diamond Weight Sub 2", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "DiamondWeightSub3", "Diamond Weight Sub 3", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "DiamondQuantity", "Diamond Quantity", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "DiamondQuantitySub1", "Diamond Quantity Sub 1", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "DiamondQuantitySub2", "Diamond QuantitySub 2", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "DiamondQuantitySub3", "Diamond Quantity Sub 3", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "DiamondClarityCode", "Diamond Clarity Code", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "DiamondClarityCodeSub1", "Diamond Clarity Code Sub1", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "DiamondClarityCodeSub2", "Diamond Clarity Code Sub2", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "DiamondClarityCodeSub3", "Diamond Clarity Code Sub3", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "DiamondShapeCode", "Diamond Shape Code", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "DiamondShapeCodeSub1", "Diamond Shape Code Sub1", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "DiamondShapeCodeSub2", "Diamond Shape Code Sub2", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "DiamondShapeCodeSub3", "Diamond Shape Code Sub3", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "DiamondColorCode", "Diamond Color Code", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "DiamondColorCodeSub1", "Diamond Color Code Sub1", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "DiamondColorCodeSub2", "Diamond Color Code Sub2", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "DiamondColorCodeSub3", "Diamond Color Code Sub3", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "DiamondCertificateNo", "Diamond Certificate No", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "DiamondCertificateNoSub1", "Diamond Certificate No Sub1", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "DiamondCertificateNoSub2", "Diamond Certificate No Sub2", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "DiamondCertificateNoSub3", "Diamond Certificate No Sub3", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "DiamondItem", "Diamond Item", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "DiamondItemSub1", "Diamond Item Sub1", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "DiamondItemSub2", "Diamond Item Sub2", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "DiamondItemSub3", "Diamond Item Sub3", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "MetalWeightType", "Metal Weight Type", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "StockPoint", "Stock Point", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "Size", "Size", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "MetalWeightSub1Type", "Metal Weight Sub1 Type", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "MetalWeightSub1", "Metal Weight Sub1", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "MetalWeightSub2Type", "Metal Weight Sub2 Type", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "MetalWeightSub2", "Metal Weight Sub2", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "MetalLoss", "Metal Loss", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "StoneWeightSub1Type", "Stone Weight Sub1 Type", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "StoneWeightSub1", "Stone Weight Sub1", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "StoneWeightSub2Type", "Stone Weight Sub2 Type", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "StoneWeightSub2", "Stone Weight Sub2", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "StoneWeightSub3Type", "Stone Weight Sub3 Type", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "StoneWeightSub3", "Stone Weight Sub3", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "Notes", "Notes", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "BufferValueBC", "Buffer Value BC", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "BufferConsiderationType", "Buffer Consideration Type", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "TotalWeight", "Total Weight", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "ReplacementCost", "Replacement Cost", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);
        B1Provider.CreateUDF("OSRN", "UOMCode", "UOM Code", BoFieldTypes.db_Float, BoFldSubTypes.st_None, 0, null, null, null);


        #endregion


    }



}
