using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;

namespace DataAccessLayer
    {
    public sealed class Database
        {
        #region Private Variables...

        private readonly DataBaseType _dbType;
        private readonly ArrayList _paramList = new ArrayList();
        private bool _isTransactionBegin;
        private SqlCommand _sqlCmd;
        private SqlConnection _sqlCon;
        private string _parameterStringEmptyValue = string.Empty;

        #endregion Private Variables
        #region Private Property

        private int GetInt(object value)
            {
            try
                {
                return int.Parse(value.ToString());
                }
            catch
                {
                return -1;
                }
            }

        private long GetLong(object value)
            {
            try
                {
                return long.Parse(value.ToString());
                }
            catch
                {
                return 0;
                }
            }

        private decimal GetDecimal(object value)
            {
            try
                {
                return decimal.Parse(value.ToString());
                }
            catch
                {
                return -1;
                }
            }

        private bool GetBit(object value)
            {
            try
                {
                return Convert.ToBoolean(value);
                }
            catch
                {
                return false;
                }
            }

        private string GetDateTime(object value)
            {
            try
                {
                return DateTime.Parse(value.ToString()).ToString();
                }
            catch
                {
                return null;
                }
            }

        private Guid GetGuid(object value)
            {
            try
                {
                return Guid.Parse(value.ToString());
                }
            catch
                {
                return Guid.Empty;
                }
            }

        #endregion Property
        #region GetConnection

        private string GetConnectionString
            {
            get
                {
                switch (_dbType)
                    {
                    case DataBaseType.MSSQL:
                        return ConfigurationManager.ConnectionStrings["D_ForceConnectionString"].ConnectionString;
                        break;

                    default:
                        return ConfigurationManager.ConnectionStrings["D_ForceConnectionString"].ConnectionString;
                        break;
                    }
                }
            }

        private SqlConnection GetSqlConnection()
            {
            if ((_sqlCon == null) || (_sqlCon.State == ConnectionState.Closed))
                return new SqlConnection(GetConnectionString);
            return _sqlCon;
            }


        #endregion GetConnection

        #region DataTable

        public DataTable ReturnDataTable(string sqlQuery)
            {
            return ReturnDataTable(sqlQuery, DataPortalQueryType.SqlQuery);
            }

        public DataTable ReturnDataTable(string sqlQuery, DataPortalQueryType cmdType)
            {
            switch (_dbType)
                {
                case DataBaseType.MSSQL:
                    switch (cmdType)
                        {
                        case DataPortalQueryType.StoredProcedure:
                            return ReturnDataTable(sqlQuery, null);
                            break;
                        case DataPortalQueryType.SqlQuery:
                            return ReturnDataTableByQuery(sqlQuery);
                            break;
                        case DataPortalQueryType.Table:
                            break;
                        }
                    break;

                }
            return null;
            }

        private DataTable ReturnDataTable(string procName, string temp)
            {
            DataSet ds = new DataSet();
            var conn = new SqlConnection(GetConnectionString);

            try
                {
                var cmd = new SqlCommand(procName, conn);
                if ((_paramList != null) && (_paramList.Count != 0))
                    cmd.Parameters.AddRange(_paramList.ToArray());
                cmd.CommandType = CommandType.StoredProcedure;
                var adapter = new SqlDataAdapter(cmd);
                conn.Open();
                cmd.Prepare();
                cmd.CommandTimeout = 1000;
                adapter.Fill(ds);
                }

            catch (Exception ex)
                {
                //log the error
                //throw the exception
                throw ex;
                }
            finally
                {
                //either close the DB or return the conn to the pool
                conn.Close();
                }

            return ds.Tables[0];
            }

        private DataTable ReturnDataTableByQuery(string sqlQuery)
            {
            var ds = new DataSet();
            try
                {
                var adapter = new SqlDataAdapter(sqlQuery, GetConnectionString);
                adapter.Fill(ds);
                }

            catch (Exception ex)
                {
                //log the error
                //throw the exception
                throw ex;
                }
            return ds.Tables[0];
            }

        #endregion DataTable

        public void AddStringParameter(string paramName, string paramValue)
            {
            AddStringParameter(paramName, paramValue, false);
            }

        public void AddStringParameter(string paramName, string paramValue, bool paramIsOutput)
            {            
             AddParamList(SqlDbType.VarChar, paramName, paramValue, paramIsOutput);
            }

        public void AddIntParameter(string paramName, int paramValue)
            {
            AddIntParameter(paramName, paramValue, false);
            }

        public void AddBitParameter(string paramName, bool paramValue)
        {
            AddBitParameter(paramName, paramValue, false);
        }
        public void AddBitParameter(string paramName, bool paramValue, bool paramIsOutput)
        {
            AddParamList(SqlDbType.Bit, paramName, paramValue, paramIsOutput);
        }
        
            
        public void AddIntParameter(string paramName, int paramValue, bool paramIsOutput)
            {
             
            AddParamList(SqlDbType.Int, paramName, paramValue, paramIsOutput);
            //if (dbType == DataBaseType.MSSQL)
            //{
            //    SqlDbType sqlType = SqlDbType.VarChar;
            //    sqlType = SqlDbType.Int;
            //    //Create the parameter, set its properties, and add it to the Parameter List.
            //    SqlParameter newParam = new SqlParameter(paramName, sqlType);
            //    newParam.Value = paramValue;
            //    if (paramIsOutput) { newParam.Direction = ParameterDirection.Output; }
            //    paramList.Add(newParam);
            //}
            }

        public void AddLongParameter(string paramName, Int64 paramValue)
            {
            AddLongParameter(paramName, paramValue, false);
            }

        public void AddLongParameter(string paramName, Int64 paramValue, bool paramIsOutput)
            {
            AddParamList(SqlDbType.BigInt, paramName, paramValue, paramIsOutput);
            }
            

        public void AddDateTimeParameter(string paramName, DateTime paramValue)
            {
            AddDateTimeParameter(paramName, paramValue, false);
            }

        public void AddDateTimeParameter(string paramName, DateTime paramValue, bool paramIsOutput)
            {
            AddParamList(SqlDbType.DateTime, paramName, paramValue, paramIsOutput);
            }

        private void AddParamList(SqlDbType sqlType, string paramName, object paramValue, bool paramIsOutput)
            {
            SqlParameter newParam = null;
            if (_dbType != DataBaseType.MSSQL) return;
            switch (sqlType)
                {
                case SqlDbType.VarChar:
                    sqlType = SqlDbType.VarChar;
                    newParam = new SqlParameter(paramName, sqlType)
                    {
                        Value = (paramValue.ToString() == string.Empty) &&
                                (SetStringParameterEmptyValue == null)
                                    ? DBNull.Value
                                    : paramValue
                    };
                    break;
                case SqlDbType.Int:
                    sqlType = SqlDbType.Int;
                    newParam = new SqlParameter(paramName, sqlType)
                    {
                        Value = (GetInt(paramValue) == -1) ? DBNull.Value : paramValue
                    };
                    break;
                case SqlDbType.BigInt:
                    sqlType = SqlDbType.BigInt;
                    newParam = new SqlParameter(paramName, sqlType)
                    {
                        Value = (GetLong(paramValue) == 0) ? DBNull.Value : paramValue
                    };
                    break;
                case SqlDbType.Decimal:
                    sqlType = SqlDbType.Decimal;
                    newParam = new SqlParameter(paramName, sqlType)
                    {
                        Value = (GetDecimal(paramValue) == -1) ? DBNull.Value : paramValue
                    };
                    break;
                case SqlDbType.Bit:
                    sqlType = SqlDbType.Bit;
                    newParam = new SqlParameter(paramName, sqlType)
                    {
                        Value = paramValue
                    };
                    break;
                case SqlDbType.DateTime:
                    sqlType = SqlDbType.DateTime;
                    newParam = new SqlParameter(paramName, sqlType)
                    {
                        Value = (GetDateTime(paramValue) == null)
                                    ? DBNull.Value
                                    : (paramValue.ToString().IndexOf("1/1/0001") > -1
                                           ? DBNull.Value
                                           : paramValue)
                    };
                    break;
                case SqlDbType.UniqueIdentifier:
                    sqlType = SqlDbType.UniqueIdentifier;
                    newParam = new SqlParameter(paramName, sqlType)
                    {
                        Value = GetGuid(paramValue)
                        //Value = (GetGuid(paramValue) == Guid.Empty) ? DBNull.Value : paramValue
                    };
                    break;
                }

            if (paramIsOutput)
                {
                newParam.Direction = ParameterDirection.Output;
                }
            _paramList.Add(newParam);
            }

        public object GetParameterValue(string arg)
            {
            return
                (from object t in _paramList where arg == t.ToString() select ((SqlParameter)t).Value).FirstOrDefault();
            }

        public string SetStringParameterEmptyValue
            {
            get { return _parameterStringEmptyValue; }
            set { _parameterStringEmptyValue = value; }
            }

        public void SubmitData(string sqlQuery, DataPortalQueryType cmdType)
            {
            switch (_dbType)
                {
                case DataBaseType.MSSQL:
                    switch (cmdType)
                        {
                        case DataPortalQueryType.StoredProcedure:
                            SubmitData(sqlQuery);
                            break;
                        case DataPortalQueryType.SqlQuery:
                            SubmitDataQuery(sqlQuery);
                            break;
                        case DataPortalQueryType.Table:
                            break;
                        }
                    break;
                
                }
            }
       
        private void SubmitData(string procName)
            {
            var ds = new DataSet();
            try
                {
                _sqlCon = GetSqlConnection();
                _sqlCmd = new SqlCommand(procName, _sqlCon);
                if ((_paramList != null) && (_paramList.Count != 0))
                    _sqlCmd.Parameters.AddRange(_paramList.ToArray());
                _sqlCmd.CommandType = CommandType.StoredProcedure;
                var adapter = new SqlDataAdapter(_sqlCmd);
                if (_sqlCon.State == ConnectionState.Closed) _sqlCon.Open();
                //CheckTransaction(_sqlCmd);
                _sqlCmd.Prepare();
                _sqlCmd.CommandTimeout = 1000;
                adapter.Fill(ds);
                //log.WriteLine(ProcName+" Stored Procedure executed succssfully"); 
                }
            catch (Exception ex)
                {
                //log the error
                // log.WriteLine(ex.Message.ToString());
                //if (_isTransactionBegin) RollBackTransaction();
                throw ex;
                }
            finally
                {
                if (!_isTransactionBegin)
                    CloseSqlConnection(_sqlCon, _sqlCmd);
                }
            }

        private void SubmitDataQuery(string query)
            {
            try
                {
                _sqlCon = GetSqlConnection();
                _sqlCmd = new SqlCommand(query, _sqlCon)
                {
                    CommandType = CommandType.Text
                };
                if (_sqlCon.State == ConnectionState.Closed) _sqlCon.Open();
               // CheckTransaction(_sqlCmd);
                _sqlCmd.Prepare();
                _sqlCmd.CommandTimeout = 1000;
                _sqlCmd.ExecuteNonQuery();
                }
            catch (Exception ex)
                {
                //log the error
               // if (_isTransactionBegin) RollBackTransaction();
                throw ex;
                }
            finally
                {
                if (!_isTransactionBegin) CloseSqlConnection(_sqlCon, _sqlCmd);
                }
            }

        private void CloseSqlConnection(SqlConnection con, SqlCommand cmd)
            {
            if (con != null)
                {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Dispose();
                }
            if (cmd != null)
                {
                cmd = null;
                //cmd.Dispose();  
                }
            }

        #region Using Dataset

        public DataSet ReturnDataSet(string sqlString)
            {
            return ReturnDataSet(sqlString, DataPortalQueryType.SqlQuery);
            }

        public DataSet ReturnDataSet(string sqlString, DataPortalQueryType cmdType)
            {
            switch (_dbType)
                {
                case DataBaseType.MSSQL:
                    switch (cmdType)
                        {
                        case DataPortalQueryType.StoredProcedure:
                            return ReturnDataSet(sqlString, "");
                            break;
                        case DataPortalQueryType.SqlQuery:
                            return GetDataSetByQuery(sqlString);
                            break;
                        case DataPortalQueryType.Table:
                            return GetDataSetByText();
                            break;
                        }
                    break;
                
                }
            return null;
            }

        private DataSet GetDataSetByText()
            {
            return null;
            }

        private DataSet GetDataSetByQuery(string sqlQuery)
            {
            var ds = new DataSet();
            try
                {
                var adapter = new SqlDataAdapter(sqlQuery, GetConnectionString);
                adapter.Fill(ds);
                }
            
            catch (Exception ex)
                {
                //log the error
                //throw the exception
                throw ex;
                }
            return ds;
            }

        private DataSet ReturnDataSet(string procName, string temp)
            {
            var ds = new DataSet();
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
                {
                conn = new SqlConnection(GetConnectionString);
                cmd = new SqlCommand(procName, conn);
                if ((_paramList != null) && (_paramList.Count != 0))
                    cmd.Parameters.AddRange(_paramList.ToArray());
                cmd.CommandType = CommandType.StoredProcedure;
                var adapter = new SqlDataAdapter(cmd);
                conn.Open();
                cmd.Prepare();
                cmd.CommandTimeout = 1000;
                adapter.Fill(ds);
                }
           
            catch (Exception ex)
                {
                //log the error
                //throw the exception
                throw ex;
                }
            finally
                {
                //either close the DB or return the conn to the pool
                CloseSqlConnection(conn, cmd);
                }

            return ds;
            }

        

        #endregion Using Dataset
        }
    }
