using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TwainDotNet;
using System.IO;
using TwainDotNet.WinFroms;
using IWshRuntimeLibrary;
namespace BHSScannerApp
{
    using TwainDotNet.TwainNative;
    using IWshRuntimeLibrary;
    using System.Runtime.InteropServices;
    using bhsScandll;

    public partial class Settings : Form
    {
        Twain _twain;
        ScanSettings _settings;
        string RequestNumber;
        string ReleaseCode;
        int count = 0;
        string ClipData;
        bool allowVisible = false;
        public Settings(Twain _tw)
        {
            try
            {
                _twain = _tw;
                InitializeComponent();
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("Settings.cs", "Settings(Twain _tw)", ex.Message);
            }
        }

        public Settings()
        {
            try
            {
                InitializeComponent();
                checkBox1.Checked = true;
                try
                {
                    _twain = new Twain(new WinFormsWindowMessageHook(this));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Scanner driver not found.");
                    Application.Exit();
                    return;
                }

                string DocType = "";
                string RequestType = "";
                string UserId = "";
                string Id = "";
                _twain.TransferImage += delegate(Object sender, TransferImageEventArgs args)
                {
                    try
                    {
                        if (args.Image != null)
                        {
                            if (args.Image != null)
                            {
                                if (rbtnMediRecd.Checked)
                                {
                                    DocType = "M";
                                }
                                else if (rbtnReqLett.Checked)
                                {
                                    DocType = "R";
                                }

                                if (DocType == "M" || DocType == "R")
                                {
                                    BHSScanlibrary.BHSScan clscan = new BHSScanlibrary.BHSScan();
                                    clscan.ImageUpload(args.Image, DocType, RequestNumber, ReleaseCode);
                                }
                                else
                                {
                                    BHSScanlibrary.SalixScan salixscan = new BHSScanlibrary.SalixScan();
                                    string str = ClipData;
                                    if (!str.Contains("-"))
                                        str = salixscan.SalixGetClipdata();
                                    string[] words = str.Split('-');
                                    DocType = words[0];
                                    RequestType = words[1];
                                    UserId = words[2];
                                    Id = words[3];
                                    if (RequestType == "TR" || RequestType == "PR" || RequestType == "PO" || RequestType == "FR")
                                    {
                                        salixscan.SalixImageUpload(args.Image, DocType, RequestType, UserId, Id, count);
                                    }
                                }
                                count = count + 1;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error " + ex.Message);
                    }
                };
                _twain.ScanningComplete += delegate
                {
                    Enabled = true;
                    lblCount.Text = count.ToString();
                    // string _type = "";
                    if (DocType == "M" || DocType == "R")
                    {
                        ScanProcess objScan = new ScanProcess(count, DocType, this);
                        objScan.Show();
                    }
                    else
                    {
                        BHSScanlibrary.SalixScan salixscan = new BHSScanlibrary.SalixScan();
                        string str = salixscan.SalixGetClipdata();
                        string[] words = str.Split('-');
                        DocType = words[0];
                        RequestType = words[1];
                        UserId = words[2];
                        Id = words[3];
                        if (RequestType == "TR" || RequestType == "PR" || RequestType == "PO" || RequestType == "FR")
                        {
                            ScanProcess objScan = new ScanProcess(count, RequestType, this);
                            objScan.Show();
                            IsRunning = false;
                        }
                    }

                    //if (objScan.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    //{
                    //    PerformScan();
                    //}
                    ////if (MessageBox.Show("    Total Page(s) scanned: " + count + Environment.NewLine + Environment.NewLine + 
                    ////    "     Do you want to continue..? ", "Continue Scanning..",
                    ////    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    ////{

                    ////}
                    //else
                    //{
                    //    txtNumber.Text = "";
                    //    txtNumber.Enabled = true;
                    //    rbtnReqLett.Checked = true;
                    //    groupBox1.Enabled = true;
                    //    count = 0;
                    //}
                };
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("Settings.cs", "Settings()", ex.Message);
            }
        }

        private void selectSource_Click(object sender, EventArgs e)
        {
            try
            {
                _twain.SelectSource();
            }
            catch (Exception ex)
            {
                ex.ToString();
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("Settings.cs", "selectSource_Click", ex.Message);
            }

        }

        private void diagnostics_Click(object sender, EventArgs e)
        {
            try
            {
                diagnosticsfrm obj = new diagnosticsfrm();
                obj.Show();
                obj.Diagnostics(new WinFormsWindowMessageHook(this));
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("Settings.cs", "diagnostics_Click", ex.Message);
            }
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            try
            {
                lblversion.Text = "©2016 BHS Cincinnati, OH, All rights reserved. version- 1.0.0.28";
                RegisterHotKey(this.Handle, HOTKEY_ID, KeyModifiers.Alt, Keys.D1);
                BHSScanlibrary.BHSScan clsobj = new BHSScanlibrary.BHSScan();
                clsobj.chkSettingfile();
                string tempFilename = Application.StartupPath + "\\Settings.ini";
                string settings = System.IO.File.ReadAllText(tempFilename);
                settings = settings.Replace("\r", "");
                string[] Setting = settings.Split('\n');
                for (int i = 0; i < Setting.GetUpperBound(0); i++)
                {
                    string[] subSplit = Setting[i].Split('=');
                    switch (subSplit[0])
                    {
                        case "useadp":
                            SetControlSettings(useAdfCheckBox, subSplit[1]);
                            break;
                        case "bw":
                            SetControlSettings(blackAndWhiteCheckBox, subSplit[1]);
                            break;
                        case "autoborder":
                            SetControlSettings(autoDetectBorderCheckBox, subSplit[1]);
                            break;
                        case "useui":
                            SetControlSettings(useUICheckBox, subSplit[1]);
                            break;
                        case "useduplex":
                            SetControlSettings(useDuplexCheckBox, subSplit[1]);
                            break;
                        case "grabarea":
                            SetControlSettings(checkBoxArea, subSplit[1]);
                            break;
                        case "autorotate":
                            SetControlSettings(autoRotateCheckBox, subSplit[1]);
                            break;
                        case "showprogress":
                            SetControlSettings(showProgressIndicatorUICheckBox, subSplit[1]);
                            break;
                        case "autorun":
                            string autorun = subSplit[1];
                            if (autorun == "True")
                            {
                                checkBox1.Checked = true;
                            }
                            else
                            {
                                checkBox1.Checked = false;
                            }
                            break;
                    }
                }

                if (_twain != null)
                {
                    lblScanner.Text = _twain.DefaultSourceName;
                }

                //useadp = 
                //bw = 
                //autoborder = 
                //useui = 
                //useduplex = 
                //grabarea = 
                //autorotate = 
                //showprogress = 
            }
            catch (Exception ex)
            {
                ex.ToString();
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("Settings.cs", "Settings_Load", ex.Message);
            }

        }
        private void SetControlSettings(CheckBox chk, string value)
        {
            try
            {
                if (value.ToUpper() == "TRUE")
                {
                    chk.Checked = true;
                }
                else
                {
                    chk.Checked = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("Settings.cs", "SetControlSettings", ex.Message);
            }
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                string tempFilename = Application.StartupPath + "\\Settings.ini";
                StreamWriter SW = new StreamWriter(tempFilename);
                SW.WriteLine("useadp=" + useAdfCheckBox.Checked.ToString());
                SW.WriteLine("bw=" + blackAndWhiteCheckBox.Checked.ToString());
                SW.WriteLine("autoborder=" + autoDetectBorderCheckBox.Checked.ToString());
                SW.WriteLine("useui=" + useUICheckBox.Checked.ToString());
                SW.WriteLine("useduplex=" + useDuplexCheckBox.Checked.ToString());
                SW.WriteLine("grabarea=" + checkBoxArea.Checked.ToString());
                SW.WriteLine("autorotate=" + autoRotateCheckBox.Checked.ToString());
                SW.WriteLine("showprogress=" + showProgressIndicatorUICheckBox.Checked.ToString());


                try
                {
                    if (checkBox1.Checked)
                    {
                        string path = Application.StartupPath;
                        string startmenupath = Environment.GetFolderPath(Environment.SpecialFolder.Programs) + @"\BHS  inc\BHS Scanner\BHS Scanner Application.appref-ms";
                        string startUpPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + @"\BHS Scanner Application.appref-ms";
                        System.IO.File.Copy(startmenupath, startUpPath);
                        SW.WriteLine("autorun=True");
                    }
                    else
                    {
                        string startUpPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + @"\BHS Scanner Application.appref-ms";
                        System.IO.File.Delete(startUpPath);
                        SW.WriteLine("autorun=False");
                    }
                }
                catch
                {
                    SW.WriteLine("autorun=True");
                }


                SW.Close();
                SW.Dispose();

                this.Close();
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("Settings.cs", "saveButton_Click", ex.Message);
            }
        }
        private void fillform()
        {
            try
            {
                if (ReadKey("useui").ToString() != "")
                {
                    if (Convert.ToBoolean(ReadKey("useui")))
                    {
                        useUICheckBox.Checked = true;
                    }
                    else
                    {
                        useUICheckBox.Checked = false;
                    }
                }

                if (ReadKey("showprogress").ToString() != "")
                {
                    if (Convert.ToBoolean(ReadKey("showprogress")))
                    {

                    }
                    else
                    {
                    }
                }

                if (ReadKey("useduplex").ToString() != "")
                {
                    if (Convert.ToBoolean(ReadKey("useduplex")))
                    {
                    }
                    else
                    {
                    }
                }

                if (ReadKey("autorotate").ToString() != "")
                {
                    if (Convert.ToBoolean(ReadKey("autorotate")))
                    {
                    }
                    else
                    {
                    }
                }

                if (ReadKey("autoborder").ToString() != "")
                {
                    if (Convert.ToBoolean(ReadKey("autoborder")))
                    {
                    }
                    else
                    {
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("Settings.cs", "fillform()", ex.Message);
            }
        }
        private string ReadKey(string key)
        {
            try
            {
                return System.Configuration.ConfigurationSettings.AppSettings[key].ToString();
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("Settings.cs", "ReadKey", ex.Message);
                return null;
            }
        }

        protected override void SetVisibleCore(bool value)
        {
            if (!allowVisible)
            {
                value = false;
                if (!this.IsHandleCreated) CreateHandle();
            }
            base.SetVisibleCore(value);
        }

        public void PerformScan()
        {
            try
            {
                RequestNumber = txtNumber.Text.Trim();
                if (RequestNumber.Contains("-"))
                {
                    BHSScanlibrary.SalixScan clscan = new BHSScanlibrary.SalixScan();
                    ReleaseCode = clscan.getPaymentIdSalix(RequestNumber);
                    ClipData = clscan.SalixGetClipdata();
                }
                else
                {
                    BHSScanlibrary.BHSScan clscan = new BHSScanlibrary.BHSScan();
                    ReleaseCode = clscan.getReleaseCode(RequestNumber);
                }

                if (ReleaseCode != "")
                {
                    Enabled = false;

                    bool useadp = false;
                    bool bw = false;
                    bool autoborder = false;
                    bool useui = false;
                    bool useduplex = false;
                    bool grabarea = false;
                    bool autorotate = false;
                    bool showprogress = false;
                    BHSScanlibrary.BHSScan clsobj = new BHSScanlibrary.BHSScan();
                    clsobj.chkSettingfile();
                    string tempFilename = Application.StartupPath + "\\Settings.ini";
                    string settings = System.IO.File.ReadAllText(tempFilename);
                    settings = settings.Replace("\r", "");
                    string[] Setting = settings.Split('\n');
                    for (int i = 0; i < Setting.GetUpperBound(0); i++)
                    {
                        string[] subSplit = Setting[i].Split('=');
                        switch (subSplit[0])
                        {
                            case "useadp":
                                SetControlSettings(ref useadp, subSplit[1]);
                                break;
                            case "bw":
                                SetControlSettings(ref bw, subSplit[1]);
                                break;
                            case "autoborder":
                                SetControlSettings(ref autoborder, subSplit[1]);
                                break;
                            case "useui":
                                SetControlSettings(ref useui, subSplit[1]);
                                break;
                            case "useduplex":
                                SetControlSettings(ref useduplex, subSplit[1]);
                                break;
                            case "grabarea":
                                SetControlSettings(ref grabarea, subSplit[1]);
                                break;
                            case "autorotate":
                                SetControlSettings(ref autorotate, subSplit[1]);
                                break;
                            case "showprogress":
                                SetControlSettings(ref showprogress, subSplit[1]);
                                break;
                        }
                    }



                    //string str = Clipboard.GetText();
                    _settings = new ScanSettings();

                    _settings.UseDocumentFeeder = useadp;//false;// useAdfCheckBox.Checked;
                    _settings.ShowTwainUI = useui;// ReadBoolKey("useui");// useUICheckBox.Checked;
                    _settings.ShowProgressIndicatorUI = showprogress;// ReadBoolKey("showprogress"); //showProgressIndicatorUICheckBox.Checked;
                    _settings.UseDuplex = useduplex;// ReadBoolKey("useduplex");// useDuplexCheckBox.Checked;
                    _settings.Resolution = ResolutionSettings.Fax;
                    
                    // blackAndWhiteCheckBox.Checked
                    // ? ResolutionSettings.Fax : ResolutionSettings.ColourPhotocopier;
                    _settings.Area = null;// !checkBoxArea.Checked ? null : AreaSettings;
                    _settings.ShouldTransferAllPages = true;
                                        
                    _settings.Rotation = new RotationSettings()
                    {
                        AutomaticRotate = autorotate,//ReadBoolKey("autorotate"),  //autoRotateCheckBox.Checked,
                        AutomaticBorderDetection = autoborder//ReadBoolKey("autoborder") // autoDetectBorderCheckBox.Checked
                    };
                    _settings.Resolution.Dpi = 200;

                    try
                    {
                        _twain.StartScanning(_settings);
                    }
                    catch (TwainException ex)
                    {
                        ErrorLog objLog = new ErrorLog();
                        objLog.logerror("Settings.cs", "PerformScan", ex.Message);
                        MessageBox.Show(ex.Message);
                        Enabled = true;
                        IsRunning = false;
                        //scan.Text = "+" + Environment.NewLine + "Another page";
                    }
                }
                else
                {
                    frmMessage objmsg = new frmMessage();
                    objmsg.ShowDialog();
                    objmsg.Dispose();
                    IsRunning = false;
                    // MessageBox.Show("Please go to document entry screen and click the scan button.");
                }
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("Settings.cs", "PerformScan()", ex.Message);
            }
        }

        private void scan_Click(object sender, EventArgs e)
        {
            try
            {
                PerformScan();
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("Settings.cs", "scan_Click", ex.Message);
            }
        }

        private void SetControlSettings(ref bool chk, string value)
        {
            try
            {
                if (value.ToUpper() == "TRUE")
                {
                    chk = true;
                }
                else
                {
                    chk = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("Settings.cs", "SetControlSettings", ex.Message);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                tabControl1.SelectedIndex = 0;
                //Settings objSetting = new Settings(_twain);
                //objSetting.ShowDialog();
                //objSetting.Dispose();
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("Settings.cs", "linkLabel1_LinkClicked", ex.Message);
            }
        }

        private bool ReadBoolKey(string key)
        {
            try
            {
                if (System.Configuration.ConfigurationManager.AppSettings[key].ToString() == "")
                {
                    return false;
                }
                else
                {
                    return Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings[key]);
                }
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("Settings.cs", "ReadBoolKey", ex.Message);
                return false;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("Settings.cs", "exitToolStripMenuItem_Click", ex.Message);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (this.WindowState == FormWindowState.Normal)
                {
                    e.Cancel = true;
                    this.Hide();
                    this.WindowState = FormWindowState.Minimized;
                }
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("Settings.cs", "MainForm_FormClosing", ex.Message);
            }
        }
        Boolean IsRunning = false;
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (IsRunning == true)
                {
                    AutoClosingMessageBox.Show("Scanner is Running \nPlease Wait", "BHS Scanner Application", 3000);
                }
                if (IsRunning == false)
                {
                    IsRunning = true;
                    BHSScanlibrary.BHSScan clscan = new BHSScanlibrary.BHSScan();
                    string str = clscan.GetClipdata();
                    if (str != "")
                    {
                        txtNumber.Text = str;
                        string[] strArr = str.Split('-');
                        txtNumber.Text = strArr[1];
                        if (strArr[0] == "M")
                        {
                            rbtnMediRecd.Checked = true;
                        }
                        else if (strArr[0] == "R")
                        {
                            rbtnReqLett.Checked = true;
                        }
                        else
                        {
                            rbtnMediRecd.Checked = false;
                            rbtnReqLett.Checked = false;
                            txtNumber.Text = strArr[1] + "-" + strArr[3];
                        }
                        txtNumber.Enabled = false;
                        groupBox1.Enabled = false;
                    }
                    PerformScan();
                    //IsRunning = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("Settings.cs", "notifyIcon1_MouseDoubleClick", ex.Message);
            }
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            try
            {
                txtNumber.Text = "";
                txtNumber.Enabled = true;
                rbtnReqLett.Checked = true;
                groupBox1.Enabled = true;
                count = 0;
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("Settings.cs", "btnComplete_Click", ex.Message);
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            try
            {
                if (WindowState == FormWindowState.Minimized)
                {
                    this.Hide();
                    this.WindowState = FormWindowState.Minimized;
                }
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("Settings.cs", "MainForm_Resize", ex.Message);
            }
        }

        private void seToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Settings objSetting = new Settings(_twain);
                //objSetting.Show();
                allowVisible = true;
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.Activate();
                this.Focus();
                tabControl1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("Settings.cs", "seToolStripMenuItem_Click", ex.Message);
            }
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.Activate();
                this.Focus();
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("Settings.cs", "showToolStripMenuItem_Click", ex.Message);
            }
        }

        public void EndScanning()
        {
            try
            {
                txtNumber.Text = "";
                count = 0;

                string DocType = "";
                string RequestType = "";
                string UserId = "";
                string Id = "";
                if (rbtnMediRecd.Checked)
                {
                    DocType = "M";
                }
                else if (rbtnReqLett.Checked)
                {
                    DocType = "R";
                }

                if (DocType == "M" || DocType == "R")
                {
                    BHSScanlibrary.BHSScan clscan = new BHSScanlibrary.BHSScan();
                    clscan.EndScanning(RequestNumber, ReleaseCode, DocType);
                }
                else
                {
                    BHSScanlibrary.SalixScan salixscan = new BHSScanlibrary.SalixScan();
                    string str = salixscan.SalixGetClipdata();
                    string[] words = str.Split('-');
                    DocType = words[0];
                    RequestType = words[1];
                    UserId = words[2];
                    Id = words[3];
                    if (RequestType == "TR" || RequestType == "PR" || RequestType == "PO" || RequestType == "FR")
                    {
                        salixscan.SalixEndScanning(DocType, RequestType, UserId, Id);
                    }
                    txtNumber.Enabled = true;
                    rbtnReqLett.Checked = true;
                    groupBox1.Enabled = true;
                }
                Clipboard.Clear();
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("Settings.cs", "EndScanning", ex.Message);
            }
        }



        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(
            IntPtr hWnd, // handle to window    
            int id, // hot key identifier    
            KeyModifiers fsModifiers, // key-modifier options    
            Keys vk    // virtual-key code    
            );
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(
            IntPtr hWnd, // handle to window    
            int id      // hot key identifier    
            );
        const int HOTKEY_ID = 31197; //Any number to use to identify the hotkey instance
        public enum KeyModifiers        //enum to call 3rd parameter of RegisterHotKey easily
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            Windows = 8
        }
        const int WM_HOTKEY = 0x0312;//magic hotkey message identifier

