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
    

        public static bool SetFilters()
        {

            // Create a new EventFilters object
            oFilters = new SAPbouiCOM.EventFilters();

            SAPbouiCOM.EventFilter oFilter;
 
            oFilter = oFilters.Add(SAPbouiCOM.BoEventTypes.et_COMBO_SELECT);
            oFilter = oFilters.Add(SAPbouiCOM.BoEventTypes.et_CLICK);
       
            


            //// assign the form type on which the event would be processed
            oFilter.Add(143); // GRPO
             

            #region All

            oFilter = oFilters.Add(SAPbouiCOM.BoEventTypes.et_ALL_EVENTS);
            oFilter.AddEx("ESY_OWOR");
            oFilter.AddEx("ESY_BRS");
            oFilter.AddEx("ESY_SERIAL");
            oFilter.AddEx("65051");

            #endregion



           // Application.SBO_Application.SetFilter(oFilters);




            // events handled by SBO_Application_ItemEvent
            // Application.SBO_Application.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(SBO_Application_ItemEvent);

            // events handled by SBO_Application
            Application.SBO_Application.FormDataEvent += new SAPbouiCOM._IApplicationEvents_FormDataEventEventHandler(ref FormDataEvent);
 
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

                if (BusinessObjectInfo.FormTypeEx == "65051" && BusinessObjectInfo.ActionSuccess==true)
                {
                    SAPbouiCOM.Form serialForm = Application.SBO_Application.Forms.Item(BusinessObjectInfo.FormUID);
                   SerialNumberDetails form = new SerialNumberDetails(serialForm.DataSources.DBDataSources.Item(0).GetValue("DistNumber",0));
                    form.Show();

                    serialForm.Close();
                }

            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
        }
         

    }
}
