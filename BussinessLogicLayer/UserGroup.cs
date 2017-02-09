using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccessLayer;

namespace BussinessLogicLayer
{
    public class UserGroup
    {
        public static DataSet ContactGroup(int flag, Int64 GroupId=0)
        {
            var db = new Database();
            DataSet ds = new DataSet();
                if (flag == 1)
                {
                    db.AddIntParameter("@Flag", flag);
                    ds = db.ReturnDataSet("sp_GetUserGroupList", DataPortalQueryType.StoredProcedure);
                }
                else if (flag == 2 || flag == 3 || flag == 4)
                {
                    db.AddIntParameter("@Flag", flag);
                    db.AddLongParameter("@GroupId", GroupId);
                    ds=db.ReturnDataSet("sp_GetUserGroupList", DataPortalQueryType.StoredProcedure);
                }             
           return ds;
        }
       
        public static bool CreateGroup(String GName, string UId, int flag)
        {
            var database = new Database();
            database.AddStringParameter("@GroupName", GName);
            database.AddStringParameter("@UId", UId);
            database.AddIntParameter("@Flag", flag);
            try
            {
                database.SubmitData("sp_CreateGroup", DataPortalQueryType.StoredProcedure);
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }

        }
        public static bool EditGroupByGroupId(Int64 GroupId, string strUId, int flag)
        {
            var database = new Database();
            database.AddLongParameter("@GroupId", GroupId);
            database.AddStringParameter("@UId", strUId);
            database.AddIntParameter("@Flag", flag);
            try
            {
                database.SubmitData("sp_CreateGroup", DataPortalQueryType.StoredProcedure);
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }

        }
        public static bool checkGroup(string GroupName, out Int64 count)
        {
            var database = new Database();
            database.AddStringParameter("@GroupName", GroupName);
            database.AddLongParameter("count", 0, true);
            try
            {
                database.SubmitData("sp_checkGroupName", DataPortalQueryType.StoredProcedure);
                count = Convert.ToInt64(database.GetParameterValue("count"));
                return true;
            }
            catch (Exception ex)
            {
                //ex.ToString();
                count = 0;
                return false;
            }

        }
        public static void DeleteGroupByGroupId(Int64 GroupId, Int64 UserId, int flag)
        {
            var database = new Database();
            database.AddLongParameter("@GroupId", GroupId);
            database.AddLongParameter("@UserId", UserId);
            database.AddIntParameter("@Flag", flag);
            database.SubmitData("sp_CreateGroup", DataPortalQueryType.StoredProcedure);
        }

      
        public static bool CreateUserGroup(Int64 UserId, Int64 GId, int flag)
        {
            var database = new Database();
            database.AddLongParameter("@UserId", UserId);
            database.AddLongParameter("@GroupId", GId);
            database.AddIntParameter("@Flag", flag);
            try
            {
                database.SubmitData("sp_CreateUserGroup", DataPortalQueryType.StoredProcedure);
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }

        }

        public static DataSet CheckUserGroupExist(Int64 GroupId)
        {
            var database = new Database();
            database.AddLongParameter("@GroupId", GroupId);
            return database.ReturnDataSet("sp_GetUserGroupExist", DataPortalQueryType.StoredProcedure);
        }

    }
}
