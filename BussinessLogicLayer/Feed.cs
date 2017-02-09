using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccessLayer;

namespace BussinessLogicLayer
{
    public class Feed
    {
        public static DataSet GetContactList()
        {
            var db = new Database();
            return db.ReturnDataSet("GetContactList", DataPortalQueryType.StoredProcedure);
        }
       
        public static bool CreateFeed(feedDetails feedDetail, out Int64 FeedId)
        {
            try
            {
                var db = new Database();
                db.AddLongParameter("@UserId", feedDetail.UserId);
                db.AddStringParameter("@Feed", feedDetail.Feed);
                db.AddBitParameter("@SendToList", feedDetail.SendToList);
                db.AddStringParameter("@SendTo", feedDetail.SendTo);
                db.AddLongParameter("FeedId", 0, true);
                db.SubmitData("sp_CreateFeed", DataPortalQueryType.StoredProcedure);
                FeedId = Convert.ToInt64(db.GetParameterValue("FeedId"));
                return true;
            }
            catch (Exception ex)
            {
                FeedId = 0;
                return false;
            }
        }
        public static DataSet GetUserIdbyUserName(string UserName)
        {
            var dl = new Database();
            DataSet ds = dl.ReturnDataSet("Select * from  dbo.UserLoginInfo where UserName='" + UserName + "'", DataPortalQueryType.SqlQuery);
            return ds;
        }

        public static DataSet GetInvoiceHtmlById(Int64 InvoiceId)
        {
            var dl = new Database();
            DataSet ds = dl.ReturnDataSet("Select InvoiceHTML from dbo.Invoices where InvoiceId='" + InvoiceId + "'", DataPortalQueryType.SqlQuery);
            return ds;
        }

        public static DataSet GetFeedListByUserId(Int64 UserId)
        {
            var dl = new Database();
            DataSet ds = dl.ReturnDataSet("select * from Feeds where SentTo ='" + UserId + "' and SentToList=0 and Len(Rtrim(Ltrim(Feed))) > 0", DataPortalQueryType.SqlQuery);
            return ds;
        }
        public static DataSet GetUserByUserId(Int64 UserId)
        {
            var dl = new Database();
            DataSet ds = dl.ReturnDataSet("select UserName from dbo.UserLoginInfo where RecID=" + UserId + "", DataPortalQueryType.SqlQuery);
            return ds;
        }

        public static DataSet GetRelCodeById(string relCode)
        {
            var dl = new Database();
            DataSet ds = dl.ReturnDataSet("select top(1) ReleaseCode,Auth_Pass_PDF_User,Auth_Pass_PDF_Owner from Requests where ReleaseCode='" + relCode + "'", DataPortalQueryType.SqlQuery);
            return ds;
        }
    }
}
