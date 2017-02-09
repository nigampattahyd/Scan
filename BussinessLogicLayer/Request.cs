using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccessLayer;
using System.IO;


namespace BussinessLogicLayer
{
    public class Request
    {

        //***************Request Details**************************//

        public static DataTable GetAllRequestTypeByReferenceNumber(Int64 refNumber)
        {
            var dl = new Database();
            return dl.ReturnDataTable("Select * from RequestTypes where MedicalFacilityRefNo=" + refNumber, DataPortalQueryType.SqlQuery);
        }
        public static DataTable GetAllRequests()
        {
            var dl = new Database();
            return dl.ReturnDataTable("Select * from RequestDetail", DataPortalQueryType.SqlQuery);
        }
        public static DataSet GetAllRequestsSubmittedInTimeSpan(DateTime dtFrom, DateTime dtTo)
        {
            var db = new Database();
            db.AddDateTimeParameter("@dtFrom", dtFrom);
            db.AddDateTimeParameter("@dtTo", dtTo);
            return db.ReturnDataSet("Get_RequestDetailsByTimeSpan", DataPortalQueryType.StoredProcedure);
        }
        public static DataSet GetAllRequests1()
        {
            var dl = new Database();
            return dl.ReturnDataSet("Select * from RequestDetail", DataPortalQueryType.SqlQuery);
           
        }
        public static DataSet GetAllReleaseReasons(int rTypeId)
        {
            var dl = new Database();
            return dl.ReturnDataSet("select ReleaseReasonId,ReleaseReason from ReleaseReasons where RequestType=" + rTypeId + " order by ReleaseReason asc", DataPortalQueryType.SqlQuery);
        }
        public static DataTable GetAllRelReasons(int rTypeId)
        {
            var dl = new Database();
            return dl.ReturnDataTable("select ReleaseReasonId,ReleaseReason from ReleaseReasons where RequestType=" + rTypeId + " order by ReleaseReason asc", DataPortalQueryType.SqlQuery);
        }
        public static DataSet GetAllRelReasons(string rType)
        {
            var dl = new Database();
            return dl.ReturnDataSet("select ReleaseReasonId,ReleaseReason from ReleaseReasons where RequestType=" + rType + " order by ReleaseReason asc", DataPortalQueryType.SqlQuery);
        }
        public static DataSet GetAllStates()
        {
            var dl = new Database();
            return dl.ReturnDataSet("select StateID,StateName from dbo.states", DataPortalQueryType.SqlQuery);
        }
        public static DataSet GetRequestDetailsByRequestNumber(Int64 requestId)
        {
            var db = new Database();
            db.AddLongParameter("@RequestNumber", requestId);
            return db.ReturnDataSet("sp_GetRequestDetailsByRequestNumber", DataPortalQueryType.StoredProcedure);
        }
        public static DataSet GetAllRequestTypes()
        {
            var dl = new Database();
            return dl.ReturnDataSet("select * from RequestTypes order by RequestType asc", DataPortalQueryType.SqlQuery);
        }
        public static DataSet GetAllRequestInvStatus()
        {
            var dl = new Database();
            return dl.ReturnDataSet("Select * From dbo.RequestStatus where Active=1 order by StatusName asc", DataPortalQueryType.SqlQuery);
        }
        public static void DeleteRequestType(int requestTypes)
        {
            var db = new Database();

            db.SubmitData("delete from RequestTypes where RequestTypeId=" + requestTypes, DataPortalQueryType.SqlQuery);
        }

        public static void UpdateRequestType(RequestType requestTypes)
        {
            string Query;
            var db = new Database();
            if (requestTypes.Default == true)
            {
                Query = "update RequestTypes set DefaultValue=0 where MedicalFacilityRefNo=" + requestTypes.MedicalReferenceNumber;
                db.SubmitData(Query, DataPortalQueryType.SqlQuery);
            }
            Query = " update RequestTypes set MedicalFacilityRefNo=" + requestTypes.MedicalReferenceNumber + ", RequestType='" +
                        requestTypes.RequestTypeText + "', Active=" + Convert.ToInt32(requestTypes.Active) + ", DefaultValue=" + Convert.ToInt32(requestTypes.Default)
                        + ", CreatedOn=getdate() where RequestTypeId=" + requestTypes.RequestTypeId;
            db.SubmitData(Query, DataPortalQueryType.SqlQuery);
        }

        public static void InsertRequestTypes(RequestType requestTypes)
        {
            string Query;
            var db = new Database();
            if (requestTypes.Default == true)
            {
                Query = "update RequestTypes set DefaultValue = 0 where MedicalFacilityRefNo=" + requestTypes.MedicalReferenceNumber;
                db.SubmitData(Query, DataPortalQueryType.SqlQuery);
            }
            Query = " insert into RequestTypes (MedicalFacilityRefNo,RequestType,Active,DefaultValue,CreatedOn) values(" + requestTypes.MedicalReferenceNumber + ",'" +
                        requestTypes.RequestTypeText + "', " +
                        Convert.ToInt32(requestTypes.Active) + ", " +
                        Convert.ToInt32(requestTypes.Default)
                        + ",getdate())";
            db.SubmitData(Query, DataPortalQueryType.SqlQuery);
        }


