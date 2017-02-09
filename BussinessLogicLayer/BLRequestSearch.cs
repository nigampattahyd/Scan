using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccessLayer;

namespace BussinessLogicLayer
{
   public class BLRequestSearch
   {
       #region PaymentHistory
       public DataTable GetAllPaymentMethods()
       {
           var dl = new Database();
           dl.AddIntParameter("@Flag", 1);
           return dl.ReturnDataTable("GetPaymentHistory", DataPortalQueryType.StoredProcedure);
       }
       public Int64 SavePaymentEntry(Int64 InvoiceId,DateTime pmtDate,double Amount,int pmtMethodId,string ReferenceNumber,string Notes,string uid)
       {
           Int64 PmtId = 0;
           var database = new Database();
           database.AddLongParameter("@InvoiceId", InvoiceId);
           database.AddDateTimeParameter("@pmtDate", pmtDate);
           database.AddStringParameter("@Amount", Amount.ToString());
           database.AddIntParameter("@pmtMethodId", pmtMethodId);
           database.AddStringParameter("@ReferenceNumber", ReferenceNumber);         
           database.AddStringParameter("@Notes", Notes);
           database.AddLongParameter("@UserId", Convert.ToInt64(uid));
           database.AddLongParameter("PmtId", 0, true);
           try
           {
               database.SubmitData("SavePaymentEntry", DataPortalQueryType.StoredProcedure);
               PmtId = Convert.ToInt64(database.GetParameterValue("PmtId"));
           }
           catch (Exception ex)
           {
               PmtId = -1;
               ex.ToString();
           }
           return PmtId;
       }
       public DataTable GetAllPaymenHistory(Int64 invoiceNum)
       {
           var dl = new Database();
           dl.AddLongParameter("@InvoiceId", invoiceNum);
           dl.AddIntParameter("@Flag", 2);
           return dl.ReturnDataTable("GetPaymentHistory", DataPortalQueryType.StoredProcedure);
       }
       #endregion
   }
}
