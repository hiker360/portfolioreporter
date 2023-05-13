using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;

namespace PortfolioReporter
{
    public static class AppConfig
    {
        private static Configuration appConfig = ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location);

        public static String GetEmailUserName()
        {
            return appConfig.AppSettings.Settings["EmailUserName"].Value;
        }

        private static string emailPassword = "";
        public static String GetEmailPassword()
        {
            if (String.IsNullOrEmpty(emailPassword))
            {
                emailPassword = appConfig.AppSettings.Settings["EmailPassword"].Value;

            }
            return emailPassword;
        }

        public static String GetEmailSendTo()
        {
            return appConfig.AppSettings.Settings["EmailSendTo"].Value;
        }



    }
}