        protected override void WndProc(ref System.Windows.Forms.Message message)
        {
            try
            {
                switch (message.Msg)
                {
                    case WM_HOTKEY:
                        Keys key = (Keys)(((int)message.LParam >> 16) & 0xFFFF);
                        KeyModifiers modifier = (KeyModifiers)((int)message.LParam & 0xFFFF);
                        //put your on hotkey code here
                        // MessageBox.Show("HotKey Pressed :" + modifier.ToString() + " " + key.ToString());


                        if (!IsRunning)
                        {
                            IsRunning = true;
                            BHSScanlibrary.BHSScan clscan = new BHSScanlibrary.BHSScan();
                            string str = clscan.GetClipdata();
                            if (clscan.GetClipdata() != "")
                            {
                                string[] strArr = str.Split('-');
                                txtNumber.Text = strArr[1];
                                if (strArr[0] == "M")
                                {
                                    rbtnMediRecd.Checked = true;
                                }
                                else if (strArr[0] == "R")
                                {
                                    rbtnReqLett.Checked = true;
                                }
                                else
                                {
                                    rbtnMediRecd.Checked = false;
                                    rbtnReqLett.Checked = false;
                                    txtNumber.Text = strArr[1] + "-" + strArr[3];
                                }
                                txtNumber.Enabled = false;
                                groupBox1.Enabled = false;
                            }
                            PerformScan();
                            IsRunning = false;
                        }



                        //end hotkey code
                        break;
                }
                base.WndProc(ref message);
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("Settings.cs", "WndProc(ref System.Windows.Forms.Message message)", ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            //var wsh = new IWshShell_Class();
            //IWshRuntimeLibrary.IWshShortcut shortcut = wsh.CreateShortcut(
            //Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\bhsscaninvoke.lnk") as IWshRuntimeLibrary.IWshShortcut;
            //shortcut.TargetPath = path + @"\ScanAppInvoker.exe";
            //shortcut.Save();
        }

        public class AutoClosingMessageBox
        {
            System.Threading.Timer _timeoutTimer;
            string _caption;
            AutoClosingMessageBox(string text, string caption, int timeout)
            {
                _caption = caption;
                _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                    null, timeout, System.Threading.Timeout.Infinite);
                MessageBox.Show(text, caption);
            }
            public static void Show(string text, string caption, int timeout)
            {
                new AutoClosingMessageBox(text, caption, timeout);
            }
            void OnTimerElapsed(object state)
            {
                IntPtr mbWnd = FindWindow(null, _caption);
                if (mbWnd != IntPtr.Zero)
                    SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                _timeoutTimer.Dispose();
            }
            const int WM_CLOSE = 0x0010;
            [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
            static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
            [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
            static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        }

    }

}
