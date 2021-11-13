using System;

namespace ConRandomTest1
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r2 = new Random();
            int v = r2.Next(10,32);
            Console.WriteLine(v.ToString());
            string rqst = Guid.NewGuid().ToString().Replace("-", "").Substring(0, v);
            Console.WriteLine(rqst.Length);
            Console.WriteLine(rqst);
            Console.ReadKey();
        }
    }
}
