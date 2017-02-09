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
    public class SalixScan
    {

       public string ImageUpload(Bitmap img, string DocType, string RequestNumber, string ReleaseCode)
       {
           try
           {
               byte[] byteArr = imageToByteArray(img);
               string fileName = DocType + "_" + RequestNumber + "_" + DateTime.Now.ToString("MM-dd-yyyy hh~mm~ss") + ".png";
               bhsScandll.BHSDocUploadWebService2.bhsDocsUploadService1SoapClient objUpload = new bhsScandll.BHSDocUploadWebService2.bhsDocsUploadService1SoapClient();
               string str = objUpload.UploadFile(byteArr, DocType, fileName, RequestNumber, ReleaseCode);
               objUpload.Close();
               return str;
           }
           catch (Exception ex)
           {
               ErrorLog objLog = new ErrorLog();
               objLog.logerror("SalixScan.cs", "ImageUpload", ex.Message);
               return null;
           }
       }

        public string SalixImageUpload(Bitmap img, string DocType, string RequestType, string UserId, string Id,int count)
        {
            try
            {
                byte[] byteArr = imageToByteArray(img);
                string fileName = DocType + "_" + "Scan" + "_" + DateTime.Now.ToString("MM-dd-yyyy hh~mm~ss") + ".png";
                bhsScandll.BHSDocUploadWebService2.bhsDocsUploadService1SoapClient objUpload = new bhsScandll.BHSDocUploadWebService2.bhsDocsUploadService1SoapClient();
                string str = objUpload.SalixUploadFile(byteArr, DocType, RequestType, UserId, Id, count);
                objUpload.Close();
                return str;
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("SalixScan.cs", "SalixImageUpload", ex.Message);
                return null;
            }
        }

        public string EndScanning(string RequestNumber, string ReleaseCode, string _type)
        {
            try
            {
                bhsScandll.BHSDocUploadWebService2.bhsDocsUploadService1SoapClient objUpload = new bhsScandll.BHSDocUploadWebService2.bhsDocsUploadService1SoapClient();
                string str = objUpload.FinalizeAllFiles(RequestNumber, ReleaseCode, _type);
                objUpload.Close();
                return str;
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("SalixScan.cs", "EndScanning", ex.Message);
                return null;
            }
        }

        public string SalixEndScanning(string DocType, string RequestType, string User, string Id)
        {
            try
            {
                bhsScandll.BHSDocUploadWebService2.bhsDocsUploadService1SoapClient objUpload = new bhsScandll.BHSDocUploadWebService2.bhsDocsUploadService1SoapClient();
                string str = objUpload.SalixFinalizeAllFiles(DocType, RequestType, User, Id);
                objUpload.Close();
                return str;
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("SalixScan.cs", "SalixEndScanning", ex.Message);
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
                objLog.logerror("SalixScan.cs", "imageToByteArray", ex.Message);
                return null;
            }
        }

        public string getPaymentIdSalix(string reqno)
        {
            try
            {
                string releasecode = "";
                string[] words = new string[0];
                if (reqno == "")
                {
                    return "";
                }
                else if (reqno.Contains("-"))
                {
                    words = reqno.Split('-');
                    reqno = words[1];
                }

                //string conStr = @"server=bhsonline.us; uid=sa; password=sa!2014; database=chca_bhs";
                string conStr = ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
                System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conStr);
            Retry:
                try
                {
                    con.Open();
                    var Stmt = string.Empty;
                    if (words[0] == "TR")
                    {
                        Stmt = "SELECT TransportId from Transport_Request where TransportId=";
                    }
                    else if (words[0] == "PR")
                    {
                        Stmt = "SELECT PaymentID from Payment_Request where PaymentID=";
                    }
                    else if (words[0] == "PO")
                    {
                        Stmt = "SELECT PurchaseOrderId from Purchaseorder where PurchaseOrderId=";
                    }
                    else if (words[0] == "FR")
                    {
                        Stmt = "SELECT EventId from event where EventId=";
                    }
                    System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(Stmt + reqno, con);
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
                objLog.logerror("SalixScan.cs", "getPaymentIdSalix", ex.Message);
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
                objLog.logerror("SalixScan.cs", "GetEmailOptions", ex.Message);
                return null;
            }
        }

        public string SalixGetClipdata()
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
                objLog.logerror("SalixScan.cs", "SalixGetClipdata", ex.Message);
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
                        sw.WriteLine("autorotate=Falsee");
                        sw.WriteLine("showprogress=True");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog objLog = new ErrorLog();
                objLog.logerror("SalixScan.cs", "chkSettingfile", ex.Message);
            }
        }
    }
}
