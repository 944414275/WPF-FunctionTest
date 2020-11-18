using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebSSLTest1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => 
            WebHost.CreateDefaultBuilder(args)
            .UseUrls("https://*:5001")
            .UseKestrel(option =>
            {
                option.ConfigureHttpsDefaults(i =>
                {
                    i.ServerCertificate = new System.Security.Cryptography.X509Certificates.X509Certificate2("./944414275.top.pfx", "wepe53nqo4jg7");
                });
            }).UseStartup<Startup>(); 
    }
}
