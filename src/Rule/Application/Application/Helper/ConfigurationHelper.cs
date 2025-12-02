using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleApplication.Helper
{
    public class ConfigurationHelper
    {
        private static IConfiguration GetConfig()
        {
            return new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
        }

        public static string GetLoginUri()
        {
            var uri = GetConfig().GetSection("EndPoints").GetSection("LoginAPI").Value;
            return uri;
        }

        public static(string userName, string password) GetHashes()
        {
            var userName = GetConfig().GetSection("Hashes").GetSection("Hash1").Value;
            var password = GetConfig().GetSection("Hashes").GetSection("Hash2").Value;

            return (userName, password);
        }
    }
}