        //***************Requestor Details**************************//

        public static DataTable GetAllRequestor()
        {
            var db = new Database();
            return db.ReturnDataTable("select * from dbo.Requestors ", DataPortalQueryType.SqlQuery);
        }

        public static DataTable GetAllRequestorTypeByReferenceNumber(Int64 refNumber)
        {
            var dl = new Database();
            return dl.ReturnDataTable("Select * from RequestorTypes where MedicalFacilityRefNo=" + refNumber, DataPortalQueryType.SqlQuery);
        }

        public static void DeleteRequestorType(int requestorTypes)
        {
            var db = new Database();

            db.SubmitData("delete from RequestorTypes where RequestorTypeId=" + requestorTypes, DataPortalQueryType.SqlQuery);
        }

        public static void DeleteRequestor(Int64 requestorId)
        {
            var db = new Database();

            db.SubmitData("delete from dbo.Requestors where RequestorId=" + requestorId, DataPortalQueryType.SqlQuery);
        }

        public static void UpdateRequestorType(RequestorType requestorTypes)
        {
            string Query;
            var db = new Database();
            if (requestorTypes.Default == true)
            {
                Query = "update RequestorTypes set DefaultValue=0 where MedicalFacilityRefNo=" + requestorTypes.MedicalReferenceNumber;
                db.SubmitData(Query, DataPortalQueryType.SqlQuery);
            }
            Query = " update RequestorTypes set MedicalFacilityRefNo=" + requestorTypes.MedicalReferenceNumber + ", RequestorType='" +
                        requestorTypes.RequestorTypeText + "', Active=" + Convert.ToInt32(requestorTypes.Active) + ", DefaultValue=" + Convert.ToInt32(requestorTypes.Default)
                        + ", CreatedOn=getdate() where RequestorTypeId=" + requestorTypes.RequestorTypeId;
            db.SubmitData(Query, DataPortalQueryType.SqlQuery);
        }

        public static void InsertRequestorTypes(RequestorType requestorTypes)
        {
            string Query;
            var db = new Database();
            if (requestorTypes.Default == true)
            {
                Query = "update RequestorTypes set DefaultValue = 0 where MedicalFacilityRefNo=" + requestorTypes.MedicalReferenceNumber;
                db.SubmitData(Query, DataPortalQueryType.SqlQuery);
            }
            Query = " insert into RequestorTypes (MedicalFacilityRefNo,RequestorType,Active,DefaultValue,CreatedOn) values(" + requestorTypes.MedicalReferenceNumber + ",'" +
                        requestorTypes.RequestorTypeText + "', " +
                        Convert.ToInt32(requestorTypes.Active) + ", " +
                        Convert.ToInt32(requestorTypes.Default)
                        + ",getdate())";
            db.SubmitData(Query, DataPortalQueryType.SqlQuery);
        }

        public static bool CreateRequestor(RequestorStuff requestor, out Int64 reqNumber)
        {
            try
            {
                var db = new Database();
                db.AddLongParameter("@RequestorId", requestor.RequestorId);
                db.AddStringParameter("@RequestorName", requestor.RequestorName);
                db.AddStringParameter("@AttentionTo", requestor.AttentionTo);
                db.AddStringParameter("@Address1", requestor.Address1);
                db.AddStringParameter("@Address2", requestor.Address2);

                db.AddStringParameter("@EmailAddress", requestor.EmailAddress);
                db.AddStringParameter("@City", requestor.City);
                db.AddStringParameter("@ZIP", requestor.ZIP);
                db.AddStringParameter("@Fax", requestor.FAX);
                db.AddStringParameter("@PhoneNumber", requestor.PhoneNumber);
                db.AddIntParameter("@State", requestor.State);
                db.AddIntParameter("@Country", requestor.Country);
                db.AddIntParameter("@RequestorType", requestor.RequestorType);
                db.AddLongParameter("reqNumber", 0, true);
                db.SubmitData("sp_CreateRequestor", DataPortalQueryType.StoredProcedure);
                reqNumber = Convert.ToInt64(db.GetParameterValue("reqNumber"));
                return true;
            }
            catch (Exception ex)
            {
                reqNumber = 0;
                return false;
            }
        }

        public static DataTable GetSchema()
        {
            var db = new Database();
            return db.ReturnDataTable("select * from dbo.Requestors where 1 !='1'", DataPortalQueryType.SqlQuery);
        }

        public static void BulkInsert(DataRow data, string[] columnName)
        {
            var dp = new Database();
            string query = "insert into dbo.Patients (";
            for (int i = 0; i < columnName.Length; i++)
            {
                query += columnName[i];
                if (i < columnName.Length - 1)
                    query += ",";
            }
            query += ") values (";
            for (int i = 0; i < columnName.Length; i++)
            {
                query += "'" + data[i] + "'";
                if (i < columnName.Length - 1)
                    query += ",";
            }
            query += ")";

            dp.SubmitData(query, DataPortalQueryType.SqlQuery);
        }

