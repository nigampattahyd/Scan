using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer
    {
    public enum DataPortalDataType
        {
        Date,
        Double,
        Integer,
        String,
        Text,
        Long
        }

    public enum DataPortalQueryType
        {
        StoredProcedure,
        SqlQuery,
        Table
        }

    public enum DataBaseType
        {
        MSSQL,
        MySql,
        Oracle,
        MsAccess
        }
    }
