using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;
using System.Data;
using System.IO;
using System.Net;


namespace BussinessLogicLayer
{
   public class UploadBulkData
    {
        public static bool SaveBulkData(bulkData blkData,int RecorsStorage,out Int64 savestatus)
        {
            var database = new Database();
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
            database.AddStringParameter("@rAddress2", blkData.rAddress2);
            database.AddStringParameter("@rCity", blkData.rCity);
            database.AddIntParameter("@rState", blkData.rState);
            database.AddStringParameter("@rZip", blkData.rZip);
            database.AddStringParameter("@rFax", blkData.rFax);
            database.AddStringParameter("@rPhone", blkData.rPhone);
            database.AddStringParameter("@rMobile", blkData.rMobile);
            database.AddStringParameter("@rEmailAdds", blkData.rEmailAdds);
            database.AddIntParameter("@rRequestType", blkData.rRequestType);
            database.AddIntParameter("@rReleaseReason", blkData.rReleaseReason);
            database.AddLongParameter("@UserId", blkData.UserId);
            database.AddLongParameter("@FileID", blkData.FieldId);
            database.AddLongParameter("@MedicalFacilityRefNo", blkData.MedicalFacilityRefNo);
            database.AddIntParameter("@BillToId", blkData.RBillTo);
            database.AddStringParameter("@StateName", blkData.ShippingInfo1);
            database.AddStringParameter("@RequestTypeName", blkData.RequestTypeName);
            database.AddStringParameter("@ReleaseReasonName", blkData.ReleaseReasonName);
            database.AddStringParameter("@BillToName", blkData.BillToName);
            database.AddDateTimeParameter("@ProcDate", blkData.ProcessDate);
            database.AddStringParameter("@ReqStatus", blkData.Status);
            database.AddDateTimeParameter("@UploadDate", blkData.UploadDate);
            database.AddStringParameter("@EnteredBy", blkData.EnteredBy);
            database.AddStringParameter("@MRPageCount", blkData.MRPageCount.ToString());
            database.AddIntParameter("@RecorsStorage", RecorsStorage);           
            database.AddLongParameter("SaveStatus", 0, true);
            try
            {
                database.SubmitData("sp_SaveBulkData", DataPortalQueryType.StoredProcedure);
                savestatus = Convert.ToInt64(database.GetParameterValue("SaveStatus"));
                return true;
            }
            catch (Exception ex)
            {
                savestatus = 0;
                return false;
            }
        }

        public static bool SaveBulkDataByPDFFile(bulkData blkData, string releaseid, out Int64 savestatus)
        {
            DateTime dob = DateTime.Now;
            var database = new Database();
            database.AddStringParameter("@PFirstName", blkData.PFirstName);
            database.AddStringParameter("@PLastName", blkData.PLastName);
            database.AddDateTimeParameter("@PDOB", blkData.PDOB);
            database.AddLongParameter("@MedicalFacilityRefNo", blkData.MedicalFacilityRefNo);
            database.AddLongParameter("@UserId", blkData.UserId);
            database.AddStringParameter("@FileID", releaseid);
            database.AddDateTimeParameter("@ProcDate", blkData.ProcessDate);
            database.AddDateTimeParameter("@UploadDate", blkData.UploadDate);
            database.AddIntParameter("@MRPageCount", blkData.MRPageCount);           
            database.AddLongParameter("SaveStatus", 0, true);
            try
            {
                database.SubmitData("sp_SaveBulkDataByPDFFile", DataPortalQueryType.StoredProcedure);
                savestatus = Convert.ToInt64(database.GetParameterValue("SaveStatus"));
                return true;
            }
            catch (Exception ex)
            {
                savestatus = 0;
                return false;
            }

        }
       
        public static DataSet stam(string xmlData)
        {
            StringReader theReader = new StringReader(xmlData);
            DataSet theDataSet = new DataSet();
            theDataSet.ReadXml(theReader);
            return theDataSet;
        }
        public static int CountStringOccurrences(string text, string pattern)
        {
            // Loop through all instances of the string 'text'.
            int count = 0;
            int i = 0;
            while ((i = text.IndexOf(pattern, i)) != -1)
            {
                i += pattern.Length;
                count++;
            }
            return count;
        }
        public static string UploadToRemoteServer(string FTPServer, string _Fname, string fileToUpload, string user, string password)
        {
            string statusDescription = string.Empty;         
            //  ManualResetEvent waitObject;
            Uri target = new Uri(FTPServer + @"/" + _Fname);
            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(target);
            request.Method = WebRequestMethods.Ftp.UploadFile;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential(user, password);

            // Copy the contents of the file to the request stream.
            StreamReader sourceStream = new StreamReader(fileToUpload);
            byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            sourceStream.Close();
            request.ContentLength = fileContents.Length;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);
            requestStream.Close();

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            statusDescription = response.StatusDescription;
            //  Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);

