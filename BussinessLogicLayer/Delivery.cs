using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccessLayer;

namespace BussinessLogicLayer
    {
   public class Delivery
        {

       public static DataTable GetAllDeliveryTypesByReferenceNumber(Int64 refNumber)
            {
            var dl = new Database();
            return dl.ReturnDataTable("Select * from DeliveryTypes where MedicalFacilityRefNo=" + refNumber, DataPortalQueryType.SqlQuery);
            }

       public static void DeleteDeliveryTypes(int deliveryTypes)
            {
            var db = new Database();

            db.SubmitData("delete from DeliveryTypes where DeliveryTypeId=" + deliveryTypes, DataPortalQueryType.SqlQuery);
            }

       public static void UpdateDeliveryTypes(DeliveryTypes deliveryType)
            {
            string Query;
            var db = new Database();
            if (deliveryType.Default == true)
                {
                Query = "update DeliveryTypes set DefaultValue=0 where MedicalFacilityRefNo=" + deliveryType.MedicalReferenceNumber;
                db.SubmitData(Query, DataPortalQueryType.SqlQuery);
                }
            Query = " update DeliveryTypes set MedicalFacilityRefNo=" + deliveryType.MedicalReferenceNumber + ", DeliveryType='" +
                        deliveryType.DeliveryType + "', Active=" + Convert.ToInt32(deliveryType.Active) + ", DefaultValue=" + Convert.ToInt32(deliveryType.Default)
                        + ", CreatedOn=getdate() where DeliveryTypeId=" + deliveryType.DeliveryTypeId;
            db.SubmitData(Query, DataPortalQueryType.SqlQuery);
            }

       public static void InsertDeliveryTypes(DeliveryTypes deliveryType)
            {
            string Query;
            var db = new Database();
            if (deliveryType.Default == true)
                {
                Query = "update DeliveryTypes set DefaultValue = 0 where MedicalFacilityRefNo=" + deliveryType.MedicalReferenceNumber;
                db.SubmitData(Query, DataPortalQueryType.SqlQuery);
                }
            Query = " insert into DeliveryTypes (MedicalFacilityRefNo,DeliveryType,Active,DefaultValue,CreatedOn) values(" + deliveryType.MedicalReferenceNumber + ",'" +
                        deliveryType.DeliveryType + "', " +
                        Convert.ToInt32(deliveryType.Active) + ", " +
                        Convert.ToInt32(deliveryType.Default)
                        + ",getdate())";
            db.SubmitData(Query, DataPortalQueryType.SqlQuery);
            }
        }
    }
