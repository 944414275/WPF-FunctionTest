using System;
using System.Data;
using System.Data.SqlClient;

namespace ConMSSQLTest
{
    class Program
    {
        static void Main(string[] args)
        {

            //Server:这边是填写数据库地址的地方。可以是IP，或者.\local\localhost\电脑名+实例名
            //Database:数据库的名称
            //uid:数据库用户名，一般为sa
            //pwd：数据库密码 
            string ss = "Server=DESKTOP-3OCLDF2;DataBase=master;Trusted_Connection=SSPI";
            string constrWin = "Data Source=.;database=master;integrated security=SSPI;";
            string constrSql = "server=192.168.1.7;database=master;uid=666666;pwd=123456;";

            //SqlConnection conn1 = new SqlConnection();
            try
            {
                SqlConnection conn = new SqlConnection(constrSql);
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    Console.WriteLine("数据库已经打开");
                    conn.Close();
                }
                else { Console.WriteLine("连接失败"); }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            } 
        }
    }
}
