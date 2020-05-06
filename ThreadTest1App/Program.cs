using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadTest1App
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestClass testClass = new TestClass();
            //Thread thread1 = new Thread(testClass.TestMethod);

            BookShop bookShop = new BookShop();
            Thread t1 = new Thread(bookShop.Sale);//线程1
            Thread t2 = new Thread(bookShop.Sale);//线程2
            //t1.IsBackground = true;
            //t2.IsBackground = true;
            //开启线程
            t1.Start();
            t2.Start();
            Thread.Sleep(1000);
            Console.ReadKey();
        }

        class TestClass
        {
            public void TestMethod()
            {
                Console.WriteLine("TestMethod()");
            }
        }
        class BookShop
        {
            //剩余图书数量
            public int num = 2;
            private static readonly object locker = new object();
            public void Sale()
            {
                lock(locker)
                {
                    int temp = num;
                    if(temp>0)
                    {
                        Thread.Sleep(1000);
                        num -= 1;
                        Console.WriteLine("售出一本书，还剩余{0}本",num);
                        string strThread = Thread.CurrentThread.ManagedThreadId.ToString("00");
                        Console.WriteLine(strThread);
                    }
                    
                    else Console.WriteLine("没有了");
                }
            }
        }

    }
}
