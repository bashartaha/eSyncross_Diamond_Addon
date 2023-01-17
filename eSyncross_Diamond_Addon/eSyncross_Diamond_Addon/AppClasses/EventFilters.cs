using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using SAPbouiCOM.Framework;

using Diamond_Addon.Utilities;
using Diamond_Addon.Providers;
using Newtonsoft.Json;
using Diamond_Addon.Handlers;
using Diamond_Addon.Forms;

namespace Diamond_Addon
{
    public class EventFilters
    {
        

        public static SAPbouiCOM.EventFilters oFilters;   
        private static string originalUserName="";

        public static bool SetFilters()
        {

            // Create a new EventFilters object
            oFilters = new SAPbouiCOM.EventFilters();

            SAPbouiCOM.EventFilter oFilter;
            // add an event type to the container
            // this method returns an EventFilter object
            oFilter = oFilters.Add(SAPbouiCOM.BoEventTypes.et_COMBO_SELECT);
            oFilter = oFilters.Add(SAPbouiCOM.BoEventTypes.et_CLICK);
            //oFilter = oFilters.Add(SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD);
            //oFilter = oFilters.Add(SAPbouiCOM.BoEventTypes.et_FORM_DATA_UPDATE);
            //oFilter = oFilters.Add(SAPbouiCOM.BoEventTypes.et_FORM_DATA_LOAD);
            


            //// assign the form type on which the event would be processed
            oFilter.Add(143); // GRPO
             

            #region All

            oFilter = oFilters.Add(SAPbouiCOM.BoEventTypes.et_ALL_EVENTS);
            oFilter.AddEx("ESY_OWOR");
            oFilter.AddEx("ESY_BRS");
            oFilter.AddEx("ESY_SERIAL");
            oFilter.AddEx("65051");

            #endregion



            Application.SBO_Application.SetFilter(oFilters);
            // events handled by SBO_Application_ItemEvent
             Application.SBO_Application.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(SBO_Application_ItemEvent);

            // events handled by SBO_Application
            Application.SBO_Application.FormDataEvent += new SAPbouiCOM._IApplicationEvents_FormDataEventEventHandler(ref FormDataEvent);


            // events handled by SBO_Application
          Application.SBO_Application.RightClickEvent += new SAPbouiCOM._IApplicationEvents_RightClickEventEventHandler(ref SBO_Application_RightClickEvent);

            Application.SBO_Application.LayoutKeyEvent += Application_LayoutKeyEvent;

            return true;

        }



