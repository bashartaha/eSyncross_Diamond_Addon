using DiamondAddon.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondAddon.Providers
{
    class AddonProvider
    {

        public static void CreateDatabase()
        {
            string[,] YesNOValidValues = new string[2, 2] { { "Y", "Yes" }, { "N", "No" } };



            #region BOE

            #region Header
            //added by imran to test github

            Table EVO_OPICK = new Table("EVO_OPICK", "EVO Picklist Header");
            EVO_OPICK.TableType = SAPbobsCOM.BoUTBTableType.bott_Document;
            EVO_OPICK.FieldsList = new List<UserDefinedField>();

            EVO_OPICK.FieldsList.Add(new UserDefinedField("@EVO_OPICK", "CardCode", "BP Code", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50, null, null, null));
            EVO_OPICK.FieldsList.Add(new UserDefinedField("@EVO_OPICK", "CardName", "BP Name", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 200, null, null, null));
            EVO_OPICK.FieldsList.Add(new UserDefinedField("@EVO_OPICK", "ContactPerson", "Contact Person", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 20, null, null, null));
            EVO_OPICK.FieldsList.Add(new UserDefinedField("@EVO_OPICK", "NumAtCard", "BP Reference", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50, null, null, null));

            EVO_OPICK.FieldsList.Add(new UserDefinedField("@EVO_OPICK", "OrderNum", "PO Order #", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50, null, null, null));
            EVO_OPICK.FieldsList.Add(new UserDefinedField("@EVO_OPICK", "OrderEntry", "PO DocEntry", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50, null, null, null));
            EVO_OPICK.FieldsList.Add(new UserDefinedField("@EVO_OPICK", "PickMethod", "PO DocEntry", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50, null, null, null));

            EVO_OPICK.FieldsList.Add(new UserDefinedField("@EVO_OPICK", "ProjectFree", "ProjectFree", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 1, null, null, null));
            EVO_OPICK.FieldsList.Add(new UserDefinedField("@EVO_OPICK", "FilterProject", "FilterProject", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 1, null, null, null));
            EVO_OPICK.FieldsList.Add(new UserDefinedField("@EVO_OPICK", "Remarks", "Original Docs Received", SAPbobsCOM.BoFieldTypes.db_Memo, SAPbobsCOM.BoFldSubTypes.st_None, 0, null, null, null));
            EVO_OPICK.FieldsList.Add(new UserDefinedField("@EVO_OPICK", "Project", "Project", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 20, null, null, null));
            EVO_OPICK.FieldsList.Add(new UserDefinedField("@EVO_OPICK", "TargetNum", "TargetNum", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50, null, null, null));
            EVO_OPICK.FieldsList.Add(new UserDefinedField("@EVO_OPICK", "TargetEntry", "TargetEntry", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50, null, null, null));
            EVO_OPICK.FieldsList.Add(new UserDefinedField("@EVO_OPICK", "TargetType", "TargetType", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50, null, null, null));
            EVO_OPICK.FieldsList.Add(new UserDefinedField("@EVO_OPICK", "TargetLocation", "Target Location", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50, null, null, null));
            EVO_OPICK.FieldsList.Add(new UserDefinedField("@EVO_OPICK", "TargetLocationId", "Target Location Id", SAPbobsCOM.BoFieldTypes.db_Numeric, SAPbobsCOM.BoFldSubTypes.st_None, 0, null, null, null));
            EVO_OPICK.FieldsList.Add(new UserDefinedField("@EVO_OPICK", "WhseCode", "WhseCode ", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50, null, null, null));
            EVO_OPICK.FieldsList.Add(new UserDefinedField("@EVO_OPICK", "WhseName", "WhseName ", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50, null, null, null));
            EVO_OPICK.FieldsList.Add(new UserDefinedField("@EVO_OPICK", "ManagedByBin", "ManagedByBin ", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50, null, null, null));

            B1Provider.Build_UDTs_And_UDFs(EVO_OPICK);

            #endregion


            #region Childs

            #region Picklist Document Rows

            Table EVO_PICK1 = new Table("EVO_PICK1", "EVO Picklist Rows");
            EVO_PICK1.TableType = SAPbobsCOM.BoUTBTableType.bott_DocumentLines;
            EVO_PICK1.FieldsList = new List<UserDefinedField>();

            EVO_PICK1.FieldsList.Add(new UserDefinedField("@EVO_PICK1", "ItemCode", "ItemCode", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50, null, null, null));
            EVO_PICK1.FieldsList.Add(new UserDefinedField("@EVO_PICK1", "ItemName ", "Item Name ", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 200, null, null, null));
            EVO_PICK1.FieldsList.Add(new UserDefinedField("@EVO_PICK1", "OrderQuantity ", "Order Quantity ", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Quantity, 0, null, null, null));
            EVO_PICK1.FieldsList.Add(new UserDefinedField("@EVO_PICK1", "OpenQuantity ", "Open Quantity ", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Quantity, 0, null, null, null));

            EVO_PICK1.FieldsList.Add(new UserDefinedField("@EVO_PICK1", "PickQuantity", "Pick Quantity", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Quantity, 0, null, null, null));
            EVO_PICK1.FieldsList.Add(new UserDefinedField("@EVO_PICK1", "PickInvQuantity", "Pick Inv Quantity", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Quantity, 0, null, null, null));

            EVO_PICK1.FieldsList.Add(new UserDefinedField("@EVO_PICK1", "Remarks ", "Remarks ", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50, null, null, null));


            EVO_PICK1.FieldsList.Add(new UserDefinedField("@EVO_PICK1", "BaseLine ", "Base Line ", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50, null, null, null));
            EVO_PICK1.FieldsList.Add(new UserDefinedField("@EVO_PICK1", "BaseEntry", "Base Entry", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50, null, null, null));
            EVO_PICK1.FieldsList.Add(new UserDefinedField("@EVO_PICK1", "BaseNum", "Base Num", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50, null, null, null));
            EVO_PICK1.FieldsList.Add(new UserDefinedField("@EVO_PICK1", "BaseType", "Base Type", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50, null, null, null));

            EVO_PICK1.FieldsList.Add(new UserDefinedField("@EVO_PICK1", "NumPerUnit", "NumPerUnit", SAPbobsCOM.BoFieldTypes.db_Numeric, SAPbobsCOM.BoFldSubTypes.st_None, 0, null, null, null));
            EVO_PICK1.FieldsList.Add(new UserDefinedField("@EVO_PICK1", "InvUomCode", "InvUomCode", SAPbobsCOM.BoFieldTypes.db_Numeric, SAPbobsCOM.BoFldSubTypes.st_None, 0, null, null, null));
            EVO_PICK1.FieldsList.Add(new UserDefinedField("@EVO_PICK1", "UomCode", "UomCode", SAPbobsCOM.BoFieldTypes.db_Numeric, SAPbobsCOM.BoFldSubTypes.st_None, 0, null, null, null));
            EVO_PICK1.FieldsList.Add(new UserDefinedField("@EVO_PICK1", "InvUomName", "InvUomName", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50, null, null, null));
            EVO_PICK1.FieldsList.Add(new UserDefinedField("@EVO_PICK1", "UomName", "UomName", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50, null, null, null));


            B1Provider.Build_UDTs_And_UDFs(EVO_PICK1);

            #endregion

            #region Picklist Document Suggested

            Table EVO_PICK2 = new Table("EVO_PICK2", "EVO Picklist Pick Suggested");
            EVO_PICK2.TableType = SAPbobsCOM.BoUTBTableType.bott_DocumentLines;
            EVO_PICK2.FieldsList = new List<UserDefinedField>();

            EVO_PICK2.FieldsList.Add(new UserDefinedField("@EVO_PICK2", "ItemCode", "ItemCode", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50, null, null, null));
            EVO_PICK2.FieldsList.Add(new UserDefinedField("@EVO_PICK2", "ItemName", "Item Name ", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 200, null, null, null));
            EVO_PICK2.FieldsList.Add(new UserDefinedField("@EVO_PICK2", "SerialBatch", "SerialBatch", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 200, null, null, null));

            EVO_PICK2.FieldsList.Add(new UserDefinedField("@EVO_PICK2", "BinId", "Bin Id", SAPbobsCOM.BoFieldTypes.db_Numeric, SAPbobsCOM.BoFldSubTypes.st_None, 0, null, null, null));
            EVO_PICK2.FieldsList.Add(new UserDefinedField("@EVO_PICK2", "BinCode", "Bin Code", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 200, null, null, null));
            EVO_PICK2.FieldsList.Add(new UserDefinedField("@EVO_PICK2", "WhseCode", "WhseCode", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50, null, null, null));

            EVO_PICK2.FieldsList.Add(new UserDefinedField("@EVO_PICK2", "PickQuantity", "Pick Quantity", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Quantity, 0, null, null, null));

            EVO_PICK2.FieldsList.Add(new UserDefinedField("@EVO_PICK2", "Sequence", "Sequence", SAPbobsCOM.BoFieldTypes.db_Numeric, SAPbobsCOM.BoFldSubTypes.st_None, 0, null, null, null));



            B1Provider.Build_UDTs_And_UDFs(EVO_PICK2);

            #endregion


            #region Picklist Document Actual

            Table EVO_PICK3 = new Table("EVO_PICK3", "EVO Picklist Pick Actual");
            EVO_PICK3.TableType = SAPbobsCOM.BoUTBTableType.bott_DocumentLines;
            EVO_PICK3.FieldsList = new List<UserDefinedField>();

            EVO_PICK3.FieldsList.Add(new UserDefinedField("@EVO_PICK3", "ItemCode", "ItemCode", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50, null, null, null));
            EVO_PICK3.FieldsList.Add(new UserDefinedField("@EVO_PICK3", "ItemName", "Item Name ", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 200, null, null, null));
            EVO_PICK3.FieldsList.Add(new UserDefinedField("@EVO_PICK3", "SerialBatch", "SerialBatch", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 200, null, null, null));

            EVO_PICK3.FieldsList.Add(new UserDefinedField("@EVO_PICK3", "BinId", "Bin Id", SAPbobsCOM.BoFieldTypes.db_Numeric, SAPbobsCOM.BoFldSubTypes.st_None, 0, null, null, null));
            EVO_PICK3.FieldsList.Add(new UserDefinedField("@EVO_PICK3", "BinCode", "Bin Code", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 200, null, null, null));
            EVO_PICK3.FieldsList.Add(new UserDefinedField("@EVO_PICK3", "WhseCode", "WhseCode", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 50, null, null, null));

            EVO_PICK3.FieldsList.Add(new UserDefinedField("@EVO_PICK3", "PickQuantity", "Pick Quantity", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Quantity, 0, null, null, null));

            EVO_PICK3.FieldsList.Add(new UserDefinedField("@EVO_PICK3", "Sequence", "Sequence", SAPbobsCOM.BoFieldTypes.db_Numeric, SAPbobsCOM.BoFldSubTypes.st_None, 0, null, null, null));



            B1Provider.Build_UDTs_And_UDFs(EVO_PICK3);
            
            #endregion




            #endregion


            #region Object


                        UserDefinedObject Obj_EVO_OPICK = new UserDefinedObject();

            Obj_EVO_OPICK.Table = EVO_OPICK;
            Obj_EVO_OPICK.ObjType = SAPbobsCOM.BoUDOObjType.boud_Document;
            Obj_EVO_OPICK.AllowFind = SAPbobsCOM.BoYesNoEnum.tYES;
            Obj_EVO_OPICK.AllowClose = SAPbobsCOM.BoYesNoEnum.tYES;
            Obj_EVO_OPICK.AllowCancel = SAPbobsCOM.BoYesNoEnum.tYES;

            Obj_EVO_OPICK.FindColumns = new string[] { "U_CardCode", "U_CardName", "DocEntry", "DocNum", "Status", "Canceled", "U_TargetNum" };

            Obj_EVO_OPICK.AllowCreateDafultForm = SAPbobsCOM.BoYesNoEnum.tNO;
            Obj_EVO_OPICK.EnableEnhancedForm = SAPbobsCOM.BoYesNoEnum.tNO;


            Obj_EVO_OPICK.CanLog = SAPbobsCOM.BoYesNoEnum.tNO;
            Obj_EVO_OPICK.Childs = new List<Table>();
            Obj_EVO_OPICK.Childs.Add(EVO_PICK1);
            Obj_EVO_OPICK.Childs.Add(EVO_PICK2);
            Obj_EVO_OPICK.Childs.Add(EVO_PICK3);
            B1Provider.Build_UDO(Obj_EVO_OPICK);


            #endregion

            #endregion

            #region Create Views

            #endregion


        }

        public static void UploadReports()
        {
            try
            {
                string file = string.Format("{0}\\Resources\\CrystalReports\\Picklist.rpt", System.AppDomain.CurrentDomain.BaseDirectory);
                B1Provider.uploadCrystalReport(file, "Picklist", "EVO_OPICK", "WMS - EVO", "EVO_OPICK", true);

                LogProvider.LogReportUpload("Add", "Picklist", true, "");
            }
            catch (Exception ex)
            {
                LogProvider.LogReportUpload("Add", "Picklist", false, ex.Message + "|" + ex.StackTrace);
            }

        }
    }
}