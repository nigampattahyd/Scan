using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace bhsScandll
{
    class ErrorLog
    {
        public void logerror(string page, string method, string error)
        {
            try
            {
                error = error.Replace("'", "");
                //string constr = @"server=bhsonline.us; uid=sa; password=sa!2014; database=chca_bhs";
                string conStr = ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
                DateTime dt = DateTime.Now;
                SqlConnection con = new SqlConnection(conStr);
                string strQuery = "Insert into ScanErrorlogs(UpdatedBy,UpdatedOn,module,targetmethod,message) " +
                                "values(1,'" + dt + "' ,'" + page + "','" + method + "','" + error + "')";
                SqlCommand cmd = new SqlCommand(strQuery, con);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
