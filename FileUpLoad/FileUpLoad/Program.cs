using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FileUpLoad
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    { 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(serverOptions =>
                    {
                        serverOptions.Listen(IPAddress.Any, 43358, listenOptions =>
                        {
                            listenOptions.UseHttps(
                                @"G:\\ssl\\IIS\\944414275.top.pfx",
                                "wepe53nqo4jg7");
                        });
                    });
                    webBuilder.UseStartup<Startup>()
                   .ConfigureKestrel(c => c.Limits.MaxRequestBodySize = 1024 * 1024 * 100); // 全局的大小100M
                }).UseServiceProviderFactory(new AutofacServiceProviderFactory());
    }
}
