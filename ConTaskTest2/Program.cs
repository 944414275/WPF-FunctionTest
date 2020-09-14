using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConTaskTest2
{
    class Program
    {
        static Task<int> CreateTask(string name)
        {
            return new Task<int>(()=>TaskMethod(name));
        }

        static void Main(string[] args)
        {
            TaskMethod("Main Thread Task");

            Task<int> task = CreateTask("Task 1");
            task.Start();
            int result = task.Result;
            Console.WriteLine("Task 1 Result is: {0}", result);

            task = CreateTask("Task 2");
            //该任务会运行在主线程中
            task.RunSynchronously();
            result = task.Result;
            Console.WriteLine("Task 2 Result is: {0}", result);

            task = CreateTask("Task 3");
            Console.WriteLine(task.Status);
            task.Start();

            while (!task.IsCompleted)
            {
                Console.WriteLine(task.Status);
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
            }

            Console.WriteLine(task.Status);
            result = task.Result;
            Console.WriteLine("Task 3 Result is: {0}", result);

            #region 常规使用方式
            //创建任务
            Task<int> getsumtask = new Task<int>(()=>Getsum());
            //启动任务,并安排到当前任务队列线程中执行任务(System.Threading.Tasks.TaskScheduler)
            getsumtask.Start();
            Console.WriteLine("主线程执行其他处理");
            //等待任务的完成执行过程。
            getsumtask.Wait();
            //获得任务的执行结果
            Console.WriteLine("任务执行结果：{0}", getsumtask.Result.ToString());
            #endregion
             
        }

        static int TaskMethod(string name)
        {
            Console.WriteLine("Task {0} is running on a thread id {1}. Is thread pool thread: {2}",
                name,Thread.CurrentThread.ManagedThreadId,Thread.CurrentThread.IsThreadPoolThread);
            Thread.Sleep(TimeSpan.FromSeconds(2));
            return 42;
        }

        static int Getsum()
        {
            int sum = 0;
            Console.WriteLine("使用Task执行异步操作.");
            for (int i = 0; i < 100; i++)
            {
                sum += i;
            }
            return sum; 
        }
    }
}
