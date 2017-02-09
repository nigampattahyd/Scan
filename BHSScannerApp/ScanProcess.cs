using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using bhsScandll;

namespace BHSScannerApp
{
    public partial class ScanProcess : Form
    {
        public ScanProcess()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("ScanProcess.cs", "ScanProcess()", ex.Message);
            }
        }
        MainForm frm;
        Settings frmsettings;
        // SalixSettings frmsalixsettings;
        private int count;
        private string DocType;
        public ScanProcess(int count, string _type, MainForm frmMain)
        {
            try
            {
                InitializeComponent();

                if (_type == "R")
                {
                    label2.Text = count.ToString() + " pages have been scanned in to Request records";
                }
                else if (_type == "M")
                {
                    label2.Text = count.ToString() + " pages have been scanned in to Medical records";
                }
                else if (_type == "TR")
                {
                    label2.Text = count.ToString() + " pages have been scanned in to Transport Request";
                }
                else if (_type == "PR")
                {
                    label2.Text = count.ToString() + " pages have been scanned in to Payment Request";
                }
                else if (_type == "PO")
                {
                    label2.Text = count.ToString() + " pages have been scanned in to Purchase Order";
                }
                else if (_type == "FR")
                {
                    label2.Text = count.ToString() + " pages have been scanned in to Facility Request";
                }
                frm = frmMain;
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("ScanProcess.cs", "ScanProcess(int count,string _type,MainForm frmMain)", ex.Message);
            }
        }
        public ScanProcess(int count, string _type, Settings frmMain)
        {
            try
            {
                InitializeComponent();

                if (_type == "R")
                {
                    label2.Text = count.ToString() + " pages have been scanned in to Request records";
                }
                else if (_type == "M")
                {
                    label2.Text = count.ToString() + " pages have been scanned in to Medical records";
                }
                else if (_type == "TR")
                {
                    label2.Text = count.ToString() + " pages have been scanned in to Transport Request";
                }
                else if (_type == "PR")
                {
                    label2.Text = count.ToString() + " pages have been scanned in to Payment Request";
                }
                else if (_type == "PO")
                {
                    label2.Text = count.ToString() + " pages have been scanned in to Purchase Order";
                }
                else if (_type == "FR")
                {
                    label2.Text = count.ToString() + " pages have been scanned in to Facility Request";
                }
                frmsettings = frmMain;
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("ScanProcess.cs", "ScanProcess(int count, string _type, Settings frmMain)", ex.Message);
            }
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            try
            {
                if (frm != null)
                {
                    frm.PerformScan();
                }
                else
                {
                    frmsettings.PerformScan();
                }

                this.Close();
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("ScanProcess.cs", "btnScan_Click", ex.Message);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (frm != null)
                {
                    frm.EndScanning();
                }
                else
                {
                    frmsettings.EndScanning();
                }

                this.Close();
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("ScanProcess.cs", "btnOk_Click", ex.Message);
            }
        }
    }
}


