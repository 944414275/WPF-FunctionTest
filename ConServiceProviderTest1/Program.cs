using Microsoft.Extensions.DependencyInjection;
using System;

namespace ConServiceProviderTest1
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services = services.AddTransient<ITransient, Transient>();
            services = services.AddScoped<IScoped, Scoped>();
            services = services.AddSingleton<ISingleton, Singleton>();

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var te = serviceProvider.GetService<Singleton>();
            string ss = te.s;

            Console.WriteLine(ReferenceEquals(serviceProvider.GetService<ITransient>(), serviceProvider.GetService<ITransient>()));
            Console.WriteLine(ReferenceEquals(serviceProvider.GetService<IScoped>(), serviceProvider.GetService<IScoped>()));
            Console.WriteLine(ReferenceEquals(serviceProvider.GetService<ISingleton>(), serviceProvider.GetService<ISingleton>()));
            
            IServiceProvider serviceProvider1 = serviceProvider.CreateScope().ServiceProvider;
            IServiceProvider serviceProvider2 = serviceProvider.CreateScope().ServiceProvider;
             
            Console.WriteLine(ReferenceEquals(serviceProvider1.GetService<IScoped>(), serviceProvider1.GetService<IScoped>()));
            Console.WriteLine(ReferenceEquals(serviceProvider1.GetService<IScoped>(), serviceProvider2.GetService<IScoped>()));
            Console.WriteLine(ReferenceEquals(serviceProvider1.GetService<ISingleton>(), serviceProvider2.GetService<ISingleton>()));

            Console.WriteLine("Hello World!");
        }

        //public void SomeRandomMethod()
        //{
        //    var valueService = ServiceLocator.Instance.GetService<IValueService>();

        //    // Do something with service
        //}

        //public void Configure(IApplicationBuilder app)
        //{
        //    ServiceLocator.Instance = app.ApplicationServices;
        //}
    }

    //public static class ServiceLocator
    //{
    //    public static IServiceProvider Instance { get; set; }
    //}

    interface ITransient { }
    class Transient : ITransient { }
    interface ISingleton {  }
    class Singleton : ISingleton { public string s = "1"; }
    interface IScoped { }
    class Scoped : IScoped {  }
}
