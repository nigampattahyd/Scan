using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccessLayer;

namespace BussinessLogicLayer
    {
   public class Patient
        {

       public static DataTable GetAllPatient()
           {
           var dl = new Database();
           return dl.ReturnDataTable("Select * from dbo.Patients	left join	 states on	Patients.State=States.StateID ", DataPortalQueryType.SqlQuery);
           }


       public static DataTable GetPatientByPatientId(Int64 patientId)
           {
           var dl = new Database();
           return dl.ReturnDataTable("Select * from dbo.Patients	left join	 states on	Patients.State=States.StateID  where PatientId=" + patientId, DataPortalQueryType.SqlQuery);
           }


       public static bool CreatePatient(PatientDetails patient, out Int64 patientId)
           {
           var database = new Database();
           database.AddLongParameter("@PatientId", patient.PatientId);
           database.AddStringParameter("@FirstName", patient.FirstName);
           database.AddStringParameter("@LastName", patient.LastName);
           database.AddStringParameter("@Guardian", patient.Guardian);
           database.AddStringParameter("@SSN", patient.SSN);
           database.AddDateTimeParameter("@DOB", patient.DOB);
           database.AddStringParameter("@EmailAddress", patient.EmailId);
           //database.AddDateTimeParameter("@DateOfService", patient.DateOfService);
           //database.AddStringParameter("@AccountNumber", patient.AccountNumber);
           //database.AddStringParameter("@MedicalRecordNumber", patient.MedicalRecordNumber);
           database.AddStringParameter("@PhoneNumber", patient.PhoneNumber);
           database.AddStringParameter("@Initials", patient.Initials);
           database.AddStringParameter("@Address1", patient.Address1);
           database.AddStringParameter("@Address2", patient.Address2);
           database.AddStringParameter("@City", patient.City);
           database.AddIntParameter("@State", patient.State);
           database.AddIntParameter("@Country", patient.Country);
           database.AddStringParameter("@ZIP", patient.ZIP);
           //database.AddDateTimeParameter("@CreatedOn", patient.CreatedOn);
           database.AddStringParameter("@Notes", patient.Notes);
           database.AddLongParameter("NewPatientId", 0, true);
           try
               {
               database.SubmitData("sp_CreatePatient", DataPortalQueryType.StoredProcedure);
               patientId = Convert.ToInt64(database.GetParameterValue("NewPatientId"));
               return true;
               }
           catch (Exception ex)
               {
               patientId = 0;
               return false;
               }

           }

       public static void DeletePatientByPatientId(Int64 patientId)
           {
           var database = new Database();
           database.SubmitData("delete from PatientMR where PatientId=" + patientId, DataPortalQueryType.SqlQuery);
           database.SubmitData("delete from dbo.Patients where PatientId="+ patientId, DataPortalQueryType.SqlQuery);
           }


       public static DataTable SearchPatient(List<string> value, List<string> Type, List<string> Field)
           {
           string type = "";

           for (int i = 0; i < Type.Count; i++)
               {
               bool isField = false;
               if (Type[i] == "Contains")
                   {
                   isField = true;
                   type += Field[i] + " like '%" + value[i] + "%'";
                   }
               if (Type[i] == "Is")
                   {
                   isField = true;
                   type += Field[i] + " like '" + value[i] + "'";
                   }
               if (Type[i] == "Ends with")
                   {
                   isField = true;
                   type += Field[i] + " like '%" + value[i] + "'";
                   }
               if (Type[i] == "Starts with")
                   {
                   isField = true;
                   type += Field[i] + " like '" + value[i] + "%'";
                   }
               //if (Type[i] == "State")
               //    {
               //    isField = true;
               //    type += " State = " + value[i] + " ";
               //    }
               if (isField && i < Type.Count - 1) type += " and ";
               }
           var dl = new Database();
           if (type == "") type = "1='1'";
           //string query = "Select * from dbo.Patients where " + type;
           string query = "Select * from dbo.Patients	left join	 states on	Patients.State=States.StateID  where " + type;
           return dl.ReturnDataTable(query, DataPortalQueryType.SqlQuery);

           }

       public static DataTable GetSchema()
           {
           var db = new Database();
           return db.ReturnDataTable("select * from dbo.Patients where 1 !='1'", DataPortalQueryType.SqlQuery);
           }

       public static void BulkInsert(DataRow data, string[] columnName)
           {
           var dp = new Database();
           string query = "insert into dbo.Patients (";
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

       public static void CheckDuplicatePatient(string ssn,out int count)
           {
           var db = new Database();
           db.AddStringParameter("@SSN", ssn);
           //db.AddStringParameter("@EmailAddress", email);
           db.AddIntParameter("count", 0,true);

           try
               {
               db.SubmitData("sp_CheckDuplicatePatient", DataPortalQueryType.StoredProcedure);
               count = Convert.ToInt32(db.GetParameterValue("count"));
               
               }
           catch (Exception ex)
               {
               count = 1;
               }

           }
        }
    }
