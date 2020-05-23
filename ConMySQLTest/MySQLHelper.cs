using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql;
using MySql.Data.Common;
using System.Data;
using System.Collections;
using System.Configuration;

namespace ConMySQLTest
{
    public class MySQLHelper
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public static readonly string connectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
        public static readonly string conStr = "data source=localhost;database=test;user id=root;password=20200303;pooling=false;charset=utf8";
        /// <summary>
        /// 执行查询操作，返回DataSet
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string commandText, CommandType commandType, params MySqlParameter[] parameters)
        {
            using (MySqlConnection connection = new MySqlConnection(conStr))
            {
                MySqlCommand command = new MySqlCommand();
                PrepareCommand(connection, command, null, commandText, commandType, parameters);
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    command.Parameters.Clear();
                    return dataSet;
                }
            }
        }

        /// <summary>
        /// 执行查询操作，返回DataTable
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string commandText, CommandType commandType, params MySqlParameter[] parameters)
        {
            using (MySqlConnection connection = new MySqlConnection(conStr))
            {
                MySqlCommand command = new MySqlCommand();
                PrepareCommand(connection, command, null, commandText, commandType, parameters);
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    command.Parameters.Clear();
                    return dataTable;
                }
            }
        }

        /// <summary>
        /// 执行查询操作，返回MySqlDataReader
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static MySqlDataReader ExecuteReader(string commandText, CommandType commandType, params MySqlParameter[] parameters)
        {
            MySqlConnection connection = new MySqlConnection(conStr);
            MySqlCommand command = new MySqlCommand();
            try
            {
                PrepareCommand(connection, command, null, commandText, commandType, parameters);
                MySqlDataReader reader = command.ExecuteReader();
                command.Parameters.Clear();
                return reader;
            }
            catch
            {
                command.Parameters.Clear();
                connection.Close();
                throw new Exception();
            }
        }

        /// <summary>
        /// 执行查询操作，返回第一行第一列
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string commandText, CommandType commandType, params MySqlParameter[] parameters)
        {
            using (MySqlConnection connection = new MySqlConnection(conStr))
            {
                MySqlCommand command = new MySqlCommand();
                PrepareCommand(connection, command, null, commandText, commandType, parameters);
                object obj = command.ExecuteScalar();
                command.Parameters.Clear();
                return obj;
            }
        }

        /// <summary>
        /// 执行非查询操作，返回受影响的行数
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string commandText, CommandType commandType, params MySqlParameter[] parameters)
        {
            using (MySqlConnection connection = new MySqlConnection(conStr))
            {
                MySqlCommand command = new MySqlCommand();
                PrepareCommand(connection, command, null, commandText, commandType, parameters);
                try
                {
                    int result = command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    return result;
                }
                catch
                {
                    command.Parameters.Clear();
                    return 0;
                }
            }
        }

        /// <summary>
        /// 执行数据库事务，不带参数
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(IList<string> list)
        {
            using (MySqlConnection connection = new MySqlConnection(conStr))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand();
                MySqlTransaction transaction = connection.BeginTransaction();
                PrepareCommand(connection, command, transaction, null, CommandType.Text, null);
                try
                {
                    int count = 0;
                    foreach (string sql in list)
                    {
                        command.CommandText = sql;
                        count += command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    return count;
                }
                catch
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }

        /// <summary>
        /// 执行数据库事务，带参数
        /// </summary>
        /// <param name="hashtable"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(Hashtable hashtable)
        {
            using (MySqlConnection connection = new MySqlConnection(conStr))
            {
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    int count = 0;
                    foreach (DictionaryEntry entity in hashtable)
                    {
                        MySqlCommand command = new MySqlCommand();
                        PrepareCommand(connection, command, transaction, entity.Key.ToString(), CommandType.Text, entity.Value as MySqlParameter[]);
                        count += command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    return count;
                }
                catch
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }

        /// <summary>
        /// 设置MySqlCommand
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="command"></param>
        /// <param name="transaction"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        private static void PrepareCommand(MySqlConnection connection, MySqlCommand command, MySqlTransaction transaction, string commandText, CommandType commandType, params MySqlParameter[] parameters)
        {
            // 建立连接
            if (connection.State != ConnectionState.Open)
            {
                connection.Close();
                connection.Open();
            }
            command.Connection = connection;

            // 设置SQL
            if (!string.IsNullOrEmpty(commandText))
            {
                command.CommandText = commandText;
            }
            command.CommandType = commandType;

            // 开启事务
            if (transaction != null)
            {
                command.Transaction = transaction;
            }

            // 设置参数
            if (parameters != null)
            {
                foreach (MySqlParameter parameter in parameters)
                {
                    if (parameter.Value == null || parameter.Value.ToString() == "")
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }
        } 
    }
}
