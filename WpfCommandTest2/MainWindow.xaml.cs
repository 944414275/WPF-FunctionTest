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

namespace WpfCommandTest2
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

        public static RoutedCommand rouutedCommand = new RoutedCommand("Clear", typeof(MainWindow));
        private void cb_Execute(object sender, ExecutedRoutedEventArgs e)
        { 
            txt1.Clear();
            //避免事件继续向上传递而降低程序性能
            e.Handled = true;
        }
        //当探测命令是否可执行的时候该方法会被调用
        private void cb_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        { 
            if (string.IsNullOrEmpty(txt1.Text))
            {
                e.CanExecute = false;
            }
            else
            {
                if (e.Parameter.ToString() == "btn1")
                    e.CanExecute = true;
            }
            //避免事件继续向上传递而降低程序性能
            e.Handled = true;
        }
    }
}
