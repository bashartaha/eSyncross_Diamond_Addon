using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoAddon
{
    class Connection
    {

        public static SAPbouiCOM.Application SBO_Application;
        public static SAPbobsCOM.Company oCompany;

        private void SetApplication()
        {

            SAPbouiCOM.SboGuiApi SboGuiApi;
            string sConnectionString;

            SboGuiApi = new SAPbouiCOM.SboGuiApi();

            // by following the steps specified above, the following
            // statment should be suficient for either development or run mode

            //sConnectionString = Environment.GetCommandLineArgs().GetValue(0).ToString();
            sConnectionString = "0030002C0030002C00530041005000420044005F00440061007400650076002C0050004C006F006D0056004900490056";

            // connect to a running SBO Application

            SboGuiApi.Connect(sConnectionString);

            // get an initialized application object

            SBO_Application = SboGuiApi.GetApplication(-1);

        }

        public void Class_Initialize_Connection()
        {
            try
            {
                //*************************************************************
                // set SBO_Application with an initialized application object
                //*************************************************************

                SetApplication();

                //*************************************************************
                // Connect to DI
                //*************************************************************

                oCompany = new SAPbobsCOM.Company();

                //get DI company (via UI)

                oCompany = (SAPbobsCOM.Company)SBO_Application.Company.GetDICompany();


                //Set Filters
               EventFilters.SetFilters();

             
                //*************************************************************
                // send a message
                //*************************************************************
                SBO_Application.SetStatusBarMessage("EVO - Go Smart Addon is successfully connected ... ", SAPbouiCOM.BoMessageTime.bmt_Short, false);
                //*************************************************************
                // set SBO_Application with an initialized EventFilters object
                //*******

            }
            catch (Exception ex)
            {
                SBO_Application.SetStatusBarMessage("(EVO - Go Smart Addon failed to connect to the company ... )" + ex, SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }

        }


    }
}
