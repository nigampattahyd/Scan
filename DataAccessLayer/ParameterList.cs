using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace DataAccessLayer
{
   public class ParameterList
    {
        private  readonly DataBaseType _dbType;
        private  readonly ArrayList _paramList = new ArrayList();
        private  string _parameterStringEmptyValue = string.Empty;

        #region Private Property

        private  int GetInt(object value)
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

        private  long GetLong(object value)
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

        private  decimal GetDecimal(object value)
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

        private  bool GetBit(object value)
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

        private  string GetDateTime(object value)
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

        private  Guid GetGuid(object value)
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

        #region Add parameters
        public void AddStringParameter(string paramName, string paramValue)
            {
            AddStringParameter(paramName, paramValue, false);
            }

        public void AddStringParameter(string paramName, string paramValue, bool paramIsOutput)
            {
            //var pList = new ParameterList();
            this.AddParamList(SqlDbType.VarChar, paramName, paramValue, paramIsOutput);
            }

        public void AddIntParameter(string paramName, int paramValue)
            {
            AddIntParameter(paramName, paramValue, false);
            }

        public void AddIntParameter(string paramName, int paramValue, bool paramIsOutput)
            {
            //var pList = new ParameterList();
            this.AddParamList(SqlDbType.Int, paramName, paramValue, paramIsOutput);
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
            //var pList = new ParameterList();
            this.AddParamList(SqlDbType.BigInt, paramName, paramValue, paramIsOutput);

            }

        public void AddDateTimeParameter(string paramName, DateTime paramValue)
            {
            AddDateTimeParameter(paramName, paramValue, false);
            }

        public void AddDateTimeParameter(string paramName, DateTime paramValue, bool paramIsOutput)
            {
            //var pList = new ParameterList();
            this.AddParamList(SqlDbType.DateTime, paramName, paramValue, paramIsOutput);
            }
        #endregion

        public void AddParamList(SqlDbType sqlType, string paramName, object paramValue, bool paramIsOutput)
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

        private string SetStringParameterEmptyValue
        {
            get { return _parameterStringEmptyValue; }
            set { _parameterStringEmptyValue = value; }
        }
    }
}
