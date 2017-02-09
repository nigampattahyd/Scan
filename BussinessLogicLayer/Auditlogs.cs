using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using DataAccessLayer;
using System.Data;
using System.Text.RegularExpressions;

namespace BussinessLogicLayer
{
    public class Auditlogs
    {
        public enum ActionLog
        {
            Request_Create,
            Request_Update,
            Request_LoggingOnly,
            Request_UpdateLoggingOnly,
            ImportRequestLetter,
            ScanRequestLetter,
            ImportMedicalRecords,
            ScanMedicalRecords,
            ViewInvoice,
            DocSaveToQueue,
            InvSaveToQueue,
            Request_CompleteAndQA,
            SendToSpecialist,
            Request_Delete,
            Request_EditFromViewQueue,
            Request_EditFromSupScr,
            Request_GoTofulfillment,
            Request_HoldForSupervisor,
            Request_SendToRequester,
            Request_Fax,
            Request_Print,
            Request_Portal,
            OpenProcessorScreen,
            OpenSupervisorScreen,
            OpenReqDocScreen,
            User_Login,
            User_Logout,
            Facility_Create,
            Facility_Update,
            Facility_Active,
            Facility_Inactive,
            User_Create,
            User_Update,
            User_Active,
            User_Inactive,
            AuthorizeScanApplication,
            RemoveAssociation,
            AssociatewithThisUser,
            User_Password,
            MR_Reviewed,
        };
        public enum ActionResult
        {
            Success,
            Failure
        }
        // RequestNumber   
        // UpdatedBy
        // UpdatedOn
        // Action
        // Result
      public int UserLog(string userid, ActionLog _action, ActionResult _result)
        {
            int isSuccess =1;
            DateTime dt = DateTime.UtcNow;
            bool result = false;
            if (_result == ActionResult.Success)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            string strQuery = "Insert into Auditlogs(UpdatedBy,UpdatedOn,Action,UserIP,Result,FKLogId) " +
                            "values(" + Convert.ToInt64(userid) + ",'" + dt + "' ,'" + _action.ToString() + "','" + GetUser_IP() + "','" + _result.ToString() + "'," + GetActionText(_action) + ")";
            var database = new Database();
            database.SubmitData(strQuery, DataPortalQueryType.SqlQuery);
            return isSuccess;
        }

