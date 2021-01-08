using Microsoft.Extensions.DependencyInjection;
using System;

namespace ConServiceProvider
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceProvider root = new ServiceCollection()
                .AddTransient<IFoo, Foo>()
                .AddScoped<IBar,Bar>()
                .AddSingleton<IBaz,Baz>()
                .BuildServiceProvider();
            IServiceProvider child1 = root.GetService<IServiceScopeFactory>().CreateScope().ServiceProvider;
            IServiceProvider child2 = root.GetService<IServiceScopeFactory>().CreateScope().ServiceProvider;

            //ServiceProvider child3= (ServiceProvider)root.GetService<IServiceScopeFactory>().CreateScope().ServiceProvider;

            var s = child1.GetService<IBaz>(); 
            //string sss = s.str;

            Console.WriteLine("ReferenceEquals(root.GetService<IFoo>(), root.GetService<IFoo>() = {0}", ReferenceEquals(root.GetService<IFoo>(), root.GetService<IFoo>()));//false
            Console.WriteLine("ReferenceEquals(child1.GetService<IBar>(), child1.GetService<IBar>() = {0}", ReferenceEquals(child1.GetService<IBar>(), child1.GetService<IBar>()));//true
            Console.WriteLine("ReferenceEquals(child1.GetService<IBar>(), child2.GetService<IBar>() = {0}", ReferenceEquals(child1.GetService<IBar>(), child2.GetService<IBar>()));//false
            Console.WriteLine("ReferenceEquals(child1.GetService<IBaz>(), child2.GetService<IBaz>() = {0}", ReferenceEquals(child1.GetService<IBaz>(), child2.GetService<IBaz>()));//true
        }
    }

    public interface IFoo { }
    public interface IBar { }
    public interface IBaz { }
     
    public class Foo : IFoo { }
    public class Bar : IBar { }
    public class Baz : IBaz { public string str = "11"; }
}
