using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleThreadStop
{
    class Program
    {
        static void Main(string[] args)
        {
            F();
            //让F创建的3个线程运行3秒钟
            Thread.Sleep(3000);
            //让3个线程都退出
            IsExit = true;
            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }
        static void F()
        {
            t1 = new Thread(o => { Run(o); });
            t1.Start("线程1");

            t2 = new Thread(delegate (object o) { Run(o); });
            t2.Start("线程2");

            t3 = new Thread(Run);
            t3.Start("线程3");
        }
        static void Run(object o)
        {
            while (!IsExit)
            {
                Console.WriteLine(o + ":正在运行中。。。");
                Thread.Sleep(1000);
            }
        }
        static bool IsExit = false;
        static Thread t1, t2, t3;

    }
}