      public int RequestLog(string RequestNumber, string userid, ActionLog _action, ActionResult _result)
        {
            int isSuccess = 1;
            DateTime dt = DateTime.UtcNow;
            bool result = false;
            if (_result == ActionResult.Success)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            string strQuery = "Insert into Auditlogs(RequestNumber,UpdatedBy,UpdatedOn,Action,UserIP,Result,FKLogId) " +
                "values('" + RequestNumber + "'," + userid + ",'" + dt + "' ,'" + _action.ToString() + "','" + GetUser_IP() + "','" + _result.ToString() + "'," + GetActionText(_action) + ")";
            var database = new Database();
            database.SubmitData(strQuery, DataPortalQueryType.SqlQuery);
            return isSuccess;
        }
      public int MGAndUserMgt(string userid, ActionLog _action, ActionResult _result, string strname)//Medical facility and user management
      {
          int isSuccess = 1;
          DateTime dt = DateTime.UtcNow;
          bool result = false;
          string Name = strname.Replace("'", "''");

          if (_result == ActionResult.Success)
          {
              result = true;
          }
          else
          {
              result = false;
          }
          string strQuery = "Insert into Auditlogs(UpdatedBy,UpdatedOn,Action,UserIP,Result,FKLogId,Remarks) " +
                          "values(" + Convert.ToInt64(userid) + ",'" + dt + "' ,'" + _action.ToString() + "','" + GetUser_IP() + "','" + _result.ToString() + "'," + GetActionText(_action) + ",'" + Name + "')";
          var database = new Database();
          database.SubmitData(strQuery, DataPortalQueryType.SqlQuery);
          return isSuccess;
      }
      protected string GetUser_IP()
        {
            string VisitorsIPAddr = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress;
            }
            //VisitorsIPAddr = "84.218.102.100";
            return VisitorsIPAddr;
        }
      private Int64 GetActionText(ActionLog _action)
      {
          Int64 addstr = 0;          
          switch (_action)
          {
              case ActionLog.User_Login:
                  return addstr=1; // + "- user login";
              case ActionLog.User_Logout:
                  return addstr = 2; // + "- user logout";
              case ActionLog.Request_Create:
                  return addstr=3;// + "- Request created [doc entry]";
              case ActionLog.Request_Update:
                  return addstr=4;// + "- Request updated by doc entry";
              case ActionLog.Request_LoggingOnly:
                  return addstr = 5;// + "- Request created [logging only]";
              case ActionLog.Request_UpdateLoggingOnly:
                  return addstr=6;// + "- Request update by logging only";
              case ActionLog.ImportRequestLetter:
                  return addstr=7;// + "- Import request letter";
              case ActionLog.ScanRequestLetter:
                  return addstr=8;// + "- Scan request letter";
              case ActionLog.ImportMedicalRecords:
                  return addstr=9;// + "- Import medical records";
              case ActionLog.ScanMedicalRecords:
                  return addstr=10;// + "- Scan medical records";
              case ActionLog.ViewInvoice:
                  return addstr=11;// + "- View Invoice";
              case ActionLog.DocSaveToQueue:
                  return addstr=12;// + "- Save To Queue from doc entry";
              case ActionLog.InvSaveToQueue:
                  return addstr = 13;// + "- Save To Queue from invoice";
              case ActionLog.Request_CompleteAndQA:
                  return addstr=14;// + "- Request complete and sent to QA";
              case ActionLog.SendToSpecialist:
                  return addstr=15;// + "- Request send to specialist";
              case ActionLog.Request_HoldForSupervisor:
                  return addstr = 16;// + "- Request hold for supervisor";
              case ActionLog.Request_SendToRequester:
                  return addstr=17;// + "- Request send to requester";
              case ActionLog.Request_Fax:
                  return addstr=18;// + "- fax sent";
              case ActionLog.Request_Print:
                  return addstr=19;// + "- print Dociments";
              case ActionLog.Request_Portal:
                  return addstr=20;// + "- email sent by portal";
              case ActionLog.OpenProcessorScreen:
                  return addstr=21;// + "- Open processor screen";
              case ActionLog.OpenSupervisorScreen:
                  return addstr=22;// + "- Open supervisor screen";
              case ActionLog.OpenReqDocScreen:
                  return addstr=23;// + "- Open request document screen";
              case ActionLog.Request_EditFromViewQueue:
                  return addstr=24;// + "- click on edit from view queue";
              case ActionLog.Request_EditFromSupScr:
                  return addstr=25;// + "- click on edit request from supervisor screen";
              case ActionLog.Request_GoTofulfillment:
                  return addstr=26;// + "- click on go to fulfillment from supervisor screen";
              case ActionLog.Request_Delete:
                  return addstr=27;// + "- Request delete";
              case ActionLog.Facility_Create:
                  return addstr = 28;//Successful created Medical facility
              case ActionLog.Facility_Update:
                  return addstr = 29;// Successful updated Medical facility
              case ActionLog.Facility_Active:
                  return addstr = 30;// Successful active Medical facility
              case ActionLog.Facility_Inactive:
                  return addstr = 31;// Successful inactive Medical facility
              case ActionLog.User_Create:
                  return addstr = 32;// Successful created user
              case ActionLog.User_Update:
                  return addstr = 33;//Successful updated user
              case ActionLog.User_Active:
                  return addstr = 34;// Successful active user
              case ActionLog.User_Inactive:
                  return addstr = 35;// Successful inactive user
              case ActionLog.AuthorizeScanApplication:
                  return addstr = 36;// Authorize scan application
              case ActionLog.AssociatewithThisUser:
                  return addstr = 37;// Associate with This User
              case ActionLog.RemoveAssociation:
                  return addstr = 38;// Remove Association with This User
              case ActionLog.MR_Reviewed:
                  return addstr = 39;// 100% medical records have been reviewed
              default:
                  return addstr;
          }
      }
      public DataTable GetUserLogsList(int flag,string searhName)
        {
            //string strQuery = "SELECT al.RequestNumber,ul.FullName as UserName,UserIP as UserIP,UpdatedOn as Time,ActionText as Action FROM Auditlogs al INNER JOIN UserLoginInfo ul ON al.UpdatedBy = ul.RecID order by al.auditlogid desc";  
            var db = new Database();
            db.AddIntParameter("@flag", flag);
            db.AddStringParameter("@SName", searhName);
            return db.ReturnDataTable("usp_UserLogsList", DataPortalQueryType.StoredProcedure);
        }

