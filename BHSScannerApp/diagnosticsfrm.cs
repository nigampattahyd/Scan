using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TwainDotNet.TwainNative;
using TwainDotNet;
using TwainDotNet.WinFroms;

namespace BHSScannerApp
{
    public partial class diagnosticsfrm : Form
    {
        public diagnosticsfrm()
        {
            InitializeComponent();
            this.Visible = false;
            btndetails.Visible = false;
        }
        public void Diagnostics(IWindowsMessageHook messageHook)
        {
            btnSend.Enabled = false;
           
            using (var dataSourceManager = new DataSourceManager(DataSourceManager.DefaultApplicationId, messageHook))
            {
                
                dataSourceManager.SelectSource();
                this.Visible = true;
                this.TopMost = true; ;
              //  this.TopMost = true;
                this.BringToFront();
                var dataSource = dataSourceManager.DataSource;
                dataSource.OpenSource();
                bool IsSuccess = true;
                lblmessage.Text = "Please wait...";
                foreach (Capabilities capability in Enum.GetValues(typeof(Capabilities)))
                {

                    try
                    {
                        var result = Capability.GetBoolCapability(capability, dataSourceManager.ApplicationId, dataSource.SourceId);
                        ListViewItem objlst = new ListViewItem(result + "    " + capability);
                        if (capability.ToString() == "DeviceOnline")
                        {
                            if (!result)
                            {
                                objlst.ForeColor = Color.Red;
                                IsSuccess = false;
                            }
                        }
                        listView1.Items.Add(objlst);
                        //listBox1.Items.Add(result + "    " + capability);
                        //listBox1.Invalidate();
                        Application.DoEvents();
                        //Console.WriteLine("{0}: {1}", capability, result);
                    }
                    catch (TwainException e)
                    {
                        listView1.Items.Add("Info   " + capability + "   " + e.Message + "    " + e.ReturnCode + "     " + e.ConditionCode);
                        //Console.WriteLine("{0}: {1} {2} {3}", capability, e.Message, e.ReturnCode, e.ConditionCode);
                    }
                }
                if (IsSuccess)
                {
                    lblmessage.Text = "Scanner is OK";
                }
                else
                {
                    lblmessage.Text = "Scanner is not communicating";
                }
                btndetails.Visible = true;
            }
            btnSend.Enabled = true;
        }


        private void btnSend_Click(object sender, EventArgs e)
        {
            BHSScanlibrary.BHSScan objScan = new BHSScanlibrary.BHSScan();
            DataTable dt = objScan.GetEmailOptions("PORTAL_EMAIL");
            string strBody = "";

            for (int i = 0; i < listView1.Items.Count - 1; i++)
            {
                if (listView1.Items[i].ForeColor == Color.Red)
                       strBody = strBody + "<h2> " + listView1.Items[i].Text + " </h2> <Br>";
                else
                    strBody = strBody + listView1.Items[i].Text + " <Br>";
                
            }
            if (SendEmail("", "Scan Setting Troublshoot", dt, strBody))
                this.Close();
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool SendEmail(string RPT_FilePath, string strEmailSubject, DataTable dt,string strBody)
        {
            string m_UserID = "";
            string m_Password = "";
            //Dim m_IncommingServer = System.Configuration.ConfigurationManager.AppSettings("IncommingServer").ToString()
            string m_OutgoingServer = "";
            int m_Port = 0;
            string m_From = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string _name = dt.Rows[i]["field_name"].ToString();
                string _val = dt.Rows[i]["field_value"].ToString();
                switch (_name)
                {
                    case "SMTP":
                        m_OutgoingServer = _val;
                        break;
                    case "EMAILID":
                        m_UserID = _val;
                        break;
                    case "PASS":
                        m_Password = _val;
                        break;
                    case "PORT":
                        m_Port = Convert.ToInt16(_val);
                        break;
                }

            }

            

            System.Net.Mail.MailMessage mMsg = new System.Net.Mail.MailMessage();
            //From requires an instance of the MailAddress type
            mMsg.From = new System.Net.Mail.MailAddress(m_UserID);

            //To is a collection of MailAddress types
            mMsg.To.Add("hirendrasingh_sisodiya@priyanet.com");


            mMsg.Subject = strEmailSubject;
            //mMsg.Attachments.Add(new System.Net.Mail.Attachment(RPT_FilePath));
            ///   dynamic data1 = new System.Net.Mail.Attachment(RPT_FilePath);
            // mMsg.Attachments.Add(data1);
            //if (lstQueries.SelectedIndex != -1)
            //{
            //    mMsg.Body = lstQueries.SelectedItem.Text.Replace("_", " ") + "<br/><br/>" + htmltable;
            //}
            //else
            //{
            //    mMsg.Body = txtSQL.Text + "<br/><br/>" + htmltable;
            //}
            mMsg.Body = strBody;
            mMsg.IsBodyHtml = true;
            //Create the SMTPClient object and specify the SMTP GMail server
            System.Net.Mail.SmtpClient SMTPServer = new System.Net.Mail.SmtpClient(m_OutgoingServer);
            SMTPServer.Port = m_Port;
            SMTPServer.Credentials = new System.Net.NetworkCredential(m_UserID, m_Password);
            //SMTPServer.EnableSsl = true;
            try
            {
                SMTPServer.Send(mMsg);
                MessageBox.Show("Email Sent.");
                return true;
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return false;
            }
        }

        private void btndetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Size = new Size(652, 451);
        }

       
        
    }
}
