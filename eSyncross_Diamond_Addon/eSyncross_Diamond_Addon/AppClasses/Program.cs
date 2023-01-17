using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using SAPbouiCOM.Framework; 
using Diamond_Addon.Providers;

namespace Diamond_Addon
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Application oApp = null;
                if (args.Length < 1)
                {
                    oApp = new Application();
                }
                else
                {
                    oApp = new Application(args[0]);
                }


                B1Provider.oCompany = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();


                if (B1Provider.oCompany.Connected == true)
                {

                    Menu MyMenu = new Menu();
                    MyMenu.AddMenuItems();
                    oApp.RegisterMenuEventHandler(MyMenu.SBO_Application_MenuEvent);

                EventFilters.SetFilters();


                    Application.SBO_Application.SetStatusBarMessage("Diamond addon is successfully connected ... ", SAPbouiCOM.BoMessageTime.bmt_Short, false);


                }
                else
                {
                    Application.SBO_Application.SetStatusBarMessage(B1Provider.oCompany.GetLastErrorDescription(), SAPbouiCOM.BoMessageTime.bmt_Short, true);
                }


                Application.SBO_Application.AppEvent += new SAPbouiCOM._IApplicationEvents_AppEventEventHandler(SBO_Application_AppEvent);
                           

                //start SignalR service
               // SignalR.InitiateConnection();


                oApp.Run();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

      

        static void SBO_Application_AppEvent(SAPbouiCOM.BoAppEventTypes EventType)
        {
            switch (EventType)
            {
                case SAPbouiCOM.BoAppEventTypes.aet_ShutDown:
                    //Exit Add-On
                    // Application.SBO_Application.Menus.RemoveEx("ROO");
                   
                    System.Windows.Forms.Application.Exit();

                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_CompanyChanged:
                    //Exit Add-On
                    //Application.SBO_Application.Menus.RemoveEx("ROO");
                    
                    System.Windows.Forms.Application.Exit();

                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_FontChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_LanguageChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_ServerTerminition:
                    //Exit Add-On
                    //Application.SBO_Application.Menus.RemoveEx("ROO");
                   
                    System.Windows.Forms.Application.Exit();
                    break;
                default:
                    break;
            }
        }

        
        

    }
}
