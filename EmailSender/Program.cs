using System;
using System.Net.Mail;
using System.Data.SqlClient;

namespace EmailSender
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            SmtpClient client = new SmtpClient();
            client.Host = "mail.visionet.co.id";
            client.Port = 25;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("noreply", "n0reply!99");

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("noreply@visionet.co.id");
            mailMessage.To.Add("wahyu.dzulhikam@visionet.co.id");
            mailMessage.Body = "Test Body Email";
            mailMessage.Subject = "Test Subject Email";
            mailMessage.IsBodyHtml = true;
            //client.Send(mailMessage);
            Console.WriteLine("Email Sent!");

            try 
            { 
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "10.10.115.23,1433\\dotEXTERNALDB"; 
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
