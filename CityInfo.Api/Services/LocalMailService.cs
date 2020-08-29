using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Api.Services
{
    public class LocalMailService : IMailService
    {
        private readonly IConfiguration _configuration;
        //private string _mailTo = "admin@nycompany.com";
        //private string _mailFrom = "noreply@mycompany.com";

        public LocalMailService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void send(string subject, string message)
        {
            Debug.WriteLine($"Mail from {_configuration["mailSetting:mailFrom"]} to {_configuration["mailSetting:mailTo"]}, with LocalMailService");
            Debug.WriteLine($"Subject: {subject}");
            Debug.WriteLine($"Message: {message}");
        }
    }
}
