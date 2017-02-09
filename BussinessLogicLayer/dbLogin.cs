using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccessLayer;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

namespace BussinessLogicLayer
{
    public class dbLogin : System.Web.UI.Page
    {
        public static DataSet GetAllMedicalFacilities(int flag, Int64 userId, string sname, int pageno, int pagesize, string sortorder)
        {
            var dl = new Database();
            dl.AddLongParameter("@UserId", userId);
            dl.AddStringParameter("@Sname", sname);
            dl.AddIntParameter("@Flag", flag);
            dl.AddIntParameter("@PageNo", pageno);
            dl.AddIntParameter("@PageSize", pagesize);
            dl.AddStringParameter("@SortOrder", sortorder);
            return dl.ReturnDataSet("sp_MedicalFacilityList", DataPortalQueryType.StoredProcedure);

            //var dl = new Database();
            //dl.AddLongParameter("@UserId", userId);
            //dl.AddIntParameter("@Flag", flag);
            //return dl.ReturnDataTable("sp_MedicalFacilityList", DataPortalQueryType.StoredProcedure);
        }

        public static DataTable GetAllUsers(Int64 userId, int flag)
        {
            var dl = new Database();
            dl.AddLongParameter("@UserId", userId);
            dl.AddIntParameter("@Flag", flag);
            return dl.ReturnDataTable("sp_GetUserList", DataPortalQueryType.StoredProcedure);
        }

        public static DataTable GetAllUsersByReasonId(Int64 relReasonId)
        {
            var dl = new Database();
            return dl.ReturnDataTable("Select * from  dbo.UserLoginInfo left join	 states on	dbo.UserLoginInfo.State=States.StateID where RecID=" + relReasonId, DataPortalQueryType.SqlQuery);
        }

        public static DataTable GetSearchReqPOP(Int64 RequestNumber, int flag)
        {
            var db = new Database();
            db.AddLongParameter("@RequestNumber", RequestNumber);
            db.AddLongParameter("@flag", flag);
            return db.ReturnDataTable("sp_SearchReqPOP", DataPortalQueryType.StoredProcedure);
        }
        public static DataTable GetAllMedicalFacilityByReferenceNumber(Int64 refNumber)
        {
            var dl = new Database();
            return dl.ReturnDataTable("select CONVERT(VARCHAR(10),CreatedOn,101) as CreatedOn1 ,* from  MedicalFacility left join states on MedicalFacility.State=States.StateID where MedicalFacilityRefNo=" + refNumber, DataPortalQueryType.SqlQuery);
        }

        public static DataTable GetAllMedicalFacilityByUserName(Int64 refNumber)
        {
            var dl = new Database();
            return dl.ReturnDataTable("select ul.FullName from UserLoginInfo ul left join MedicalFacility mf on mf.AddedBy = ul.RecID where mf.MedicalFacilityRefNo=" + refNumber, DataPortalQueryType.SqlQuery);
        }

        public static DataTable GetUserDetailsByUserName(string username)
        {
            var dl = new Database();

            return dl.ReturnDataTable("Select * from  dbo.UserLoginInfo left join states on	dbo.UserLoginInfo.State=States.StateID where UserName='" + username + "' ", DataPortalQueryType.SqlQuery); //AND IsActive=1
        }
        public static DataTable GetUserDetailsByUserId(Int64 userId)
        {
            var db = new Database();
            return db.ReturnDataTable("Select * from  dbo.UserLoginInfo left join states on	dbo.UserLoginInfo.State=States.StateID where RecID=" + userId, DataPortalQueryType.SqlQuery);
        }

        public static DataTable GetUserRightsByUserId(Int64 userId)
        {
            var dl = new Database();
            return dl.ReturnDataTable("Select * from dbo.UserRights where UserAccountNumber=" + userId, DataPortalQueryType.SqlQuery);
        }

        public static DataTable GetAccociatedMedicalFacilitiesByUserId(Int64 userId)
        {
            var dl = new Database();
            dl.AddLongParameter("@UserId", userId);
            return dl.ReturnDataTable("sp_GetAccociatedMedicalFacilitiesByUserId", DataPortalQueryType.StoredProcedure);
        }

