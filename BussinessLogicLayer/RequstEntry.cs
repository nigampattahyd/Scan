using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccessLayer;
using System.IO;

namespace BussinessLogicLayer
{
    public class RequstEntry
    {
        public static Int64 SaveViewQueueData(bulkData blkData, int RequestQueueStatus, int RequestStatus, Int64 RequestorLoginId, Int64 CopyReqNumberId)
        {
            Int64 savestatus = 0;
            var database = new Database();
            database.AddLongParameter("@RequestNumber", blkData.FieldId);
            database.AddStringParameter("@ReleaseId", blkData.ReleaseCode);
            database.AddStringParameter("@PFirstName", blkData.PFirstName);
            database.AddStringParameter("@PLastName", blkData.PLastName);
            database.AddDateTimeParameter("@PDOB", blkData.PDOB);
            database.AddStringParameter("@PSSN", blkData.PSSN);
            database.AddStringParameter("@PMedicalRecord", blkData.PmedicalRecord);
            database.AddStringParameter("@PAccount", blkData.PAccount);
            database.AddDateTimeParameter("@PDateOfService", blkData.PDOS);
            database.AddStringParameter("@PPhone", blkData.PPhone);
            database.AddStringParameter("@PMobile", blkData.PMobile);
            database.AddStringParameter("@PFax", blkData.PFax);
            database.AddStringParameter("@PEmailAdds", blkData.PEmailAdds);
            database.AddStringParameter("@PNotes", blkData.PNotes);

            database.AddStringParameter("@ReqName", blkData.ReqName);
            database.AddStringParameter("@rAttentionTo", blkData.rAttentionTo);
            database.AddStringParameter("@rAddress1", blkData.rAddress1);
            //database.AddStringParameter("@rAddress2", blkData.rAddress2);
            database.AddStringParameter("@CaseNumber", blkData.CaseNumber);
            database.AddStringParameter("@rCity", blkData.rCity);
            database.AddIntParameter("@rState", blkData.rState);
            database.AddStringParameter("@rZip", blkData.rZip);
            database.AddStringParameter("@rFax", blkData.rFax);
            database.AddStringParameter("@rPhone", blkData.rPhone);
            database.AddStringParameter("@rMobile", blkData.rMobile);
            database.AddStringParameter("@rReqId", blkData.rReqID);
            database.AddStringParameter("@rEmailAdds", blkData.rEmailAdds);
            database.AddIntParameter("@rRequestType", blkData.rRequestType);
            database.AddIntParameter("@rReleaseReason", blkData.rReleaseReason);
            database.AddLongParameter("@BillToId", blkData.RBillTo);
            database.AddLongParameter("@MedicalFacilityRefNo", blkData.MedicalFacilityRefNo);
            database.AddStringParameter("@ShippingInfo1", blkData.ShippingInfo1);
            database.AddStringParameter("@ShippingInfo2", blkData.ShippingInfo2);
            database.AddStringParameter("@ShippingInfo3", blkData.ShippingInfo3);
            database.AddStringParameter("@ShippingInfo4", blkData.ShippingInfo4);
            database.AddStringParameter("@ShippingInfo5", blkData.ShippingInfo5);
            database.AddStringParameter("@ShippingInfo6", blkData.ShippingInfo6);
            database.AddStringParameter("@ShippingInfo7", blkData.ShippingInfo7);
            database.AddStringParameter("@ShippingInfo8", blkData.ShippingInfo8);
            database.AddStringParameter("@ShippingInfo9", blkData.ShippingInfo9);
            database.AddStringParameter("@ShippingInfo10", blkData.ShippingInfo10);
            database.AddLongParameter("@ShippingStateId", blkData.ShippingStateId);
            database.AddLongParameter("@UserId", blkData.UserId);
            database.AddIntParameter("@RequestQueueStatus", RequestQueueStatus);
            database.AddIntParameter("@RequestStatus", RequestStatus);
            database.AddLongParameter("@RequestorLoginId", RequestorLoginId);
            database.AddStringParameter("@CityState", blkData.rCityState);
            database.AddLongParameter("@PatientId", blkData.PatientId);
            database.AddLongParameter("@CopyReqNumber", CopyReqNumberId);

            database.AddStringParameter("@ESMDClaimId", blkData.ESMDClaimId);
            database.AddStringParameter("@ESMDCaseId", blkData.ESMDCaseId);
            database.AddStringParameter("@IntendedRecipient", blkData.IntendedRecipient);
            database.AddStringParameter("@Author", blkData.Author);
            database.AddStringParameter("@AuthorInstitution", blkData.AuthorInstitution);

            database.AddStringParameter("@AuthorPerson", blkData.AuthorPerson);
            database.AddStringParameter("@ContentTypeCode", blkData.ContentTypeCode);
            database.AddStringParameter("@EntryUUID", blkData.EntryUUID);
            database.AddStringParameter("@TargetCommunityId", blkData.TargetCommunityId);
            database.AddStringParameter("@DocumentTitle", blkData.DocumentTitle);

            database.AddDateTimeParameter("@CreationTime", blkData.CreationTime);
            database.AddDateTimeParameter("@ServiceStartTime", blkData.ServiceStartTime);
            database.AddDateTimeParameter("@ServiceStopTime", blkData.ServiceStopTime);
            database.AddDateTimeParameter("@SubmissionTime", blkData.SubmissionTime);
            database.AddStringParameter("@UniqueId", blkData.UniqueId);

            database.AddStringParameter("@ConfidentialityCode", blkData.ConfidentialityCode);
            database.AddStringParameter("@ClassCode", blkData.ClassCode);
            database.AddStringParameter("@FormatCode", blkData.FormatCode);
            database.AddStringParameter("@HelthcareFacility", blkData.HelthcareFacility);
            database.AddStringParameter("@SubmissionSet", blkData.SubmissionSet);

            database.AddStringParameter("@BatchProcess", blkData.BatchProcess);
            
            database.AddLongParameter("SaveStatus", 0, true);
            try
            {
                database.SubmitData("Sp_InsertViewQueue", DataPortalQueryType.StoredProcedure);
                savestatus = Convert.ToInt64(database.GetParameterValue("SaveStatus"));
            }
            catch (Exception ex)
            {
                savestatus = 0;
                ex.ToString();
            }
            return savestatus;
        }

