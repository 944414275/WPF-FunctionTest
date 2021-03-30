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
            string constrKeeperHouseSql = "server=172.16.17.10,6677;database=lk2020;uid=sa;pwd=Smx2655020;";
            string WINU5SN910P6FOSql = "server=127.0.0.1;database=lk2020;uid=sa;pwd=smx2655020;";
            string testMachine = "server=172.16.18.100,6677;database=housekeeper;uid=sa;pwd=smx2655020;";

            //SqlConnection conn1 = new SqlConnection();
            try
            {
                SqlConnection conn = new SqlConnection(testMachine);
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
