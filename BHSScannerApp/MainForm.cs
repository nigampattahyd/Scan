using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TwainDotNet;
using System.IO;
using TwainDotNet.WinFroms;
using TwainDotNet.TwainNative;

using System.Configuration;

namespace BHSScannerApp
{
    public partial class MainForm : Form
    {
        //private static AreaSettings AreaSettings = new AreaSettings(Units.Centimeters, 0.1f, 5.7f, 0.1F + 2.6f, 5.7f + 2.6f);

        Twain _twain;
        ScanSettings _settings;
        string RequestNumber;
        string ReleaseCode;
        int count = 0;
        public MainForm()
        {
            InitializeComponent();

            try
            {
                _twain = new Twain(new WinFormsWindowMessageHook(this));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Exit();
            }
         
            _twain.TransferImage += delegate(Object sender, TransferImageEventArgs args)
            {
                if (args.Image != null)
                {

                    if (args.Image != null)
                    {
                        string DocType = "";
                        if (rbtnMediRecd.Checked)
                        {
                            DocType = "M";
                        }
                        else
                        {
                            DocType = "R";
                        }
                        BHSScanlibrary.BHSScan clscan = new BHSScanlibrary.BHSScan();
                        clscan.ImageUpload(args.Image, DocType, RequestNumber, ReleaseCode);
                    }
                }
            };
            _twain.ScanningComplete += delegate
            {
                Enabled = true;
                count = count + 1;
                lblCount.Text = count.ToString();
                string _type="";
                ScanProcess objScan = new ScanProcess(count,_type,this);
                objScan.Show();

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

        public void PerformScan()
        {
            RequestNumber = txtNumber.Text.Trim();
            BHSScanlibrary.BHSScan clscan = new BHSScanlibrary.BHSScan();
            ReleaseCode = clscan.getReleaseCode(RequestNumber);
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



                string str = Clipboard.GetText();
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

                try
                {
                    _twain.StartScanning(_settings);
                }
                catch (TwainException ex)
                {
                    MessageBox.Show(ex.Message);
                    Enabled = true;
                    //scan.Text = "+" + Environment.NewLine + "Another page";
                }
            }
            else
            {
                frmMessage objmsg = new frmMessage();
                objmsg.ShowDialog();
                objmsg.Dispose();
               // MessageBox.Show("Please go to document entry screen and click the scan button.");
            }
        }
      
        private void scan_Click(object sender, EventArgs e)
        {
            PerformScan();
        }

        private void SetControlSettings(ref bool chk, string value)
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Settings objSetting = new Settings(_twain);
            objSetting.ShowDialog();
            objSetting.Dispose();
        }

        private bool ReadBoolKey(string key)
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



        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (this.WindowState == FormWindowState.Normal)
            {
                e.Cancel = true;
                this.Hide();
                this.WindowState = FormWindowState.Minimized;
            }


        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           // this.Show();
           // this.WindowState = FormWindowState.Normal;
          //  this.Activate();
         //   this.Focus();
            BHSScanlibrary.BHSScan clscan = new BHSScanlibrary.BHSScan();
            string str =  clscan.GetClipdata();
            if (clscan.GetClipdata() != "")
            {
               string[] strArr = str.Split('-');
               txtNumber.Text = strArr[1];
               if (strArr[0] == "M")
               {
                   rbtnMediRecd.Checked = true;
               }
               else
               {
                   rbtnReqLett.Checked = true;
               }
               txtNumber.Enabled = false;
               groupBox1.Enabled = false;
            }
            PerformScan();
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            txtNumber.Text = "";
            txtNumber.Enabled = true;
            rbtnReqLett.Checked = true;
            groupBox1.Enabled = true;
            count = 0;  
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
              
                this.Hide();
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void seToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings objSetting = new Settings(_twain);
            objSetting.Show();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
            this.Focus();
        }

        public void EndScanning()
        {
            txtNumber.Text = "";
            txtNumber.Enabled = true;
            rbtnReqLett.Checked = true;
            groupBox1.Enabled = true;
            count = 0;
            Clipboard.Clear();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

       
       

      
      
    }
}