        public static DataTable GetUserById(Int64 userId)
        {
            var db = new Database();
            return db.ReturnDataTable("Select ui.FullName,ut.UserType from  dbo.UserLoginInfo ui inner join UserTypes ut on ui.UserType=ut.UserTypeId where ui.RecID=" + userId, DataPortalQueryType.SqlQuery);
        }
        public static DataTable GetFeed()
        {
            var db = new Database();
            return db.ReturnDataTable("SELECT top(4) CONVERT(VARCHAR(10),fd.CreatedOn,101) as CreatedOn,fd.Feed,uli.fullname FROM feeds fd WITH (NOLOCK) INNER JOIN UserLoginInfo uli ON fd.UserId=uli.RecID order by fd.CreatedOn desc", DataPortalQueryType.SqlQuery);
        }
        public static DataTable GetReleaseId(string rReqID, string ReqName, string rFax, string rPhone)
        {
            var db = new Database();
            db.AddStringParameter("@RequestorId", rReqID);
            db.AddStringParameter("@ReqName", ReqName);
            db.AddStringParameter("@rFax", rFax);
            db.AddStringParameter("@rPhone", rPhone);
            return db.ReturnDataTable("sp_GetReleaseId", DataPortalQueryType.StoredProcedure);
        }
        public static DataTable GetBillTo()
        {
            var db = new Database();
            return db.ReturnDataTable("select * from BillTo", DataPortalQueryType.SqlQuery);
        }
        public static DataTable GetViewQueueList(int flg, Int64 userid)
        {
            DataTable dt = new DataTable();
            var db = new Database();
            if (flg == 1)
            {
                dt = db.ReturnDataTable("select vql.QueueId,vql.LastName,vql.FirstName,vql.ReqName,vql.ReleaseId,vql.Notes,CONVERT(VARCHAR(10),vql.CreatedOn,101) as CreatedOn,vql.UpdatedDate, " +
                    "mf.Name as FacilityName FROM dbo.ViewQueueList vql WITH (NOLOCK) INNER JOIN dbo.MedicalFacility mf ON vql.MedicalFacilityRefNo = mf.MedicalFacilityRefNo " +
                    "ORDER BY CreatedOn DESC", DataPortalQueryType.SqlQuery);
            }
            else if (flg == 3)
            {
                dt = db.ReturnDataTable("select vql.QueueId,vql.LastName,vql.FirstName,vql.ReqName,vql.ReleaseId,vql.Notes,CONVERT(VARCHAR(10),vql.CreatedOn,101) as CreatedOn,vql.UpdatedDate, " +
                    "mf.Name as FacilityName FROM dbo.ViewQueueList vql WITH (NOLOCK) INNER JOIN dbo.MedicalFacility mf ON vql.MedicalFacilityRefNo = mf.MedicalFacilityRefNo " +
                    "where vql.CreatedBy=" + userid + " ORDER BY CreatedOn DESC", DataPortalQueryType.SqlQuery);
            }
            return dt;
        }
        public static DataSet FillViewQueueList(Int64 vqId, int flag)
        {
            var db = new Database();
            db.AddLongParameter("@RequestNumber", vqId);
            db.AddIntParameter("@Flag", flag);
            return db.ReturnDataSet("sp_GetViewQueueList", DataPortalQueryType.StoredProcedure);

            //    var db = new Database();
            //    return db.ReturnDataTable("RequestNumber,ReleaseCode,PatientId,LastName,FirstName,DOB,SSN,PPhoneNumber,PEmailAdds,PNotes,PMobileNumber,PFaxNumber,RequestorId,RequestorName, AttentionTo, Address1, CaseNumber,City, [State],ZIP,PhoneNumber,Fax,EmailAddress,RequestTypeId,ReleaseReasonId,BillToId from ViewQueueList where QueueId=" + vqId + "", DataPortalQueryType.SqlQuery);
        }
        public static DataTable GetPatientFilterList(string whereQur)
        {
            var db = new Database();
            return db.ReturnDataTable("SELECT PatientId from patients WITH(NOLOCK) WHERE" + whereQur + "", DataPortalQueryType.SqlQuery);
        }
        public static DataTable GetReqFilterList(string whereQur)
        {
            var db = new Database();
            return db.ReturnDataTable("SELECT RequestorId from Requestors WITH(NOLOCK) WHERE" + whereQur + "", DataPortalQueryType.SqlQuery);
        }
        public static DataTable GetPatientList(Int64 PtId)
        {
            var db = new Database();
            db.AddLongParameter("@PtId", PtId);
            return db.ReturnDataTable("sp_GetPatientList", DataPortalQueryType.StoredProcedure);
        }
        //public static DataTable GetRequestorList(Int64 ReqId)
        //{
        //    var db = new Database();
        //    return db.ReturnDataTable("SELECT top(1) req.RequestorName,req.AttentionTo,req.Address1,req.Address2,req.City,req.[State],req.ZIP,req.Fax,req.PhoneNumber, " +
        //        "req.MobileNumber,req.EmailAddress,rst.RequestType,rst.ReleaseReason FROM Requestors req WITH (NOLOCK) INNER JOIN Requests rst ON " +
        //        "req.RequestorId = rst.RequestorId  where req.RequestorId=" + ReqId + "", DataPortalQueryType.SqlQuery);
        //}
        public static DataTable GetRequestorList(Int64 ReqId)
        {
            var db = new Database();
            db.AddLongParameter("@ReqId", ReqId);
            return db.ReturnDataTable("sp_GetRequestorList", DataPortalQueryType.StoredProcedure);
        }
        public static DataTable GetPatientsName(string LNameKey,Int64 MedicalFacilityRefNo,int flg)
        {
            var db = new Database();
            db.AddIntParameter("@flag", flg);
            db.AddStringParameter("@Sname", LNameKey);
            db.AddLongParameter("@MedicalFacilityRefNo", MedicalFacilityRefNo);
            return db.ReturnDataTable("sp_FillPatient", DataPortalQueryType.StoredProcedure);
        }
        public static DataTable GetReqName(string ReqKey)
        {
            var db = new Database();
            return db.ReturnDataTable("SELECT distinct top 10 rs.RequestorId,rs.RequestorName,rs.AttentionTo,rs.Address1,st.[StateName],rs.CreatedOn FROM Requestors rs WITH (NOLOCK) LEFT OUTER JOIN Requests rst ON rs.RequestorId = rst.RequestorId INNER JOIN states st on rs.state = st.StateID WHERE " + ReqKey + "order by rs.CreatedOn desc", DataPortalQueryType.SqlQuery);
        }
        public static DataTable GetViewQueueListWhereQuery(string whereQur, int flg, Int64 UserId)
        {
            var db = new Database();
            DataTable dt = new DataTable();
            if (flg == 1 || flg == 6)
            {
                dt = db.ReturnDataTable("SELECT vql.QueueId,vql.LastName,vql.FirstName,vql.ReqName,vql.ReleaseId,vql.Notes,CONVERT(VARCHAR(10),vql.CreatedOn,101) as CreatedOn,vql.UpdatedDate, " +
                    "mf.Name as FacilityName FROM dbo.ViewQueueList vql WITH (NOLOCK) INNER JOIN dbo.MedicalFacility mf ON vql.MedicalFacilityRefNo = mf.MedicalFacilityRefNo " + whereQur + " ORDER BY CreatedOn ASC", DataPortalQueryType.SqlQuery);
            }
            else if (flg == 3)
            {
                dt = db.ReturnDataTable("SELECT vql.QueueId,vql.LastName,vql.FirstName,vql.ReqName,vql.ReleaseId,vql.Notes,CONVERT(VARCHAR(10),vql.CreatedOn,101) as CreatedOn,vql.UpdatedDate, " +
                       "mf.Name as FacilityName FROM dbo.ViewQueueList vql WITH (NOLOCK) INNER JOIN dbo.MedicalFacility mf ON vql.MedicalFacilityRefNo = mf.MedicalFacilityRefNo  " +
                       "WHERE vql.CreatedBy= " + UserId + " AND " + whereQur + " ORDER BY CreatedOn ASC", DataPortalQueryType.SqlQuery);
            }
            return dt;
        }

