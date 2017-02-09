using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;
using System.Collections.Generic;
namespace BussinessLogicLayer
{
    public class MedFacilManager : MedicalFcility
    {
        public static bool UpdateMedicalFacility(MedicalFcility facility, out Int64 refNumber)
        {
            try
            {
                var dp = new Database();
                #region Comment for test
                /* dp.SubmitData("INSERT INTO [DivineForceHMS].[dbo].[MedicalFacility] ([Name] ,[Nickname] ,[Address1] ,[Address2] ,[Address3] ,[City] ,[State]  ,[Country],"+  
                "[ZIP] ,[PhoneNumber1] ,[PhoneNumber2] ,[PhoneNumber3] ,[PhoneNumber4] ,[PhoneNumber5] ,[Fax1] ,[Fax2] ,[Fax3] ,[EmailAddress1] ,[EmailAddress2], "+
                "[EmailAddress3] ,[EmailAddress4] ,[EmailAddress5] ,[CreatedOn] ,[CreatedBy]) "+
                "values('" + 
                facility.Name + "','" 
                + facility.Nickname + "','"
                + facility.Address1 + "','"
                 + facility.Address2 + "','"
                  + facility.Address3 + "','"
                   + facility.City + "',"
                    + facility.State + ","
                     + facility.Country + ",'"
                      + facility.ZIP + "','"
                       + facility.PhoneNumber1 + "','"
                        + facility.PhoneNumber2 + "','"
                         + facility.PhoneNumber3 + "','"
                          + facility.PhoneNumber4 + "','"
                           + facility.PhoneNumber5 + "','"
                             + facility.Fax1 + "','"
                               + facility.Fax2 + "','"
                                 + facility.Fax3 + "','"
                                   + facility.EmailAddress1 + "','"
                                    + facility.EmailAddress2 + "','"
                                     + facility.EmailAddress3 + "','"
                                      + facility.EmailAddress4 + "','"
                                       + facility.EmailAddress5 + "',"
                
                +"getdate(),getdate())", DataPortalQueryType.SqlQuery);*/
                #endregion
                dp.AddLongParameter("@MedicalFacilityRefNo", facility.MedicalReferenceNumber);
                dp.AddStringParameter("@Name", facility.Name);
                dp.AddStringParameter("@Nickname", facility.Nickname);
                dp.AddStringParameter("@Address1", facility.Address1);
                dp.AddStringParameter("@Address2", facility.Address2);
                dp.AddStringParameter("@Address3", facility.Address3);
                dp.AddStringParameter("@City", facility.City);
                dp.AddIntParameter("@State", facility.State);
                dp.AddIntParameter("@Country", facility.Country);
                dp.AddStringParameter("@ZIP", facility.ZIP);
                dp.AddStringParameter("@PhoneNumber1", facility.PhoneNumber1);
                dp.AddStringParameter("@PhoneNumber2", facility.PhoneNumber2);
                dp.AddStringParameter("@PhoneNumber3", facility.PhoneNumber3);
                dp.AddStringParameter("@PhoneNumber4", facility.PhoneNumber4);
                dp.AddStringParameter("@PhoneNumber5", facility.PhoneNumber5);
                dp.AddStringParameter("@Fax1", facility.Fax1);
                dp.AddStringParameter("@Fax2", facility.Fax2);
                dp.AddStringParameter("@Fax3", facility.Fax3);
                dp.AddStringParameter("@EmailAddress1", facility.EmailAddress1);
                dp.AddStringParameter("@EmailAddress2", facility.EmailAddress2);
                dp.AddStringParameter("@EmailAddress3", facility.EmailAddress3);
                dp.AddStringParameter("@EmailAddress4", facility.EmailAddress4);
                dp.AddStringParameter("@EmailAddress5", facility.EmailAddress5);

                dp.AddStringParameter("@ServicedFrom", facility.ServicedFrom);
                dp.AddStringParameter("@MRContactName", facility.MRContactName);
                dp.AddStringParameter("@MRContactPhone", facility.MRContactPhone);
                dp.AddStringParameter("@MRContactEmailAddress", facility.MRContactEmailAddress);
                dp.AddStringParameter("@PracticeManagerContactName", facility.PracticeManagerContactName);
                dp.AddStringParameter("@PracticeManagerContactPhoneNumber", facility.PracticeManagerContactPhoneNumber);
                dp.AddStringParameter("@PracticeManagerContactEmailAddress", facility.PracticeManagerContactEmailAddress);
                dp.AddStringParameter("@AreaManagerName", facility.AreaManagerName);
                dp.AddStringParameter("@AreaManagerAddress1", facility.AreaManagerAddress1);
                dp.AddStringParameter("@AreaManagerAddress2", facility.AreaManagerAddress2);
                dp.AddStringParameter("@AreaManagerCity", facility.AreaManagerCity);
                dp.AddIntParameter("@AreaManagerState", facility.AreaManagerState);
                dp.AddStringParameter("@AreaManagerZIP", facility.AreaManagerZIP);
                dp.AddStringParameter("@AreaManagerPhoneNumber", facility.AreaManagerPhoneNumber);
                dp.AddStringParameter("@AreaManagerFaxNumber", facility.AreaManagerFaxNumber);
                dp.AddStringParameter("@AreaManagerMobileNumber", facility.AreaManagerMobileNumber);
                dp.AddStringParameter("@AreaManagerEmailAddress", facility.AreaManagerEmailAddress);
                dp.AddIntParameter("@EstNumberOfRequestPerMonth", facility.EstNumberOfRequestPerMonth);
                dp.AddIntParameter("@PhysicianCounts", facility.PhysicianCounts);
                dp.AddStringParameter("@Speciality", facility.Speciality);
                dp.AddStringParameter("@EstMonthlyRevenue", facility.EstMonthlyRevenue.ToString());
                dp.AddLongParameter("@AddedBy", facility.AddedBy);
                dp.AddLongParameter("refNumber", 0, true);
                dp.SubmitData("sp_UpdateMedicalFacitily", DataPortalQueryType.StoredProcedure);
                refNumber = Convert.ToInt64(dp.GetParameterValue("refNumber"));
                return true;
            }
            catch (Exception)
            {
                refNumber = 0;
                return false;
            }
        }

