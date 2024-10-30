using AssestOrderingApplication.Interfaces;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;

namespace AssestOrderingApplication.Services
{
    public class MailNotification: INotification
    {
        public MailNotification(string message) 
        {
            _message = message;
        }

        public string _message { get; set; }
        public string _fromAddress = "praneethprasad.vemuluri@insightsoftware.com";

        public bool sendNotiifcation(string sender)
        {
            throw new NotImplementedException();
        }
        public static void CreateTestMessage2()
        {
            System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            string to = "praneethlucky132@gmail.com";
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient();
            mail.To.Add(to);
            mail.From = new MailAddress("praneethlucky132@gmail.com");
            mail.Subject = "jbd";
            mail.IsBodyHtml = true;
            mail.Body = "jkhas";
            SmtpServer.Host = "smtp.office365.com";
            SmtpServer.Port = 25;
            SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            try
            {
                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception Message: " + ex.Message);
                if (ex.InnerException != null)
                    Debug.WriteLine("Exception Inner:   " + ex.InnerException);
            }
        }
    }
}