            response.Close();
            return statusDescription;
        }
        public static string FtpMakeDirectory(string directoryPath, string ftpUser, string ftpPassword)
        {
            string IsCreated = "";
            try
            {
                WebRequest request1 = WebRequest.Create(directoryPath);
                request1.Method = WebRequestMethods.Ftp.MakeDirectory;
                request1.Credentials = new NetworkCredential(ftpUser, ftpPassword);
                using (var resp = (FtpWebResponse)request1.GetResponse())
                {
                    IsCreated = resp.StatusCode.ToString();
                }
            }
            catch (WebException ex)
            {
                ex.ToString();
            }
            return IsCreated;
        }
        public static bool FtpDirectoryExists(string directoryPath, string ftpUser, string ftpPassword)
        {
            bool IsExists = true;
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(directoryPath);
                request.Credentials = new NetworkCredential(ftpUser, ftpPassword);
                request.Method = WebRequestMethods.Ftp.GetDateTimestamp;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                IsExists = false;
            }
            return IsExists;
        }

        public static Int64 SaveFileUploadStatus(Int64 reqNo, string FieldId, string FolderName, string AllFileName, int fstatus, Int64 UserId, Int64 TotalRecords,string FilePath, out Int64 uploadStatus)
        {          
            var database = new Database();
            database.AddStringParameter("@RequestNumber", reqNo.ToString());
            database.AddStringParameter("@FieldId", FieldId);
            database.AddStringParameter("@FilePath", FilePath);
            database.AddStringParameter("@FileName", AllFileName);
            database.AddIntParameter("@Status", fstatus);
            database.AddStringParameter("@FolderName", FolderName);
            database.AddStringParameter("@UserId", UserId.ToString());
            database.AddStringParameter("@TotalRecords", TotalRecords.ToString());
            database.AddLongParameter("UploadStatusId", 0, true);
            try
            {
                database.SubmitData("sp_FileUploadStatus", DataPortalQueryType.StoredProcedure);
                uploadStatus = Convert.ToInt64(database.GetParameterValue("UploadStatusId"));              
            }
            catch (Exception ex)
            {
                uploadStatus = 0;               
            }
            return uploadStatus;
        }
        public static DataTable GetFileUploadStatusList(string FieldId)
        {
            var dl = new Database();
            dl.AddStringParameter("@FieldId", FieldId);
            return dl.ReturnDataTable("sp_FileUploadStatusList", DataPortalQueryType.StoredProcedure);
        }
        public static DataTable GetftpDetailsList()
        {
            var dl = new Database();
            return dl.ReturnDataTable("sp_ftpDetailsList", DataPortalQueryType.StoredProcedure);
        }
        public static DataTable GetFileUploadList()
        {
            var dl = new Database();
            return dl.ReturnDataTable("sp_FileUploadList", DataPortalQueryType.StoredProcedure);
        }
        public static DataTable GetMFListForArchive()
        {
            var dl = new Database();
            return dl.ReturnDataTable("sp_MFListForArchive", DataPortalQueryType.StoredProcedure);
        }
        public static DataTable GetFileUplodedHistory()
        {
            var dl = new Database();
            return dl.ReturnDataTable("sp_FileUploadHistory", DataPortalQueryType.StoredProcedure);
        }

        public static Int64 SaveFaxData(Int64 ReqstId,String CoverPage,string faxnumber,string Subject,string filepath,string RequesterName)
        {
            Int64 faxid = 0;
            var database = new Database();
            database.AddLongParameter("@RequestId", ReqstId);
            database.AddStringParameter("@CoverPage", CoverPage);
            database.AddStringParameter("@faxnumber", faxnumber);
            database.AddStringParameter("@Subject", Subject);
            database.AddStringParameter("@filepath", filepath);
            database.AddStringParameter("@RequesterName", RequesterName);
            database.AddLongParameter("faxid", 0, true);
            try
            {
                database.SubmitData("sp_SaveFaxData", DataPortalQueryType.StoredProcedure);
                faxid = Convert.ToInt64(database.GetParameterValue("faxid"));                
            }
            catch (Exception ex)
            {
                faxid = 0;               
            }
            return faxid;
        }
   }
}
