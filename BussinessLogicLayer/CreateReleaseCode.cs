using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace BussinessLogicLayer
{
    public class CreateReleaseCode
    {
        public string GetNewReleaseNumber(Int64 Uid, Int64 MedRefNo,string Requsterid,string ReqName,string rFax,string rPhone)
        {
            string toret = string.Empty;     //RequestNumber(7)+UserId(4)+Medical Facility(4)+RequesterId(8)+Datetime(12) 
            string staff = FormatInteger("0000", Uid);
            string loc = FormatInteger("0000", MedRefNo);
            string dt = DateTime.UtcNow.ToString("yyMMddhhmmss");
            string RequesterID = "00000000";
            string RequestNumber = "0000000";
            DataTable dtque = new DataTable();         
            dtque = RequstEntry.GetReleaseId(Requsterid,ReqName,rFax,rPhone);
            if (dtque.Rows.Count > 0)
            {
                RequesterID = FormatInteger("00000000", dtque.Rows[0]["RequestorId"].ToString());
                RequestNumber = FormatInteger("0000000", dtque.Rows[0]["RequestNumber"].ToString());            
            }        
            toret = RequestNumber + staff + loc+ RequesterID + dt;
            return toret;
        }
        public string GetNewReleaseNumberOld(Int64 Uid, Int64 MedRefNo)
        {
            string toret = string.Empty;
            string lrno = "000000";
            string staff = FormatInteger("000000", Uid);
            string loc = FormatInteger("000000", MedRefNo);
            string dt = DateTime.UtcNow.ToString("yyyyMMddhhmmss");
            DataTable dtque = new DataTable();
         //  dtque = RequstEntry.GetReleaseId();
            if (dtque.Rows.Count > 0)
            {
                Int64 vqReleaseId = 0;
                Int64 reqReleaseId = 0;
                if (dtque.Rows[0]["vqReleaseId"].ToString() != "")
                {
                    vqReleaseId = Convert.ToInt64(dtque.Rows[0]["vqReleaseId"]);
                }
                if (dtque.Rows[0]["reqReleaseId"].ToString() != "")
                {
                    reqReleaseId = Convert.ToInt64(dtque.Rows[0]["reqReleaseId"]);
                }
                if (vqReleaseId >= reqReleaseId)
                    lrno = vqReleaseId.ToString();
                else
                    lrno = reqReleaseId.ToString();
            }
            //lrno = lrno.Substring(lrno.Length - 6);
            //dtque.DefaultView.Sort = dtque.Columns[0] + " Desc";
            //dtque = dtque.DefaultView.ToTable();       
            //var code = dtque.Rows[0][0].ToString();
            //if (code != null)
            //  code = code.Substring(code.Length - 6);

            lrno = (Convert.ToInt32(lrno) + 1).ToString();
            lrno = FormatInteger("000000", lrno);
            toret = staff + loc + dt + lrno;
            return toret;
        }
        public string FormatInteger(string format, long value)
        {
            return FormatInteger(format, value.ToString());
        }
        private string FormatInteger(string format, string value)
        {
            if (!string.IsNullOrWhiteSpace(format))
            {
                int lenVal = value.ToString().Trim().Length;
                int lenFormat = format.ToString().Trim().Length;
                if (lenFormat > lenVal)
                {
                    string toRet = "";
                    for (int i = 1; i <= lenFormat - lenVal; i++)
                    {
                        toRet += "0";
                    }
                    toRet += value.ToString();
                    return toRet;
                }
                else
                    return value.ToString();
            }
            else
                return value.ToString();
        }

        //private DataTable GetQueue(string filePath)
        //{
        //    if (!File.Exists(filePath))
        //    {
        //        File.Create(filePath);
        //    }
        //    DataSet ds = new DataSet();
        //    ds.ReadXml(filePath);
        //    return ds.Tables[0];
        //}
    }
}