        public static int DeleteMedicalFacility(MedicalFcility facility)
        {
            var db = new Database();
            db.AddLongParameter("@MedFacilNumber", facility.MedicalReferenceNumber);
            db.AddLongParameter("IsActiveVal", -1, true);
            db.SubmitData("DeleteMedicalFacility", DataPortalQueryType.StoredProcedure);
            return Convert.ToInt32(db.GetParameterValue("IsActiveVal"));
        }
        
       // public string SearchMedicalFacility(List<string> value, List<string> Type, List<string> Field, Int64 userId, int flg)
        public string SearchMedicalFacility(List<string> value, List<string> Field)
        {
            string SearchCondition = "";
            for (int i = 0; i < value.Count; i++)
            {
                if (value[i].EndsWith("*") || value[i].StartsWith("*"))
                {
                    if (value[i].StartsWith("*"))
                    {
                        SearchCondition += Field[i] + " like '%" + value[i].TrimStart('*') + "'";
                    }
                    else if (value[i].EndsWith("*"))
                    {
                        SearchCondition += Field[i] + " like '" + value[i].TrimEnd('*') + "%'";
                    }
                }
                else
                {
                    SearchCondition += Field[i] + " like '" + value[i] + "%'";
                }

                if (i < value.Count - 1)
                {
                    SearchCondition += " and ";
                }
            }

            //for (int i = 0; i < Type.Count; i++)
            //{
            //    bool isField = false;
            //    if (Type[i] == "Contains")
            //    {
            //        isField = true;
            //        type += Field[i] + " like '%" + value[i] + "%'";
            //    }
            //    if (Type[i] == "Is")
            //    {
            //        isField = true;
            //        type += Field[i] + " like '" + value[i] + "'";
            //    }
            //    if (Type[i] == "Ends with")
            //    {
            //        isField = true;
            //        type += Field[i] + " like '%" + value[i] + "'";
            //    }
            //    if (Type[i] == "Starts with")
            //    {
            //        isField = true;
            //        type += Field[i] + " like '" + value[i] + "%'";
            //    }
            //    if (Type[i] == "State")
            //    {
            //        isField = true;
            //        type += " State = " + value[i] + " ";
            //    }
            //    if (isField && i < Type.Count - 1) type += " and ";
            //}
            //var dl = new Database();
            ////if (type == "") type = "1='1'";
            //string query = string.Empty;
            //if (flg == 1 || flg == 6)
            //{
            //    query = "select mf.*,states.StateName from MedicalFacility mf left join states on mf.State=States.StateID \n" +
            //    "where mf.IsActive=1 And " + SearchCondition + " order by Name asc";
            //}
            //else //if (flg == 2 || flg == 3 || flg == 4 || flg == 5)//Ravi- Added flg==5 for bug ID #5698 because processor can search and Edit Medical Facility.
            //{
            //    query = "select mf.*,st.StateName from MedicalFacility mf left join states st on mf.State=st.StateID \n" +
            //      "Inner Join UserAssignedMedicalFacilities uam on mf.MedicalFacilityRefNo=uam.MedicalFacilityRefNo  \n" +
            //       "where uam.IsActive=1 And uam.UserAccountNumber=" + userId + " AND " + SearchCondition + "  AND mf.IsActive =1 order by Name asc";
            //}

            //return dl.ReturnDataTable(query, DataPortalQueryType.SqlQuery);
            return SearchCondition;
        }

