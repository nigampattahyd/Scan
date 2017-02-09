using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Data;
using System.Windows.Forms;
using bhsScandll;
using System.Configuration;
namespace BHSScanlibrary
{
    public class BHSScan
    {

        public string ImageUpload(Bitmap img, string DocType, string RequestNumber, string ReleaseCode)
        {
            try
            {
                byte[] byteArr = imageToByteArray(img);
                string fileName = DocType + "_" + RequestNumber + "_" + DateTime.Now.ToString("MM-dd-yyyy hh~mm~ss") + ".png";
                bhsScandll.BHSDocUploadWebService.bhsDocsUploadService1SoapClient objUpload = new bhsScandll.BHSDocUploadWebService.bhsDocsUploadService1SoapClient();
                string str = objUpload.UploadFile(byteArr, DocType, fileName, RequestNumber, ReleaseCode);
                objUpload.Close();
                return str;
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("BHSScan.cs", "ImageUpload", ex.Message);
                return null;
            }
        }
        public string EndScanning(string RequestNumber, string ReleaseCode, string _type)
        {
            try
            {
                bhsScandll.BHSDocUploadWebService.bhsDocsUploadService1SoapClient objUpload = new bhsScandll.BHSDocUploadWebService.bhsDocsUploadService1SoapClient();
                string str = objUpload.FinalizeAllFiles(RequestNumber, ReleaseCode, _type);
                objUpload.Close();
                return str;
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("BHSScan.cs", "EndScanning", ex.Message);
                return null;
            }
        }


        private byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("BHSScan.cs", "imageToByteArray", ex.Message);
                return null;
            }
        }

        public string getReleaseCode(string reqno)
        {
            try
            {
                string releasecode = "";
                if (reqno == "")
                {
                    return "";
                }
                //string conStr = @"server=bhsonline.us; uid=sa; password=sa!2014; database=chca_bhs";
                string conStr = ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
                System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conStr);
            Retry:
                try
                {

                    con.Open();
                    System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand("select ReleaseCode from Requests where RequestNumber = " + reqno, con);
                    System.Data.SqlClient.SqlDataAdapter adap = new System.Data.SqlClient.SqlDataAdapter(comm);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                releasecode = ds.Tables[0].Rows[0][0].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errmsg = "There is a network related error connecting to the server" + Environment.NewLine + Environment.NewLine;
                    if (MessageBox.Show(errmsg, "BHS Scanner App", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) == DialogResult.Retry)
                    {
                        goto Retry;
                    }
                    else
                    {
                    }
                }
                finally
                {
                    con.Close();
                }
                return releasecode;
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("BHSScan.cs", "getReleaseCode", ex.Message);
                return null;
            }
        }

        public string getPaymentIdSalix(string reqno)
        {
            try
            {
                string releasecode = "";
                if (reqno == "")
                {
                    return "";
                }
                //string conStr = @"server=bhsonline.us; uid=sa; password=sa!2014; database=chca_bhs";
                string conStr = ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
                System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conStr);
            Retry:
                try
                {
                    con.Open();
                    System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand("SELECT PaymentID from Payment_Request where PaymentID=" + reqno, con);
                    System.Data.SqlClient.SqlDataAdapter adap = new System.Data.SqlClient.SqlDataAdapter(comm);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                releasecode = ds.Tables[0].Rows[0][0].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errmsg = "There is a network related error connecting to the server" + Environment.NewLine + Environment.NewLine;
                    if (MessageBox.Show(errmsg, "BHS Scanner App", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) == DialogResult.Retry)
                    {
                        goto Retry;
                    }
                    else
                    {
                    }
                }
                finally
                {
                    con.Close();
                }
                return releasecode;
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("BHSScan.cs", "getPaymentIdSalix", ex.Message);
                return null;
            }
        }

        public DataTable GetEmailOptions(string field_code)
        {
            try
            {
                //string conStr = @"server=bhsonline.us; uid=sa; password=sa!2014; database=chca_bhs";
                string conStr = ConfigurationManager.ConnectionStrings["Constr"].ConnectionString; 
                System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conStr);
                DataSet ds = new DataSet();
            Retry:
                try
                {
                    con.Open();

                    System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand("select * from  Options where field_code ='" + field_code + "'", con);
                    System.Data.SqlClient.SqlDataAdapter adap = new System.Data.SqlClient.SqlDataAdapter(comm);
                    comm.CommandTimeout = 3000;
                    adap.Fill(ds);
                    con.Close();
                }
                catch (Exception ex)
                {
                    string errmsg = "There is a network related error connecting to the server" + Environment.NewLine + Environment.NewLine;
                    if (MessageBox.Show(errmsg, "BHS Scanner App", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) == DialogResult.Retry)
                    {
                        goto Retry;
                    }
                    else
                    {
                    }
                }
                finally
                {
                    con.Close();
                }
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("BHSScan.cs", "GetEmailOptions", ex.Message);
                return null;
            }
        }
        public string GetClipdata()
        {
            try
            {
                string clipText = "";
                if (Clipboard.ContainsText())
                {
                    clipText = Clipboard.GetText();
                    if (clipText.StartsWith("BHSRN-"))
                    {
                        clipText = clipText.Remove(0, 6);
                    }
                    else if (clipText.StartsWith("Scan-"))
                    {
                        clipText = clipText.Remove(0, 5);
                    }
                    else
                    {
                        clipText = "";
                    }
                }
                return clipText;
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("BHSScan.cs", "GetClipdata", ex.Message);
                return null;
            }
        }

        public void chkSettingfile()
        {
            try
            {
                string tempFilename = Application.StartupPath + "\\Settings.ini";
                if (!System.IO.File.Exists(tempFilename))
                {
                    using (StreamWriter sw = File.CreateText(tempFilename))
                    {
                        sw.WriteLine("useadp=True");
                        sw.WriteLine("bw=True");
                        sw.WriteLine("autoborder=True");
                        sw.WriteLine("useui=False");
                        sw.WriteLine("useduplex=False");
                        sw.WriteLine("grabarea=False");
                        sw.WriteLine("autorotate=False");
                        sw.WriteLine("showprogress=True");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("BHSScan.cs", "chkSettingfile", ex.Message);
            }
        }
    }
}
