using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccessLayer;

namespace BussinessLogicLayer
{
   public class BLProcessior
    {
       #region For Processor
       public DataTable PatientAndRequestForProcessor(Int64 reqid,int flag)
       {
           var db = new Database();
           db.AddLongParameter("@RequestId", reqid);
           db.AddIntParameter("@flag", flag);
           return db.ReturnDataTable("GetPatientAndRequestInfo", DataPortalQueryType.StoredProcedure);
       }
       public static DataSet ProcessorItemList(Int64 RequestNumber)
       {
           var db = new Database();
           db.AddLongParameter("@RequestNumber", RequestNumber);
           return db.ReturnDataSet("sp_ProcessorItemList", DataPortalQueryType.StoredProcedure);
       }

       public static Int64 SaveProcessorInfo(Int64 RequestId, string items, string itemComnt, int Flag, int OrderNo, int UserType)
       {
           Int64 ReqStatus = -1;
           var database = new Database();
           database.AddLongParameter("@RequestNumber", RequestId);         
           database.AddStringParameter("@Items", items);
           database.AddStringParameter("@ItemComments", itemComnt);
           database.AddIntParameter("@flag", Flag);
           database.AddIntParameter("@OrderNo", OrderNo);
           database.AddIntParameter("@UserType", UserType);
           database.AddLongParameter("ReqStatus", 0, true);
           try
           {
               database.SubmitData("sp_SaveProcessorQA", DataPortalQueryType.StoredProcedure);
               ReqStatus = Convert.ToInt64(database.GetParameterValue("ReqStatus"));
           }
           catch (Exception ex)
           {
               ReqStatus = -1;
               ex.ToString();
           }
           return ReqStatus;
       }
      
       public static Int64 SendFaxToRequester(Int64 RequestId,string CoverPage,string faxnumber,string Subject,string filepath,string RequesterName, string Comments,string XMLData, int Flag)
       {
           Int64 faxid = 0;
           var database = new Database();
           database.AddLongParameter("@RequestId", RequestId);
           database.AddStringParameter("@CoverPage", CoverPage);
           database.AddStringParameter("@faxnumber", faxnumber);
           database.AddStringParameter("@Subject", Subject);
           database.AddStringParameter("@filepath", filepath);
           database.AddStringParameter("@RequesterName", RequesterName);
           database.AddStringParameter("@Comments", Comments);
           database.AddStringParameter("@XMLData", XMLData);
           database.AddIntParameter("@Flag", Flag);
           database.AddLongParameter("faxid", 0, true);
           try
           {
               database.SubmitData("sp_SendFaxToRequester", DataPortalQueryType.StoredProcedure);
               faxid = Convert.ToInt64(database.GetParameterValue("faxid"));
           }
           catch (Exception ex)
           {
               faxid = 0;
               ex.ToString();
           }
           return faxid;          
       }
       public static Int64 SaveQAInfoByProcessor(Int64 RequestId, string Comments,string XMLData,string RequestStatus,Int64 LogedUserId,string strnotes,int Flag)
       {
           Int64 saveStatus = 0;
           var database = new Database();
           database.AddLongParameter("@RequestId", RequestId);          
           database.AddStringParameter("@Comments", Comments);
           database.AddStringParameter("@XMLData", XMLData);
           database.AddStringParameter("@RequestStatus", RequestStatus);
           database.AddLongParameter("@AddedBy", LogedUserId);
           database.AddStringParameter("@Notes", strnotes);
           database.AddIntParameter("@Flag", Flag);
           database.AddLongParameter("faxid", 0, true);
           try
           {
               database.SubmitData("sp_SendFaxToRequester", DataPortalQueryType.StoredProcedure);
               saveStatus = Convert.ToInt64(database.GetParameterValue("faxid"));
           }
           catch (Exception ex)
           {
               saveStatus = 0;
               ex.ToString();
           }
           return saveStatus;
       }
       public Int64 SaveNewInfoByProcessor(Int64 RequestNumber, string lName, string fName, DateTime dob, string MRN, string SSN, string ItemComment, Int64 LogedUserId, string strnotes)
       {
           Int64 saveStatus = 0;
           var database = new Database();
           database.AddLongParameter("@RequestNumber", RequestNumber);
           database.AddStringParameter("@LastName", lName);
           database.AddStringParameter("@FirstName", fName);          
           database.AddDateTimeParameter("@DOB", dob);
           database.AddStringParameter("@MRN", MRN);
           database.AddStringParameter("@SSN", SSN);
           database.AddStringParameter("@XMLData", ItemComment);            
           database.AddLongParameter("@AddedBy", LogedUserId);
           database.AddStringParameter("@Notes", strnotes);         
           database.AddLongParameter("faxid", 0, true);
           try
           {
               database.SubmitData("usp_SaveNewInformation", DataPortalQueryType.StoredProcedure);
               saveStatus = Convert.ToInt64(database.GetParameterValue("faxid"));
           }
           catch (Exception ex)
           {
               saveStatus = 0;
               ex.ToString();
           }
           return saveStatus;
       }
       public static DataSet CountProcessorItem(Int64 RequestNumber)
       {
           var db = new Database();
           db.AddLongParameter("@RequestId", RequestNumber);
           return db.ReturnDataSet("sp_CountProcessorItem", DataPortalQueryType.StoredProcedure);          
       }

