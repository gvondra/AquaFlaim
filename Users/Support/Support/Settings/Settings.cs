using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.User.Support
{
    public static class Settings
    {
        public static string AuthorizationApiBaseAddress => ConfigurationManager.AppSettings["AuthorizationApiBaseAddress"];
        public static string LogApiBaseAddress => ConfigurationManager.AppSettings["LogApiBaseAddress"];
    }
}
