using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;
using System.Data;

namespace BussinessLogicLayer
{
   public class CompleteAndSend
    {
       public int InsertPatient(bulkData Pinfo, out Int64 PatientId)
       {
           int InUpStatus = 0;
           PatientId = -1; 
           try
           {
               var database = new Database();
               database.AddStringParameter("@PFirstName", Pinfo.PFirstName);
               database.AddStringParameter("@PLastName", Pinfo.PLastName);
               database.AddDateTimeParameter("@PDOB", Pinfo.PDOB);
               database.AddStringParameter("@PSSN", Pinfo.PSSN);       
               database.AddStringParameter("@PPhone", Pinfo.PPhone);
               database.AddStringParameter("@PMobile", Pinfo.PMobile);
               database.AddStringParameter("@PFax", Pinfo.PFax);
               database.AddStringParameter("@PEmailAdds", Pinfo.PEmailAdds);
               database.AddStringParameter("@PNotes", Pinfo.PNotes);
               database.AddStringParameter("@PMedicalRecord", Pinfo.MedicalRecordNumber);
               database.AddLongParameter("PatientId", 0, true);
               database.AddIntParameter("InUpStatus", 0, true);
               database.SubmitData("sp_AddOrUpdatePatient", DataPortalQueryType.StoredProcedure);
               PatientId = Convert.ToInt32(database.GetParameterValue("PatientId"));
               InUpStatus = Convert.ToInt32(database.GetParameterValue("InUpStatus"));              
           }
           catch (Exception ex)
           {
               InUpStatus = 0;
               PatientId = -1;
               ex.ToString();
           }
           return InUpStatus;
       }
       public int InsertRequestor(bulkData Rinfo, out Int64 RequestorId, out Int64 ReqsLoginId, out int LogInUpStatus)
       {
           int InUpStatus = 0;
           RequestorId = -1;
           LogInUpStatus = 0;
           ReqsLoginId = -1;
           try
           {
               var database = new Database();
               database.AddStringParameter("@ReqName", Rinfo.ReqName);
               database.AddStringParameter("@rAttentionTo", Rinfo.rAttentionTo);
               database.AddStringParameter("@rAddress1", Rinfo.rAddress1);
               database.AddStringParameter("@rAddress2", Rinfo.rAddress2);
               database.AddStringParameter("@rCity", Rinfo.rCity);
               database.AddIntParameter("@rState", Rinfo.rState);
               database.AddStringParameter("@rZip", Rinfo.rZip);
               database.AddStringParameter("@rFax", Rinfo.rFax);
               database.AddStringParameter("@rPhone", Rinfo.rPhone);
               database.AddStringParameter("@rMobile", Rinfo.rMobile);
               database.AddStringParameter("@rEmailAdds", Rinfo.rEmailAdds);
               database.AddLongParameter("@ReqIdForEdit", Rinfo.ReqIdForEdit);
               database.AddLongParameter("RequestorId", 0, true);
               database.AddLongParameter("InUpStatus", 0, true);
               database.AddLongParameter("ReqsLoginId", 0, true);
               database.AddLongParameter("LogInUpStatus", 0, true);

               database.SubmitData("sp_AddOrUpdateRequestor", DataPortalQueryType.StoredProcedure);
               RequestorId = Convert.ToInt32(database.GetParameterValue("RequestorId"));
               InUpStatus = Convert.ToInt32(database.GetParameterValue("InUpStatus"));
               ReqsLoginId = Convert.ToInt32(database.GetParameterValue("ReqsLoginId"));
               LogInUpStatus = Convert.ToInt32(database.GetParameterValue("LogInUpStatus"));  
           }
           catch (Exception ex)
           {
               InUpStatus = 0;
               RequestorId = -1;
               LogInUpStatus = 0;
               ReqsLoginId = -1;
               ex.ToString();
           }
           return InUpStatus;
       }
       public Int64 AddInvoice( InvoiceDetails objinvoice)
       {
           Int64 InvoiceId = -1;
           try
           {
               var database = new Database();
               database.AddStringParameter("@ReleaseCode", objinvoice.ReleaseCode);
               database.AddStringParameter("@AmountDue", objinvoice.AmountDue.ToString());
               database.AddStringParameter("@AmountPaid", objinvoice.AmountDue.ToString());
               database.AddDateTimeParameter("@DueDate", objinvoice.DueDate);
               database.AddDateTimeParameter("@PaidDate", objinvoice.PaidDate);
               database.AddStringParameter("@TotalAmount", objinvoice.TotalAmount.ToString());          
               database.AddIntParameter("@InvoiceStatus", objinvoice.InvoiceStatus);
               database.AddIntParameter("@InvoiceBillable", objinvoice.InvoiceBillable);

               database.AddStringParameter("@BillingInfo1",objinvoice.BillingInfo1);
               database.AddStringParameter("@BillingInfo2", objinvoice.BillingInfo2);
               database.AddStringParameter("@BillingInfo3",objinvoice.BillingInfo3);
               database.AddStringParameter("@BillingInfo4", objinvoice.BillingInfo4);
               database.AddStringParameter("@BillingInfo5", objinvoice.BillingInfo5);
               database.AddStringParameter("@BillingInfo6", objinvoice.BillingInfo6);
               database.AddStringParameter("@BillingInfo7", objinvoice.BillingInfo7);
               database.AddStringParameter("@BillingInfo8", objinvoice.BillingInfo8);
               database.AddStringParameter("@BillingInfo9", objinvoice.BillingInfo9);
               database.AddStringParameter("@BillingInfo10", objinvoice.BillingInfo10);

               database.AddStringParameter("@PatientInfo1", objinvoice.PatientInfo1);
               database.AddStringParameter("@PatientInfo2", objinvoice.PatientInfo2);
               database.AddStringParameter("@PatientInfo3", objinvoice.PatientInfo3);
               database.AddStringParameter("@PatientInfo4", objinvoice.PatientInfo4);
               database.AddStringParameter("@PatientInfo5", objinvoice.PatientInfo5);
               database.AddStringParameter("@PatientInfo6", objinvoice.PatientInfo6);
               database.AddStringParameter("@PatientInfo7", objinvoice.PatientInfo7);
               database.AddStringParameter("@PatientInfo8", objinvoice.PatientInfo8);
               database.AddStringParameter("@PatientInfo9", objinvoice.PatientInfo9);

               database.AddStringParameter("@ShippingInfo1", objinvoice.ShippingAdds1);
               database.AddStringParameter("@ShippingInfo2", objinvoice.ShippingAdds2);
               database.AddStringParameter("@ShippingInfo3", objinvoice.ShippingAdds3);
               database.AddStringParameter("@ShippingInfo4", objinvoice.ShippingAdds4);
               database.AddStringParameter("@ShippingInfo5", objinvoice.ShippingAdds5);
               database.AddStringParameter("@ShippingInfo6", objinvoice.ShippingAdds6);
               database.AddStringParameter("@ShippingInfo7", objinvoice.ShippingAdds7);
               database.AddStringParameter("@ShippingInfo8", objinvoice.ShippingAdds8);
               database.AddStringParameter("@ShippingInfo9", objinvoice.ShippingAdds9);
               database.AddStringParameter("@ShippingInfo10", objinvoice.ShippingAdds10);

               database.AddLongParameter("InvoiceId", 0, true);
               database.SubmitData("sp_InsertInvoice", DataPortalQueryType.StoredProcedure);
               InvoiceId = Convert.ToInt32(database.GetParameterValue("InvoiceId"));              
           }
           catch (Exception ex)
           {             
               InvoiceId = -1;
               ex.ToString();
           }
           return InvoiceId;
       }
       public Int64 AddInvoiceItems(Int64 InvoiceId, DataTable Inv, out List<Int64> InvoiceItemIds)
       {
           InvoiceItemIds = new List<Int64>();
           Int64 invItemId = -1;          
           try
           {
               for (int i = 0; i <Inv.Rows.Count; i++)
               {
                   var database = new Database();
                   database.AddLongParameter("@InvoiceId", InvoiceId);
                   database.AddStringParameter("@ItemText", Inv.Rows[i]["Activity"].ToString());
                   database.AddIntParameter("@ItemQuantity", Convert.ToInt32(Inv.Rows[i]["Quantity"].ToString()));
                   database.AddStringParameter("@ItemUnitPrice", Inv.Rows[i]["Rate"].ToString());
                   database.AddStringParameter("@ItemPrice", Inv.Rows[i]["Amount"].ToString());
                   database.AddLongParameter("invItemId", 0, true);
                   database.SubmitData("sp_InsertItems", DataPortalQueryType.StoredProcedure);
                   invItemId = Convert.ToInt32(database.GetParameterValue("invItemId"));
                   if (invItemId>0)
                   {
                       InvoiceItemIds.Add(invItemId);
                   }
                   else
                   {
                       RollBackInvoiceItems(InvoiceItemIds);
                       InvoiceItemIds.Clear();
                   }
               }             
           }
           catch (Exception ex)
           {
               invItemId = -1;
               ex.ToString();
           }
           return invItemId;
       }
       public void AddRequest(RequestData ReqData, out Int64 requestId)
       {
           requestId = -1; 
           try
           {
               var database = new Database();
               database.AddLongParameter("@MedicalFacilityRefNo", ReqData.MedicalFacilityRefNo);
               database.AddStringParameter("@ReleaseCode", ReqData.ReleaseCode);
               database.AddLongParameter("@PatientId", ReqData.PatientId);
               database.AddLongParameter("@RequestorId", ReqData.RequestorId);
               database.AddIntParameter("@RequestType", ReqData.RequestType);
               database.AddIntParameter("@ReleaseReason", ReqData.ReleaseReason);
               database.AddLongParameter("@SpecialProcessInfoId", ReqData.SpecialProcessInfoId);
               database.AddStringParameter("@ReleaseLetterRefNo", ReqData.ReleaseLetterRefNo);
               database.AddStringParameter("@RejectionLetterRefNo", ReqData.RejectionLetterRefNo);
               database.AddIntParameter("@RequestStatus", ReqData.RequestStatus);
               database.AddStringParameter("@RequestAccess", ReqData.RequestAccess);
               database.AddLongParameter("@CreatedBy", ReqData.CreatedBy);
               database.AddDateTimeParameter("@DateCompleted", ReqData.DateCompleted);
               database.AddDateTimeParameter("@ActualDateReceived", DateTime.Now);             
               database.AddLongParameter("@InvoiceId", ReqData.InvoiceId);
               database.AddStringParameter("@AccountNumber", ReqData.AccountNumber);
               database.AddStringParameter("@MedicalRecordNumber1", ReqData.MedicalRecordNumber1);
               database.AddDateTimeParameter("@DateOfService", ReqData.DateOfService);              
               database.AddStringParameter("@Auth_Pass_PDF_User", ReqData.Auth_Pass_PDF_User);
               database.AddStringParameter("@Auth_Pass_PDF_Owner", ReqData.Auth_Pass_PDF_Owner);
               database.AddStringParameter("@Auth_Pass_ZIP", ReqData.Auth_Pass_ZIP);
               database.AddLongParameter("@InvoiceNumber", ReqData.InvoiceNumber);
               database.AddLongParameter("@RequestorLoginId", ReqData.RequestorLoginId);
               database.AddIntParameter("@BillToId", ReqData.BillToId);
               database.AddStringParameter("@InvoiceNumberTag", ReqData.InvoiceNumberTag);
               database.AddLongParameter("requestId", 0, true);
               database.SubmitData("sp_InsertRequest", DataPortalQueryType.StoredProcedure);
               requestId = Convert.ToInt32(database.GetParameterValue("requestId"));
           }
           catch (Exception ex)
           {              
               requestId = -1;
               ex.ToString();
           }          
       }
       public void RollBackPatient(Int64 patientId,int flg=1)
       {
           var database = new Database();
           database.AddLongParameter("@PatientId", patientId);
           database.AddIntParameter("@Flg", flg);
           database.SubmitData("sp_RollBackRequest", DataPortalQueryType.StoredProcedure);
       }
       public void RollBackRequestor(Int64 RequestorId, int flg = 2)
       {
           var database = new Database();
           database.AddLongParameter("@RequestorId", RequestorId);
           database.AddIntParameter("@Flg", flg);
           database.SubmitData("sp_RollBackRequest", DataPortalQueryType.StoredProcedure);
       }
       public void RollBackInvoice(Int64 InvoiceId, int flg = 3)
       {
           var database = new Database();
           database.AddLongParameter("@InvoiceId", InvoiceId);
           database.AddIntParameter("@Flg", flg);
           database.SubmitData("sp_RollBackRequest", DataPortalQueryType.StoredProcedure);
       }
       public void RollBackInvoiceItem(Int64 InvoiceId, int flg = 4)
       {
           var database = new Database();
           database.AddLongParameter("@InvoiceId", InvoiceId);
           database.AddIntParameter("@Flg", flg);
           database.SubmitData("sp_RollBackRequest", DataPortalQueryType.StoredProcedure);
       } 
       private void RollBackInvoiceItems(List<long> itmIds)
       {
           string ids = "";
           foreach (long id in itmIds)
           {
               if (!string.IsNullOrWhiteSpace(ids))
                   ids += "," + id.ToString();
               else
                   ids = id.ToString();
           }
           string strQ = "Delete From InvoiceItems Where InvoiceItemId in (" + ids + ")";
           var db = new Database();        
           db.SubmitData(strQ, DataPortalQueryType.SqlQuery); 
       }
       public void DeleteInvoiceItems(Int64 InvIds)
       {
           string strQ = "Delete From InvoiceItems Where InvoiceId=" + InvIds + " And ItemText!='Basic Fee'";
           var db = new Database();
           db.SubmitData(strQ, DataPortalQueryType.SqlQuery);
       }
       public void RollBackRequestorLogin(Int64 ReqsLoginId, int flg = 6)
       {
           var database = new Database();
           database.AddLongParameter("@ReqLoginId", ReqsLoginId);
           database.AddIntParameter("@Flg", flg);
           database.SubmitData("sp_RollBackRequest", DataPortalQueryType.StoredProcedure);
       }
       public Int64 UpdateInvoice(InvoiceDetails objinvoice, Int64 Invoiceid, Int64 RequestNumber, Int64 addedBy, string strnotes)
       {
           Int64 UpdatedId = -1;
           try
           {
               var database = new Database();
               database.AddLongParameter("@RequestNumber", RequestNumber);
               database.AddLongParameter("@InvoiceId", Invoiceid);
               database.AddStringParameter("@ReleaseCode", objinvoice.ReleaseCode);
               database.AddStringParameter("@AmountDue", objinvoice.AmountDue.ToString());
               database.AddStringParameter("@AmountPaid", objinvoice.AmountDue.ToString());
               database.AddDateTimeParameter("@DueDate", objinvoice.DueDate);
               database.AddDateTimeParameter("@PaidDate", objinvoice.PaidDate);
               database.AddStringParameter("@TotalAmount", objinvoice.TotalAmount.ToString());
               database.AddIntParameter("@InvoiceStatus", objinvoice.InvoiceStatus);
               database.AddIntParameter("@InvoiceBillable", objinvoice.InvoiceBillable);
               database.AddLongParameter("@AddedBy", addedBy);
               database.AddStringParameter("@Notes", strnotes);
               database.AddLongParameter("UpdatedId", 0, true);
               database.SubmitData("sp_UpdateInvoice", DataPortalQueryType.StoredProcedure);
               UpdatedId = Convert.ToInt32(database.GetParameterValue("UpdatedId"));
           }
           catch (Exception ex)
           {
               UpdatedId = -1;
               ex.ToString();
           }
           return UpdatedId;
       }

       public Int64 UpdateInvoicetoStoreQA(Int64 RequestNumber, Int64 RequestType)
       {
           Int64 UpdatedID = -1;
           try
           {
               var database = new Database();
               database.AddLongParameter("@RequestNumber", RequestNumber);
               database.AddLongParameter("@RequestType", RequestType);
               database.AddLongParameter("UpdatedID", 0, true);
               database.SubmitData("sp_UpdateInvoicetoQAStore", DataPortalQueryType.StoredProcedure);
               UpdatedID = Convert.ToInt64(database.GetParameterValue("UpdatedID"));
           }
           catch (Exception ex)
           {
               UpdatedID = -1;
               ex.ToString();
           }
           return UpdatedID;
       }
   }
}
