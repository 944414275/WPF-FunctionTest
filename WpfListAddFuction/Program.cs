using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WpfListAddFuction
{
    class Program
    { 
        public static void Main(string[] args)
        {
            List<People> lists = new List<People>();
            People p = new People();
            p.Name = "Joesen";
            p.Phone = "123";
            p.Address = "重庆";
            lists.Add(p);
            lists[0].Name = "null";
            lists[0] = null;

            Console.WriteLine(p.Name);
            Console.ReadKey();
        } 
    }
    public class People
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
