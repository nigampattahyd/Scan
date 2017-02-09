using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccessLayer;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace BussinessLogicLayer
{
   public class BLPortal
    {
        public Int64 RequestNumber;
        public string  AccessDocs;
        public Int64 RecordNumber;
        public string From;
        public string To;

        public string EmailActPassword;
        public string SMTPhost;
        public int SMTPPortNumber;

        public string EmailSubject;
        public HostTypes HostType;
        public int PasswordValidDays = 60;

        public string Template;
        private string LoginCode;
        private string password;
        private string requestorName;
        public string requestorid;

        public enum HostTypes
        {
            Hotmail,
            Gmail,
            Priyanet
        }

        public enum TaskStatus
        {
            Success,
            Password_Expire,
            Invalid_LoginCode,
            Invalid_RequestNumber,
            DataBase_Error,
            Wrong_DOB,
            Wrong_SSN,
            SSN_notfound,
            RequestorEmail_NotFound,
            Invalid_Password,
            Expire_Session,
            Record_NotFound,
            Unknown_Error,
            Mailbox_Error

        }
       
        private string _UserName;
        public string UserName
        {
            get
            {
                return "";
            }
        }

        private string _UserEmail;
        public string UserEmail
        {
            get
            {
                return "";
            }
        }
       
        #region GeneratePassword And SendEmail              
        public TaskStatus GeneratePassword()
        {
            int n;        
            try
            {
                string activationCode = Guid.NewGuid().ToString();
                Random _r = new Random();
                n = _r.Next(1000000, 9999999);
                password = n.ToString();             
                LoginCode = Guid.NewGuid().ToString();
                DataSet ds = GetPortalDetails(3,RequestNumber);//Flag =1        
                //string requestorid = "";
                string patientid = "";
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        requestorid = ds.Tables[0].Rows[0]["RequestorId"].ToString();
                        patientid = ds.Tables[0].Rows[0]["PatientId"].ToString();     
                        requestorName = ds.Tables[0].Rows[0]["RequestorName"].ToString();
                        if (To == "" || To == null)
                        {
                            if (ds.Tables[0].Rows[0]["EmailAddress"] != DBNull.Value)
                            {
                                if (ds.Tables[0].Rows[0]["EmailAddress"].ToString().Trim() != "")
                                {
                                    To = ds.Tables[0].Rows[0]["EmailAddress"].ToString();
                                }
                                else
                                {
                                    return TaskStatus.RequestorEmail_NotFound;
                                }
                            }
                            else
                            {
                                return TaskStatus.RequestorEmail_NotFound;
                            }
                        }
                    }
                    else
                    {
                        return TaskStatus.Record_NotFound;
                    }
                }
                else
                {
                    return TaskStatus.Invalid_RequestNumber;
                }

                RecordNumber = SaveUserLoginDetails(1, RequestNumber, password, LoginCode, Convert.ToInt64(requestorid), Convert.ToInt64(patientid), AccessDocs, To);//Flag=1
            }
            catch (Exception ex)
            {
                return TaskStatus.Unknown_Error;
            }

            return TaskStatus.Success;
        }
        public TaskStatus SendLoginEmail()
        {           
            string activationCode = Guid.NewGuid().ToString();
            string rootUrl = ConfigurationManager.AppSettings["websitepath"].ToString();
            try
            {
                using (MailMessage BodyMessage = new MailMessage(From, To))
                {
                    if (EmailSubject == "")
                    {
                              BodyMessage.Subject = "Account Activation";
                    }
                    else { BodyMessage.Subject = EmailSubject; }
              
                    BodyMessage.Priority = MailPriority.High;
                    BodyMessage.IsBodyHtml = true;

                    if (Template != "")
                    {
                        Template = Template.Replace("{name}", requestorName);
                        Template = Template.Replace("{password}", requestorName);
                    }
                    else
                    {


                        Template += "Hello " + requestorName;
                        Template += "<br /></br>Please <a href = '" + rootUrl + "Portal/securelogin.aspx?confirm=" + LoginCode + "'>click HERE</a> to access the medical records requested by " + requestorName + " and authorized by the patient.";
                        Template += "<br /><br /> To maintain protected health information security, you will be asked for verification information and a password.  Please enter this password when requested:  " + password;
                        Template += "<br /><br />Thanks";


                        //Template = "Hello " + requestorName;
                        //Template += "<br /><br />Please click the following link to activate your account";
                        //Template += "<br /><a href = '" +rootUrl+ "Portal/securelogin.aspx?confirm=" + LoginCode + "'>Click here to Login your account.</a>";
                        //Template += "<br /><br />Use this password: " + password;
                        //Template += "<br /><br />Thanks";
                    }

                    BodyMessage.Body = Template;
                    SmtpClient smtp = new SmtpClient();
                    if (SMTPhost == "")
                    {
                        if (HostType == HostTypes.Hotmail)
                        {
                            smtp.Host = "smtp.live.com";
                        }
                        else if (HostType == HostTypes.Gmail)
                        {
                            smtp.Host = "smtp.gmail.com";
                        }
                        else if (HostType == HostTypes.Priyanet)
                        {
                            smtp.Host = "smtp.1and1.com";
                        }
                    }
                    else
                    {
                        smtp.Host = SMTPhost;
                    }

                    if (SMTPPortNumber == 0)
                    {
                        if (HostType == HostTypes.Hotmail)
                        {
                            SMTPPortNumber = 587;
                        }
                        else if (HostType == HostTypes.Gmail)
                        {
                            SMTPPortNumber = 25;
                        }
                        else if (HostType == HostTypes.Priyanet)
                        {
                            SMTPPortNumber = 25;
                        }
                    }

                    smtp.Port = SMTPPortNumber;
                    smtp.EnableSsl = false;
                    NetworkCredential NetworkCred = new NetworkCredential(From, EmailActPassword);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.Timeout = 30000;
                    smtp.Send(BodyMessage);
                    Template = "";
                }
            }
            catch (SmtpException ex1)
            {
                DeleteUserLoginInfoDeatails(2, RecordNumber);//Flag =2 For Delete Record.
                return TaskStatus.Mailbox_Error;
            }
            catch (Exception ex)
            {
                DeleteUserLoginInfoDeatails(2, RecordNumber);//Flag =2 For Delete Record.
                ex.ToString();
                return TaskStatus.Unknown_Error;
            }
            return TaskStatus.Success;
        }
        #endregion

        #region Start Secure Login
      //  public DataSet StartSecureLogin(string _logincode)
        //{
            //DataSet dsLogin = new DataSet();
            //dsLogin = GetSecureLogin(1, _logincode);//Flag=1;
           // string result = "NAN";
            //string str = "SELECT Patients.DOB, Patients.SSN, UserLoginInfoDeatails.Password,  UserLoginInfoDeatails.PatientId  " +
            //              "FROM  UserLoginInfoDeatails INNER JOIN " +
            //             "Patients ON UserLoginInfoDeatails.PatientId = Patients.PatientId where Logincode = '" + _logincode + "'";
            //SqlConnection con = new SqlConnection(constr);
            //con.Open();
            //SqlCommand sqlcom = new SqlCommand(str, con);
            //SqlDataAdapter sqladap = new SqlDataAdapter(sqlcom);

            //sqladap.Fill(ds);
            //Int64 _recid = 0;

            //if (ds != null)
            //{
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        result = GetJson(ds.Tables[0]);
            //        //_recid = Convert.ToInt64(ds.Tables[0].Rows[0]["RecID"]);
            //        //string hexdec = _recid.ToString("X");
            //        //string dtstamp = DateTime.Now.ToString("MMddyyHHmmss");
            //        //string _token = _recid + "-" + dtstamp;
            //    }
            //    else
            //    {
            //        return result;
            //    }

            //}
            //else
            //{
            //    return result;
            //}
            //return dsLogin;
       // }

        //private string GetJson(DataTable dt)
        //{
        //    System.Web.Script.Serialization.JavaScriptSerializer Jserializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        //    List<Dictionary<string, object>> rowsList = new List<Dictionary<string, object>>();
        //    Dictionary<string, object> row = null;
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        row = new Dictionary<string, object>();
        //        foreach (DataColumn col in dt.Columns)
        //        {
        //            row.Add(col.ColumnName, dr[col]);
        //        }
        //        rowsList.Add(row);
        //    }
        //    return Jserializer.Serialize(rowsList);
        //}
        public TaskStatus ValidatePassword(string _logincode, string _password, ref Int64 _recordnumber)
        {
            string str = "select CreatedDate,RecID from UserLoginInfoDeatails where Logincode = '" + _logincode + "'  and Password ='" + _password + "'";
            var db = new Database();
            //db.AddLongParameter("@RequestId", requestNumer);
            //db.AddIntParameter("@flag", flag);
            DataSet ds = new DataSet();
            ds = db.ReturnDataSet(str, DataPortalQueryType.SqlQuery);
            //SqlConnection con = new SqlConnection(constr);
            //con.Open();
            //SqlCommand sqlcom = new SqlCommand(str, con);
            //SqlDataAdapter sqladap = new SqlDataAdapter(sqlcom);

            //sqladap.Fill(ds);
            DateTime dt;
            string result = "NAN";
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dt = Convert.ToDateTime(ds.Tables[0].Rows[0][0]);
                    _recordnumber = Convert.ToInt64(ds.Tables[0].Rows[0][1]);
                    if (dt.AddDays(60) < DateTime.Now)
                    {
                        return TaskStatus.Password_Expire;
                    }
                    else
                    {
                        return TaskStatus.Success;
                    }


                    //_recid = Convert.ToInt64(ds.Tables[0].Rows[0]["RecID"]);
                    //string hexdec = _recid.ToString("X");
                    //string dtstamp = DateTime.Now.ToString("MMddyyHHmmss");
                    //string _token = _recid + "-" + dtstamp;
                }
                else
                {
                    return TaskStatus.Invalid_Password;
                }

            }
            else
            {
                return TaskStatus.Invalid_Password;
            }


        }
        public TaskStatus ValidateDOB(string _logincode, Int64 RecordID, DateTime _DOB)
        {
            DataSet ds = new DataSet();
            string str = "SELECT     Patients.DOB, Patients.PatientId " +
    "FROM         Patients INNER JOIN " +
                          "UserLoginInfoDeatails ON Patients.PatientId = UserLoginInfoDeatails.PatientId where UserLoginInfoDeatails.RecId =" + RecordID;


            //SqlConnection con = new SqlConnection(constr);
            //con.Open();
            //SqlCommand sqlcom = new SqlCommand(str, con);
            //SqlDataAdapter sqladap = new SqlDataAdapter(sqlcom);
            var db = new Database();
            //db.AddLongParameter("@RequestId", requestNumer);
            //db.AddIntParameter("@flag", flag);
         
            ds = db.ReturnDataSet(str, DataPortalQueryType.SqlQuery);
            //sqladap.Fill(ds);
            DateTime dt;
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["DOB"] != DBNull.Value)
                    {
                        dt = Convert.ToDateTime(ds.Tables[0].Rows[0]["DOB"]);
                        if ((dt.Month == _DOB.Month) && (dt.Day == _DOB.Day) && (dt.Year == _DOB.Year))
                        {
                            return TaskStatus.Success;
                        }
                        else
                        {
                            return TaskStatus.Wrong_DOB;
                        }
                    }
                    else
                    {
                        return TaskStatus.Wrong_DOB;
                    }

                }
                else
                {
                    return TaskStatus.Record_NotFound;
                }

            }
            else
            {
                return TaskStatus.Record_NotFound;
            }


        }
        public TaskStatus ValidateSSN(string _logincode, Int64 RecordID, string SSN)
        {
            DataSet ds = new DataSet();
            string str = "SELECT     Patients.SSN, Patients.PatientId " +
    "FROM         Patients INNER JOIN " +
                          "UserLoginInfoDeatails ON Patients.PatientId = UserLoginInfoDeatails.PatientId where UserLoginInfoDeatails.RecId =" + RecordID;


            //SqlConnection con = new SqlConnection(constr);
            //con.Open();
            //SqlCommand sqlcom = new SqlCommand(str, con);
            //SqlDataAdapter sqladap = new SqlDataAdapter(sqlcom);
            var db = new Database();
            //db.AddLongParameter("@RequestId", requestNumer);
            //db.AddIntParameter("@flag", flag);

            ds = db.ReturnDataSet(str, DataPortalQueryType.SqlQuery);
            //sqladap.Fill(ds);
           
            string dbSSN = "";
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["SSN"] == DBNull.Value)
                    {
                        return TaskStatus.SSN_notfound;
                    }
                    dbSSN = ds.Tables[0].Rows[0]["SSN"].ToString();
                    if (dbSSN.Trim()==SSN.Trim())
                    {
                        return TaskStatus.Success;
                    }
                    else
                    {
                        return TaskStatus.Wrong_SSN;
                    }


                }
                else
                {
                    return TaskStatus.Record_NotFound;
                }

            }
            else
            {
                return TaskStatus.Record_NotFound;
            }


        }
        #endregion
       

        private string CreateToken()
        {
            return "fdfd";
        }

       
     

