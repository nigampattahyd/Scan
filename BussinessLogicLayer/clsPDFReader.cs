using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Data;
using DataAccessLayer;
using PdfSharp.Drawing;
namespace BussinessLogicLayer
{
    public class clsPDFReader
    {
        #region "Pdf Read and write"

        public static PdfDocument PDFreader(string sFilename, string dFolder)
        {
            PdfDocument reader = new PdfDocument();
            try
            {
                reader = PdfReader.Open(sFilename, PdfDocumentOpenMode.Import);
            }
            catch (PdfSharp.Pdf.IO.PdfReaderException)
            {
                //workaround if pdfsharp doesnt dupport this pdf
                string newName = WriteCompatiblePdf(sFilename, dFolder);
                reader = PdfReader.Open(newName, PdfDocumentOpenMode.Import);

                if (File.Exists(newName))
                    File.Delete(newName);
            }
            return reader;
        }

        static private string WriteCompatiblePdf(string sFilename, string dFolder)
        {          
            string sNewPdf = dFolder + Guid.NewGuid().ToString() + ".pdf";

            iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(sFilename);

            // we retrieve the total number of pages
            int n = reader.NumberOfPages;
            // step 1: creation of a document-object
            iTextSharp.text.Document document = new iTextSharp.text.Document(reader.GetPageSizeWithRotation(1));
            // step 2: we create a writer that listens to the document
            iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, new FileStream(sNewPdf, FileMode.Create));
            //write pdf that pdfsharp can understand
            writer.SetPdfVersion(iTextSharp.text.pdf.PdfWriter.PDF_VERSION_1_4);
            // step 3: we open the document
            document.Open();
            iTextSharp.text.pdf.PdfContentByte cb = writer.DirectContent;
            iTextSharp.text.pdf.PdfImportedPage page;

            int rotation;

            int i = 0;
            while (i < n)
            {
                i++;
                document.SetPageSize(reader.GetPageSizeWithRotation(i));
                document.NewPage();
                page = writer.GetImportedPage(reader, i);
                rotation = reader.GetPageRotation(i);
                if (rotation == 90 || rotation == 270)
                {
                    cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                }
                else
                {
                    cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                }
            }
            // step 5: we close the document
            document.Close();
            return sNewPdf;
        }

        #endregion

