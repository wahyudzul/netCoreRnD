using System.Data.SqlClient;

namespace EmailSender
{
    class dbHelper
    {
        public static SqlConnection createWFConfigDBConn()
        {            
            SqlConnectionStringBuilder dbCon = new SqlConnectionStringBuilder();
            dbCon.DataSource = "10.10.115.23,1433\\EXTERNALDB"; 
            dbCon.UserID = "admin";            
            dbCon.Password = "Password1!";     
            dbCon.InitialCatalog = "WorkflowConfiguration";    
            return new SqlConnection(dbCon.ConnectionString);
        }
        public static SqlConnection createMIPDBConn()
        {            
            SqlConnectionStringBuilder dbCon = new SqlConnectionStringBuilder();
            dbCon.DataSource = "VN-HRIS-DB01"; 
            dbCon.UserID = "VN_USER_CANDIDATE";            
            dbCon.Password = "123456";     
            dbCon.InitialCatalog = "MIP_VN";    
            return new SqlConnection(dbCon.ConnectionString);
        }
    }
}