        public static DataTable SearchRequestor(List<string> value, List<string> Type, List<string> Field)
        {
            string type = "";
            // string[] strField = new string[4] { "Name", "NickName", "State", "ZIP" };
            for (int i = 0; i < Type.Count; i++)
            {
                bool isField = false;
                if (Type[i] == "Contains")
                {
                    isField = true;
                    type += Field[i] + " like '%" + value[i] + "%'";
                }
                if (Type[i] == "Is")
                {
                    isField = true;
                    type += Field[i] + " like '" + value[i] + "'";
                }
                if (Type[i] == "Ends with")
                {
                    isField = true;
                    type += Field[i] + " like '%" + value[i] + "'";
                }
                if (Type[i] == "Starts with")
                {
                    isField = true;
                    type += Field[i] + " like '" + value[i] + "%'";
                }
                if (Type[i] == "State")
                {
                    isField = true;
                    type += " State = " + value[i] + " ";
                }
                if (isField && i < Type.Count - 1) type += " and ";
            }
            var dl = new Database();
            if (type == "") type = "1='1'";
            string query = "Select * from dbo.Requestors where " + type;
            return dl.ReturnDataTable(query, DataPortalQueryType.SqlQuery);

        }
        public static int GetRequestorTypeIdByRequestorType(string RequestorType)
        {
            //var db = new Database();
            //return db.ReturnDataTable("select UserTypeId from dbo.UserTypes where UserType='" + UserType+"'", DataPortalQueryType.SqlQuery);
            int RequestorTypeId;
            var dp = new Database();
            DataSet ds;
            string query = "select RequestTypeId from RequestTypes  where RequestType='" + RequestorType + "'";
            ds = dp.ReturnDataSet(query, DataPortalQueryType.SqlQuery);
            if (ds.Tables[0].Rows.Count > 0)
            {
                RequestorTypeId = Convert.ToInt32(ds.Tables[0].Rows[0]["RequestTypeId"]);
            }
            else
            { 
               RequestorTypeId=0;
            }
            return RequestorTypeId;
        }

        public static void CheckDuplicateEmailId(string email,string endUser, out int count)
        {
            var db = new Database();
           
            db.AddStringParameter("@EmailAddress", email);
            db.AddStringParameter("@EndUser", endUser);
            db.AddIntParameter("count", 0, true);

            try
            {
                db.SubmitData("sp_CheckDuplicateEmailID", DataPortalQueryType.StoredProcedure);
                count = Convert.ToInt32(db.GetParameterValue("count"));

            }
            catch (Exception ex)
            {
                count = 1;
            }

        }
        public static bool CreateUpdateNotes(string strnotes,Int64 ReqId,Int64 AddedBy,int flg,Int64 NotesId, out Int64 NotsRetId)
        {
            try
            {
                var db = new Database();
                db.AddLongParameter("@NotesId", NotesId);
                db.AddLongParameter("@RequestorId", ReqId);
                db.AddStringParameter("@NotsDesc", strnotes);
                db.AddLongParameter("@AddedBy", AddedBy);
                db.AddLongParameter("@flg", flg);                 
                db.AddLongParameter("NotsRetId", 0, true);
                db.SubmitData("sp_CreateNotes", DataPortalQueryType.StoredProcedure);
                NotsRetId = Convert.ToInt64(db.GetParameterValue("NotsRetId"));
                return true;
            }
            catch (Exception ex)
            {
                NotsRetId = 0;
                return false;
            }
        }
        
        public static bool DeleteNotes(Int64 NotesId, int flg, out Int64 NotsRetId)
        {
            try
            {
                var db = new Database();
                db.AddLongParameter("@NotesId", NotesId);
                db.AddLongParameter("@flg", flg);
                db.AddLongParameter("NotsRetId", 0, true);
                db.SubmitData("sp_CreateNotes", DataPortalQueryType.StoredProcedure);
                NotsRetId = Convert.ToInt64(db.GetParameterValue("NotsRetId"));
                return true;
            }
            catch (Exception ex)
            {
                NotsRetId = 0;
                return false;
            }
        }
    
        public static DataTable NotesList(Int64 ReqId, int flg)
        {
            var db = new Database();
            db.AddLongParameter("@RequestorId", ReqId);
            db.AddLongParameter("@flg", flg);
            db.AddLongParameter("NotsRetId", 0, true);
            return db.ReturnDataTable("sp_CreateNotes", DataPortalQueryType.StoredProcedure);           
        }

        public static DataSet NotesList1(Int64 ReqId, int flg)
        {
            var dl = new Database();
            dl.AddLongParameter("@RequestorId", ReqId);
            dl.AddLongParameter("@flg", flg);
            dl.AddLongParameter("NotsRetId", 0, true);
            return dl.ReturnDataSet("sp_CreateNotes", DataPortalQueryType.StoredProcedure);
        }
                
    }
}
