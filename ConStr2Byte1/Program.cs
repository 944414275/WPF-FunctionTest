using System;
using System.Text;

namespace ConStr2Byte1
{
    class Program
    {
        static void Main(string[] args)
        {
            string sdata = "我是一个字符串";
            Encoding code = Encoding.GetEncoding("utf-8");
            byte[] buffer = code.GetBytes(sdata);
            Console.WriteLine("Hello World!");
        }
    }
}
