using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql;
using MySql.Data.Common;
using System.Data;

namespace ConMySQLTest
{
    class Program
    {
        static void Main(string[] args)
        {

            //string s = "0000";
            //int i = Convert.ToInt32(s);


            string connstr = "data source=localhost;database=test;user id=root;password=123456;pooling=true;charset=utf8;";
            CommandType type = CommandType.StoredProcedure;
            DataTable dataTable = new DataTable();
            dataTable = MySQLHelper.ExecuteDataTable(connstr, type,null);






            using (MySqlConnection mySqlConnection = new MySqlConnection(connstr))
            { 
                string sqlSelect = "SELECT SQL_CALC_FOUND_ROWS * FROM test WHERE id > 10 and id<100 LIMIT 1,10;";
                string sql1 = "select id=1 ";
                string sqlCount = "select FOUND_ROWS();";

                MySqlCommand cmdSelect = new MySqlCommand(sqlSelect,mySqlConnection);
                //MySqlCommand cmdCount = new MySqlCommand(sqlCount, mySqlConnection);

                mySqlConnection.Open();
                //MySqlDataReader reader = cmdSelect.ExecuteReader();
                //mySqlConnection.Close(); 
                try
                {
                    int count = cmdSelect.ExecuteNonQuery();
                    Console.WriteLine(count.ToString());
                    Console.ReadKey();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    throw;
                }
                

                //MySqlCommand cmdCount = new MySqlCommand(sqlCount);
                //cmdCount.Connection = mySqlConnection;
                
            }


            //using (MySqlConnection mySqlConnection = new MySqlConnection(connstr))
            //{
            //    string sqlCount = "select FOUND_ROWS();";
            //    MySqlCommand cmdCount = new MySqlCommand(sqlCount, mySqlConnection);
            //    mySqlConnection.Open();
            //    int count = cmdCount.ExecuteNonQuery();
            //    Console.WriteLine(count.ToString());
            //    Console.ReadKey();

            //}
        }


    }
}

