using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Hosting;
using System.Net;

namespace AspNetCoreStartup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args) 
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.ConfigureKestrel(serverOptions=>
        //            {
        //                //����������Kestrel��һЩ��������
        //                serverOptions.Listen(IPAddress.Any, 5001, listenOptions =>
        //                 {
        //                     listenOptions.UseHttps(
        //                         @"local.pfx",
        //                         "123456");
        //                 });
        //            })
        //            .UseStartup<Startup>();
        //        });
    }
}