        static void SBO_Application_ItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent pVal, out bool BubbleEvent)
        {

            // *************************************************************************
            //  BubbleEvent sets the behavior of SAP Business One.
            //  False means that the application will not continue processing this event.


            // *************************************************************************
            BubbleEvent = true;


            try
            {
               

                if (pVal.FormType == 143 && pVal.ItemUID == "10000330" && pVal.EventType == SAPbouiCOM.BoEventTypes.et_CLICK && pVal.BeforeAction == true)
                {
                    SAPbouiCOM.Form oForm = Application.SBO_Application.Forms.ActiveForm;

                    if (((SAPbouiCOM.ComboBox)oForm.Items.Item("10000330").Specific).ValidValues.Count == 5)
                    {
                        ((SAPbouiCOM.ComboBox)oForm.Items.Item("10000330").Specific).ValidValues.Add("File", "File");
                    }
                }


                if (pVal.FormType == 143 && pVal.ItemUID == "10000330" && pVal.EventType == SAPbouiCOM.BoEventTypes.et_COMBO_SELECT && pVal.BeforeAction == false)
                {
                    SAPbouiCOM.Form oForm = Application.SBO_Application.Forms.ActiveForm;

                    if (((SAPbouiCOM.ComboBox)oForm.Items.Item("10000330").Specific).Selected.Value == "File")
                    {
                        BrowseImportFile form = new BrowseImportFile(oForm);
                        form.Show();
                    }
                }

            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
        }


        static void FormDataEvent(ref SAPbouiCOM.BusinessObjectInfo BusinessObjectInfo, out bool BubbleEvent)
        {
            BubbleEvent = true;

            try
            {
 

            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
        }

        static void SBO_Application_RightClickEvent(ref SAPbouiCOM.ContextMenuInfo eventInfo, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
               
                // Get the menu collection of the newly added pop-up item
                SAPbouiCOM.IMenuItem oMenuItem = SAPbouiCOM.Framework.Application.SBO_Application.Menus.Item("1280"); //Data 
                SAPbouiCOM.Menus oMenus = oMenuItem.SubMenus;

                if (Application.SBO_Application.Forms.ActiveForm.Type == 139)
                {

                    if (eventInfo.BeforeAction == true)
                    {

                        //Parent
                        B1Provider.CreateMenuItem("1280", "EVO_1280", "WMS - EVO", SAPbouiCOM.BoMenuType.mt_POPUP, null, -1);

                        B1Provider.CreateMenuItem("EVO_1280", "EVO_1280_1", "Create New Picklist", SAPbouiCOM.BoMenuType.mt_STRING, null, -1);
                        B1Provider.CreateMenuItem("EVO_1280", "EVO_1280_2", "View Picklists", SAPbouiCOM.BoMenuType.mt_STRING, null, -1);

                        #region Remove row 

                        //if ((eventInfo.FormUID == "N_SMSC" || eventInfo.FormUID == "N_SEMLT" || eventInfo.FormUID == "N_REMLT" ||
                        //    eventInfo.FormUID == "N_SOME" || eventInfo.FormUID == "N_SOMR" || eventInfo.FormUID == "N_SOMERT") && eventInfo.ItemUID == "38")
                        //{
                        //    B1Provider.CreateMenuItem("1280", "N_RemoveRow", "Delete Line", SAPbouiCOM.BoMenuType.mt_STRING, null, -1);

                        //}


                        #endregion
                    }

                    else
                    { 
                        if (SAPbouiCOM.Framework.Application.SBO_Application.Menus.Exists("EVO_1280"))
                        {
                            oMenus.RemoveEx("EVO_1280");
                        }



                    }

                }
            }
            catch (Exception ex)
            {
                SAPbouiCOM.Framework.Application.SBO_Application.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
        }


        private static void LogRecord(string objCode, int docEntry, string id, string errorMessage, string payloadMessage)
        {
            try
            {

                SAPbobsCOM.UserTable userTable = (SAPbobsCOM.UserTable)B1Provider.oCompany.UserTables.Item("MF_LOG");

                if (userTable.GetByKey(objCode + "-" + docEntry.ToString()))
                {
                    userTable.Name = DateTime.Now.ToString("yyMMddhhmmss");


                    userTable.UserFields.Fields.Item("U_MF_Id").Value = id;
                    userTable.UserFields.Fields.Item("U_ErrorMessage").Value = errorMessage;
                    userTable.UserFields.Fields.Item("U_PayloadMessage").Value = payloadMessage;
                    userTable.UserFields.Fields.Item("U_Date").Value = DateTime.Now.Date;

                    int isUpdated = userTable.Update();
                    if (isUpdated != 0)
                    {
                        Application.SBO_Application.SetStatusBarMessage(B1Provider.oCompany.GetLastErrorDescription(), SAPbouiCOM.BoMessageTime.bmt_Medium, true);
                    }
                }
                else
                {
                    userTable.Code = objCode + "-" + docEntry.ToString();
                    userTable.Name = DateTime.Now.ToString("yyMMddhhmmss");

                    userTable.UserFields.Fields.Item("U_ObjCode").Value = objCode;
                    userTable.UserFields.Fields.Item("U_DocEntry").Value = docEntry.ToString();

                    userTable.UserFields.Fields.Item("U_ErrorMessage").Value = errorMessage;
                    userTable.UserFields.Fields.Item("U_PayloadMessage").Value = payloadMessage;
                    userTable.UserFields.Fields.Item("U_MF_Id").Value = id;
                    userTable.UserFields.Fields.Item("U_Date").Value = DateTime.Now.Date;
                    int isAdded = userTable.Add();
                    if (isAdded != 0)
                    {
                        Application.SBO_Application.SetStatusBarMessage(B1Provider.oCompany.GetLastErrorDescription(), SAPbouiCOM.BoMessageTime.bmt_Medium, true);
                    }

                }

                userTable = null;
            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, true);

            }
        }

        private static void Application_LayoutKeyEvent(ref SAPbouiCOM.LayoutKeyInfo eventInfo, out bool BubbleEvent)
        {
            try
            {
                SAPbouiCOM.Form frm =  Application.SBO_Application.Forms.Item(eventInfo.FormUID);
                if (frm.BusinessObject.Type == "EVO_OPICK")
                {
                   SAPbouiCOM.DBDataSource ds = frm.DataSources.DBDataSources.Item(0);
                    string docentry = ds.GetValue("DocEntry", 0).Trim();
                    eventInfo.LayoutKey = docentry;
                }
            }
            catch
            {
            }
            BubbleEvent = true;
        }

    }
}