        //public static DataSet GetViewQueueListFilter(List<string> value, List<string> Field, List<string> choise, int userType, Int64 userId)
        //{
        //    string type = "";
        //    int value1 = value.Count;
        //    for (int c = 0; c < choise.Count; c++)
        //    {
        //        bool isField = false;
        //        if (choise[c] == "Balance" || choise[c] == "DOB" || choise[c] == "ActualDateReceived" || choise[c] == "CreatedOn" || choise[c] == "DateCompleted")
        //        {
        //            if (choise[c] == "Balance")
        //            {
        //                isField = true;
        //                type += Field[c] + value[c];
        //                value1 = value1 - 1;
        //                if (isField && c < value.Count - 1) type += " and ";
        //            }
        //            else if (choise[c] == "DOB" || choise[c] == "ActualDateReceived" || choise[c] == "CreatedOn" || choise[c] == "DateCompleted")
        //            {
        //                isField = true;
        //                type += Field[c] + " ='" + value[c] + "'";
        //                value1 = value1 - 1;
        //                if (isField && c < value.Count - 1) type += " and ";
        //            }
        //        }
        //        else
        //        {
        //            if (choise[c] == "QueueStatus")
        //            {
        //                isField = true;
        //                type += Field[c] + " = " + value[c];
        //                if (isField && c < value.Count - 1) type += " and ";

