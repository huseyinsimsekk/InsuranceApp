using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceApp.Web.Helpers
{
    public class ConfigHelper
    {

        private static ConfigHelper _companyUrlSetting;

        public string companyUrlValue { get; set; }

        public static string CompanyUrlSetting(string Key)
        {
            _companyUrlSetting = GetCurrentSettings(Key);
            return _companyUrlSetting.companyUrlValue;
        }

        public ConfigHelper(IConfiguration config, string Key)
        {
            this.companyUrlValue = config.GetValue<string>(Key);
        }

        public static ConfigHelper GetCurrentSettings(string Key)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();

            var settings = new ConfigHelper(configuration.GetSection("CompanUrls"), Key);

            return settings;
        }
    }
}
