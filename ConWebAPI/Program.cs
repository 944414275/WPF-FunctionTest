using ConWebAPITest.Services;
using System;
using System.Threading.Tasks;

namespace ConWebAPITest
{
    class Program
    {
        private static HttpService _http;

        static void Main(string[] args)
        {
            string portStr=Console.ReadLine();
            StartHttpService(portStr); 
        }

        /// <summary>
        /// start the http server.
        /// </summary>
        private static async void StartHttpService(string s)
        {
            /**
             * start.
             */
            try
            {
                var port = Convert.ToInt32(s);

                /**
                 * initialize http service.
                 */
                _http = new HttpService(port);

                await _http.StartHttpServer();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{exception.Message}");
            }
        }

        /// <summary>
        /// close the http server.
        /// </summary>
        private async Task CloseHttpService()
        {
            /**
             * close.
             */
            try
            {
                await _http.CloseHttpServer();
                _http.Dispose();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{exception.Message}");
            }
        }
    }
}