       public static int CountSupervisorItem(Int64 RequestNumber)
       {          
           int countstatus = 0;
           var db = new Database();
           db.AddLongParameter("@RequestId", RequestNumber);
           db.AddIntParameter("CountItem", 0, true);
           try
           {
               db.SubmitData("sp_CountSupervisorItem", DataPortalQueryType.StoredProcedure);
               countstatus = Convert.ToInt32(db.GetParameterValue("CountItem"));
           }
           catch (Exception ex)
           {
               countstatus = -1;
               ex.ToString();
           }
           return countstatus;
       }

       public static Int64 UpdateRequestStatus(int flag, Int64 RequestNumber, Int64 spcUserId = 0, Int64 addedBy=0, string strnotes="")
       {
           Int64 ReqId = 0;
           string strQ = "";
           try
           {
               if (flag == 1)
               {
                   RequstEntry reqobj = new RequstEntry();
                   int RequestQueueId = reqobj.GetReqQueueId("Customer Service");
                   if (RequestQueueId > 0)
                   {
                       if (spcUserId > 0)
                           strQ = "update Requests set RequestStatus=10, QueueStatus=" + RequestQueueId + ", SpcUserId=" + spcUserId + " Where RequestNumber=" + RequestNumber + "";//Customer Service,Holde for specilist(10)
                       else
                           strQ = "update Requests set RequestStatus=10,QueueStatus=" + RequestQueueId + " Where RequestNumber=" + RequestNumber + "";//Customer Service,Holde for specilist(10)
                   }
               }
               else if (flag == 2)
                   strQ = "update Requests set RequestStatus=11,IsDeleted=0 Where RequestNumber=" + RequestNumber + "";//Deleted status(11)
               else if (flag == 3)
                   strQ = "update Requests set RequestStatus=8,QueueStatus=5,DateCompleted='" + DateTime.Now + "' Where RequestNumber=" + RequestNumber + ";\n" +  //Completed status(8)Complete and QA=5;
                            " insert into ReqNotes(RequestorId,NotsDescription,CreatedBy)values(" + RequestNumber + ",'" + strnotes + "'," + addedBy + ")";
               else if (flag == 4)
                   strQ = "insert into ReqNotes(RequestorId,NotsDescription,CreatedBy)values(" + RequestNumber + ",'" + strnotes + "'," + addedBy + ")";
              
               var db = new Database();
              db.SubmitData(strQ, DataPortalQueryType.SqlQuery);
              ReqId = RequestNumber;
           }
           catch (Exception ex)
           {
               ex.ToString();
               ReqId = -1;
           }
           return RequestNumber;
       }

       public static string UpdateReqStatusToComplete(string RequestNo)
       {
           string strsql = "";
           strsql = "UPDATE Requests SET RequestStatus = 12 WHERE RequestNumber = '" + RequestNo + "' ";
           var db = new Database();
           db.SubmitData(strsql, DataPortalQueryType.SqlQuery);
           return RequestNo.ToString();
       }

       #endregion;
    }
}
