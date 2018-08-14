using System.Data.SqlClient;
using System;
using Microsoft.Extensions.Configuration;
using log4net;
using System.Reflection;
using System.IO;

namespace EmailSender
{
    class Person
    {
        public string userName {get;set;}
        public string email {get;set;}
        public string fullName {get;set;}
    }

    class userHelper
    {
        public static IConfiguration Configuration { get; set; }
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Program));
        public static Person getManager(string userName,int positionLevel)
        {
            Person retVal = new Person();

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            log4net.Config.XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            using (SqlConnection connection = dbHelper.createMIPDBConn())
            {
                try 
                {                     
                    connection.Open();    
                    String sql = "[SPU_MIS_GetManagerByPositionLevel]";
                    String sqlEmail = "select Email from VU_EmployeeMAC where LoginName = @LoginName ";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("iUserLogin",userName);
                        command.Parameters.AddWithValue("iPositionLevel",positionLevel);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                retVal.userName = reader.GetString(2);
                                retVal.fullName = reader.GetString(1);
                            }
                        }
                    }
                    using (SqlCommand command = new SqlCommand(sqlEmail, connection))
                    {
                        command.Parameters.AddWithValue("LoginName",retVal.userName);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                retVal.email = reader.GetString(0);
                            }
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                    log.ErrorFormat("Error query to database : {0}",e.ToString());
                }
                finally
                {
                    connection.Close();
                }
            }
            return retVal;
        }
    }
}