      #region portal Activity Log
      public Int64 SavePortalActLogs(int Flag, Int64 RequestNum, Int64 Fk_RecId, string EmailId, string FaxNumber)
      {
          Int64 PortalLogId = 0;
          var database = new Database();
          database.AddLongParameter("@RequestNumber", RequestNum);
          database.AddLongParameter("@Fk_RecId", Fk_RecId);
          database.AddStringParameter("@FaxNumber", FaxNumber);
          database.AddStringParameter("@EmailId", EmailId);
          database.AddIntParameter("@flag", Flag);
          database.AddLongParameter("PortalLogId", 0, true);
          try
          {
              database.SubmitData("sp_SavePortalLog", DataPortalQueryType.StoredProcedure);
              PortalLogId = Convert.ToInt64(database.GetParameterValue("PortalLogId"));
          }
          catch (Exception ex)
          {
              PortalLogId = -1;
              ex.ToString();
          }
          return PortalLogId;
      }
      public Int64 UpdateDOBForActLogs(Int64 RequestNum, Int64 Fk_RecId, DateTime DOB)
      {
          Int64 PortalLogId = 0;
          string sqlQuery = "update PortalActivityLog set DOB='" + DOB + "' where Fk_RecId=" + Fk_RecId + " and ActionStatus=0";
          var database = new Database();
          //database.AddLongParameter("@RequestNumber", RequestNum);
          //database.AddLongParameter("@Fk_RecId", Fk_RecId);
          //database.AddDateTimeParameter("@DOB", DOB);
          //database.AddStringParameter("@SSN", ssn);
          //database.AddIntParameter("@flag", Flag);
          //database.AddLongParameter("PortalLogId", 0, true);
          try
          {
              database.SubmitData(sqlQuery, DataPortalQueryType.SqlQuery);
              PortalLogId = Fk_RecId;
          }
          catch (Exception ex)
          {
              PortalLogId = -1;
              ex.ToString();
          }
          return PortalLogId;
      }
      public Int64 UpdateSSNForActLogs(Int64 RequestNum, Int64 Fk_RecId, string ssn)
      {
          Int64 PortalLogId = 0;
          string sqlQuery = "update PortalActivityLog set SSN='" + ssn + "' where Fk_RecId=" + Fk_RecId + " and ActionStatus=0";
          var database = new Database();
          try
          {
              database.SubmitData(sqlQuery, DataPortalQueryType.SqlQuery);
              PortalLogId = Fk_RecId;
          }
          catch (Exception ex)
          {
              PortalLogId = -1;
              ex.ToString();
          }
          return PortalLogId;
      }
      public Int64 UpdateViewedActLogs(Int64 portallogid, Int64 Fk_RecId, string action, string userIp, string DocRL = "", string DocMD = "")
      {
          userIp = GetUser_IP();
          Int64 PortalLogId = 0;
          string sqlQuery = string.Empty;
          if (DocRL != "" && DocMD != "")
              sqlQuery = "update PortalActivityLog set ActionStatus=1, UserIP='" + userIp + "',Action='" + action + "', PrintedRL='" + DocRL + "',PrintedMD='" + DocMD + "'" +
              "where PortalLogId=" + portallogid + " and Fk_RecId=" + Fk_RecId + " ";
          else if (DocRL != "")
              sqlQuery = "update PortalActivityLog set ActionStatus=1, UserIP='" + userIp + "',Action='" + action + "', PrintedRL='" + DocRL + "'" +
              "where PortalLogId=" + portallogid + " and Fk_RecId=" + Fk_RecId + " ";
          else if (DocMD != "")
              sqlQuery = "update PortalActivityLog set ActionStatus=1, UserIP='" + userIp + "',Action='" + action + "',PrintedMD='" + DocMD + "'" +
              "where PortalLogId=" + portallogid + " and Fk_RecId=" + Fk_RecId + " ";
          else
              sqlQuery = "update PortalActivityLog set ActionStatus=1, Action='" + action + "' where PortalLogId=" + portallogid + " and Fk_RecId=" + Fk_RecId + " ";

          var database = new Database();
          try
          {
              database.SubmitData(sqlQuery, DataPortalQueryType.SqlQuery);
              PortalLogId = Fk_RecId;
          }
          catch (Exception ex)
          {
              PortalLogId = -1;
              ex.ToString();
          }
          return PortalLogId;
      }



      #endregion
    }

    public class Errorlogs
    {
        public int ErrorLog(object userid, string _module, string _targetmethod, string _description)
        {
            int isSuccess = 1;
            string messageDesc = _description.Replace("'", "''");
            Int64 _userid = 0;
            if (userid != null)
            {
                _userid = Convert.ToInt64(userid);
            }
            try
            {              
                DateTime dt = DateTime.UtcNow;
                bool result = false;
                string strQuery = "Insert into Errorlogs(UpdatedBy,UpdatedOn,module,targetmethod,message) " +
                                "values(" + _userid + ",'" + dt + "' ,'" + _module + "','" + _targetmethod + "','" + messageDesc + "')";
                var database = new Database();
                database.SubmitData(strQuery, DataPortalQueryType.SqlQuery);               
            }
            catch
            {
            }
            return isSuccess;
        }
    }
    
}