        public static DataTable SearchMedicalFacilityToAssoUser(List<string> value, List<string> Type, List<string> Field, Int64 userId)
        {

            string SearchCondition = "";
            for (int i = 0; i < value.Count; i++)
            {
                if (value[i].EndsWith("*") || value[i].StartsWith("*"))
                {
                    if (value[i].StartsWith("*"))
                    {
                        SearchCondition += Field[i] + " like '%" + value[i].TrimStart('*') + "'";
                    }
                    else if (value[i].EndsWith("*"))
                    {
                        SearchCondition += Field[i] + " like '" + value[i].TrimEnd('*') + "%'";
                    }
                }
                else
                {
                    SearchCondition += Field[i] + " like '" + value[i] + "%'";
                }

                if (i < value.Count - 1)
                {
                    SearchCondition += " and ";
                }
            }

            //string type = "";
            //for (int i = 0; i < Type.Count; i++)
            //{
            //    bool isField = false;
            //    if (Type[i] == "Contains")
            //    {
            //        isField = true;
            //        type += Field[i] + " like '%" + value[i] + "%'";
            //    }
            //    if (Type[i] == "Is")
            //    {
            //        isField = true;
            //        type += Field[i] + " like '" + value[i] + "'";
            //    }
            //    if (Type[i] == "Ends with")
            //    {
            //        isField = true;
            //        type += Field[i] + " like '%" + value[i] + "'";
            //    }
            //    if (Type[i] == "Starts with")
            //    {
            //        isField = true;
            //        type += Field[i] + " like '" + value[i] + "%'";
            //    }
            //    if (Type[i] == "State")
            //    {
            //        isField = true;
            //        type += " State = " + value[i] + " ";
            //    }
            //    if (isField && i < Type.Count - 1) type += " and ";
            //}

            var dl = new Database();
            if (SearchCondition == "") SearchCondition = "1='1'";
            var db = new Database();
            db.AddLongParameter("@UserId", userId);
            db.AddStringParameter("@Sname", SearchCondition);
            return db.ReturnDataTable("sp_SearchMfForAssociateToUser", DataPortalQueryType.StoredProcedure);
        }

        public static DataTable GetSchema()
        {
            var db = new Database();
            return db.ReturnDataTable("select * from dbo.MedicalFacility where 1 !='1'", DataPortalQueryType.SqlQuery);
        }

        public static int GetStateIdByStateName(string stateName)
        {
            var dp = new Database();
            DataSet ds;
            string query = "select StateID from states where StateName like '%" + stateName + "%'";
            ds = dp.ReturnDataSet(query, DataPortalQueryType.SqlQuery);
            if (ds.Tables[0].Rows.Count > 0)
            {
                int StateId = Convert.ToInt32(ds.Tables[0].Rows[0]["StateID"]);
                return StateId;
            }
            else
            {
                return 0;
            }

        }
        //public static int GetCountryIdByCountryName(string Country)
        //{
        //    var dp = new Database();
        //    DataSet ds;
        //    string query = "select CountryCode from states where CountryName like '%" + Country + "%'";
        //    ds = dp.ReturnDataSet(query, DataPortalQueryType.SqlQuery);
        //    int CountryCode = Convert.ToInt32(ds.Tables[0].Rows[0]["CountryCode"]);
        //    return CountryCode;