        //            }
        //            else if (choise[c] == "PatientInfo4")
        //            {

        //                isField = true;
        //                type += Field[c] + " like '%" + value[c] + "'";
        //                if (isField && c < value.Count - 1) type += " and ";

        //            }
        //            else if (choise[c] != "Balance")
        //            {

        //                isField = true;
        //                type += Field[c] + " like '" + value[c] + "%'";
        //                if (isField && c < value.Count - 1) type += " and ";

        //            }

        //        }
        //    }
        //    DataSet dsSearch = new DataSet();
        //    var db = new Database();

        //    if (type == "") type = "1='1'";
        //    {
        //        db.AddLongParameter("@UserId", userId);
        //        db.AddStringParameter("@Sname", type);
        //        db.AddIntParameter("@Flag", userType);
        //        dsSearch = db.ReturnDataSet("sp_GetViewQueueListFilter", DataPortalQueryType.StoredProcedure);
        //    }
        //    return dsSearch;
        //}

        public DataSet GetViewQueueListFilter(string str, int userType, Int64 userId, int pageNo, int PageSize, string SortOrder = "")
        {
            DataSet dsSearch = new DataSet();
            var db = new Database();

            if (str == "") str = "1='1'";
            {
                db.AddLongParameter("@UserId", userId);
                db.AddStringParameter("@Sname", str);
                db.AddIntParameter("@Flag", userType);
                db.AddIntParameter("@PageNo", pageNo);
                db.AddIntParameter("@PageSize", PageSize);
                db.AddStringParameter("@SortOrder", SortOrder);
                dsSearch = db.ReturnDataSet("sp_GetViewQueueListFilter", DataPortalQueryType.StoredProcedure);
            }
            return dsSearch;
        }

        public static DataSet GetAllViewQueueList(int flg, Int64 userid)
        {
            var dl = new Database();
            dl.AddIntParameter("@flg", flg);
            dl.AddLongParameter("UserId", userid);
            return dl.ReturnDataSet("sp_GetAllViewQueueList", DataPortalQueryType.StoredProcedure);
        }
        public static DataSet GetAllRequest()
        {
            var dl = new Database();
            return dl.ReturnDataSet("select * from ViewQueueStatus WITH (NOLOCK) Where IsActive= 1 order by Item asc", DataPortalQueryType.SqlQuery);
        }
        public static DataTable GetAssignedMedicalFacility(Int64 ReqId)
        {
            var db = new Database();
            db.AddLongParameter("@RecId", ReqId);
            return db.ReturnDataTable("Get_AssignedMedicalFacilityInformationFromUserAccountNumber", DataPortalQueryType.StoredProcedure);
        }
        public static DataTable GetInvoiceItemPrice(int stateId, int requestorTypeId, int releaseReasonId)
        {
            var db = new Database();
            db.AddIntParameter("@StateId", stateId);
            db.AddIntParameter("@RequestTypeId", requestorTypeId);
            db.AddIntParameter("@ReleaseReasonId", releaseReasonId);
            return db.ReturnDataTable("Get_InvoiceItemsWithPrice", DataPortalQueryType.StoredProcedure);
        }
        public static DataTable GetViewQueueList(Int64 vqId, string relId)
        {
            var db = new Database();
            db.AddLongParameter("@RequestNumber", vqId);
            db.AddStringParameter("@ReleaseId", relId);
            return db.ReturnDataTable("sp_GetInvoiceData", DataPortalQueryType.StoredProcedure);
        }
        public static int DeleteFromViewQueue(Int64 vqId, string relId)
        {
            int retVal = 0;
            try
            {
                var db = new Database();
                db.AddLongParameter("@QueueId", vqId);
                db.AddStringParameter("@ReleaseId", relId);
                db.SubmitData("sp_DeleteFromViewQueue", DataPortalQueryType.StoredProcedure);
                retVal = 1;
            }
            catch (Exception ex)
            {
                ex.ToString();
                retVal = -1;
            }
            return retVal;
        }
        public static void UpdateViewQueueStatus(int QueueStatusVal, Int64 RequestNumber)
        {
            var db = new Database();
            db.SubmitData("Update Requests set QueueStatus=" + QueueStatusVal + " where RequestNumber=" + RequestNumber + "", DataPortalQueryType.SqlQuery);
        }

