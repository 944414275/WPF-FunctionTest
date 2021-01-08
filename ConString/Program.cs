using System;

namespace ConString
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "{AB__CD}";
            Console.WriteLine(s.Trim('_')); // 移除字符串中头部和尾部的'_'字符，输出"AB__CD"
            Console.WriteLine(s.TrimStart('{')); // 移除字符串中头部的'_'字符，输出"AB__CD__"
            Console.WriteLine(s.TrimEnd('}')); // 移除字符串中尾部的'_'字符，输出"__AB__CD"
            Console.WriteLine(s.TrimStart('{').TrimEnd('}'));
            Console.WriteLine();
        }
    }
}
