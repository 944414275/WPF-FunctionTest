using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConGenericTest1
{
    class Program
    {
        static void Main(string[] args)
        {
            MySQLHelp<Message> mySQLHelp = new MySQLHelp<Message>(new Message());
                 
        }
    }

    //泛型类：
    public class MySQLHelp<T>
    {
        private T t;
        public MySQLHelp(T t)
        {
            this.t = t;
        }
    }

    //其他类
    public class Message
    {

    }
}