        public static DataSet GetPatientDetails(string type, int flg,Int64 MedicalfacilityRefNo=0)
        {
            var dl = new Database();
            string SPName = "";
            if (type != "")
            {
                type = " WHERE " + type;
                dl.AddStringParameter("@SearchType", type);
                if (flg == 1)
                {
                    dl.AddLongParameter("@MedicalFacilityRefNo", MedicalfacilityRefNo);
                    SPName = "sp_SearchPatientInfo";
                }
                if (flg == 2)
                    SPName = "sp_SearchRequestorInfo";
            }
            return dl.ReturnDataSet(SPName, DataPortalQueryType.StoredProcedure);
        }
        public static DataTable SearchByCriteria(List<string> value, List<string> Type, List<string> Field, Int64 userId, int flg)
        {
            string type = "";

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
                if (Type[i] == "Equal")
                {
                    isField = true;
                    type += Field[i] + " = '" + value[i] + "'";
                }
                if (Type[i] == "SSN")
                {
                    isField = true;
                    type += Field[i] + " like '" + value[i] + "%'";
                }
                if (isField && i < Type.Count - 1) type += " and ";
            }
            var dl = new Database();
            string SPName = "";
            if (type != "")
            {
                type = " WHERE " + type;
                dl.AddStringParameter("@SearchType", type);
                if (flg == 1)
                    SPName = "sp_SearchPatientInfo";
                if (flg == 2)
                    SPName = "sp_SearchRequestorInfo";
            }
            return dl.ReturnDataTable(SPName, DataPortalQueryType.StoredProcedure);
        }
        public static DataTable GetBilltoStatus(Int64 reqNumber)
        {
            var dl = new Database();
            return dl.ReturnDataTable("Select BillToId from Requests where RequestNumber=" + reqNumber, DataPortalQueryType.SqlQuery);
        }

        #region Document Manage Methods
        public Int64 AddUpdateDocument(Int64 RequestNumber, string ReleaseCode, string FileName, string DispFileName, string FileExt, string Filetype)
        {
            Int64 uploadStatus = 0;
            var database = new Database();
            database.AddLongParameter("@RequestNumber", RequestNumber);
            database.AddStringParameter("@ReleaseId", ReleaseCode);
            database.AddStringParameter("@FileName", FileName);
            database.AddStringParameter("@DispFileName", DispFileName);
            database.AddStringParameter("@FileExt", FileExt);
            database.AddStringParameter("@FileType", Filetype);
            database.AddLongParameter("SaveStatus", 0, true);
           // db.SubmitData("Insert into ReqDocument (RequestNumber,ReleaseId,FileName,DispFileName,FileExt,FileType)values(" + RequestNumber + ",'" + ReleaseCode + "','" + FileName + "','" + DispFileName + "','" + FileExt + "','" + Filetype + "')", DataPortalQueryType.SqlQuery);
            try
            {
                database.SubmitData("ReqDocumentSaveUpdt", DataPortalQueryType.StoredProcedure);
                uploadStatus = Convert.ToInt64(database.GetParameterValue("SaveStatus"));
            }
            catch (Exception ex)
            {
                uploadStatus = -1;
                ex.ToString();
            }
            return uploadStatus;

        }
        public Int64 DeleteDocument(Int64 DocId, int DeletedVal = 0)
        {
            Int64 retVal = 0;
            try
            {
                var db = new Database();
                db.SubmitData("Update ReqDocument set IsDeleted=" + DeletedVal + " where DocId=" + DocId + "", DataPortalQueryType.SqlQuery);
                retVal = DocId;
            }
            catch (Exception ex)
            {
                ex.ToString();
                retVal = -1;
            }
            return retVal;
        }
        public Int64 DeleteInvoice(Int64 RequestNumber, string relid,string Filetype)
        {
            Int64 retVal = 0;
            try
            {
                var db = new Database();
                db.SubmitData("Update ReqDocument set IsDeleted=1 where RequestNumber=" + RequestNumber + " And ReleaseId='" + relid + "' And FileType='" + Filetype + "'", DataPortalQueryType.SqlQuery);
                retVal = RequestNumber;
            }
            catch (Exception ex)
            {
                ex.ToString();
                retVal = -1;
            }
            return retVal;
        }
        
