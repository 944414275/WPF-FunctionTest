using System;
using System.Globalization;

namespace ConJson
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "Sat Jun 26 12:10:34 CST 2021";
            DateTime dt = DateTime.ParseExact(str, "ddd MMM dd HH:mm:ss CST yyyy", new CultureInfo("en-us"));
            
            Console.Write(dt.ToString());
            Console.ReadLine();
            //原文链接：https://blog.csdn.net/qq_35507418/article/details/80184301
            //CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture("en-US");
            //string format = "ddd MMM d HH:mm:ss zz00 yyyy";
            //string datastr = "Sat Jun 26 12:10:34 CST 2021";
            //DateTime dateTime =Convert.ToDateTime(datastr);
            //DateTime datetime = DateTime.ParseExact("Sat Jun 26 12:10:34 CST 2021", format, cultureInfo); // 将字符串转换成日期
            //Console.WriteLine(datetime.ToString());
            //Console.ReadLine();
        }
    }
}
