using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DataHelper.Test
{
    class Settings
    {
        public static string DatabaseName
        {
            get { return ConfigurationManager.AppSettings["database"]; }
        }

        public static string ServerName
        {
            get { return ConfigurationManager.AppSettings["server"]; }
        }
    }
}