        public DataTable GetDocFileList(Int64 RequestNumber, string sName)
        {
            var db = new Database();
            if (sName == "I")
            {
                return db.ReturnDataTable("select * from ReqDocument where RequestNumber=" + RequestNumber + " And fileType= '" + sName + "'  and IsDeleted = 0", DataPortalQueryType.SqlQuery);
            }
            else
            {
                return db.ReturnDataTable("select * from ReqDocument where RequestNumber=" + RequestNumber + " And fileType= '" + sName + "'  and IsDeleted = 0" +
               " order by SUBSTRING(DispFileName,charindex('_',DispFileName)+1,len(DispFileName)-charindex('_',DispFileName)) desc , SUBSTRING(DispFileName,4,charindex('_',DispFileName)-4) asc", DataPortalQueryType.SqlQuery);
            }
        }
        public DataTable GetDocFileList_byRelcode(string RequestNumber, string sName)
        {
            var db = new Database();
            return db.ReturnDataTable("select * from ReqDocument where ReleaseId='" + RequestNumber + "' And [FileType] = '" + sName + "' and IsDeleted = 0" +
           " order by SUBSTRING(DispFileName,charindex('_',DispFileName)+1,len(DispFileName)-charindex('_',DispFileName)) desc , SUBSTRING(DispFileName,4,charindex('_',DispFileName)-4) asc", DataPortalQueryType.SqlQuery);
        }
        public int Getpageorder(string ReleaseId, string _type, ref Int32 pageorder, ref Int32 subpageorder)
        {
            var db = new Database();
            DataTable dt = db.ReturnDataTable("SELECT max(CAST(REPLACE(REPLACE(PARSENAME(REPLACE(DispFileName, '_', '.'), 2),'IMP',''),'SCN','') AS int)) as PageOrderNo,max(CAST(PARSENAME(REPLACE(DispFileName, '_', '.'), 1)AS int)) as SubPageOrderNo from ReqDocument " +
                                   "where ReleaseId='" + ReleaseId + "' and FileType = '" + _type  + "'", DataPortalQueryType.SqlQuery);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["PageOrderNo"] != DBNull.Value)
                    {
                        pageorder = Convert.ToInt32(dt.Rows[0]["PageOrderNo"]);
                    }
                    else
                    {
                        pageorder = 0;
                    }

