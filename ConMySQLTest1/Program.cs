using System;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace ConMySQLTest1
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "data source=localhost;database=lk_product;user id=root;password=123456;pooling=true;charset=utf8;";
            using (MySqlConnection msc = new MySqlConnection(str))
            {
                //写入sql语句
                string sql = "select * from logmodel";
                //创建命令对象
                MySqlCommand cmd = new MySqlCommand(sql, msc);
                //打开数据库连接
                msc.Open();
                //执行命令,ExcuteReader返回的是DataReader对象
                MySqlDataReader reader = cmd.ExecuteReader();
                //循环单行读取数据，当读取为null时，就退出循环
                while (reader.Read())
                {
                    //输出第一列字段值
                    Console.Write(reader.GetInt32(0) + "\t");
                    //Console.Write(reader.GetInt32("id") + "\t");

                    //判断字段"username"是否为null，为null数据转换会失败
                    if (!reader.IsDBNull(1))
                    {
                        //输出第二列字段值
                        Console.Write(reader.GetString(1) + "\t");
                        //Console.Write(reader.GetString("username") + "\t");
                    }
                    //判断字段"password"是否为null，为null数据转换会失败
                    if (!reader.IsDBNull(2))
                    {
                        //输出第三列字段值
                        Console.Write(reader.GetString(2) + "\n");
                        //Console.Write(reader.GetString("password") + "\t");
                    }
                }
            } 
            Console.ReadKey(); 
             
        }
    }
}
