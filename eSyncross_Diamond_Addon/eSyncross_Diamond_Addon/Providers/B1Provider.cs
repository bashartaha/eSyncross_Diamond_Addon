using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SAPbobsCOM;
using System.IO;
using System.Data;
using Newtonsoft.Json;
 
using Diamond_Addon;
using Diamond_Addon.Models;
using SAPbouiCOM.Framework;
using System.Xml;

namespace Diamond_Addon.Providers
{
    public static class B1Provider
    {
        public static SAPbouiCOM.Application SBO_Application;
        public static SAPbobsCOM.Company oCompany;

      

        public static SAPbobsCOM.Recordset oRecordset(string command)
        {
            SAPbobsCOM.Recordset oRecordset = (SAPbobsCOM.Recordset)B1Provider.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            oRecordset.DoQuery(command);

            return oRecordset;
        }

        internal static string GetReceivingWarehouseCode(string value)
        {
            SAPbobsCOM.Recordset result = oRecordset($"Select \"WhsCode\" from \"OWHS\" where \"U_ESY_StockType\" = '{value}'");

            if (result.RecordCount > 0)
            {
                return result.Fields.Item(0).Value.ToString();
            }
            else
            {
                return null;
            }
        }

        internal static string GetNextSerialNumber(string itemCode)
        {
            SAPbobsCOM.Recordset result = oRecordset($" EXEC ESY_SP_GetNextSerialNumber '{itemCode}'");

            if (result.RecordCount > 0)
            {
                return  result.Fields.Item(0).Value.ToString();
            }
            else
            {
                return null;
            }
        }


        public static string getDocEntryByXML(string xml)
        {
            System.Xml.XmlDocument oXml = null;
            oXml = new XmlDocument();
            oXml.LoadXml(xml);
            return oXml.SelectSingleNode("/DocumentParams/DocEntry").InnerText;
        }

        #region UDs

        #region UDs Creations Functinos

        public static int CreateTables(string tableName, string tableDescription, SAPbobsCOM.BoUTBTableType tableType)
        {
            int lRetCode = -1;

            string sErrMsg;

            //****************************************************************************
            // Add Header and Row tables
            //****************************************************************************
            SAPbobsCOM.UserTablesMD oUserTablesMD = null;
            oUserTablesMD = ((SAPbobsCOM.UserTablesMD)(oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables)));

            bool exist = TableExist(tableName);
            if (exist == false)
            {
                oUserTablesMD.TableName = tableName;
                oUserTablesMD.TableDescription = tableDescription;
                oUserTablesMD.TableType = tableType;
                // Add the table
                lRetCode = oUserTablesMD.Add();

                // check for errors in the process
                if (lRetCode != 0)
                {
                    LogProvider.LogTableCreation("Add", tableName, false, oCompany.GetLastErrorDescription());
                }
                else
                {
                    LogProvider.LogTableCreation("Add", tableName, true, "");
                }



            }
            else
            {

                // oUserTablesMD.TableDescription = tableDescription;

                // Add the table
                lRetCode = 0;

                LogProvider.LogTableCreation("Update", tableName, true, "");

            }





            System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserTablesMD);
            GC.Collect(); // Release the handle to the User Fields

