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
        //                //在这里设置Kestrel的一些配置属性
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
