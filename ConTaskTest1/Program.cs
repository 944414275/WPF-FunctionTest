using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConTaskTest1
{
    class Program
    {
        bool flag = false;
        static void Main(string[] args)
        {
            Console.WriteLine("主线程执行业务处理.");
            Asyncfunction();
            //Thread.Sleep(1000);

            Console.WriteLine("主线程执行其他处理");
            for(int i = 0; i < 10; i++)
            {
                Console.WriteLine(string.Format("Main:i={0}", i));
            }
            Console.ReadLine();
        }
        async static void Asyncfunction()
        {
            await Task.Delay(1);//等待当前等待完成才能进行下面操作
            Console.WriteLine("使用System.Threading.Tasks.Task执行异步操作.");
            for(int i=0;i<10;i++)
            {
                Console.WriteLine(string.Format("AsyncFunction:i={0}",i));
            }
        }
    }
}
