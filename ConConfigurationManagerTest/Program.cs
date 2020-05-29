using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConConfigurationManagerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string _connectionString = ConfigurationManager.ConnectionStrings["test"].ConnectionString;
            Console.WriteLine(_connectionString);
            Console.ReadKey();
        }
    }
}
