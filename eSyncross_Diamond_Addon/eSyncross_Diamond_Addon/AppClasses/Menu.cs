using System;
using System.Collections.Generic;
using System.Text;
using SAPbouiCOM.Framework;
using Diamond_Addon.Providers;
using Diamond_Addon.Forms;
using EvoAddon.Forms;

namespace Diamond_Addon
{
    class Menu
    {
        public void AddMenuItems()
        {         

            try
            {
 

                B1Provider.CreateMenuItem("43520", "ESY_DIO", "Diamond", SAPbouiCOM.BoMenuType.mt_POPUP, null, -1);

                B1Provider.CreateMenuItem("ESY_DIO", "ESY_DIO_PRD", "Produce", SAPbouiCOM.BoMenuType.mt_STRING, null, -1);
                B1Provider.CreateMenuItem("ESY_DIO", "ESY_DIO_IMT", "Import From Excel", SAPbouiCOM.BoMenuType.mt_STRING, null, -1);
                B1Provider.CreateMenuItem("ESY_DIO", "ESY_DIO_TAG", "Tag Inquiry", SAPbouiCOM.BoMenuType.mt_STRING, null, -1);

                B1Provider.CreateMenuItem("ESY_DIO", "ESY_DIO_INI", "Initialize", SAPbouiCOM.BoMenuType.mt_STRING, null, -1);



            }
            catch (Exception er)
            { //  Menu already exists
               Application.SBO_Application.SetStatusBarMessage("Menu Already Exists", SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
        }

        public void SBO_Application_MenuEvent(ref SAPbouiCOM.MenuEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            try
            {

                if (pVal.BeforeAction == false & (pVal.BeforeAction == false && pVal.MenuUID == "ESY_DIO_INI"))
                {
                    SAPbouiCOM.ProgressBar oBar = (SAPbouiCOM.ProgressBar)Application.SBO_Application.StatusBar.CreateProgressBar("Please wait", 100, false);

                    try
                    {
                        oBar.Text = "Please wait";
                        oBar.Value = 1;
                        if (Application.SBO_Application.MessageBox("Do you want to initialize the addon? new object will be created!", 1, "Yes", "No") == 1)
                        {
                            AddonProvider.CreateDatabase();
                         

                            oBar.Stop();
                        }

                        Application.SBO_Application.MessageBox(LogProvider.setupErrorProcessLoggerBuilder.ToString());
                    }
                    catch (Exception ex)
                    {
                        oBar.Stop();

                        Application.SBO_Application.MessageBox(ex.Message);

                    }

                    oBar.Stop();
                }


                if (pVal.BeforeAction == false & (pVal.BeforeAction == false && pVal.MenuUID == "ESY_DIO_PRD"))
                { 

                }


                if (pVal.BeforeAction == false & (pVal.BeforeAction == false && pVal.MenuUID == "ESY_DIO_IMT"))
                {
                    ImportSerialNumbers form = new ImportSerialNumbers();
                    form.Show();
                }

                if (pVal.BeforeAction == false & (pVal.BeforeAction == false && pVal.MenuUID == "ESY_DIO_TAG"))
                {
                    SerialNumberDetails form = new SerialNumberDetails();
                    form.Show();
                }


            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message + ex.StackTrace, SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
        }




    }
}
