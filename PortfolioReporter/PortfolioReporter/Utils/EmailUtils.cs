using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using log4net;


namespace PortfolioReporter.Utils
{
    public static class EmailUtils
    {
        private readonly static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void SendMail(String subject, String msg)
        {
            String un = AppConfig.GetEmailUserName();
            String pw = AppConfig.GetEmailPassword();
            String to = AppConfig.GetEmailSendTo();
            //log.Debug("Sending email from=" + un + "; to=" + to);
            SendMail(un, to, pw, subject, msg);

        }
        public static void SendMail(String from, String to, String pass, String subject, String body)
        {


            string smtpServer = "smtp-mail.outlook.com";
            int port = 587;
            bool secure = true;

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(smtpServer);

            mail.From = new MailAddress(from);

            var toEmails = to.Split(';');
            foreach (var toEmail in toEmails) {
                mail.To.Add(toEmail);
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpServer.Port = port;
            SmtpServer.Credentials = new System.Net.NetworkCredential(from, pass);
            SmtpServer.EnableSsl = secure;
            SmtpServer.Timeout = 10*1000;

            var retry = 0;
            var mailSent = false;
            while (retry < 2 && !mailSent)
            {
                try
                {
                    SmtpServer.Send(mail);
                    mailSent = true;
                }

                catch (Exception e)
                {
                    retry++;
                    log.Error(e);
                    Task.Delay(1000).Wait();

                }
            }

            if (!mailSent)
                throw new Exception("Unable to send email");

        }


    }
}
