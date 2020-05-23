using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql;
using MySql.Data.MySqlClient;

namespace WpfMySQLTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string connstr = "data source=localhost;database=test;user id=root;password=20200303;pooling=false;charset=utf8";
            using (MySqlConnection mySqlConnection = new MySqlConnection(connstr))
            {
                string sqlSelect = "SELECT SQL_CALC_FOUND_ROWS *FROM test WHERE id > 10 and id<100 LIMIT 1,10;";
                //string sqlCount = "select FOUND_ROWS();";

                MySqlCommand cmdSelect = new MySqlCommand(sqlSelect, mySqlConnection);
                //MySqlCommand cmdCount = new MySqlCommand(sqlCount);

                mySqlConnection.Open();
                MySqlDataReader reader = cmdSelect.ExecuteReader();
                dataGrid.DataContext = reader;
                mySqlConnection.Close();
                mySqlConnection.Open();
                int count = cmdSelect.ExecuteNonQuery();

                //MySqlCommand cmdCount = new MySqlCommand(sqlCount);
                //cmdCount.Connection = mySqlConnection;
                //int count = cmdCount.ExecuteNonQuery();

                //Console.WriteLine(count.ToString());

                //Console.ReadKey();  
            }
        }
    }
}
