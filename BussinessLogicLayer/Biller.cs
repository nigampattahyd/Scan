using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccessLayer;

namespace BussinessLogicLayer
    {
   public class Biller
        {
       public static DataTable GetAllBillerTypesByReferenceNumber(Int64 refNumber)
            {
            var dl = new Database();
            return dl.ReturnDataTable("Select * from BillerTypes where MedicalFacilityRefNo=" + refNumber, DataPortalQueryType.SqlQuery);
            }

       public static void DeleteBillerTypes(int billerTypeId)
            {
            var db = new Database();

            db.SubmitData("delete from BillerTypes where BillerTypeId=" + billerTypeId, DataPortalQueryType.SqlQuery);
            }

       public static void UpdateBillerTypes(BillerTypes billerTypes)
            {
            string Query;
            var db = new Database();
            if (billerTypes.Default == true)
                {
                Query = "update BillerTypes set DefaultValue=0 where MedicalFacilityRefNo=" + billerTypes.MedicalReferenceNumber;
                db.SubmitData(Query, DataPortalQueryType.SqlQuery);
                }
            Query = " update BillerTypes set MedicalFacilityRefNo=" + billerTypes.MedicalReferenceNumber + ", Biller='" +
                        billerTypes.Biller + "', Active=" + Convert.ToInt32(billerTypes.Active) + ", DefaultValue=" + Convert.ToInt32(billerTypes.Default)
                        + ", CreatedOn=getdate() where BillerTypeId=" + billerTypes.BillerTypeId;
            db.SubmitData(Query, DataPortalQueryType.SqlQuery);
            }

       public static void InsertBillerTypes(BillerTypes billerTypes)
            {
            string Query;
            var db = new Database();
            if (billerTypes.Default == true)
                {
                Query = "update BillerTypes set DefaultValue = 0 where MedicalFacilityRefNo=" + billerTypes.MedicalReferenceNumber;
                db.SubmitData(Query, DataPortalQueryType.SqlQuery);
                }
            Query = " insert into BillerTypes (MedicalFacilityRefNo,Biller,Active,DefaultValue,CreatedOn) values(" + billerTypes.MedicalReferenceNumber + ",'" +
                        billerTypes.Biller + "', " +
                        Convert.ToInt32(billerTypes.Active) + ", " +
                        Convert.ToInt32(billerTypes.Default)
                        + ",getdate())";
            db.SubmitData(Query, DataPortalQueryType.SqlQuery);
            }

        }
    }
