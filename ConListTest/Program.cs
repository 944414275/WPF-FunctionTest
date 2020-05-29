using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConListTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string a = "0";
            string b = "1";
            string c = "2";

            List<string> intList = new List<string>()
            {
                a,b,c
            };

            Console.WriteLine(intList[0]);
            Console.WriteLine(intList[1]);
            Console.WriteLine(intList[2]);
            Console.ReadLine();

        }
    }
}
