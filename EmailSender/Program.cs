using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using log4net;
using System.IO;
using System.Reflection;
using System.Text;

namespace EmailSender
{
    class Program
    {
        public static IConfiguration Configuration { get; set; }
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            //initialize logging
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            log4net.Config.XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            Console.WriteLine("Hello World!");            
            log.Info("Email Sent");

            EmailHelper mail = new EmailHelper();
            mail.sendMail("Email Subject","Email Body");
            
            log.Info("Email Sent");
            Console.WriteLine("Email Sent!");

            try 
            { 
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "10.10.115.23,1433\\EXTERNALDB"; 
                builder.UserID = "admin";            
                builder.Password = "Password1!";     
                builder.InitialCatalog = "WorkflowConfiguration";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();    
                    String sql = "SELECT TOP 20 prjCode,prjName from tbProject";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
                            }
                        }
                    }                    
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadLine(); 
        }
    }
}