        public bool Check_Session
        {
            get
            {
                if (Session["UserID"] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public void Set_Page(string redirectUrl)
        {
            //storage current page information before redirecting for relogin on session expire
            Session["redirecturl"] = redirectUrl;

        }

        public static DataTable GetUserTypes(int flag = 0, int usertypeid = 0)
        {
            string strquery = string.Empty;
            if (flag == 1 || flag == 6)
            {
                if (usertypeid == 8)
                {
                    strquery = "Select * from UserTypes where UserTypeId =8";
                }
                else
                {
                    strquery = "Select * from UserTypes where UserTypeId <> 8";
                }
            }
            else
            {
                strquery = "Select * from UserTypes where UserTypeId = " + usertypeid + " ";
            }

            var database = new Database();
            return database.ReturnDataTable(strquery, DataPortalQueryType.SqlQuery);
        }
        public static DataTable GetUserTypesForCreate()
        {
            var database = new Database();
            return database.ReturnDataTable("Select * from UserTypes where UserTypeId !=8", DataPortalQueryType.SqlQuery);
        }
        public static bool CreateUser(User user, out Int64 userid)
        {
            var database = new Database();
            database.AddLongParameter("@UserAccNumber", user.UserId);
            database.AddStringParameter("@UserName", user.UserName);
            database.AddStringParameter("@EmailID", user.EmailId);
            database.AddStringParameter("@PhoneNumber", user.PhoneNumber);
            database.AddIntParameter("@UserType", user.UserType);
            database.AddStringParameter("@FullName", user.FullName);
            database.AddStringParameter("@Initials", user.Initials);
            database.AddStringParameter("@Address1", user.Address1);
            database.AddStringParameter("@Address2", user.Address2);
            database.AddStringParameter("@Address3", user.Address3);
            database.AddStringParameter("@City", user.City);
            database.AddIntParameter("@State", user.State);
            database.AddIntParameter("@Country", user.Country);
            database.AddStringParameter("@ZIP", user.ZIP);
            database.AddStringParameter("@FaxNumber", user.FaxNumber);
            database.AddLongParameter("userid", 0, true);
            database.AddStringParameter("@Password", user.Password);

            try
            {
                database.SubmitData("sp_CreateUser", DataPortalQueryType.StoredProcedure);
                userid = Convert.ToInt64(database.GetParameterValue("userid"));
                return true;
            }
            catch (Exception ex)
            {
                userid = 0;
                return false;
            }

        }

        public static int DeleteUserByUserId(Int64 UserId)
        {
            var database = new Database();
            database.AddLongParameter("UserId", UserId);
            database.AddLongParameter("IsActiveVal", -1, true);
            database.SubmitData("sp_DeleteUserByUserId", DataPortalQueryType.StoredProcedure);
            return Convert.ToInt32(database.GetParameterValue("IsActiveVal"));
        }

        public static DataTable CheckUserNameAvailability(string username)
        {
            var dl = new Database();
            return dl.ReturnDataTable("Select UserName from UserLoginInfo where UserName='" + username + "'", DataPortalQueryType.SqlQuery);
        }
        public static DataTable CheckUserTypeAvailability(string userType)
        {
            var dl = new Database();
            return dl.ReturnDataTable("Select count(*) from UserTypes where UserType='" + userType + "'", DataPortalQueryType.SqlQuery);
        }
        public static void SaveMedicalFacilityWithUserType(Int64 UserId, Int64 MedicalFacility)
        {
            var dl = new Database();
            dl.AddLongParameter("@UserId", UserId);
            dl.AddLongParameter("@Medicalfacility", MedicalFacility);
            dl.SubmitData("sp_AssociateMedicalFacilitiesWithUserId", DataPortalQueryType.StoredProcedure);
        }
        public static void RemoveMedicalFacilityAssociationByUserId(Int64 UserId, Int64 MedicalFacility)
        {
            var dl = new Database();
            dl.AddLongParameter("@UserId", UserId);
            dl.AddLongParameter("@Medicalfacility", MedicalFacility);
            dl.SubmitData("sp_DeleteAssociateMedicalFacilitiesWithUserId", DataPortalQueryType.StoredProcedure);
        }

        public string SearchUser(List<string> value, List<string> Field)
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
                    if (value[i] == "All User")
                    {
                        //SearchCondition += Field[i] + " IN " + "(1, 3, 5, 6, 7, 8, 9, 10, 11)";
                        SearchCondition += Field[i] + " IN " + "(select UserTypeId from UserTypes)";
                    }
                    else if (value[i] == "Internal User")
                    {
                        //SearchCondition += Field[i] + " IN " + "(1, 10, 7, 5)";
                        SearchCondition += Field[i] + " NOT IN " + "('Facility2','Facility1', 'Requester')";
                    }
                    else if (value[i] == "Requester User")
                    {
                        //SearchCondition += Field[i] + " IN " + "(8)";
                        SearchCondition += Field[i] + " IN " + "('Requester')";
                    }
                    else if (value[i] == "Facility User")
                    {
                        //SearchCondition += Field[i] + " IN " + "(9, 11)";
                        SearchCondition += Field[i] + " IN " + "('Facility2','Facility1')";
                    }
                    
                    else
                    {
                        SearchCondition += Field[i] + " like '" + value[i] + "%'";
                    }
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
            //    //if (Type[i] == "State")
            //    //    {
            //    //    isField = true;
            //    //    type += " State = " + value[i] + " ";
            //    //    }
            //    if (isField && i < Type.Count - 1) type += " and ";
            //}

           // var dl = new Database();
            //if (type == "") type = "1='1'";
           // string query = "";
            //if (flg == 1 || flg == 6)
            //{
            //    query = " Select ul.*,st.StateName,ut.UserType as UTypeName from  dbo.UserLoginInfo ul left join states st on ul.State=st.StateID \n" +
            //    " inner join UserTypes ut on ul.userType=ut.UserTypeId where " + SearchCondition;
            //}
            //if (flg == 2 || flg == 3 || flg == 4)
            //{
            //    query = " Select ul.*,st.StateName,ut.UserType as UTypeName from  dbo.UserLoginInfo ul left join states st on ul.State=st.StateID \n" +
            //  " inner join UserTypes ut on ul.userType=ut.UserTypeId where RecID=" + userId + " AND " + SearchCondition;
            //}
            //return dl.ReturnDataTable(query, DataPortalQueryType.SqlQuery);
            return SearchCondition;
        }

        public static DataTable GetSchema()
        {
            var db = new Database();
            return db.ReturnDataTable("select * from dbo.UserLoginInfo where 1 !='1'", DataPortalQueryType.SqlQuery);
        }

        public static void InsertBulkUser(DataRow data, string[] columnName)
        {
            var dp = new Database();
            string query = "insert into dbo.UserLoginInfo (";
            for (int i = 0; i < columnName.Length; i++)
            {
                query += columnName[i];
                if (i < columnName.Length - 1)
                    query += ",";
            }
            query += ") values (";
            for (int i = 0; i < columnName.Length; i++)
            {
                query += "'" + data[i] + "'";
                if (i < columnName.Length - 1)
                    query += ",";
            }
            query += ")";

            dp.SubmitData(query, DataPortalQueryType.SqlQuery);

        }

        public static DataTable GetAllCountries()
        {
            var db = new Database();
            return db.ReturnDataTable("select distinct Countryname,countrycode from dbo.states", DataPortalQueryType.SqlQuery);
        }

        public static DataTable GetStateByStateId(int stateId)
        {
            var db = new Database();
            return db.ReturnDataTable("select * from dbo.states where StateID=" + stateId, DataPortalQueryType.SqlQuery);
        }

        public static DataTable GetCountryByCountryId(int countryId)
        {
            var db = new Database();
            return db.ReturnDataTable("select distinct Countryname,countrycode from dbo.states where CountryCode" + countryId, DataPortalQueryType.SqlQuery);
        }

        public static DataTable GetAllStatesByCountryId(int countryid)
        {
            var db = new Database();
            return db.ReturnDataTable("select * from dbo.states where CountryCode=" + countryid + " and Active = 'True'", DataPortalQueryType.SqlQuery);
        }
        public static DataTable GetAllStates()
        {
            var db = new Database();
            return db.ReturnDataTable("select * from dbo.states where Active = 'True' order by StateName", DataPortalQueryType.SqlQuery);
        }
        public static int GetUserTypeIdByUserType(string UserType)
        {
            //var db = new Database();
            //return db.ReturnDataTable("select UserTypeId from dbo.UserTypes where UserType='" + UserType+"'", DataPortalQueryType.SqlQuery);
            int UserTypeId;
            var dp = new Database();
            DataSet ds;
            string query = "select UserTypeId from dbo.UserTypes where UserType='" + UserType + "'";
            ds = dp.ReturnDataSet(query, DataPortalQueryType.SqlQuery);
            return UserTypeId = Convert.ToInt32(ds.Tables[0].Rows[0]["UserTypeId"]);

        }

        public static DataTable GetInvoiceDetails(Int64 requestNumber)
        {
            var db = new Database();
            return db.ReturnDataTable("SELECT rs.Fax,inv.PatientInfo1, inv.PatientInfo2,Req.RequestNumber,inv.BillingInfo1,Req.RequestStatus,Req.ReleaseCode,rs.EmailAddress,rs.RequestorId FROM dbo.Requests AS Req Left outer JOIN dbo.Requestors AS rs ON " +
                "Req.RequestorId =rs.RequestorId INNER JOIN dbo.Invoices AS inv ON Req.InvoiceId = inv.InvoiceId where Req.RequestNumber=" + requestNumber + "", DataPortalQueryType.SqlQuery);
        }

        //public static DataSet GetSortField(string name, Int64 userId, int flag)
        //{
        //    var db = new Database();
        //    db.AddLongParameter("@UserId", userId);
        //    db.AddStringParameter("@Sname", name);
        //    db.AddIntParameter("@Flag", flag);
        //    return db.ReturnDataSet("sp_MedicalFacilitySort", DataPortalQueryType.StoredProcedure);           
        //}

        public static DataSet GetUserSortField(string name, Int64 userId, int flag)
        {
            var db = new Database();
            db.AddLongParameter("@UserId", userId);
            db.AddStringParameter("@Sname", name);
            db.AddIntParameter("@Flag", flag);
            return db.ReturnDataSet("sp_UserSort", DataPortalQueryType.StoredProcedure);
        }
        public static void byteArrayToImage(MemoryStream byteArrayIn)
        {
            try
            {
                //MemoryStream ms = new MemoryStream(byteArrayIn, 0, byteArrayIn.Length);
                //ms.Write(byteArrayIn, 0, byteArrayIn.Length);
                //return Image.FromStream(ms); 

                //Stream stream = new MemoryStream(byteArrayIn);
                //ms.Seek(0, SeekOrigin.Begin);
                // Image returnImage = Image.FromStream(ms);
                //  var temp = Image.FromStream(byteArrayIn);
                //Bitmap returnImage = new Bitmap(stream);
                // return returnImage;
                //using (var ms = new MemoryStream(byteArrayIn))
                //using (var bmp = Image.FromStream(ms))
                //{
                //    // do something with the bitmap
                //}}
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public static string NormalizeLength(string value, int maxLength)
        {
            if (value.Length > maxLength)
            {
                return value.Substring(0, maxLength);
            }
            else
                return value;
        }
        public DataTable GetRequestQueueCount(int flg, Int64 userid, Int64 MedicalFacilityRefNo)
        {
            var db = new Database();
            db.AddLongParameter("@UserId", userid);
            db.AddLongParameter("@MedicalFacilityRefNo", MedicalFacilityRefNo);
            db.AddIntParameter("@Flag", flg);
            return db.ReturnDataTable("sp_GetRequestQueueCount", DataPortalQueryType.StoredProcedure);
        }
        public DataSet GetSpecialitUser(int flg, Int64 requestNumber)
        {
            var db = new Database();
            db.AddIntParameter("@Flag", flg);
            db.AddLongParameter("@RequestNumber", requestNumber);
            return db.ReturnDataSet("sp_GetSpecialistUser", DataPortalQueryType.StoredProcedure);
        }
        public DataTable GetAllMedicalFacilityByRequestNumber(Int64 requestNumber)
        {
            var dl = new Database();
            return dl.ReturnDataTable("select CONVERT(VARCHAR(10),CreatedOn,101) as CreatedOn1,MedicalFacilityRefNo ,Nickname,Name,Address1,City,StateName,ZIP from  MedicalFacility left join \n" +
                "states on MedicalFacility.State=States.StateID where MedicalFacilityRefNo=(select top(1)MedicalFacilityRefNo from Requests where RequestNumber=" + requestNumber + ")", DataPortalQueryType.SqlQuery);
        }
        public DataSet GetAllUsersList(int flag, Int64 userId, string sname, int pageno, int pagesize, string sortorder)
        {
            var dl = new Database();
            dl.AddLongParameter("@UserId", userId);
            dl.AddStringParameter("@Sname", sname);
            dl.AddIntParameter("@Flag", flag);
            dl.AddIntParameter("@PageNo", pageno);
            dl.AddIntParameter("@PageSize", pagesize);
            dl.AddStringParameter("@SortOrder", sortorder);
            return dl.ReturnDataSet("sp_GetUserList", DataPortalQueryType.StoredProcedure);
           // return dl.ReturnDataSet("sp_GetUserList_Test", DataPortalQueryType.StoredProcedure);
        }

        public static int chkUserType(int FlagUserType)
        {
            // 1- Administrator, 2- BHS Manager, 3- Executive, 4- HR Manager, 5- Processor, 6- Provider, 7- Specialist
            // 8- Requester, 9-Facility2, 10- Supervisor, 11- Facility1 

            int FlagMF = 0;
            if (FlagUserType == 1 || FlagUserType == 2 || FlagUserType == 3 || FlagUserType == 4)
            {
                FlagMF = 1;// Administrator,BHS Manager,Executive,HR Manager
            }
            else if (FlagUserType == 6)
            {
                FlagMF = 2;//Customer/provider
            }
            else if (FlagUserType == 7)
            {
                FlagMF = 3;//Specilist
            }
            else if (FlagUserType == 8)
            {
                FlagMF = 4;//Requestor
            }
            else if (FlagUserType == 5)
            {
                FlagMF = 5;//Processor
            }
            else if (FlagUserType == 10)
            {
                FlagMF = 6;//Supervissor
            }
            else if (FlagUserType == 9)
            {
                FlagMF = 7;//Facility2
            }
            else if (FlagUserType == 11)
            {
                FlagMF = 8;//Facility1
            }
            else
            {
                FlagMF = 0;//none
            }
            return FlagMF;
        }

    }
}
