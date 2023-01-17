using Diamond_Addon.Providers;
using SAPbouiCOM.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diamond_Addon.Utilities
{
    static class Functions
    {
        public static bool isFormExists(string formuid)
        {
            bool frmExist = false;

            for (int i = 0; i < Application.SBO_Application.Forms.Count; i++)
            {

                if (Application.SBO_Application.Forms.Item(i).UniqueID == formuid)
                {
                    Application.SBO_Application.Forms.Item(i).Select();
                    frmExist = true;
                }
            }
            return frmExist;
        }

       
    }
}
