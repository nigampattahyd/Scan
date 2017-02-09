using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccessLayer;

namespace BussinessLogicLayer
    {
  public  class ReleaseReason
        {

        public static DataTable GetAllReleaseReasonByReferenceNumber(Int64 refNumber)
            {
            var dl = new Database();
            return dl.ReturnDataTable("Select * from ReleaseReasons where MedicalFacilityRefNo=" + refNumber, DataPortalQueryType.SqlQuery);
            }

        public static DataTable GetReleaseReasonByRequestorType(Int64 refNumber,int requestor)
            {
            var dl = new Database();
            string Query = " Select * from ReleaseReasons where MedicalFacilityRefNo=" + refNumber + " and RequestorType= " + requestor;
            return dl.ReturnDataTable(Query, DataPortalQueryType.SqlQuery);
            }

        public static void DeleteReleaseReason(int ReleaseReason)
            {
            var db = new Database();

            db.SubmitData("delete from ReleaseReasons where ReleaseReasonId=" + ReleaseReason, DataPortalQueryType.SqlQuery);
            }

        public static void UpdateReleaseReason(ReleaseReasonStuff releaseReason)
            {
            string Query;
            var db = new Database();
            if (releaseReason.Default == true)
                {
                Query = "update ReleaseReasons set DefaultValue=0 where MedicalFacilityRefNo=" + releaseReason.MedicalReferenceNumber + " and RequestorType=" + releaseReason.RequestorType;
                db.SubmitData(Query, DataPortalQueryType.SqlQuery);
                }
            string requestor=" ";
            if (releaseReason.RequestorType != 0)
                requestor = " RequestorType=" + releaseReason.RequestorType +",";
            Query = " update ReleaseReasons set MedicalFacilityRefNo=" + releaseReason.MedicalReferenceNumber + ", ReleaseReason='" +
                        releaseReason.ReleaseReason + "',"+
                        requestor +
                        "  Active=" + Convert.ToInt32(releaseReason.Active) + ", DefaultValue=" + Convert.ToInt32(releaseReason.Default)
                        + ", CreatedOn=getdate() where ReleaseReasonId=" + releaseReason.ReleaseReasonId;
            db.SubmitData(Query, DataPortalQueryType.SqlQuery);
            }

        public static void InsertReleaseReason(ReleaseReasonStuff releaseReason)
            {
            string Query;
            var db = new Database();
            if (releaseReason.Default == true)
                {
                Query = "update ReleaseReasons set DefaultValue = 0 where MedicalFacilityRefNo=" + releaseReason.MedicalReferenceNumber + " and RequestorType="+releaseReason.RequestorType;
                db.SubmitData(Query, DataPortalQueryType.SqlQuery);
                }
            Query = " insert into ReleaseReasons (MedicalFacilityRefNo,ReleaseReason,RequestorType,Active,DefaultValue,CreatedOn) values(" + releaseReason.MedicalReferenceNumber + ",'" +
                        releaseReason.ReleaseReason + "', " +
                        releaseReason.RequestorType + ", " +
                        Convert.ToInt32(releaseReason.Active) + ", " +
                        Convert.ToInt32(releaseReason.Default)
                        + ",getdate())";
            db.SubmitData(Query, DataPortalQueryType.SqlQuery);
            }

        }
    }