                    if (dt.Rows[0]["SubPageOrderNo"] != DBNull.Value)
                    {
                        subpageorder = Convert.ToInt32(dt.Rows[0]["SubPageOrderNo"]);
                    }
                    else
                    {
                        subpageorder = 0;
                    }
                      
                }
                else
                {
                    pageorder = 0;
                    subpageorder = 0;
                }
            }
            else
            {
                pageorder = 0;
                subpageorder = 0;
            }
            return 1;
        }

        public DataTable GetDocFileType(Int64 RequestNumber)
        {
            var db = new Database();
            return db.ReturnDataTable("select FileType,ReleaseId from ReqDocument where RequestNumber='" + RequestNumber + "' And IsDeleted = 0 group by FileType,ReleaseId", DataPortalQueryType.SqlQuery);
        }
        public DataTable TotalPageCount(Int64 RequestNumber, string ftype)
        {
            var db = new Database();
            return db.ReturnDataTable("select count(RequestNumber) as NoOfPages from ReqDocument where RequestNumber='" + RequestNumber + "' And IsDeleted = 0  and FileType='" + ftype + "'", DataPortalQueryType.SqlQuery);
        }
        public DataTable GetDocFileListForPrint(Int64 RequestNumber, string sName)
        {            
            var db = new Database();
            return db.ReturnDataTable("select * from ReqDocument where RequestNumber=" + RequestNumber + " And FileType in(" + sName + ")  and IsDeleted = 0 order by CASE                  FileType WHEN 'I' THEN 1 WHEN 'R' THEN 2 WHEN 'M' THEN 3 END, "
+ " case when charindex('_',DispFileName) = 0 then 0 else SUBSTRING(DispFileName,charindex('_',DispFileName)+1,len(DispFileName)-charindex('_',DispFileName))end desc ,"+
            " case when charindex('_',DispFileName) = 0 then 0 else SUBSTRING(DispFileName,4,charindex('_',DispFileName)-4) end asc", DataPortalQueryType.SqlQuery);
           // return db.ReturnDataTable("select * from ReqDocument where RequestNumber=" + RequestNumber + " And FileType in('" + sName + "')  and IsDeleted = 0", DataPortalQueryType.SqlQuery);
        }
        public DataTable GetThumbNailImg(Int64 RequestNumber)
        {
            var db = new Database();
            return db.ReturnDataTable("select top(1)FileName,ReleaseId,FileExt from ReqDocument where RequestNumber=" + RequestNumber + " And (FileType='R' or FileType='M') and IsDeleted = 0 "
                                      + " order by CASE FileType WHEN 'R' THEN 1 WHEN 'M' THEN 2 END, [FileName] asc", DataPortalQueryType.SqlQuery);           
        }
        public DataTable GetListFilesName(Int64 RequestNumber)
        {
            var db = new Database();
            return db.ReturnDataTable("select * from ReqDocument where RequestNumber=" + RequestNumber + " And IsDeleted = 0 order by DocId asc", DataPortalQueryType.SqlQuery);
        }

        public DataSet GetRlsDocFileList(Int64 RequestNumber, string sName)
        {
            var db = new Database();
            return db.ReturnDataSet("select max(FileVersion) as FileVersion from ReqDocument where RequestNumber=" + RequestNumber + "; " +
            " select * from ReqDocument where RequestNumber=" + RequestNumber + " And fileType= '" + sName + "'  and IsDeleted = 0" +
" order by SUBSTRING(DispFileName,charindex('_',DispFileName)+1,len(DispFileName)-charindex('_',DispFileName)) desc , SUBSTRING(DispFileName,4,charindex('_',DispFileName)-4) asc", DataPortalQueryType.SqlQuery);
        }
        public int UpdateFileVersion(Int64 RequestNumber)
        {
            int retVal =0;
            try
            {
                var db = new Database();
                db.SubmitData("Update ReqDocument set BarcodeStatus=1 where RequestNumber=" + RequestNumber +"", DataPortalQueryType.SqlQuery);
                retVal = 1;
            }
            catch (Exception ex)
            {
                retVal = -1;
            }
            return retVal;
        }
        #endregion


        public enum ScanStatus
        {
            InProcess,
            Done
        }
        public int InsertOption(string field_code, string field_name, string field_value)
        {
            int retVal = 0;
            try
            {
                var db = new Database();
                db.SubmitData("Insert into Options  values ('" + field_code + "','" + field_name + "','" + field_value + "')", DataPortalQueryType.SqlQuery);
                retVal = 0;
            }
            catch (Exception ex)
            {
              
                retVal = -1;
            }
            return retVal;
        }
        public DataTable SelectOptions(string field_code)
        {
            DataTable dt;
            try
            {
                var db = new Database();
              
                dt = db.ReturnDataTable("select * from  Options where field_code ='" + field_code + "'", DataPortalQueryType.SqlQuery);
            }
            catch (Exception ex)
            {

                dt = null;
            }
            return dt;
        }
        public string SelectScanOption(string field_code, string field_name)
        {
            string retVal = "";
            try
            {
                var db = new Database();
                DataTable dt;
                dt = db.ReturnDataTable("select field_value from  Options where field_name='" + field_name + "' and field_code ='" + field_code + "'", DataPortalQueryType.SqlQuery);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        retVal = dt.Rows[0][0].ToString();
                        if (retVal == "DONE")
                        {
                            db.SubmitData("delete from Options  where field_name='" + field_name + "' and field_code ='" + field_code + "'", DataPortalQueryType.SqlQuery);
                        }
                    }
                }
              
        
            }
            catch (Exception ex)
            {

                retVal = "";
            }
            return retVal;
        }
        public int UpdateOption(string field_code, string field_name, string field_value)
        {
            int retVal = 1;
            try
            {
                var db = new Database();
                db.SubmitData("Update Options set field_value=" + field_value + " where field_name='" + field_name + "' and field_code = '"+ field_code +"'", DataPortalQueryType.SqlQuery);
                retVal = 0;
            }
            catch (Exception ex)
            {
                retVal = -1;
            }
            return retVal;
        }


        public Int64 TempAddUpdateDocument(Int64 RequestNumber, string ReleaseCode, string FileName, string DispFileName, int isdeleted, string FileExt, string Filetype)
        {
            Int64 uploadStatus = 0;
            var database = new Database();
            database.AddLongParameter("@RequestNumber", RequestNumber);
            database.AddStringParameter("@ReleaseId", ReleaseCode);
            database.AddStringParameter("@FileName", FileName);
            database.AddStringParameter("@DispFileName", DispFileName);
            database.AddStringParameter("@FileExt", FileExt);
            database.AddStringParameter("@FileType", Filetype);
            database.AddIntParameter("@isdeleted", isdeleted);
            database.AddLongParameter("SaveStatus", 0, true);
           // database.SubmitData("Insert into ReqDocument (RequestNumber,ReleaseId,FileName,DispFileName,IsDeleted,FileExt,FileType)values(" + RequestNumber + ",'" + ReleaseCode + "','" + FileName + "','" + DispFileName + "'," + isdeleted + ",'" + FileExt + "','" + Filetype + "')", DataPortalQueryType.SqlQuery);
            try
            {
                database.SubmitData("TempReqDocumentSaveUpdt", DataPortalQueryType.StoredProcedure);
                uploadStatus = Convert.ToInt64(database.GetParameterValue("SaveStatus"));
            }
            catch (Exception ex)
            {
                uploadStatus = -1;
                ex.ToString();
            }
            return uploadStatus;

        }
        public DataTable getRequestId(string releaseid)
        {
            DataTable dt;
            var db = new Database();
            dt = db.ReturnDataTable("select RequestNumber from  Requests where ReleaseCode ='" + releaseid + "'", DataPortalQueryType.SqlQuery);
            return dt;
        }
        public string GetRequestType(string RequestNum)
        {
            DataTable dt;
            string retvalue = "";
            var db = new Database();
            dt = db.ReturnDataTable("select RequestTypeId from vw_ViewQueue where RequestNumber = ' " + RequestNum + " ' ");
            if (dt.Rows.Count > 0)
            {
                retvalue = dt.Rows[0][0].ToString();
                
            }
            return retvalue;
        }
        public string GetRequestType(string RequestNum,ref string ReleaseReasonId)
        {
            DataTable dt;
            string retvalue = "";
            var db = new Database();
            dt = db.ReturnDataTable("select RequestTypeId,ReleaseReasonId from vw_ViewQueue where RequestNumber = ' " + RequestNum + " ' ");
            if (dt.Rows.Count > 0)
            {
                retvalue = dt.Rows[0][0].ToString();
                ReleaseReasonId = dt.Rows[0][1].ToString();
            }
            return retvalue;
        }

        public string GetRequestTypeAndReleaseReason(string RequestNum, ref string ReleaseReason)
        {
            DataTable dt;
            string retvalue = "";
            var db = new Database();
            
            string ReqType = GetRequestType(RequestNum,ref ReleaseReason);
            dt = db.ReturnDataTable("Select RequestTypes.RequestType,ReleaseReasons.ReleaseReason from ReleaseReasons,RequestTypes  " +
                            " where RequestTypes.RequestTypeId = ReleaseReasons.RequestType and RequestTypes.RequestTypeId = '" + ReqType + "'  " +
                            " and ReleaseReasons.ReleaseReasonId = '" + ReleaseReason + "'");
            if (dt.Rows.Count > 0)
            {
                retvalue = dt.Rows[0][0].ToString();
                ReleaseReason = dt.Rows[0][1].ToString();
            }
            return retvalue;
        }

        public bool CheckGovernmentAgency(string ReqType, string ReleaseReason)
        {
            DataTable dt;
            bool retvalue = false;
            var db = new Database();

            dt = db.ReturnDataTable("Select RequestTypes.RequestType,ReleaseReasons.ReleaseReason from ReleaseReasons,RequestTypes  " +
                            " where RequestTypes.RequestTypeId = ReleaseReasons.RequestType and RequestTypes.RequestTypeId = '" + ReqType + "'  " +
                            " and ReleaseReasons.ReleaseReasonId = '" + ReleaseReason + "'");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "Government Agency" && dt.Rows[0][1].ToString() == "State Disability")
                {
                    retvalue = true;
                }
            }
            return retvalue;
        }

        public string GetRelCode(string requestno)
        {
            DataTable dt;
            string retRC = "";
            var db = new Database();
            dt = db.ReturnDataTable("SELECT ReleaseCode FROM vw_ViewQueue WHERE RequestNumber = '" + requestno + "' ");
            if (dt.Rows.Count > 0)
            {
                retRC = dt.Rows[0][0].ToString();
            }
            return retRC;
        }
        public int GetReqQueueId(string strSearch)
        {
            DataTable dt;
            int QueueId =0;
            var db = new Database();
            dt = db.ReturnDataTable("SELECT QueueStatusId FROM ViewQueueStatus WHERE Item = '" + strSearch + "' ");
            if (dt.Rows.Count > 0)
            {
                QueueId =Convert.ToInt32(dt.Rows[0][0].ToString());
            }
            return QueueId;
        }

        public string GetRequestStatus(string RequestNum)
        {
            DataTable dt;
            string retvalue = "";
            var db = new Database();
            dt = db.ReturnDataTable("select RequestStatus from vw_ViewQueue where RequestNumber = ' " + RequestNum + " ' ");
            if (dt.Rows.Count > 0)
            {
                retvalue = dt.Rows[0][0].ToString();
            }
            return retvalue;
        }

        public static DataTable GetpatientPersonalReleaseData(string PatientID)
        {
            var db = new Database();
            return db.ReturnDataTable("select TOP 1 vq.AttentionTo, vq.CaseNumber, vq.Address1, vq.City, vq.State, vq.ZIP, vq.RequestorId, vq.Fax, " +
                " vq.PhoneNumber, vq.EmailAddress,vq.RequestorLoginId from vw_ViewQueue vq "
                + " where (vq.ReleaseReason='Patient Personal Release' or vq.ReleaseReason='Patient Personal - Print On Site') and vq.RequestType = 'Patient Service' and vq.PatientId = " + PatientID + " ORDER BY vq.RequestNumber DESC ", DataPortalQueryType.SqlQuery);
        }
               
    }
}
