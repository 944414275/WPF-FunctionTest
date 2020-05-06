using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreaaTestApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting program...");
            Thread t = new Thread(PrintNumbersWithDelay);
            t.Start();//线程开始执行
            t.Join();
            //PrintNumbers();
            Console.WriteLine("Thread completed");
            Console.ReadKey();
        }
        static void PrintNumbers()
        {
            Console.WriteLine("Starting11...");
            for (int i = 1; i < 5; i++)
            {
                Console.WriteLine(i);
            }
        }

        static void PrintNumbersWithDelay()
        {
            Console.WriteLine("Starting12...");
            for (int i = 1; i < 5; i++)
            {
                //Thread.Sleep(TimeSpan.FromSeconds(2));//暂停2S
                Console.WriteLine(i);
            }
        }
    }
}
