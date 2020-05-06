using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfDelegateTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Action action = new Action(Alert);
            action();
            Action action1 = () =>
            {
                Console.WriteLine("警告2");
            };
            action1();
            Action<string> action2 = (p) =>
            {
                Console.WriteLine(p);
            };
            action2("action2");

            Func<string> func1 = new Func<string>(Alert1);
            func1();

            Func<string> func2 = () =>
            {
                return "func2";
            };

            Func<string, string> func3 = new Func<string, string>(Alert2);
            func3("func3");

            Func<string, string> func4 = (p) =>
             {
                 return p;
             };
            func4("func4");

            string a=Alert3("12",(p)=> {
                if (p.Contains('1'))
                    return "1";
                else return "2";
            });
            Console.ReadLine();
        }

        public static void Alert2(string str,Func<string,string> func)
        {
            Console.WriteLine(func(str));
        }

        public static string Alert1()
        {
            return "func1";
        }

        public static string Alert2(string s)
        {
            return s;
        }

        public static void Alert()
        {
            Console.WriteLine("警告1");
        }

        public static string Alert3(string s,Func<string,string> func)
        {
            return func(s);
        }
    }
}
