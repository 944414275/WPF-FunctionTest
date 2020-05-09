using System;

namespace ConTypeTest1
{
    class Program
    {
        static void Main(string[] args)
        {
            Car car=new Car();
            Type type=car.GetType();

            string s = "1";
            Type sType=s.GetType();

            int i = 1;
            Type iType = i.GetType();

            bool b = true;
            Type bType = typeof(bool);

            Console.WriteLine("Type is {0} --- Type'name is {1} --- Type'full name is {2}", type, type.Name, type.FullName);
            Console.WriteLine("Type is {0} --- Type'name is {1} --- Type'full name is {2}", sType, sType.Name, sType.FullName);
            Console.WriteLine("Type is {0} --- Type'name is {1} --- Type'full name is {2}", iType,iType.Name, iType.FullName);
            Console.WriteLine("Type is {0} --- Type'name is {1} --- Type'full name is {2}", bType, bType.Name, bType.FullName);

            Console.ReadKey();
        }
    }

    public class Car
    {
        string Name { set; get; }
    }
}
