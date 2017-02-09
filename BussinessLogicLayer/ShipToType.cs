using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccessLayer;

namespace BussinessLogicLayer
    {
   public class ShipToType
        {
        public static DataTable GetAllShipToTypeByReferenceNumber(Int64 refNumber)
            {
            var dl = new Database();
            return dl.ReturnDataTable("Select * from ShipToTypes where MedicalFacilityRefNo=" + refNumber, DataPortalQueryType.SqlQuery);
            }

        public static void DeleteShipToType(int ShipToTypes)
        {
        var db = new Database();

        db.SubmitData("delete from ShipToTypes where ShipToTypeId=" + ShipToTypes, DataPortalQueryType.SqlQuery); 
            }

        public static void UpdateShipToType(ShippingType shippingType)
            {
            string Query;
            var db = new Database();
            if (shippingType.Default == true)
                {
                Query = "update ShipToTypes set DefaultValue=0 where MedicalFacilityRefNo=" + shippingType.MedicalReferenceNumber; 
                db.SubmitData(Query, DataPortalQueryType.SqlQuery);
                }
            Query = " update ShipToTypes set MedicalFacilityRefNo=" + shippingType.MedicalReferenceNumber + ", ShipToText='" +
                        shippingType.ShipToText + "', Active=" + Convert.ToInt32(shippingType.Active) + ", DefaultValue=" +Convert.ToInt32(shippingType.Default)
                        + ", CreatedOn=getdate() where ShipToTypeId=" + shippingType.ShipToTypeId;
            db.SubmitData(Query, DataPortalQueryType.SqlQuery);
            }

        public static void InsertShipToType(ShippingType shippingType)
        {
        string Query;
        var db = new Database();
        if (shippingType.Default == true)
            {
            Query = "update ShipToTypes set DefaultValue = 0 where MedicalFacilityRefNo="+shippingType.MedicalReferenceNumber;
            db.SubmitData(Query, DataPortalQueryType.SqlQuery);
            }
        Query = " insert into ShipToTypes (MedicalFacilityRefNo,ShipToText,Active,DefaultValue,CreatedOn) values(" + shippingType.MedicalReferenceNumber + ",'" +
                    shippingType.ShipToText + "', " +
                    Convert.ToInt32(shippingType.Active) + ", " +
                    Convert.ToInt32(shippingType.Default)
                    + ",getdate())";
        db.SubmitData(Query, DataPortalQueryType.SqlQuery);
            }
        }
    }
