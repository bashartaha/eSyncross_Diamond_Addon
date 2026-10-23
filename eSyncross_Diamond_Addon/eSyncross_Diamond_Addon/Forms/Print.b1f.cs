using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Diamond_Addon.Providers;
using SAPbouiCOM.Framework;

namespace DiamondAddon.Forms
{
    [FormAttribute("DiamondAddon.Forms.Print", "Forms/Print.b1f")]
    class Print : UserFormBase
    {
        private const string RfidPrinterName = "RIFD-Printer";
        private const int MaximumSerialNumberLength = 12;

        public Print()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_0").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_2").Specific));
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("Item_3").Specific));
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("Item_1").Specific));
            this.EditText0.ChooseFromListAfter += new SAPbouiCOM._IEditTextEvents_ChooseFromListAfterEventHandler(this.EditText0_ChooseFromListAfter);
            this.Folder0 = ((SAPbouiCOM.Folder)(this.GetItem("Item_5").Specific));
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_6").Specific));
            this.Button0.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.Button0_ClickAfter);
            this.Grid0 = ((SAPbouiCOM.Grid)(this.GetItem("Item_7").Specific));
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("Item_8").Specific));
            this.Button1.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.Button1_ClickAfter);
            this.Button2 = ((SAPbouiCOM.Button)(this.GetItem("Item_9").Specific));
            this.Button2.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.Button2_ClickAfter);
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
        }


        private void OnCustomInitialize()
        {
            foreach (SAPbouiCOM.Item item in UIAPIRawForm.Items)
            {
                if (item.Type == SAPbouiCOM.BoFormItemTypes.it_EDIT ||
                    item.Type == SAPbouiCOM.BoFormItemTypes.it_STATIC)
                {
                    item.Height = item.Height + 4;
                }
            }
        }

        private SAPbouiCOM.StaticText StaticText0;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.EditText EditText1;
        private SAPbouiCOM.Folder Folder0;
        private SAPbouiCOM.Button Button0;
        private SAPbouiCOM.Grid Grid0;
        private SAPbouiCOM.Button Button1;

        private void EditText0_ChooseFromListAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {

            try
            {
                SAPbouiCOM.ISBOChooseFromListEventArg cfle = (SAPbouiCOM.ISBOChooseFromListEventArg)pVal;
                string uid = cfle.ChooseFromListUID;
                SAPbouiCOM.DataTable dt = cfle.SelectedObjects;


                try { EditText0.Value = dt.GetValue("DocNum", 0).ToString(); } catch { }

                try
                {
                    Grid0.DataTable.ExecuteQuery($"EXEC ESY_SP_GetSerialNumbersList  '{ dt.GetValue("DocNum", 0).ToString()}'  ");

                    ((SAPbouiCOM.EditTextColumn)Grid0.Columns.Item("Item Code")).LinkedObjectType = "4";
                    Grid0.Columns.Item("Select").Type = SAPbouiCOM.BoGridColumnType.gct_CheckBox;

                    for (int i = 0; i < Grid0.Columns.Count; i++)
                    {
                        if (Grid0.Columns.Item(i).UniqueID != "Select")
                        {
                            Grid0.Columns.Item(i).Editable = false;
                        }
                    }

                }
                catch
                {
                }


            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message);
            }


        }

        private void Button0_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            try
            {

                int confirmation = Application.SBO_Application.MessageBox(
                   string.Format("Are you sure you want to print selected RFID tag(s)?"),
                   2,
                   "Yes",
                   "No");

                if (confirmation != 1)
                {
                    return;
                }


                List<string> selectedSerialNumbers = GetSelectedSerialNumbers();

                if (selectedSerialNumbers.Count == 0)
                {
                    Application.SBO_Application.SetStatusBarMessage(
                        "Select at least one serial number to print.",
                        SAPbouiCOM.BoMessageTime.bmt_Short,
                        true);
                    return;
                }

               

                StringBuilder printJob = new StringBuilder();
                foreach (string serialNumber in selectedSerialNumbers)
                {
                    ValidateSerialNumber(serialNumber);
                    printJob.Append(BuildRfidLabel(serialNumber));
                }

                RawPrinter.SendStringToPrinter(RfidPrinterName, printJob.ToString());

                Application.SBO_Application.SetStatusBarMessage(
                    string.Format("{0} RFID tag(s) sent to printer {1}.", selectedSerialNumbers.Count, RfidPrinterName),
                    SAPbouiCOM.BoMessageTime.bmt_Short,
                    false);
            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(
                    "RFID printing failed: " + ex.Message,
                    SAPbouiCOM.BoMessageTime.bmt_Medium,
                    true);
            }
        }

        private List<string> GetSelectedSerialNumbers()
        {
            List<string> serialNumbers = new List<string>();

            for (int rowIndex = 0; rowIndex < Grid0.DataTable.Rows.Count; rowIndex++)
            {
                string isSelected = Convert.ToString(Grid0.DataTable.GetValue("Select", rowIndex)).Trim();
                if (!string.Equals(isSelected, "Y", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                string serialNumber = Convert.ToString(
                    Grid0.DataTable.GetValue("Serial Number", rowIndex)).Trim();
                serialNumbers.Add(serialNumber);
            }

            return serialNumbers;
        }

        private static void ValidateSerialNumber(string serialNumber)
        {
            if (string.IsNullOrWhiteSpace(serialNumber))
            {
                throw new InvalidOperationException("A selected row has an empty serial number.");
            }

            if (serialNumber.Length > MaximumSerialNumberLength)
            {
                throw new InvalidOperationException(
                    string.Format("Serial number {0} exceeds the {1}-character RFID limit.",
                        serialNumber,
                        MaximumSerialNumberLength));
            }

            if (!serialNumber.All(IsAsciiLetterOrDigit))
            {
                throw new InvalidOperationException(
                    string.Format("Serial number {0} must contain letters and numbers only.", serialNumber));
            }
        }

        private static bool IsAsciiLetterOrDigit(char value)
        {
            return (value >= '0' && value <= '9') ||
                   (value >= 'A' && value <= 'Z') ||
                   (value >= 'a' && value <= 'z');
        }

        private static string BuildRfidLabel(string serialNumber)
        {
            StringBuilder zpl = new StringBuilder();
            zpl.AppendLine("^XA");
            zpl.AppendLine("^PR2,2,2");
            zpl.AppendLine("^FX === RFID ENCODE ===");
            zpl.AppendLine("^RS8");
            zpl.AppendLine("^RFW,A,,,A");
            zpl.Append("^FD").Append(serialNumber).AppendLine("^FS");
            zpl.AppendLine("^FX === PRINT SERIAL NUMBER ===");
            zpl.Append("^FO50,90^FB380,1,,L,^A0N,18,18^FD")
                .Append(serialNumber)
                .AppendLine("^FS");
            zpl.AppendLine("^PQ1,0,1,Y");
            zpl.AppendLine("^XZ");
            return zpl.ToString();
        }

        private SAPbouiCOM.Button Button2;

        private void Button1_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            UIAPIRawForm.Close();

        }

        private void Button2_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {

            try
            {
                if (EditText1.Value == "") { Application.SBO_Application.SetStatusBarMessage($"Serial Number value is missing"); return; }

                UIAPIRawForm.Freeze(true);
                SAPbobsCOM.Recordset recordset = B1Provider.GetDetailsBySerialNumber(EditText1.Value);

                if (recordset.RecordCount > 0)
                {
                    Grid0.DataTable.Rows.Add();
                    int newRowIndex = Grid0.DataTable.Rows.Count - 1;
                    Grid0.DataTable.SetValue("Select", newRowIndex, "Y");
                    Grid0.DataTable.SetValue("Serial Number", newRowIndex, recordset.Fields.Item("DistNumber").Value);
                    Grid0.DataTable.SetValue("Item Code", newRowIndex, recordset.Fields.Item("ItemCode").Value);
                    Grid0.DataTable.SetValue("Item Description", newRowIndex, recordset.Fields.Item("ItemName").Value);

                    EditText1.Value = "";

                }
                else
                {
                    Application.SBO_Application.SetStatusBarMessage($"Serial Number : {EditText1.Value} does not exists");

                }

            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message);
            }
            finally
            {
                UIAPIRawForm.Freeze(false);
            }



        }

        private static class RawPrinter
        {
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
            private struct DocumentInfo
            {
                public string DocumentName;
                public string OutputFile;
                public string DataType;
            }

            [DllImport("winspool.drv", CharSet = CharSet.Unicode, SetLastError = true)]
            private static extern bool OpenPrinter(string printerName, out IntPtr printerHandle, IntPtr defaults);

            [DllImport("winspool.drv", SetLastError = true)]
            private static extern bool ClosePrinter(IntPtr printerHandle);

            [DllImport("winspool.drv", CharSet = CharSet.Unicode, SetLastError = true)]
            private static extern int StartDocPrinter(IntPtr printerHandle, int level, ref DocumentInfo documentInfo);

            [DllImport("winspool.drv", SetLastError = true)]
            private static extern bool EndDocPrinter(IntPtr printerHandle);

            [DllImport("winspool.drv", SetLastError = true)]
            private static extern bool StartPagePrinter(IntPtr printerHandle);

            [DllImport("winspool.drv", SetLastError = true)]
            private static extern bool EndPagePrinter(IntPtr printerHandle);

            [DllImport("winspool.drv", SetLastError = true)]
            private static extern bool WritePrinter(
                IntPtr printerHandle,
                byte[] bytes,
                int byteCount,
                out int bytesWritten);

            public static void SendStringToPrinter(string printerName, string value)
            {
                IntPtr printerHandle;
                if (!OpenPrinter(printerName, out printerHandle, IntPtr.Zero))
                {
                    ThrowLastWin32Exception("Unable to open printer " + printerName + ".");
                }

                bool documentStarted = false;
                bool pageStarted = false;

                try
                {
                    DocumentInfo documentInfo = new DocumentInfo
                    {
                        DocumentName = "SAP B1 RFID Serial Numbers",
                        DataType = "RAW",
                        OutputFile = null
                    };

                    if (StartDocPrinter(printerHandle, 1, ref documentInfo) == 0)
                    {
                        ThrowLastWin32Exception("Unable to start the RFID print job.");
                    }

                    documentStarted = true;

                    if (!StartPagePrinter(printerHandle))
                    {
                        ThrowLastWin32Exception("Unable to start the RFID print page.");
                    }

                    pageStarted = true;
                    byte[] bytes = Encoding.ASCII.GetBytes(value);
                    int bytesWritten;

                    if (!WritePrinter(printerHandle, bytes, bytes.Length, out bytesWritten))
                    {
                        ThrowLastWin32Exception("Unable to send ZPL to the RFID printer.");
                    }

                    if (bytesWritten != bytes.Length)
                    {
                        throw new InvalidOperationException("The RFID printer received an incomplete print job.");
                    }
                }
                finally
                {
                    if (pageStarted)
                    {
                        EndPagePrinter(printerHandle);
                    }

                    if (documentStarted)
                    {
                        EndDocPrinter(printerHandle);
                    }

                    ClosePrinter(printerHandle);
                }
            }

            private static void ThrowLastWin32Exception(string message)
            {
                int errorCode = Marshal.GetLastWin32Error();
                throw new Win32Exception(errorCode, message + " " + new Win32Exception(errorCode).Message);
            }
        }

    }
}
