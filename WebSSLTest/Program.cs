using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
 
using System.IO;
 
using System.Net;


namespace WebSSLTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var host = new WebHostBuilder()
            //    .UseKestrel(option => {
            //        option.UseHttps("server.pfx", "linezero");
            //    })
            //    .UseUrls("https://*:443")
            //    .UseContentRoot(Directory.GetCurrentDirectory())
            //    .UseIISIntegration()
            //    .UseStartup<Startup>()
            //    .Build();

            //host.Run();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(serverOptions =>
                    {
                        serverOptions.Listen(IPAddress.Loopback, 5000);
                        serverOptions.Listen(IPAddress.Loopback, 5001,
                            listenOptions =>
                            {
                                listenOptions.UseHttps("testssl.pfx",
                                    "wepe53nqo4jg7");
                            });
                    })
                    .UseStartup<Startup>();
                });
    }
}
