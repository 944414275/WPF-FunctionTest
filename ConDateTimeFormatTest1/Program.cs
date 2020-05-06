using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConDateTimeFormatTest1
{
    class Program
    {
        static void Main(string[] args)
        {
            string dateStr = DateTime.Now.ToString("yyyy/MM/dd");
            Console.WriteLine(dateStr);
            Console.ReadKey();
        }
    }
}