        public static void MergeMultiplePDFIntoSinglePDF(string outputFilePath, FileInfo[] pdfFiles)
        {
            try
            {
                PdfDocument outputPDFDocument = new PdfDocument();
                foreach (FileInfo pdfFile in pdfFiles)
                {
                    PdfDocument inputPDFDocument = PdfReader.Open(pdfFile.FullName, PdfDocumentOpenMode.Import);
                    outputPDFDocument.Version = inputPDFDocument.Version;
                    foreach (PdfPage page in inputPDFDocument.Pages)
                    {
                        outputPDFDocument.AddPage(page);
                    }
                }
                outputPDFDocument.Save(outputFilePath);
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
        }
        public int GetPageCount(Int64 RequestNumber, string sname)
        {
            int pagCount = 0;
            RequstEntry objPageCount = new RequstEntry();
            DataTable dtpgcount = new DataTable();
            dtpgcount=objPageCount.TotalPageCount(RequestNumber, sname);
            if (dtpgcount.Rows.Count > 0)
                pagCount =Convert.ToInt32(dtpgcount.Rows[0]["NoOfPages"]);           
            return pagCount;
        }
       
        //public static int GetPageCount(string RelCode, string FolderPath)
        //{
        //    int pagCount = 0;
        //    if (Directory.Exists(FolderPath))
        //    {
        //        DirectoryInfo dir = new DirectoryInfo(FolderPath);
        //        pagCount = dir.GetFiles("mr*" + RelCode + ".pdf").Length;
        //    }
        //    return pagCount;
        //}
        //public static int GetrlPageCount(string RelCode, string FolderPath)
        //{
        //    int pagCount = 0;
        //    if (Directory.Exists(FolderPath))
        //    {
        //        DirectoryInfo dir = new DirectoryInfo(FolderPath);
        //        pagCount = dir.GetFiles("rl*" + RelCode + ".pdf").Length;
        //    }
        //    return pagCount;
        //}
        public static DataSet GetInvoice(string RelCode, Int64 viewQueueId)
        {
            double Subtotal = 0.00;
            double TaxPercent = 0.00;
            double TaxAmt = 0.00;
            double TotalAmount = 0.00;
            bool IsGovernmentAgency = false;
            RequstEntry req = new RequstEntry();
            DataTable dtviewQueue = new DataTable();
            DataTable dtInItem = new DataTable("InvoiceItem");
            dtviewQueue = RequstEntry.GetViewQueueList(viewQueueId, RelCode);
            if (dtviewQueue.Rows.Count > 0)
            {
                #region InvoiceItem   
                int PagesCount = 0;
                int RequestTypeId = 0;
                int ReleaseResoanId = 0;
                int StateIdId = 1930; //will come from medical facility state, Now facility avilable for OH State
                RequestTypeId = Convert.ToInt32(dtviewQueue.Rows[0]["RequestTypeId"]);
                if(Convert.ToString(dtviewQueue.Rows[0]["ReleaseReasonId"])!="")
                ReleaseResoanId = Convert.ToInt32(dtviewQueue.Rows[0]["ReleaseReasonId"]);
                // Check Government Agency
                IsGovernmentAgency = req.CheckGovernmentAgency(RequestTypeId.ToString(), ReleaseResoanId.ToString());

                clsPDFReader objpgcount = new clsPDFReader();
                PagesCount = objpgcount.GetPageCount(viewQueueId,"M");//medical document page count
                dtInItem = SetInvoiceItem(PagesCount,RelCode, StateIdId, RequestTypeId, ReleaseResoanId,out TaxPercent);
                if (dtInItem.Rows.Count > 0)
                {
                    object sumObject;
                    sumObject = dtInItem.Compute("Sum(Amount)", "");// ("","RelCode = 5");
                    Subtotal = Convert.ToDouble(sumObject);

                    if (Subtotal > 0 && TaxPercent > 0)
                    {
                        // TaxAmt = (Subtotal * TaxPercent) / 100;
                        TaxAmt = (Subtotal * 6.75) / 100;
                        TotalAmount = Subtotal + TaxAmt;
                        TotalAmount = Math.Round(TotalAmount, 2);
                    }
                    else if (Subtotal > 0)
                    {
                        TotalAmount = Math.Round(Subtotal, 2);
                    }
                }
                #endregion
            }
            #region Invoice
            
            
            DataTable tblInvoice = new DataTable("Invoice");
            //tblInvoice.Columns.Add("InvoiceId", typeof(string));
            //tblInvoice.Columns.Add("DueDate", typeof(string));
            //tblInvoice.Columns.Add("SubTotalAmt", typeof(string));
            //tblInvoice.Columns.Add("TotalAmt", typeof(string));
            //tblInvoice.Columns.Add("Tax", typeof(string));
            //tblInvoice.Columns.Add("TaxAmount", typeof(string));
            //tblInvoice.Columns.Add("DueAmount", typeof(string));          

            tblInvoice.Columns.Add("InReleaseId", typeof(string));
            tblInvoice.Columns.Add("FirstName", typeof(string));
            tblInvoice.Columns.Add("LastName", typeof(string));
            tblInvoice.Columns.Add("DOB", typeof(string));
            tblInvoice.Columns.Add("SSN", typeof(string));
            tblInvoice.Columns.Add("MedicalRecordNo", typeof(string));
            tblInvoice.Columns.Add("AccountNo", typeof(string));
            tblInvoice.Columns.Add("DOS", typeof(string));
            tblInvoice.Columns.Add("PPhone", typeof(string));
            tblInvoice.Columns.Add("PMobile", typeof(string));
            tblInvoice.Columns.Add("PFax", typeof(string));
            tblInvoice.Columns.Add("PEmailAdds", typeof(string));
            tblInvoice.Columns.Add("Notes", typeof(string));
            tblInvoice.Columns.Add("ReqName", typeof(string));
            tblInvoice.Columns.Add("AttentionTo", typeof(string));
            tblInvoice.Columns.Add("Address1", typeof(string));
            tblInvoice.Columns.Add("Address2", typeof(string));
            tblInvoice.Columns.Add("City", typeof(string));
            tblInvoice.Columns.Add("StateName", typeof(string));
            tblInvoice.Columns.Add("Zip", typeof(string));
            tblInvoice.Columns.Add("RPhone", typeof(string));
            tblInvoice.Columns.Add("RMobile", typeof(string));
            tblInvoice.Columns.Add("RFax", typeof(string));
            tblInvoice.Columns.Add("REmailAdds", typeof(string));
            tblInvoice.Columns.Add("ReqType", typeof(string));
            tblInvoice.Columns.Add("ReleaseReason", typeof(string));
            tblInvoice.Columns.Add("BillTo", typeof(string));
            tblInvoice.Columns.Add("MedicalFacility", typeof(string));
            tblInvoice.Columns.Add("InvoiceId", typeof(string));
            tblInvoice.Columns.Add("DueDate", typeof(string));
            tblInvoice.Columns.Add("SubTotalAmt", typeof(string));
            tblInvoice.Columns.Add("TotalAmt", typeof(string));
            tblInvoice.Columns.Add("Tax", typeof(string));
            tblInvoice.Columns.Add("TaxAmount", typeof(string));
            tblInvoice.Columns.Add("DueAmount", typeof(string));
            tblInvoice.Columns.Add("StateId", typeof(string));
            tblInvoice.Columns.Add("MedicalFacilityRefNo", typeof(string));
            tblInvoice.Columns.Add("IsGovernmentAgency", typeof(string));
            tblInvoice.Columns.Add("ShippingInfo1", typeof(string));
            tblInvoice.Columns.Add("ShippingInfo2", typeof(string));
            tblInvoice.Columns.Add("ShippingInfo3", typeof(string));
            tblInvoice.Columns.Add("ShippingInfo4", typeof(string));
            tblInvoice.Columns.Add("ShippingInfo5", typeof(string));
            tblInvoice.Columns.Add("ShippingInfo6", typeof(string));
            tblInvoice.Columns.Add("ShippingInfo7", typeof(string));
            tblInvoice.Columns.Add("ShippingInfo8", typeof(string));
            tblInvoice.Columns.Add("ShippingInfo9", typeof(string));
            tblInvoice.Columns.Add("ShippingInfo10", typeof(string));
            tblInvoice.Columns.Add("RequestorId", typeof(string));
            tblInvoice.Columns.Add("InvoiceNumberTag", typeof(string));
            tblInvoice.Columns.Add("ReqInvoiceId", typeof(string));
            tblInvoice.Columns.Add("RequestStatus", typeof(string));
            tblInvoice.Columns.Add("RequestNumber", typeof(string));
            if (dtviewQueue.Rows.Count > 0)
            {
                //tblInvoice.Rows.Add(dtviewQueue.Rows[0]["InvoiceId"].ToString(),              
                //DateTime.Now.AddDays(15).ToShortDateString(),
                //Subtotal,
                //TotalAmount,
                //TaxPercent,
                //TaxAmt,
                //"0.0");

                DateTime dtDOB = new DateTime();
                if (dtviewQueue.Rows[0]["DOB"].ToString() != "")
                {
                    dtDOB = Convert.ToDateTime(dtviewQueue.Rows[0]["DOB"]);
                }

                tblInvoice.Rows.Add(dtviewQueue.Rows[0]["ReleaseId"].ToString(),
                dtviewQueue.Rows[0]["FirstName"].ToString(),
                dtviewQueue.Rows[0]["LastName"].ToString(),
                (dtDOB.ToString().Contains("1/1/0001")) ? "" : dtDOB.ToShortDateString(),
                dtviewQueue.Rows[0]["SSN"].ToString(),
                dtviewQueue.Rows[0]["MedicalRecNo"].ToString(),
                dtviewQueue.Rows[0]["AccountNo"].ToString(),
                dtviewQueue.Rows[0]["DOS"].ToString(),
                dtviewQueue.Rows[0]["PPhone"].ToString(),
                dtviewQueue.Rows[0]["PMobile"].ToString(),
                dtviewQueue.Rows[0]["Fax"].ToString(),
                dtviewQueue.Rows[0]["PEmailAddrs"].ToString(),
                dtviewQueue.Rows[0]["Notes"].ToString(),
                dtviewQueue.Rows[0]["ReqName"].ToString(),
                dtviewQueue.Rows[0]["AttentionTo"].ToString(),
                dtviewQueue.Rows[0]["Address1"].ToString(),
                dtviewQueue.Rows[0]["CaseNumber"].ToString(),
                dtviewQueue.Rows[0]["City"].ToString(),
                "",
                dtviewQueue.Rows[0]["Zip"].ToString(),
                dtviewQueue.Rows[0]["RPhone"].ToString(),
                dtviewQueue.Rows[0]["RMobile"].ToString(),
                 dtviewQueue.Rows[0]["RFax"].ToString(),
                dtviewQueue.Rows[0]["REmailAdds"].ToString(),
                dtviewQueue.Rows[0]["RequestTypeId"].ToString(),
                dtviewQueue.Rows[0]["ReleaseReasonId"].ToString(),
                dtviewQueue.Rows[0]["BillToId"].ToString(),
                dtviewQueue.Rows[0]["MFName"].ToString(),
                dtviewQueue.Rows[0]["InvoiceNumber"].ToString(),
                DateTime.Now.AddDays(15).ToShortDateString(),
                Subtotal,
                TotalAmount,
                TaxPercent,
                TaxAmt,
                "0.0",
                dtviewQueue.Rows[0]["StateId"].ToString(),
                dtviewQueue.Rows[0]["MedicalFacilityRefNo"].ToString(),
                IsGovernmentAgency.ToString(),
                dtviewQueue.Rows[0]["ShippingInfo1"].ToString(),
                dtviewQueue.Rows[0]["ShippingInfo2"].ToString(),
                dtviewQueue.Rows[0]["ShippingInfo3"].ToString(),
                dtviewQueue.Rows[0]["ShippingInfo4"].ToString(),
                dtviewQueue.Rows[0]["ShippingInfo5"].ToString(),
                dtviewQueue.Rows[0]["ShippingInfo6"].ToString(),
                dtviewQueue.Rows[0]["ShippingInfo7"].ToString(),
                dtviewQueue.Rows[0]["ShippingInfo8"].ToString(),
                dtviewQueue.Rows[0]["ShippingInfo9"].ToString(),
                dtviewQueue.Rows[0]["ShippingInfo10"].ToString(),
                dtviewQueue.Rows[0]["RequestorId"].ToString(),
                dtviewQueue.Rows[0]["InvoiceNumberTag"].ToString(),
                dtviewQueue.Rows[0]["InvoiceId"].ToString(),
                dtviewQueue.Rows[0]["RequestStatus"].ToString(),
                viewQueueId.ToString());
            }
            #endregion

            DataSet ds = new DataSet();
            ds.Tables.Add(tblInvoice);
            ds.Tables.Add(dtInItem);
            return ds;
        }
        public static DataTable SetInvoiceItem(int PagesCount,string RelCode, int StateId, int ReqTypId, int RelId, out double TaxPercent)
        {         
            TaxPercent = 0.00;           
            DataTable tblInvoiceItm = new DataTable("InvoiceItem");
            tblInvoiceItm.Columns.Add("ReleaseId", typeof(string));
            tblInvoiceItm.Columns.Add("Activity", typeof(string));
            tblInvoiceItm.Columns.Add("Quantity", typeof(string));
            tblInvoiceItm.Columns.Add("Rate", typeof(string));
            tblInvoiceItm.Columns.Add("Amount", typeof(double));

            DataTable dtItemPrice = new DataTable();
            dtItemPrice = RequstEntry.GetInvoiceItemPrice(StateId, ReqTypId, RelId);//Only for OH State             
            if (dtItemPrice.Rows.Count > 0)
            {
                for (int i = 0; dtItemPrice.Rows.Count > i; i++)
                {
                    string itmText = dtItemPrice.Rows[i]["ListItemText"].ToString().Trim();
                    string price = dtItemPrice.Rows[i]["Price"].ToString().Trim();

                    int quantity = 1;
                    bool percent = false;
                    double priceD = 0.00;
                    if (price.Contains(".") && (!price.Contains("%")))
                        priceD = Convert.ToDouble(price);
                    else
                    {
                        if (price.ToLower().Contains("std"))
                            priceD = 0.00;
                        else if (price.Contains("%"))
                        {
                            percent = true;
                            priceD = Convert.ToDouble(price.Replace("%", ""));
                        }
                    }
                    if (itmText.ToUpper().Contains("BASIC"))
                    {
                        tblInvoiceItm.Rows.Add(RelCode, itmText, quantity.ToString(), priceD.ToString(), (quantity * priceD));                       
                    }
                    if (itmText.ToUpper().Contains("SHIP"))
                    {
                        if (priceD > 0)
                        {
                            tblInvoiceItm.Rows.Add(RelCode, itmText, quantity.ToString(), priceD.ToString(), (quantity * priceD));                          
                        }
                    }
                    if (itmText.ToUpper().Contains("CERTIFI"))
                    {
                        if (priceD > 0)
                        {
                            tblInvoiceItm.Rows.Add(RelCode, itmText, quantity.ToString(), priceD.ToString(), (quantity * priceD));                          
                        }
                    }
                    if (itmText.ToUpper().Contains("PAGES"))
                    {
                        if (PagesCount > 0)
                        {
                            if (itmText.Contains("1-10"))
                            {
                                if (PagesCount >= 10)
                                {
                                    if (PagesCount > 10)
                                        quantity = 10;
                                    else
                                        quantity = PagesCount;
                                    tblInvoiceItm.Rows.Add(RelCode, itmText, quantity.ToString(), priceD.ToString(), (quantity * priceD));                                  
                                }
                                else
                                {
                                    if (PagesCount > 0)
                                    {
                                        quantity = PagesCount;
                                        tblInvoiceItm.Rows.Add(RelCode, itmText, quantity.ToString(), priceD.ToString(), (quantity * priceD));                                      
                                    }
                                }
                            }
                            if (itmText.Contains("11-50"))
                            {
                                if (PagesCount >= 11)
                                {
                                    if (PagesCount <= 50)
                                        quantity = PagesCount - 10;
                                    else
                                        quantity = 40;
                                    tblInvoiceItm.Rows.Add(RelCode, itmText, quantity.ToString(), priceD.ToString(), (quantity * priceD));                                  
                                }
                            }
                            if (itmText.Contains("51"))
                            {
                                if (PagesCount > 50)
                                {
                                    quantity = PagesCount - 50;
                                    tblInvoiceItm.Rows.Add(RelCode, itmText, quantity.ToString(), priceD.ToString(), (quantity * priceD));                                  
                                }
                            }
                        }
                    }
                    if (itmText.ToUpper().Contains("TAX"))
                    {
                        if (percent)
                            TaxPercent = priceD;
                        //quantity = 1;
                        //if (e.TaxApplied)
                        //{
                        //    if (percent)
                        //    {
                        //        double priceToGo = (totalAmountForTax * priceD) / 100;
                        //        InvoiceItemsToBind itm = new InvoiceItemsToBind(quantity.ToString(), itmText + " (" + price + ")",
                        //            priceD.ToString() + "%", (quantity * priceToGo));
                        //        lst.Add(itm);
                        //        totalAmountForTax += quantity * priceToGo;
                        //    }
                        //    else
                        //    {
                        //        InvoiceItemsToBind itm = new InvoiceItemsToBind(quantity.ToString(), itmText + " (" + price + ")",
                        //             priceD.ToString(), (quantity * priceD));
                        //        lst.Add(itm);
                        //        totalAmountForTax += quantity * priceD;
                        //    }
                        //}
                    }
                }
            }
            return tblInvoiceItm;
        }

        public static void ArcMultiplePDFIntoSinglePDF(string outputFilePath, List<string> pdfFiles, string RootPath)
        {
            try
            {               
                PdfDocument outputPDFDocument = new PdfDocument();
                for (int i=0;i<pdfFiles.Count;i++)
                {
                    PdfDocument inputPDFDocument = new PdfDocument();
                    try
                    {
                        if (pdfFiles[i].ToString().ToLower().EndsWith(".pdf"))
                        {
                            inputPDFDocument = PdfReader.Open(pdfFiles[i], PdfDocumentOpenMode.Import);
                        }
                        else
                        {
                            string sNewPdf = RootPath + "\\Temp\\" + Guid.NewGuid().ToString() + ".pdf";
                            ArcImageToPDF(sNewPdf, pdfFiles[i]);
                            if (File.Exists(sNewPdf))
                            {
                                inputPDFDocument = PdfReader.Open(sNewPdf, PdfDocumentOpenMode.Import);
                                File.Delete(sNewPdf);
                            }
                        }
                                           
                    }
                    catch (Exception ex)
                    {
                        string newName = WriteCompatiblePdf(pdfFiles[i], RootPath);
                        inputPDFDocument = PdfReader.Open(newName, PdfDocumentOpenMode.Import);

                        if (File.Exists(newName))
                            File.Delete(newName);
                    }
                    outputPDFDocument.Version = inputPDFDocument.Version;
                    foreach (PdfPage page in inputPDFDocument.Pages)
                    {
                        outputPDFDocument.AddPage(page);
                    }
                }
                outputPDFDocument.Save(outputFilePath);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        public static void ArcImageToPDF(string outputFilePath, string fileName)
        {
            using (PdfDocument doc = new PdfDocument())
            {
                using (System.Drawing.Image myimage = System.Drawing.Image.FromFile(fileName))
                {                 
                    PdfPage page = new PdfPage();
                    //page.Width = myimage.Width + 20;
                    //page.Height = myimage.Height + 20;
                    doc.Pages.Add(page);

                    PdfSharp.Drawing.XGraphics xgr = PdfSharp.Drawing.XGraphics.FromPdfPage(doc.Pages[0]);
                    PdfSharp.Drawing.XImage img = PdfSharp.Drawing.XImage.FromFile(fileName);

                    xgr.DrawImage(img, 10, 10, page.Width, page.Height);
                    doc.Info.Author = "BHS Inc.";
                    doc.Info.CreationDate = DateTime.Now;
                    doc.Info.Creator = "BHS";
                    doc.Info.Subject = "BHS Invoice";
                    doc.Info.Title = "Bhs";
                    doc.Save(outputFilePath);
                    doc.Close();
                    img.Dispose();
                }
                doc.Dispose();
            }            
        }
        public static void LogWritter(string sourcePathg, StringBuilder message)
        {
            using (StreamWriter writer = new StreamWriter(sourcePathg, true))
            {              
                writer.WriteLine(message.ToString());
            }
        }
        public void ArcMultiplePDFIntoImages(Int64 requestNo, string releaseCode, List<string> pdfFiles, string PdfFilePath, string _typeprefix, string filetype, string tempFilePath)
        {
            try
            {
                CreateReleaseCode objforint = new CreateReleaseCode();
                string PngFilePath = PdfFilePath +"png\\";
                string PDFName = string.Empty;
                string ImgFName = string.Empty;              
                int indexInternal = 0;
                int pdfsIndex = 0;

                if (!Directory.Exists(PngFilePath))
                    Directory.CreateDirectory(PngFilePath);

                for (int i = 0; i < pdfFiles.Count; i++)
                {
                    PdfDocument docFile = null;
                    try
                    {
                        if (pdfFiles[i].ToString().ToLower().EndsWith(".pdf"))
                        {
                            docFile = PdfReader.Open(pdfFiles[i], PdfDocumentOpenMode.Import);
                        }
                        else
                        {
                            string sNewPdf = tempFilePath + Guid.NewGuid().ToString() + ".pdf";
                            ArcImageToPDF(sNewPdf, pdfFiles[i]);
                            if (File.Exists(sNewPdf))
                            {
                                docFile = PdfReader.Open(sNewPdf, PdfDocumentOpenMode.Import);
                                File.Delete(sNewPdf);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        string newName = WriteCompatiblePdf(pdfFiles[i], tempFilePath);
                        docFile = PdfReader.Open(newName, PdfDocumentOpenMode.Import);

                        if (File.Exists(newName))
                            File.Delete(newName);
                    }
                    if (docFile != null)
                    {
                        PdfPages pages = docFile.Pages;

                        foreach (PdfPage page in pages)
                        {                           
                            using (PdfDocument doc = new PdfDocument())
                            {
                                doc.AddPage(page);
                                PDFName = PdfFilePath + _typeprefix + objforint.FormatInteger("000", pdfsIndex).ToString() + "_" +
                                                           objforint.FormatInteger("000", indexInternal).ToString() + "_" + releaseCode + ".pdf";
                                ImgFName = PngFilePath + _typeprefix + objforint.FormatInteger("000", pdfsIndex).ToString() + "_" +
                                                       objforint.FormatInteger("000", indexInternal).ToString() + "_" + releaseCode + ".png";
                               
                                doc.Save(PDFName);
                                doc.Close();
                                // convert into images Invoice
                                PDFtoImage objCon = new PDFtoImage();
                                objCon.ConvertPdfImage(PDFName, ImgFName);
                            }
                            string toShow = "IMP" + objforint.FormatInteger("000", pdfsIndex).ToString() + "_" +
                                                     objforint.FormatInteger("000", indexInternal).ToString();
                            string actualFileName = _typeprefix + objforint.FormatInteger("000", pdfsIndex).ToString() + "_" +
                                                     objforint.FormatInteger("000", indexInternal).ToString() + "_" + releaseCode;
                            //for save Image file details                                      
                            RequstEntry objDoc = new RequstEntry();
                            objDoc.AddUpdateDocument(requestNo, releaseCode, actualFileName, toShow, "png", filetype);
                            pdfsIndex++;
                        }
                        docFile.Dispose();
                        indexInternal++;
                    }

                    //PdfDocument outputPDFDocument = new PdfDocument();
                    //outputPDFDocument.Version = inputPDFDocument.Version;
                    //foreach (PdfPage page in inputPDFDocument.Pages)
                    //{
                    //    outputPDFDocument.AddPage(page);
                    //    outputPDFDocument.Save(outputFilePath);

                    //    PDFName =RootPath+ _typeprefix + objforint.FormatInteger("000", pdfsIndex).ToString() + "_" +
                    //                           objforint.FormatInteger("000", indexInternal).ToString() + "_" + releaseCode + ".pdf";
                    //    ImgFName = RootPath+"png\\" + _typeprefix + objforint.FormatInteger("000", pdfsIndex).ToString() + "_" +
                    //                           objforint.FormatInteger("000", indexInternal).ToString() + "_" + releaseCode + ".png";

                    //    actualFileName = _typeprefix + objforint.FormatInteger("000", pdfsIndex).ToString() + "_" +
                    //                           objforint.FormatInteger("000", indexInternal).ToString() + "_" + releaseCode;

                    //    string toShow = "IMP" + objforint.FormatInteger("000", pdfsIndex).ToString() + "_" +
                    //                       objforint.FormatInteger("000", indexInternal).ToString();

                    //    pdfsIndex = pdfsIndex+1;
                    //}
                    //indexInternal = indexInternal + 1;
                }
               
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public void ArcInvoicePDFIntoImages(Int64 requestNo, string releaseCode, List<string> pdfFiles, string PdfFilePath, string _typeprefix, string filetype, string tempFilePath)
        {
            CreateReleaseCode objforint = new CreateReleaseCode();
            string PngFilePath = PdfFilePath + "png\\";
            string PDFName = string.Empty;
            string ImgFName = string.Empty;

            if (!Directory.Exists(PngFilePath))
                Directory.CreateDirectory(PngFilePath);

            try
            {               
                for (int i = 0; i < pdfFiles.Count; i++)
                {
                    PdfDocument docFile = null;
                    try
                    {
                        if (pdfFiles[i].ToString().ToLower().EndsWith(".pdf"))
                        {
                            docFile = PdfReader.Open(pdfFiles[i], PdfDocumentOpenMode.Import);
                        }
                        else
                        {
                            string sNewPdf =tempFilePath + Guid.NewGuid().ToString() + ".pdf";
                            ArcImageToPDF(sNewPdf, pdfFiles[i]);
                            if (File.Exists(sNewPdf))
                            {
                                docFile = PdfReader.Open(sNewPdf, PdfDocumentOpenMode.Import);
                                File.Delete(sNewPdf);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        string newName = WriteCompatiblePdf(pdfFiles[i], tempFilePath);
                        docFile = PdfReader.Open(newName, PdfDocumentOpenMode.Import);

                        if (File.Exists(newName))
                            File.Delete(newName);
                    }
                    if (docFile != null)
                    {
                      
                        PdfPages pages = docFile.Pages;

                        foreach (PdfPage page in pages)
                        {
                            using (PdfDocument doc = new PdfDocument())
                            {
                                doc.AddPage(page);
                                PDFName = PdfFilePath + _typeprefix + releaseCode + ".pdf";
                                ImgFName = PngFilePath + _typeprefix + releaseCode + ".png";                               
                                doc.Save(PDFName);
                                doc.Close();
                                // convert into images Invoice
                                PDFtoImage objCon = new PDFtoImage();
                                objCon.ConvertPdfImage(PDFName, ImgFName);
                            }                        
                          string actualFileName = _typeprefix + releaseCode;
                            //for save Image file details                                      
                            RequstEntry objDoc = new RequstEntry();
                            objDoc.AddUpdateDocument(requestNo, releaseCode, actualFileName, "Invoice", "png", filetype);                           
                        }
                        docFile.Dispose();                      
                    }
                   
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        public int GetNumberOfPages(String FilePath,string tempFilePath)
        {
            int NumberofPages = 0;       
            PdfDocument pdfReader = null;
        
            try
            {
                pdfReader = PdfReader.Open(FilePath, PdfDocumentOpenMode.ReadOnly);
               
            }
            catch (Exception ex)
            {
                string newName = WriteCompatiblePdf(FilePath, tempFilePath);
                pdfReader = PdfReader.Open(newName, PdfDocumentOpenMode.ReadOnly);

                if (File.Exists(newName))
                    File.Delete(newName);
            }

            NumberofPages = pdfReader.PageCount;

            return NumberofPages;          
        }
        public void DrawBarcodeForPDFFile(string pdfFileName, string serialnumber, Int64 userid,string releasePdfFile)
        {
            try
            {
                PdfDocument docFile = null;
                docFile = PdfSharp.Pdf.IO.PdfReader.Open(pdfFileName, PdfSharp.Pdf.IO.PdfDocumentOpenMode.Modify);
                //docFile = clsPDFReader.PDFreader(fileName, TargetFolder);           
                DrawBarCode(docFile.Pages[0], serialnumber);
                docFile.Save(releasePdfFile);
                docFile.Close();
                docFile.Dispose();
            }
            catch (Exception ex)
            {
                Errorlogs objErrorlog = new Errorlogs();
                objErrorlog.ErrorLog(userid, "Write Barcode", "DrawBarcodeForPDFFile", ex.Message);
            }
        }
        
        public PdfPage DrawBarCode(PdfPage page, string releasecode)
        {
            XGraphics gfx = XGraphics.FromPdfPage(page);
            PdfSharp.Drawing.BarCodes.Code2of5Interleaved BarCode39 = new PdfSharp.Drawing.BarCodes.Code2of5Interleaved();
            BarCode39.TextLocation = new PdfSharp.Drawing.BarCodes.TextLocation();
            BarCode39.Text = releasecode;//value of code to draw on page
            // BarCode39.StartChar = Convert.ToChar("*");
            //  BarCode39.EndChar = Convert.ToChar("*");
            BarCode39.Direction = PdfSharp.Drawing.BarCodes.CodeDirection.RightToLeft;
            XFont fontBARCODE = new XFont("Arial", 10, XFontStyle.Regular);
            XSize BARCODE_SIZE = new XSize(new XPoint(Convert.ToDouble(220), Convert.ToDouble(18)));
            BarCode39.Size = BARCODE_SIZE;
            //gfx.DrawBarCode(BarCode39, XBrushes.Black, fontBARCODE, new XPoint(Convert.ToDouble(page.Width - 260), page.Height - 50));

            XFont font = new XFont("Arial", 10, XFontStyle.Regular);
            gfx.DrawString(releasecode, font, XBrushes.Black, page.Width - 220, page.Height - 10);
            // Define a rotation transformation at the center of the page 
            //gfx.TranslateTransform(page.Width / 2, page.Height / 2);
            //gfx.RotateTransform(-Math.Atan(page.Height / page.Width) * 180 / Math.PI);
            //gfx.TranslateTransform(-page.Width / 2, -page.Height / 2);           
            return page;

        }
        public void DrawBarCodeForImage(string FileName, string serialnumber, string releasePngFile)
        {
                FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                System.Drawing.Image image = System.Drawing.Image.FromStream(fs);
                fs.Close();
                System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Regular);

                System.Drawing.Bitmap b = new System.Drawing.Bitmap(image);
                System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(b);
                graphics.DrawString(serialnumber, drawFont, System.Drawing.Brushes.Black, b.Width - 310, b.Height - 20);

                b.Save(releasePngFile, image.RawFormat);

                image.Dispose();
                b.Dispose();
            }
           
        }
    }
