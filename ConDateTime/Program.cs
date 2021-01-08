using System;

namespace ConDateTime
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime dt = DateTime.Now; 

            Console.WriteLine(dt.ToString());
            Console.WriteLine(dt.ToFileTime().ToString());
            //string ft = dt.ToFileTime().ToString();

            Console.WriteLine(dt.ToFileTimeUtc().ToString());
            Console.WriteLine(dt.ToLocalTime().ToString());
            Console.WriteLine(dt.ToLongDateString().ToString());
            Console.WriteLine(dt.ToShortDateString().ToString());
            Console.WriteLine(dt.GetDateTimeFormats('D')[1].ToString());
            Console.WriteLine(dt.GetDateTimeFormats('g')[0].ToString());
            Console.ReadLine();
        }
    }
}