            return lRetCode;

        }


        public static void CreateUDF(string tableName, string fieldName, string fieldDescription, SAPbobsCOM.BoFieldTypes type, SAPbobsCOM.BoFldSubTypes subType, int size, string[,] validValues, string defaultValue, string linkedTable)
        {

            int lRetCode;
            int i;


            SAPbobsCOM.UserFieldsMD oUserFieldsMD = null;
            oUserFieldsMD = ((SAPbobsCOM.UserFieldsMD)(oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields)));

            i = Check_UDF(tableName, fieldName);
            if (oUserFieldsMD.GetByKey(tableName, i))
            {
                #region Update

                //oUserFieldsMD.Description = fieldDescription;


                //if (size > 0)
                //{
                //    oUserFieldsMD.Size = size;
                //}

                //oUserFieldsMD.Type = type;
                //oUserFieldsMD.SubType = subType;


                //if (validValues != null)
                //{
                //    for (int j = 0; j < validValues.GetLength(0); j++)
                //    {
                //        oUserFieldsMD.ValidValues.Value = validValues[j, 0];
                //        oUserFieldsMD.ValidValues.Description = validValues[j, 1];
                //        oUserFieldsMD.ValidValues.Add();

                //    }

                //    // oUserFieldsMD.DefaultValue = "0";
                //}
                //if (!string.IsNullOrEmpty(defaultValue))
                //{
                //    oUserFieldsMD.DefaultValue = defaultValue;
                //}

                //if (!string.IsNullOrEmpty(linkedTable))
                //{
                //    oUserFieldsMD.LinkedTable = linkedTable;
                //}

                //if (linkedSystemObject != UDFLinkedSystemObjectTypesEnum.ulNone)
                //{
                //    oUserFieldsMD.LinkedSystemObject = linkedSystemObject;
                //}

                //// Adding the Field to the Table
                //lRetCode = oUserFieldsMD.Update();
                //// Check for errors
                //if (lRetCode != 0)
                //{
                //    LogProvider.LogFieldsCreation("Update", tableName, fieldName, false, oCompany.GetLastErrorDescription());
                //}
                //else
                //{
                //    LogProvider.LogFieldsCreation("Update", tableName, fieldName, true, "");
                //}

                #endregion

                LogProvider.LogFieldsCreation("Update", tableName, fieldName, true, "The field was created earlier or similar field exists");
            }
            else
            {
                #region Add

                oUserFieldsMD.TableName = tableName;
                oUserFieldsMD.Name = fieldName;
                oUserFieldsMD.Description = fieldDescription;


                if (size > 0)
                {
                    oUserFieldsMD.EditSize = size;
                }

                oUserFieldsMD.Type = type;
                oUserFieldsMD.SubType = subType;


                if (validValues != null)
                {
                    for (int j = 0; j < validValues.GetLength(0); j++)
                    {
                        oUserFieldsMD.ValidValues.Value = validValues[j, 0];
                        oUserFieldsMD.ValidValues.Description = validValues[j, 1];
                        oUserFieldsMD.ValidValues.Add();

                    }

                    // oUserFieldsMD.DefaultValue = "0";
                }
                if (!string.IsNullOrEmpty(defaultValue))
                {
                    oUserFieldsMD.DefaultValue = defaultValue;
                }

                if (!string.IsNullOrEmpty(linkedTable))
                {
                    oUserFieldsMD.LinkedTable = linkedTable;
                }



                // Adding the Field to the Table
                lRetCode = oUserFieldsMD.Add();
                // Check for errors
                if (lRetCode != 0)
                {
                    LogProvider.LogFieldsCreation("Add", tableName, fieldName, false, oCompany.GetLastErrorDescription());
                }
                else
                {
                    LogProvider.LogFieldsCreation("Add", tableName, fieldName, true, "");
                }


                #endregion
            }

            System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserFieldsMD);
            GC.Collect(); // Release the handle to the User Fields
        }

       

        public static void CreateUDO(UserDefinedObject obj)
        {
            try
            {
                if (!ObjectExist(obj.Table.TableName))
                {
                    UserObjectsMD userObjectsMD = null;
                    userObjectsMD = (UserObjectsMD)oCompany.GetBusinessObject(BoObjectTypes.oUserObjectsMD);
                    userObjectsMD.CanCancel = obj.AllowCancel;
                    userObjectsMD.CanClose = obj.AllowClose;

                    if (obj.CanLog == BoYesNoEnum.tYES)
                    {
                        userObjectsMD.CanLog = obj.CanLog;
                        userObjectsMD.LogTableName = "A" + obj.Table.TableName;
                    }

                    if (obj.AllowCreateDafultForm == BoYesNoEnum.tYES)
                    {
                        userObjectsMD.CanCreateDefaultForm = BoYesNoEnum.tYES;
                        userObjectsMD.EnableEnhancedForm = obj.EnableEnhancedForm;

                        if (obj.MenuItem == BoYesNoEnum.tYES)
                        {
                            userObjectsMD.MenuItem = BoYesNoEnum.tYES;
                            userObjectsMD.MenuCaption = obj.MenuCaption;
                            userObjectsMD.FatherMenuID = obj.FatherMenuID;
                            userObjectsMD.Position = -1;
                            userObjectsMD.MenuUID = obj.MenuUID;
                        }


                        if (obj.FormsColumns != null)
                        {
                            for (int i = 0; i < obj.FormsColumns.Count(); i++)
                            {
                                userObjectsMD.FormColumns.SonNumber = 0;
                                userObjectsMD.FormColumns.FormColumnAlias = obj.FormsColumns[i];
                                userObjectsMD.FormColumns.FormColumnDescription = obj.FormsColumns[i];
                                // userObjectsMD.FormColumns.Editable = BoYesNoEnum.tYES;
                                userObjectsMD.FormColumns.Add();
                            }

                            if (obj.Table.FieldsList != null)
                            {
                                foreach (var field in obj.Table.FieldsList)
                                {
                                    userObjectsMD.FormColumns.SonNumber = 0;
                                    userObjectsMD.FormColumns.FormColumnAlias = "U_" + field.FieldCode;
                                    userObjectsMD.FormColumns.FormColumnDescription = field.FieldName;
                                    userObjectsMD.FormColumns.Editable = BoYesNoEnum.tYES;
                                    userObjectsMD.FormColumns.Add();
                                }
                            }
                        }





                    }


                    userObjectsMD.CanDelete = obj.AllowDelete;
                    userObjectsMD.CanFind = obj.AllowFind;


                    if (obj.AllowFind == BoYesNoEnum.tYES)
                    {
                        for (int i = 0; i < obj.FindColumns.Count(); i++)
                        {
                            userObjectsMD.FindColumns.Add();
                            userObjectsMD.FindColumns.ColumnAlias = obj.FindColumns[i];
                        }

                    }

                    userObjectsMD.Code = obj.Table.TableName;
                    userObjectsMD.ManageSeries = BoYesNoEnum.tNO;
                    userObjectsMD.Name = obj.Table.TableDescription;
                    userObjectsMD.ObjectType = obj.ObjType;
                    userObjectsMD.TableName = obj.Table.TableName;
                    userObjectsMD.UseUniqueFormType = BoYesNoEnum.tYES;

                    #region Childs
                    if (obj.Childs != null)
                    {
                        int childNumber = 1;
                        foreach (Table table in obj.Childs)
                        {
                            userObjectsMD.ChildTables.Add();
                            userObjectsMD.ChildTables.TableName = table.TableName;

                            if (table.FieldsList != null)
                            {
                                if (obj.AllowCreateDafultForm == BoYesNoEnum.tYES)
                                {
                                    int columnNumber = 1;

                                    userObjectsMD.EnhancedFormColumns.ColumnAlias = "LineId";
                                    userObjectsMD.EnhancedFormColumns.ColumnIsUsed = BoYesNoEnum.tYES;
                                    userObjectsMD.EnhancedFormColumns.ColumnNumber = columnNumber; // as number 1
                                    userObjectsMD.EnhancedFormColumns.ChildNumber = childNumber;
                                    userObjectsMD.EnhancedFormColumns.Add();


                                    // add new number
                                    columnNumber++;

                                    foreach (var field in table.FieldsList)
                                    {
                                        userObjectsMD.EnhancedFormColumns.ColumnAlias = "U_" + field.FieldCode;
                                        userObjectsMD.EnhancedFormColumns.ColumnDescription = field.FieldName;
                                        userObjectsMD.EnhancedFormColumns.ColumnIsUsed = BoYesNoEnum.tYES;
                                        userObjectsMD.EnhancedFormColumns.Editable = BoYesNoEnum.tYES;
                                        userObjectsMD.EnhancedFormColumns.ColumnNumber = columnNumber;
                                        userObjectsMD.EnhancedFormColumns.ChildNumber = childNumber;
                                        userObjectsMD.EnhancedFormColumns.Add();
                                        columnNumber++;
                                    }
                                }
                            }

                            childNumber++;
                        }


                    }
                    #endregion

                    int lRetCode = userObjectsMD.Add();
                    // string xml = userObjectsMD.GetAsXML();
                    // check for errors in the process
                    if (lRetCode != 0)
                    {
                        LogProvider.LogObjectsCreation("Add", obj.Table.TableName + "-" + obj.Table.TableDescription, false, oCompany.GetLastErrorDescription());
                    }
                    else
                    {
                        LogProvider.LogObjectsCreation("Add", obj.Table.TableName + "-" + obj.Table.TableDescription, true, "");
                    }


                    System.Runtime.InteropServices.Marshal.ReleaseComObject(userObjectsMD);
                    GC.Collect(); // Release the handle to the User Fields
                    userObjectsMD = null;
                }
                else
                {
                    LogProvider.LogObjectsCreation("Update", obj.Table.TableName + "-" + obj.Table.TableDescription, true, "");
                }

            }
            catch (Exception ex)
            {
                LogProvider.LogObjectsCreation("-", obj.Table.TableName + "-" + obj.Table.TableDescription, false, ex.Message);
            }



        }

        public static void InitialValues(ParentTable parentTable)
        {
            try
            {
                GeneralService generalService = null;
                GeneralData generalData = null;
                GeneralDataParams generalDataParams = null;
                CompanyService companyService = null;
                companyService = oCompany.GetCompanyService();

                generalService = companyService.GetGeneralService(parentTable.TableName);


                generalDataParams = (GeneralDataParams)generalService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);

                generalDataParams.SetProperty("Code", parentTable.Values.Where(w => w.FieldName == "Code").FirstOrDefault().FieldValue);

                try
                {
                    generalData = generalService.GetByParams(generalDataParams);
                }
                catch
                {
                    generalData = (GeneralData)generalService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);




                    //add Parent values
                    foreach (InitialValue value in parentTable.Values)
                    {
                        generalData.SetProperty(value.FieldName, value.FieldValue);
                    }


                    //add children tables
                    if (parentTable.ChildrenTables != null)
                    {
                        foreach (ChildTable child in parentTable.ChildrenTables)
                        {
                            SAPbobsCOM.GeneralDataCollection oChildren = generalData.Child(child.TableName);
                            SAPbobsCOM.GeneralData oChild = oChildren.Add();

                            //add child values
                            foreach (InitialValue childValue in child.ChildValues)
                            {
                                oChild.SetProperty(childValue.FieldName, childValue.FieldValue);
                            }

                        }
                    }

                    generalDataParams = generalService.Add(generalData);

                }

                LogProvider.LogInitialValuesCreation(parentTable.TableName, parentTable.Values[0].FieldValue, true, "");
            }
            catch (Exception ex)
            {
                LogProvider.LogInitialValuesCreation(parentTable.TableName, parentTable.Values[0].FieldValue, false, ex.Message);
            }

        }


        public static int CreateQueryCategory(string catName)
        {
            int catId = -1;
            try
            {

                SAPbobsCOM.Recordset rsCat = (SAPbobsCOM.Recordset)B1Provider.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                QueryCategories categories = (QueryCategories)B1Provider.oCompany.GetBusinessObject(BoObjectTypes.oQueryCategories);

                string carQuery = $"select \"CategoryId\" from \"OQCN\" Where \"CatName\" = '{catName}'";
                rsCat.DoQuery(carQuery);
                if (rsCat.RecordCount == 0)
                {
                    categories.Name = catName;

                    categories.Permissions = "ALL_GROUPS_ALLOWED";

                    if (categories.Add() == 0)
                    {
                        catId = int.Parse(oCompany.GetNewObjectKey());
                        LogProvider.LogCategoryCreation("Successful", catName, true, "");
                    }
                    else
                    {
                        LogProvider.LogCategoryCreation("", catName, false, B1Provider.oCompany.GetLastErrorDescription());
                    }

                }
                else
                {
                    catId = int.Parse(rsCat.Fields.Item(0).Value.ToString());
                    LogProvider.LogCategoryCreation("Update", catName, true, "");
                }


            }
            catch (Exception ex)
            {
                LogProvider.LogCategoryCreation("", catName, false, ex.Message);
            }

            return catId;
        }


        public static int CreateQueryInQueryManager(int catId, string queryName, string queryString)
        {
            int queryId = -1;
            try
            {

                SAPbobsCOM.Recordset rsQuery = (SAPbobsCOM.Recordset)B1Provider.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                UserQueries query = (UserQueries)B1Provider.oCompany.GetBusinessObject(BoObjectTypes.oUserQueries);

                string queynameQuery = $"select \"IntrnalKey\" from \"OUQR\" where \"QName\" = '{queryName}'";

                rsQuery.DoQuery(queynameQuery);

                if (rsQuery.RecordCount == 0)
                {
                    #region Add

                    query.QueryCategory = catId;

                    query.QueryDescription = queryName;

                    query.Query = queryString;

                    if (query.Add() == 0)
                    {
                        queryId = int.Parse(oCompany.GetNewObjectKey().Split('\t')[0]);
                        LogProvider.LogQueryCreation("Successful", queryName, true, "");
                    }
                    else
                    {
                        LogProvider.LogQueryCreation("", queryName, false, B1Provider.oCompany.GetLastErrorDescription());
                    }


                    #endregion
                }
                else
                {
                    #region Update

                    queryId = int.Parse(rsQuery.Fields.Item(0).Value.ToString());
                    if (query.GetByKey(queryId, catId))
                    {
                        query.Query = queryString;

                        if (query.Update() == 0)
                        {
                            LogProvider.LogQueryCreation("Update", queryName, true, "");
                        }
                        else
                        {
                            LogProvider.LogQueryCreation("", queryName, false, B1Provider.oCompany.GetLastErrorDescription());
                        }
                    }
                    else
                    {
                        LogProvider.LogQueryCreation("", queryName, false, "Missing query. Contact your administrator");
                    }


                    #endregion

                }


            }
            catch (Exception ex)
            {
                LogProvider.LogQueryCreation("", queryName, false, ex.Message);
            }

            return queryId;
        }

        private static void CreateFMS(string formID, string itemID, string colID, int queryId, string queryName, string refreshBy)
        {
            try
            {
                string fmsQuery = $"select * from \"CSHS\" where \"QueryId\" ='{queryId}' and  \"FormID\" = '{formID}' and \"ItemID\" = '{itemID}' and \"ColID\" = '{colID}'";

                SAPbobsCOM.Recordset rsFMS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                rsFMS.DoQuery(fmsQuery);

                if (rsFMS.RecordCount == 0)
                {
                    FormattedSearches fms = (FormattedSearches)oCompany.GetBusinessObject(BoObjectTypes.oFormattedSearches);

                    fms.FormID = formID;
                    fms.ItemID = itemID;
                    if (!string.IsNullOrEmpty(colID))
                    {
                        fms.ColumnID = colID;
                    }
                    fms.QueryID = queryId;
                    fms.Action = BoFormattedSearchActionEnum.bofsaQuery;

                    if (!string.IsNullOrEmpty(refreshBy))
                    {
                        fms.Refresh = BoYesNoEnum.tYES;
                        fms.ForceRefresh = BoYesNoEnum.tYES;
                        fms.ByField = BoYesNoEnum.tYES;
                        fms.ByFieldEx = FormattedSearchByFieldEnum.fsbfWhenColumnValueChanges;
                        fms.FieldID = refreshBy;
                    }

                    if (fms.Add() == 0)
                    {
                        LogProvider.LogFMSCreation("Successful", queryName, true, "");
                    }
                    else
                    {
                        LogProvider.LogFMSCreation("", queryName, false, B1Provider.oCompany.GetLastErrorDescription());
                    }

                }
                else
                {
                    FormattedSearches fms = (FormattedSearches)oCompany.GetBusinessObject(BoObjectTypes.oFormattedSearches);
                    fms.GetByKey(int.Parse(rsFMS.Fields.Item("IndexID").Value.ToString()));

                    fms.FormID = formID;
                    fms.ItemID = itemID;
                    if (!string.IsNullOrEmpty(colID))
                    {
                        fms.ColumnID = colID;
                    }
                    fms.QueryID = queryId;
                    fms.Action = BoFormattedSearchActionEnum.bofsaQuery;

                    if (!string.IsNullOrEmpty(refreshBy))
                    {
                        fms.Refresh = BoYesNoEnum.tYES;
                        fms.ForceRefresh = BoYesNoEnum.tYES;
                        fms.ByField = BoYesNoEnum.tYES;
                        fms.ByFieldEx = FormattedSearchByFieldEnum.fsbfWhenColumnValueChanges;
                        fms.FieldID = refreshBy;
                    }

                    if (fms.Update() == 0)
                    {
                        LogProvider.LogFMSCreation("Successful - Update", queryName, true, "");
                    }
                    else
                    {
                        LogProvider.LogFMSCreation("", queryName, false, B1Provider.oCompany.GetLastErrorDescription());
                    }

                }
            }
            catch (Exception ex)
            {
                LogProvider.LogFMSCreation("", queryName, false, ex.Message);
            }
        }


        #endregion




        #region Checks For UD exitance


        private static bool TableExist(string TableName)
        {
            SAPbobsCOM.UserTablesMD oUserTablesMD = null;
            bool boolIdent = false;
            oUserTablesMD = ((SAPbobsCOM.UserTablesMD)(oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables)));
            boolIdent = oUserTablesMD.GetByKey(TableName);

            System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserTablesMD);
            oUserTablesMD = null;
            GC.Collect(); // Release the handle to the User Fields

            return (boolIdent);
        }

        private static int Check_UDF(string table_name, string UDF_AlisName)
        {
            int i = -1;
            string Sql = String.Format("SELECT T0.\"FieldID\"  FROM \"CUFD\" T0 WHERE T0.\"TableID\" = '{0}' AND T0.\"AliasID\" = '{1}'", table_name, UDF_AlisName);
            SAPbobsCOM.Recordset oRecordSet = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            oRecordSet.DoQuery(Sql);
            if (oRecordSet.RecordCount > 0)
            {
                i = Convert.ToInt32(oRecordSet.Fields.Item(0).Value.ToString());

            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecordSet);
            GC.Collect(); // Release the handle to the User Fields
            return i;
        }

        private static bool ObjectExist(string ObjectName)
        {
            UserObjectsMD userObjectsMD = null;
            userObjectsMD = (UserObjectsMD)oCompany.GetBusinessObject(BoObjectTypes.oUserObjectsMD);
            bool isExist = userObjectsMD.GetByKey(ObjectName);

            // string xml = userObjectsMD.GetAsXML();

            System.Runtime.InteropServices.Marshal.ReleaseComObject(userObjectsMD);
            GC.Collect(); // Release the handle to the User Fields
            return isExist;
        }
        #endregion

        #region UDs Creation Preparing and Method calls Builder (reads from xml)

        public static void Build_UDTs_And_UDFs(Models.Table table)
        {
            int result = CreateTables(table.TableName, table.TableDescription, table.TableType);

            if (result == 0)
            {
                if (table.FieldsList != null)
                {
                    foreach (UserDefinedField field in table.FieldsList)
                    {
                        CreateUDF(field.Table, field.FieldCode, field.FieldName, field.Types, field.SubType, field.Size, field.ValidValues, field.DefaultValue, field.LinkedTable);
                        //CreateUDF(field.Table, field.FieldCode, field.FieldName, field.Types, field.SubType, field.Size, field.ValidValues, field.DefaultValue, field.LinkedTable, field.LinkedSystemObject);
                    }
                }
            }

        }

        public static void Build_UDO(UserDefinedObject obj)
        {
            CreateUDO(obj);

        }

        public static void CreateFormattedSeachOnScreens(QueryManager queryManager)
        {

            queryManager.Category.CategoryId = CreateQueryCategory(queryManager.Category.CategoryName);

            if (queryManager.Category.CategoryId > -1)
            {
                foreach (Query query in queryManager.Queries)
                {
                    query.QueryId = CreateQueryInQueryManager(queryManager.Category.CategoryId, query.QueryName, query.QueryString);
                }


                foreach (Query query in queryManager.Queries)
                {
                    if (query.QueryId > -1)
                    {
                        CreateFMS(query.FormID, query.ItemID, query.ColID, query.QueryId, query.QueryName, query.RefreshBy);
                    }
                }

            }

        }


        #endregion

        #endregion
        #region Menus Creator
        public static void CreateMenuItem(string parentId, string menuID, string menuName, SAPbouiCOM.BoMenuType menuType, string imagePath, int position)
        {
            if (!Application.SBO_Application.Menus.Exists(menuID))
            {
                SAPbouiCOM.MenuCreationParams oCreationPackage = ((SAPbouiCOM.MenuCreationParams)(Application.SBO_Application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams)));
                //get the parent ID
                SAPbouiCOM.MenuItem oMenuItem = Application.SBO_Application.Menus.Item(parentId);
                // Create s sub menu
                SAPbouiCOM.Menus oMenus = oMenuItem.SubMenus;

                oCreationPackage.Type = menuType;
                oCreationPackage.UniqueID = menuID;
                oCreationPackage.String = menuName;
                if (!string.IsNullOrEmpty(imagePath))
                {
                    oCreationPackage.Image = imagePath;
                }
                oCreationPackage.Position = position;
                oMenus.AddEx(oCreationPackage);
            }
        }
        #endregion


        #region Crystal report
        public static string addReportType(String typeName, String AddonName, String UDOName, String menuId)
        {
            SAPbobsCOM.ReportTypesService rptTypeService;
            SAPbobsCOM.ReportType newType;
            SAPbobsCOM.ReportTypeParams newTypeParam;
            rptTypeService = oCompany.GetCompanyService().GetBusinessService(SAPbobsCOM.ServiceTypes.ReportTypesService) as SAPbobsCOM.ReportTypesService;
            newType = rptTypeService.GetDataInterface(SAPbobsCOM.ReportTypesServiceDataInterfaces.rtsReportType) as SAPbobsCOM.ReportType;
            var addReportType = "";

            try
            {

                GC.Collect();
                // newType.TypeCode = typeCode
                newType.TypeName = typeName;
                newType.AddonName = AddonName;
                newType.AddonFormType = UDOName;
                newType.MenuID = menuId;
                newTypeParam = rptTypeService.AddReportType(newType);
                addReportType = oCompany.GetNewObjectKey();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(rptTypeService);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(newType);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(newTypeParam);
            }
            catch (Exception ex)
            {
                SBO_Application.SetStatusBarMessage(ex.Message);
            }

            return addReportType;

        }

        public static string getReportTypeCode(String typeName, String udoName, String addonName, String menuId)
        {
            String ReportTypeCode = "";
            try
            {

                SAPbobsCOM.Recordset rsetField = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                var oFlag = "";
                var s = "SELECT top 1 \"CODE\", \"NAME\", \"DEFLT_REP\", \"ADD_NAME\", \"FRM_TYPE\", \"MNU_ID\", \"IS_SYS\", \"DEFLT_SEQ\", \"TYPE\"   FROM  \"RTYP\"  where \"NAME\"  = '{0}' and  \"FRM_TYPE\" ='{1}' and  \"ADD_NAME\" ='{2}' ";
                //"SELECT     top 1 CODE, NAME, DEFLT_REP, ADD_NAME, FRM_TYPE, MNU_ID, IS_SYS, DEFLT_SEQ, TYPE   FROM RTYP where NAME = '{0}' and FRM_TYPE='{1}' and ADD_NAME='{2}'";
                s = string.Format(s, typeName, udoName, addonName);
                //     s = "SELECT     top 1 CODE, NAME, DEFLT_REP, ADD_NAME, FRM_TYPE, MNU_ID, IS_SYS, DEFLT_SEQ, TYPE   FROM RTYP where NAME = '" + typeName + "' and FRM_TYPE='" + udoName + "' and ADD_NAME='" + addonName + "'";
                rsetField.DoQuery(s);
                if (rsetField.EoF)
                    ReportTypeCode = addReportType(typeName, addonName, udoName, menuId);
                else
                    ReportTypeCode = rsetField.Fields.Item("CODE").Value.ToString();



                System.Runtime.InteropServices.Marshal.ReleaseComObject(rsetField);
                rsetField = null;
                GC.Collect();
                return ReportTypeCode;
            }
            catch (Exception ex)
            {
                SBO_Application.StatusBar.SetText("Failed to Column Exists : " + ex.Message);
            }
            finally
            {
            }
            return ReportTypeCode;


        }

        public static void uploadCrystalReport(string rptFilePath, string typeName, string udoName, string addonName, string menuId,bool isDefault)
        {
            #region Upload Reports
            SAPbobsCOM.ReportLayoutsService oLayoutService = (SAPbobsCOM.ReportLayoutsService)B1Provider.oCompany.GetCompanyService().GetBusinessService(SAPbobsCOM.ServiceTypes.ReportLayoutsService);
            SAPbobsCOM.ReportLayout oReport = (SAPbobsCOM.ReportLayout)oLayoutService.GetDataInterface(SAPbobsCOM.ReportLayoutsServiceDataInterfaces.rlsdiReportLayout);

            oReport.Name = Path.GetFileNameWithoutExtension(rptFilePath);
            oReport.TypeCode = getReportTypeCode(typeName,  udoName,  addonName, menuId);
            oReport.Author = "eSyncross";
            oReport.Category = SAPbobsCOM.ReportLayoutCategoryEnum.rlcCrystal;
            

            string newReportCode;

            // Add new object 
            SAPbobsCOM.ReportLayoutParams oNewReportParams = oLayoutService.AddReportLayout(oReport);
             
            // Get code of the added ReportLayout object 
            newReportCode = oNewReportParams.LayoutCode;


            //set report as default
            //Set parameters
            if (isDefault)
            {
                SAPbobsCOM.DefaultReportParams oDefaultReportParams = (SAPbobsCOM.DefaultReportParams)oLayoutService.GetDataInterface(ReportLayoutsServiceDataInterfaces.rlsdiDefaultReportParams);
                oDefaultReportParams.LayoutCode = newReportCode;
                oDefaultReportParams.ReportCode = oReport.TypeCode;
                //Set the report as default
                oLayoutService.SetDefaultReport(oDefaultReportParams);
            }


            SAPbobsCOM.CompanyService oCompanyService = B1Provider.oCompany.GetCompanyService();
            // Specify the table and field to update 
            SAPbobsCOM.BlobParams oBlobParams = (SAPbobsCOM.BlobParams)oCompanyService.GetDataInterface(SAPbobsCOM.CompanyServiceDataInterfaces.csdiBlobParams);
            oBlobParams.Table = "RDOC";
            oBlobParams.Field = "Template";

            // Specify the record whose blob field is to be set 
            SAPbobsCOM.BlobTableKeySegment oKeySegment = oBlobParams.BlobTableKeySegments.Add();
            oKeySegment.Name = "DocCode";
            oKeySegment.Value = newReportCode;
          

            SAPbobsCOM.Blob oBlob = (SAPbobsCOM.Blob)oCompanyService.GetDataInterface(SAPbobsCOM.CompanyServiceDataInterfaces.csdiBlob);

            // Put the rpt file into buffer 
            FileStream oFile = new FileStream(rptFilePath, System.IO.FileMode.Open);
            int fileSize = (int)oFile.Length;
            byte[] buf = new byte[fileSize];
            oFile.Read(buf, 0, fileSize);
            oFile.Close();

            // Convert memory buffer to Base64 string 
            oBlob.Content = Convert.ToBase64String(buf, 0, fileSize);

            //Upload Blob to database 
            oCompanyService.SetBlob(oBlobParams, oBlob);

            #endregion
 
  

        }
        #endregion
    }



}