//private string GetActionText(ActionLog _action, ActionResult _result)
//  {
//      string addstr = "";

//      if (_result == ActionResult.Success)
//      {
//          addstr = "Successful";
//      }
//      else
//      {
//          addstr = "Failure";
//      }

//      switch (_action)
//      {
//          case ActionLog.User_Login:
//              return addstr + "- user login";
//          case ActionLog.User_Logout:
//              return addstr + "- user logout";
//          case ActionLog.Request_Create:
//              return addstr + "- Request created [doc entry]";
//          case ActionLog.Request_Update:
//              return addstr + "- Request updated by doc entry";
//          case ActionLog.Request_LoggingOnly:
//              return addstr + "- Request created [logging only]";
//          case ActionLog.Request_UpdateLoggingOnly:
//              return addstr + "- Request update by logging only";
//          case ActionLog.ImportRequestLetter:
//              return addstr + "- Import request letter";
//          case ActionLog.ScanRequestLetter:
//              return addstr + "- Scan request letter";
//          case ActionLog.ImportMedicalRecords:
//              return addstr + "- Import medical records";
//          case ActionLog.ScanMedicalRecords:
//              return addstr + "- Scan medical records";
//          case ActionLog.ViewInvoice:
//              return addstr + "- View Invoice";
//          case ActionLog.DocSaveToQueue:
//              return addstr + "- Save To Queue from doc entry";
//          case ActionLog.InvSaveToQueue:
//              return addstr + "- Save To Queue from invoice";
//          case ActionLog.Request_CompleteAndQA:
//              return addstr + "- Request complete and sent to QA";
//          case ActionLog.SendToSpecialist:
//              return addstr + "- Request send to specialist";
//          case ActionLog.Request_HoldForSupervisor:
//              return addstr + "- Request hold for supervisor";
//          case ActionLog.Request_SendToRequester:
//              return addstr + "- Request send to requester";
//          case ActionLog.Request_Fax:
//              return addstr + "- fax sent";
//          case ActionLog.Request_Print:
//              return addstr + "- print Dociments";
//          case ActionLog.Request_Portal:
//              return addstr + "- email sent by portal";
//          case ActionLog.OpenProcessorScreen:
//              return addstr + "- Open processor screen";
//          case ActionLog.OpenSupervisorScreen:
//              return addstr + "- Open supervisor screen";
//          case ActionLog.OpenReqDocScreen:
//              return addstr + "- Open request document screen";
//          case ActionLog.Request_EditFromViewQueue:
//              return addstr + "- click on edit from view queue";
//          case ActionLog.Request_EditFromSupScr:
//              return addstr + "- click on edit request from supervisor screen";
//          case ActionLog.Request_GoTofulfillment:
//              return addstr + "- click on go to fulfillment from supervisor screen";                  

//          case ActionLog.Request_Delete:
//              return addstr + "- Request delete";

//          case ActionLog.Facility_Create:
//              return addstr + "- Medical facility create";
//          case ActionLog.Facility_Active:
//              return addstr + "- Medical facility active";
//          case ActionLog.Facility_Inactive:
//              return addstr + "- Medical facility inactive";

//          default:
//              return "";
//      }
//  }
//private string GetActionTextForMF(ActionLog _action, ActionResult _result,string msg)
//{
//    string addstr = "";

//    if (_result == ActionResult.Success)
//    {
//        addstr = "Successful";
//    }
//    else
//    {
//        addstr = "Failure";
//    }

//    switch (_action)
//    {              
//        case ActionLog.Facility_Create:
//            return addstr + "- Medical facility " + msg + " create";
//        case ActionLog.Facility_Update:
//            return addstr + "- Medical facility " + msg + " update";
//        case ActionLog.Facility_Active:
//            return addstr + "- Medical facility " + msg + " active";
//        case ActionLog.Facility_Inactive:
//            return addstr + "- Medical facility " + msg + " inactive";
//        case ActionLog.User_Create:
//            return addstr + "- User " + msg + " create";
//        case ActionLog.User_Update:
//            return addstr + "- User " + msg + " update";
//        case ActionLog.User_Active:
//            return addstr + "- User " + msg + " active";
//        case ActionLog.User_Inactive:
//            return addstr + "- User " + msg + " inactive";
//        case ActionLog.User_Password:
//            return addstr + "- User " + msg + " password change";
//        default:
//            return "";
//    }
//}