        //}
        public static void BulkInsert(DataRow data, string[] columnName)
        {

            var dp = new Database();
            DataSet ds;
            //for (int j = 0; j < columnName.Length; j++)
            //{
            //    if (columnName[j] == "State")
            //    {
            //        string query1 = "select State from MedicalFacility left join states on	MedicalFacility.State=States.StateID and States.StateName="+data[j];
            //        ds = dp.ReturnDataSet(query1, DataPortalQueryType.SqlQuery);
            //        int state = Convert.ToInt32(ds.Tables[0].Rows[0][0]);

            //    }
            //}
            int state = 1895;

            string query = "insert into dbo.TestMedicalFacility (";
            for (int i = 0; i < columnName.Length; i++)
            {
                query += columnName[i];
                if (i < columnName.Length - 1)
                    query += ",";
            }
            query += ") values (";
            for (int i = 0; i < columnName.Length; i++)
            {
                if (columnName[i] == "State")
                {
                    string query1 = "select StateID from states where StateName like '%" + data[i] + "%'";
                    ds = dp.ReturnDataSet(query1, DataPortalQueryType.SqlQuery);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        state = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                        query += state;
                    }
                    else
                    {
                        query += state;
                    }
                }
                else
                {
                    query += "'" + data[i] + "'";
                }
                if (i < columnName.Length - 1)
                    query += ",";
            }
            query += ")";

            dp.SubmitData(query, DataPortalQueryType.SqlQuery);
        }

        public static DataSet SearchFeild(List<string> value, List<string> Field, List<string> choise, int userType, Int64 userId, int pageNo, int PageSize)
        {
            string type = "";
            int value1 = value.Count;
            for (int c = 0; c < choise.Count; c++)
            {
                bool isField = false;
                if (choise[c] == "Balance" || choise[c] == "DOB" || choise[c] == "ActualDateReceived" || choise[c] == "CreatedOn" || choise[c] == "DateCompleted")
                {
                    if (choise[c] == "Balance")
                    {
                        isField = true;
                        type += Field[c] + value[c];
                        value1 = value1 - 1;
                        if (isField && c < value.Count - 1) type += " and ";
                    }
                    else if (choise[c] == "DOB" || choise[c] == "ActualDateReceived" || choise[c] == "CreatedOn" || choise[c] == "DateCompleted")
                    {
                        isField = true;
                        type += Field[c] + " ='" + value[c] + "'";
                        value1 = value1 - 1;
                        if (isField && c < value.Count - 1) type += " and ";
                    }
                }
                else
                {
                    if (choise[c] == "PatientInfo4")
                    {

                        isField = true;
                        type += Field[c] + " like '%" + value[c] + "'";
                        if (isField && c < value.Count - 1) type += " and ";

                    }
                    else if (choise[c] != "Balance")
                    {

                        isField = true;
                        type += Field[c] + " like '" + value[c] + "%'";
                        if (isField && c < value.Count - 1) type += " and ";

                    }

                }
            }
            DataSet dsSearch = new DataSet();
            var db = new Database();

            if (type == "") type = "1='1'";
            {
                //string query = "Select * from RequestDetail where " + type;            
                //ds = dl.ReturnDataSet(query, DataPortalQueryType.SqlQuery);

                db.AddLongParameter("@UserId", userId);
                db.AddStringParameter("@Sname", type);
                db.AddIntParameter("@Flag", userType);
                db.AddIntParameter("@PageNo", pageNo);
                db.AddIntParameter("@PageSize", PageSize);
                dsSearch = db.ReturnDataSet("sp_SearchRequestor", DataPortalQueryType.StoredProcedure);
                //dsSearch = db.ReturnDataSet("sp_SearchRequestor_ForTest", DataPortalQueryType.StoredProcedure);
            }
            return dsSearch;
        }


        public DataSet SearchReqs(string str, int userType, Int64 userId, int pageNo, int PageSize, string SortOrder = "")
        {

            DataSet dsSearch = new DataSet();
            var db = new Database();

            if (str == "") str = "1='1'";
            {
                //string query = "Select * from RequestDetail where " + type;            
                //ds = dl.ReturnDataSet(query, DataPortalQueryType.SqlQuery);

                db.AddLongParameter("@UserId", userId);
                db.AddStringParameter("@Sname", str);
                db.AddIntParameter("@Flag", userType);
                db.AddIntParameter("@PageNo", pageNo);
                db.AddIntParameter("@PageSize", PageSize);
                db.AddStringParameter("@SortOrder", SortOrder);
                dsSearch = db.ReturnDataSet("sp_SearchRequestor", DataPortalQueryType.StoredProcedure);
                //dsSearch = db.ReturnDataSet("sp_SearchRequestor_ForTest", DataPortalQueryType.StoredProcedure);
            }
            return dsSearch;
        }
        public static int CheckStateName(string statename)
        {
            var dl = new Database();
            DataTable dt = dl.ReturnDataTable("Select count(*) from states where StateName='" + statename + "'", DataPortalQueryType.SqlQuery);
            int count = Convert.ToInt32(dt.Rows[0][0]);
            return count;

        }


    }
}