//        string constr = "server=psplserver5\\sql2008r2;database=DrivingforceBHS;User ID=sa;Password=sa!2008;Persist Security Info=True;"; // ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
//        public DataSet GetDetailsFormRequest(Int64 requestNumer)
//        {
//            DataSet ds = GetDataSet("SELECT Requests.PatientId, Requests.RequestorId, Requestors.EmailAddress,RequestorName FROM Requestors INNER JOIN " +
//"Requests ON Requestors.RequestorId = Requests.RequestorId where RequestNumber = " + requestNumer);
//            return ds;
//        }

        public DataSet GetDataSet(string Query)
        {
            DataSet ds = new DataSet();
            //SqlConnection con = new SqlConnection(constr);
            //con.Open();
            //SqlCommand sqlcom = new SqlCommand(Query, con);
            //SqlDataAdapter sqladap = new SqlDataAdapter(sqlcom);        
            //sqladap.Fill(ds);
            //if (ds != null)
            //{
            //    return ds;
            //}
            return null;
        }
        public void flush()
        {
            Template = "";
            RequestNumber = 0;
            From = "";
            To = "";
            EmailActPassword = "";
            SMTPPortNumber = 0;
            PasswordValidDays = 0;
            LoginCode = "";
            password = "";
        }

        public string GetMessage(TaskStatus _status)
        {
            string message = "";
            if (_status == BLPortal.TaskStatus.DataBase_Error)
            {
                message = "";
            }
            else if (_status == BLPortal.TaskStatus.Record_NotFound)
            {
                message = "Requested record is not found";
            }
            else if (_status == BLPortal.TaskStatus.RequestorEmail_NotFound)
            {
                message = "Sorry! Requester Email is not found";
            }
            else if (_status == BLPortal.TaskStatus.Invalid_RequestNumber)
            {
                message = "Sorry! Invalid request number";
            }
            else if (_status == BLPortal.TaskStatus.Unknown_Error)
            {
                message = "";
            }
            else if (_status == BLPortal.TaskStatus.Invalid_Password)
            {
                message = "Invalid password!";
            }
            else if (_status == BLPortal.TaskStatus.Wrong_DOB)
            {
                message = "You have entered wrong date of birth!";
            }
            else if (_status == BLPortal.TaskStatus.Mailbox_Error)
            {
                message = "Invalid SMTP configuration:!";
            }
            else if (_status == BLPortal.TaskStatus.Wrong_SSN)
            {
                message = "You have entered wrong ssn!";
            }
            else if (_status == BLPortal.TaskStatus.SSN_notfound)
            {
                message = "The entered ssn is not found in our records.";
            }
            return message;
        }


        #region Common Methods
        public DataSet GetPortalDetails(int flag,Int64 requestNumer)      
        {
            var db = new Database();
            db.AddLongParameter("@RequestId", requestNumer);
            db.AddIntParameter("@flag", flag);
            return db.ReturnDataSet("sp_GetPortalDetails", DataPortalQueryType.StoredProcedure);
        }
        public DataSet GetSecureLogin(int flag, string logincode)
        {
            var db = new Database();
            db.AddStringParameter("@Logincode", logincode);
            db.AddIntParameter("@flag", flag);
            return db.ReturnDataSet("sp_GetPortalDetails", DataPortalQueryType.StoredProcedure);
        }
        public DataSet DeleteUserLoginInfoDeatails(int flag,Int64 RecordNumer)
        {
            var db = new Database();
            db.AddLongParameter("@RecordNumber", RecordNumer);
            db.AddIntParameter("@flag", flag);
            db.AddLongParameter("RecID", 0, true);
            return db.ReturnDataSet("sp_SavePortalInfo", DataPortalQueryType.StoredProcedure);
        } 
        public Int64 SaveUserLoginDetails(int Flag, Int64 RequestId, string password, string LoginCode,Int64 reqid,Int64 patid,string docs,string email)
        {
            Int64 RecID = 0;
            var database = new Database();
            database.AddLongParameter("@RequestNumber", RequestId);
            database.AddStringParameter("@password", password);
            database.AddStringParameter("@LoginCode", LoginCode);
            database.AddLongParameter("@RequestorId", reqid);
            database.AddLongParameter("@PatientId", patid);
            database.AddStringParameter("@AccessDocs", docs);
            database.AddStringParameter("@EmailAdds", email);
            database.AddIntParameter("@flag", Flag);
            database.AddLongParameter("RecID", 0, true);
            try
            {
                database.SubmitData("sp_SavePortalInfo", DataPortalQueryType.StoredProcedure);
                RecID = Convert.ToInt64(database.GetParameterValue("RecID"));
            }
            catch (Exception ex)
            {
                RecID = -1;
                ex.ToString();
            }
            return RecID;
        }

        public Int64 GetRequestNumber(string recno)
        {
            string str = "select Requests.RequestNumber  from Requests inner join UserLoginInfoDeatails on Requests.RequestNumber = UserLoginInfoDeatails.RequestNumber where Logincode = '" + recno + "'";
            var db = new Database();
            DataSet ds;
            Int64 result = 0;
            ds = db.ReturnDataSet(str, DataPortalQueryType.SqlQuery);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result =Convert.ToInt64(ds.Tables[0].Rows[0][0].ToString()); 
                    }
                }
            }
            return result;
        }
       #endregion

        
    }
}
