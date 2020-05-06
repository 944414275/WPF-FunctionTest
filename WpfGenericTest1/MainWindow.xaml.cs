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

namespace WpfGenericTest1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

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

    //测试类
    public class Test
    {
        public static void Main()
        {
            MySQLHelp<Message> mm = new MySQLHelp<Message>(new Message());
        }
    }
    //其他类
    public class Message
    {

    }
}
