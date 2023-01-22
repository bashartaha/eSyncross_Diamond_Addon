using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;

namespace Diamond_Addon.Forms
{
    [FormAttribute("EvoAddon.Forms.SerialNumberDetails", "Forms/SerialNumberDetails.b1f")]
    class SerialNumberDetails : UserFormBase
    {
        public SerialNumberDetails()
        {

            try
            {
                Folder0.Select();
            }catch(Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message);
            }
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_2").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_3").Specific));
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_4").Specific));
            this.StaticText4 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_6").Specific));
            this.StaticText5 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_7").Specific));
            this.StaticText6 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_8").Specific));
            this.StaticText7 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_9").Specific));
            this.Folder3 = ((SAPbouiCOM.Folder)(this.GetItem("Item_13").Specific));
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("1").Specific));
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("2").Specific));
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("Item_16").Specific));
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("Item_17").Specific));
            this.EditText2 = ((SAPbouiCOM.EditText)(this.GetItem("Item_18").Specific));
            this.EditText3 = ((SAPbouiCOM.EditText)(this.GetItem("Item_19").Specific));
            this.EditText4 = ((SAPbouiCOM.EditText)(this.GetItem("Item_20").Specific));
            this.StaticText8 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_21").Specific));
            this.LinkedButton0 = ((SAPbouiCOM.LinkedButton)(this.GetItem("Item_22").Specific));
            this.EditText5 = ((SAPbouiCOM.EditText)(this.GetItem("Item_23").Specific));
            this.EditText6 = ((SAPbouiCOM.EditText)(this.GetItem("Item_24").Specific));
            this.EditText7 = ((SAPbouiCOM.EditText)(this.GetItem("Item_25").Specific));
            this.EditText8 = ((SAPbouiCOM.EditText)(this.GetItem("Item_26").Specific));
            this.StaticText9 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_28").Specific));
            this.StaticText10 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_29").Specific));
            this.StaticText11 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_30").Specific));
            this.StaticText12 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_31").Specific));
            this.StaticText13 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_32").Specific));
            this.EditText10 = ((SAPbouiCOM.EditText)(this.GetItem("Item_33").Specific));
            this.StaticText14 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_34").Specific));
            this.StaticText15 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_35").Specific));
            this.StaticText16 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_36").Specific));
            this.StaticText17 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_37").Specific));
            this.StaticText18 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_38").Specific));
            this.StaticText19 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_39").Specific));
            this.StaticText20 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_40").Specific));
            this.EditText11 = ((SAPbouiCOM.EditText)(this.GetItem("Item_56").Specific));
            this.EditText12 = ((SAPbouiCOM.EditText)(this.GetItem("Item_57").Specific));
            this.EditText13 = ((SAPbouiCOM.EditText)(this.GetItem("Item_58").Specific));
            this.EditText14 = ((SAPbouiCOM.EditText)(this.GetItem("Item_59").Specific));
            this.EditText15 = ((SAPbouiCOM.EditText)(this.GetItem("Item_60").Specific));
            this.EditText16 = ((SAPbouiCOM.EditText)(this.GetItem("Item_61").Specific));
            this.EditText17 = ((SAPbouiCOM.EditText)(this.GetItem("Item_62").Specific));
            this.EditText18 = ((SAPbouiCOM.EditText)(this.GetItem("Item_63").Specific));
            this.EditText19 = ((SAPbouiCOM.EditText)(this.GetItem("Item_64").Specific));
            this.EditText20 = ((SAPbouiCOM.EditText)(this.GetItem("Item_65").Specific));
            this.EditText22 = ((SAPbouiCOM.EditText)(this.GetItem("Item_67").Specific));
            this.EditText23 = ((SAPbouiCOM.EditText)(this.GetItem("Item_68").Specific));
            this.EditText24 = ((SAPbouiCOM.EditText)(this.GetItem("Item_69").Specific));
            this.EditText25 = ((SAPbouiCOM.EditText)(this.GetItem("Item_70").Specific));
            this.EditText26 = ((SAPbouiCOM.EditText)(this.GetItem("Item_71").Specific));
            this.StaticText42 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_82").Specific));
            this.StaticText43 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_83").Specific));
            this.StaticText44 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_84").Specific));
            this.StaticText45 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_85").Specific));
            this.StaticText46 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_86").Specific));
            this.StaticText56 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_96").Specific));
            this.StaticText57 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_97").Specific));
            this.StaticText58 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_98").Specific));
            this.StaticText59 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_99").Specific));
            this.StaticText60 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_100").Specific));
            this.StaticText61 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_101").Specific));
            this.StaticText62 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_102").Specific));
            this.EditText36 = ((SAPbouiCOM.EditText)(this.GetItem("Item_103").Specific));
            this.EditText37 = ((SAPbouiCOM.EditText)(this.GetItem("Item_104").Specific));
            this.EditText38 = ((SAPbouiCOM.EditText)(this.GetItem("Item_105").Specific));
            this.EditText39 = ((SAPbouiCOM.EditText)(this.GetItem("Item_106").Specific));
            this.EditText40 = ((SAPbouiCOM.EditText)(this.GetItem("Item_107").Specific));
            this.EditText41 = ((SAPbouiCOM.EditText)(this.GetItem("Item_108").Specific));
            this.EditText42 = ((SAPbouiCOM.EditText)(this.GetItem("Item_109").Specific));
            this.EditText43 = ((SAPbouiCOM.EditText)(this.GetItem("Item_110").Specific));
            this.EditText44 = ((SAPbouiCOM.EditText)(this.GetItem("Item_111").Specific));
            this.EditText46 = ((SAPbouiCOM.EditText)(this.GetItem("Item_113").Specific));
            this.EditText47 = ((SAPbouiCOM.EditText)(this.GetItem("Item_114").Specific));
            this.EditText48 = ((SAPbouiCOM.EditText)(this.GetItem("Item_115").Specific));
            this.EditText49 = ((SAPbouiCOM.EditText)(this.GetItem("Item_116").Specific));
            this.EditText51 = ((SAPbouiCOM.EditText)(this.GetItem("Item_118").Specific));
            this.EditText52 = ((SAPbouiCOM.EditText)(this.GetItem("Item_119").Specific));
            this.EditText53 = ((SAPbouiCOM.EditText)(this.GetItem("Item_120").Specific));
            this.EditText54 = ((SAPbouiCOM.EditText)(this.GetItem("Item_121").Specific));
            this.EditText56 = ((SAPbouiCOM.EditText)(this.GetItem("Item_123").Specific));
            this.EditText57 = ((SAPbouiCOM.EditText)(this.GetItem("Item_124").Specific));
            this.EditText58 = ((SAPbouiCOM.EditText)(this.GetItem("Item_125").Specific));
            this.EditText59 = ((SAPbouiCOM.EditText)(this.GetItem("Item_126").Specific));
            this.EditText61 = ((SAPbouiCOM.EditText)(this.GetItem("Item_128").Specific));
            this.EditText62 = ((SAPbouiCOM.EditText)(this.GetItem("Item_129").Specific));
            this.EditText63 = ((SAPbouiCOM.EditText)(this.GetItem("Item_130").Specific));
            this.EditText64 = ((SAPbouiCOM.EditText)(this.GetItem("Item_131").Specific));
            this.EditText66 = ((SAPbouiCOM.EditText)(this.GetItem("Item_133").Specific));
            this.EditText67 = ((SAPbouiCOM.EditText)(this.GetItem("Item_134").Specific));
            this.EditText68 = ((SAPbouiCOM.EditText)(this.GetItem("Item_135").Specific));
            this.EditText69 = ((SAPbouiCOM.EditText)(this.GetItem("Item_136").Specific));
            this.EditText70 = ((SAPbouiCOM.EditText)(this.GetItem("Item_137").Specific));
            this.StaticText63 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_138").Specific));
            this.StaticText64 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_139").Specific));
            this.StaticText65 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_140").Specific));
            this.StaticText66 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_141").Specific));
            this.StaticText67 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_142").Specific));
            this.StaticText68 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_143").Specific));
            this.StaticText69 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_144").Specific));
            this.StaticText70 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_145").Specific));
            this.EditText71 = ((SAPbouiCOM.EditText)(this.GetItem("Item_146").Specific));
            this.EditText72 = ((SAPbouiCOM.EditText)(this.GetItem("Item_147").Specific));
            this.StaticText71 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_148").Specific));
            this.EditText73 = ((SAPbouiCOM.EditText)(this.GetItem("Item_149").Specific));
            this.EditText74 = ((SAPbouiCOM.EditText)(this.GetItem("Item_150").Specific));
            this.StaticText72 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_151").Specific));
            this.EditText76 = ((SAPbouiCOM.EditText)(this.GetItem("Item_153").Specific));
            this.StaticText73 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_154").Specific));
            this.EditText78 = ((SAPbouiCOM.EditText)(this.GetItem("Item_156").Specific));
            this.StaticText74 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_157").Specific));
            this.EditText79 = ((SAPbouiCOM.EditText)(this.GetItem("Item_158").Specific));
            this.StaticText75 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_159").Specific));
            this.EditText80 = ((SAPbouiCOM.EditText)(this.GetItem("Item_160").Specific));
            this.StaticText76 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_161").Specific));
            this.EditText81 = ((SAPbouiCOM.EditText)(this.GetItem("Item_162").Specific));
            this.StaticText77 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_163").Specific));
            this.EditText82 = ((SAPbouiCOM.EditText)(this.GetItem("Item_164").Specific));
            this.StaticText78 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_165").Specific));
            this.StaticText81 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_171").Specific));
            this.Grid0 = ((SAPbouiCOM.Grid)(this.GetItem("Item_172").Specific));
            this.Folder0 = ((SAPbouiCOM.Folder)(this.GetItem("Item_1").Specific));
            this.Folder1 = ((SAPbouiCOM.Folder)(this.GetItem("Item_10").Specific));
            this.Folder2 = ((SAPbouiCOM.Folder)(this.GetItem("Item_11").Specific));
            this.ComboBox0 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_5").Specific));
            this.ComboBox1 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_14").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            this.DataLoadAfter += new DataLoadAfterHandler(this.Form_DataLoadAfter);

        }

        private void OnCustomInitialize()
        {
            try
            {
                EditText1.Item.SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, (int)SAPbouiCOM.BoAutoFormMode.afm_Find, SAPbouiCOM.BoModeVisualBehavior.mvb_True);
                EditText2.Item.SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, (int)SAPbouiCOM.BoAutoFormMode.afm_Find, SAPbouiCOM.BoModeVisualBehavior.mvb_True);
                EditText3.Item.SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, (int)SAPbouiCOM.BoAutoFormMode.afm_Find, SAPbouiCOM.BoModeVisualBehavior.mvb_True);
                EditText4.Item.SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, (int)SAPbouiCOM.BoAutoFormMode.afm_Find, SAPbouiCOM.BoModeVisualBehavior.mvb_True);

            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message);
            }
           
        }

        private SAPbouiCOM.StaticText StaticText0;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.StaticText StaticText2;
        private SAPbouiCOM.StaticText StaticText4;
        private SAPbouiCOM.StaticText StaticText5;
        private SAPbouiCOM.StaticText StaticText6;
        private SAPbouiCOM.StaticText StaticText7;
        private SAPbouiCOM.Folder Folder3;
        private SAPbouiCOM.Button Button0;
        private SAPbouiCOM.Button Button1;
        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.EditText EditText1;
        private SAPbouiCOM.EditText EditText2;
        private SAPbouiCOM.EditText EditText3;
        private SAPbouiCOM.EditText EditText4;
        private SAPbouiCOM.StaticText StaticText8;
        private SAPbouiCOM.LinkedButton LinkedButton0;
        private SAPbouiCOM.EditText EditText5;
        private SAPbouiCOM.EditText EditText6;
        private SAPbouiCOM.EditText EditText7;
        private SAPbouiCOM.EditText EditText8;
        private SAPbouiCOM.StaticText StaticText9;
        private SAPbouiCOM.StaticText StaticText10;
        private SAPbouiCOM.StaticText StaticText11;
        private SAPbouiCOM.StaticText StaticText12;
        private SAPbouiCOM.StaticText StaticText13;
        private SAPbouiCOM.EditText EditText10;
        private SAPbouiCOM.StaticText StaticText14;
        private SAPbouiCOM.StaticText StaticText15;
        private SAPbouiCOM.StaticText StaticText16;
        private SAPbouiCOM.StaticText StaticText17;
        private SAPbouiCOM.StaticText StaticText18;
        private SAPbouiCOM.StaticText StaticText19;
        private SAPbouiCOM.StaticText StaticText20;
        private SAPbouiCOM.EditText EditText11;
        private SAPbouiCOM.EditText EditText12;
        private SAPbouiCOM.EditText EditText13;
        private SAPbouiCOM.EditText EditText14;
        private SAPbouiCOM.EditText EditText15;
        private SAPbouiCOM.EditText EditText16;
        private SAPbouiCOM.EditText EditText17;
        private SAPbouiCOM.EditText EditText18;
        private SAPbouiCOM.EditText EditText19;
        private SAPbouiCOM.EditText EditText20;
        private SAPbouiCOM.EditText EditText22;
        private SAPbouiCOM.EditText EditText23;
        private SAPbouiCOM.EditText EditText24;
        private SAPbouiCOM.EditText EditText25;
        private SAPbouiCOM.EditText EditText26;
        private SAPbouiCOM.StaticText StaticText42;
        private SAPbouiCOM.StaticText StaticText43;
        private SAPbouiCOM.StaticText StaticText44;
        private SAPbouiCOM.StaticText StaticText45;
        private SAPbouiCOM.StaticText StaticText46;
        private SAPbouiCOM.StaticText StaticText56;
        private SAPbouiCOM.StaticText StaticText57;
        private SAPbouiCOM.StaticText StaticText58;
        private SAPbouiCOM.StaticText StaticText59;
        private SAPbouiCOM.StaticText StaticText60;
        private SAPbouiCOM.StaticText StaticText61;
        private SAPbouiCOM.StaticText StaticText62;
        private SAPbouiCOM.EditText EditText36;
        private SAPbouiCOM.EditText EditText37;
        private SAPbouiCOM.EditText EditText38;
        private SAPbouiCOM.EditText EditText39;
        private SAPbouiCOM.EditText EditText40;
        private SAPbouiCOM.EditText EditText41;
        private SAPbouiCOM.EditText EditText42;
        private SAPbouiCOM.EditText EditText43;
        private SAPbouiCOM.EditText EditText44;
        private SAPbouiCOM.EditText EditText46;
        private SAPbouiCOM.EditText EditText47;
        private SAPbouiCOM.EditText EditText48;
        private SAPbouiCOM.EditText EditText49;
        private SAPbouiCOM.EditText EditText51;
        private SAPbouiCOM.EditText EditText52;
        private SAPbouiCOM.EditText EditText53;
        private SAPbouiCOM.EditText EditText54;
        private SAPbouiCOM.EditText EditText56;
        private SAPbouiCOM.EditText EditText57;
        private SAPbouiCOM.EditText EditText58;
        private SAPbouiCOM.EditText EditText59;
        private SAPbouiCOM.EditText EditText61;
        private SAPbouiCOM.EditText EditText62;
        private SAPbouiCOM.EditText EditText63;
        private SAPbouiCOM.EditText EditText64;
        private SAPbouiCOM.EditText EditText66;
        private SAPbouiCOM.EditText EditText67;
        private SAPbouiCOM.EditText EditText68;
        private SAPbouiCOM.EditText EditText69;
        private SAPbouiCOM.EditText EditText70;
        private SAPbouiCOM.StaticText StaticText63;
        private SAPbouiCOM.StaticText StaticText64;
        private SAPbouiCOM.StaticText StaticText65;
        private SAPbouiCOM.StaticText StaticText66;
        private SAPbouiCOM.StaticText StaticText67;
        private SAPbouiCOM.StaticText StaticText68;
        private SAPbouiCOM.StaticText StaticText69;
        private SAPbouiCOM.StaticText StaticText70;
        private SAPbouiCOM.EditText EditText71;
        private SAPbouiCOM.EditText EditText72;
        private SAPbouiCOM.StaticText StaticText71;
        private SAPbouiCOM.EditText EditText73;
        private SAPbouiCOM.EditText EditText74;
        private SAPbouiCOM.StaticText StaticText72;
        private SAPbouiCOM.EditText EditText76;
        private SAPbouiCOM.StaticText StaticText73;
        private SAPbouiCOM.EditText EditText78;
        private SAPbouiCOM.StaticText StaticText74;
        private SAPbouiCOM.EditText EditText79;
        private SAPbouiCOM.StaticText StaticText75;
        private SAPbouiCOM.EditText EditText80;
        private SAPbouiCOM.StaticText StaticText76;
        private SAPbouiCOM.EditText EditText81;
        private SAPbouiCOM.StaticText StaticText77;
        private SAPbouiCOM.EditText EditText82;
        private SAPbouiCOM.StaticText StaticText78;
        private SAPbouiCOM.StaticText StaticText81;
        private SAPbouiCOM.Grid Grid0;
        private SAPbouiCOM.Folder Folder0;
        private SAPbouiCOM.Folder Folder1;
        private SAPbouiCOM.Folder Folder2;

        private void Form_DataLoadAfter(ref SAPbouiCOM.BusinessObjectInfo pVal)
        {
            UIAPIRawForm.Freeze(true);
            try
            {
                Grid0.DataTable.ExecuteQuery($"Exec ESY_SP_GetSerialTransactions '{EditText1.Value}'");

              SAPbobsCOM.Recordset oRecordset = Diamond_Addon.Providers.B1Provider.oRecordset($"Exec ESY_SP_GetSerialDetail '{EditText1.Value}'");

                if (oRecordset.RecordCount > 0)
                {
                    EditText4.Value = oRecordset.Fields.Item("TaggingDefinition").Value.ToString();
                  ComboBox0.Select( oRecordset.Fields.Item("Status").Value.ToString(),SAPbouiCOM.BoSearchKey.psk_ByValue); 
                    EditText10.Value = oRecordset.Fields.Item("Category").Value.ToString();
                    EditText11.Value = oRecordset.Fields.Item("Design").Value.ToString();
                    EditText12.Value = oRecordset.Fields.Item("Style").Value.ToString();
                    EditText13.Value = oRecordset.Fields.Item("DesignGroup").Value.ToString();
                    EditText14.Value = oRecordset.Fields.Item("SubCategory").Value.ToString();
                    EditText15.Value = oRecordset.Fields.Item("Brand").Value.ToString();
                    EditText20.Value = oRecordset.Fields.Item("Occation").Value.ToString();

                    EditText26.Value = oRecordset.Fields.Item("CurrentLocation").Value.ToString();

                }



                double v1 = 0, v2 = 0, v3 = 0, v4 = 0;
                double.TryParse(EditText66.Value.ToString(), out v1);
                double.TryParse(EditText67.Value.ToString(), out v2);
                double.TryParse(EditText68.Value.ToString(), out v3);
                double.TryParse(EditText69.Value.ToString(), out v4);

                EditText70.Value = (v1 + v2 + v3 + v4).ToString();


                double.TryParse(EditText36.Value.ToString(), out v1);
                double.TryParse(EditText37.Value.ToString(), out v2);
                double.TryParse(EditText38.Value.ToString(), out v3);
                double.TryParse(EditText39.Value.ToString(), out v4);

                EditText40.Value = (v1 + v2 + v3 + v4).ToString();
            }
            catch(Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message);
            }
            UIAPIRawForm.Freeze(false);
        }

        private SAPbouiCOM.ComboBox ComboBox0;
        private SAPbouiCOM.ComboBox ComboBox1;
    }
}
