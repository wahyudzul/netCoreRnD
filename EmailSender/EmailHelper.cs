using System;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EmailSender
{
    class EmailHelper
    {
        public EmailHelper()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSetting.json");

            Configuration = builder.Build();
        }
        public static IConfiguration Configuration { get; set; }
        public void sendMail(string mailSubject,string mailBody)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Host = Configuration["smtp:smtpHost"];
                client.Port = Convert.ToInt16(Configuration["smtp:smtpPort"]);
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(Configuration["smtp:smtpUserName"], Configuration["smtp:smtpPassword"]);

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(Configuration["smtp:emailAddress"]);
                mailMessage.To.Add("wahyu.dzulhikam@visionet.co.id");
                mailMessage.Body = mailBody;
                mailMessage.Subject = mailSubject;
                mailMessage.IsBodyHtml = true;
                //client.Send(mailMessage);
            }
            catch(SmtpException)
            {
                
            }

        }
    }
}