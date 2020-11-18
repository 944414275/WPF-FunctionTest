using System; 
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace ConWebAPITest2
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new HttpSelfHostConfiguration("http://localhost:9527");
            config.Routes.MapHttpRoute ("API Default","api/{controller}/{action}/{id}",new { id = RouteParameter.Optional });

            using (var server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();
                Console.WriteLine("请开始您的表演");
                Console.ReadLine();
            }
        }
    }
}
