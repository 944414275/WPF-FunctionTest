using System;

namespace ConDateTime
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime dt = DateTime.Now; 

            Console.WriteLine(dt.ToString());//2021-03-11 11:31:40
            Console.WriteLine(dt.ToFileTime().ToString());//132599071004418848
            //string ft = dt.ToFileTime().ToString();

            Console.WriteLine(dt.ToFileTimeUtc().ToString());//132599071004418848
            Console.WriteLine(dt.ToLocalTime().ToString());//2021-03-11 11:31:40
            Console.WriteLine(dt.ToLongDateString().ToString());//2021年3月11日
            Console.WriteLine(dt.ToShortDateString().ToString());//2021-03-11
            Console.WriteLine(dt.GetDateTimeFormats('D')[1].ToString());//2021年3月11日，星期四
            Console.WriteLine(dt.GetDateTimeFormats('g')[0].ToString());//2021-03-11 11:31
            Console.WriteLine(dt.ToString("yyyyMMdd")); //20210329
            Console.WriteLine((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000); //20210329
            Console.ReadLine(); 
        }
    }
}
