using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfDispatcher
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

        private void btnAppBeginInvoke_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ModifyUI() 
        { 
            // 模拟一些工作正在进行 
            Thread.Sleep(TimeSpan.FromSeconds(2));
            //lblHello.Content = "欢迎你光临WPF的世界,Dispatcher"; 

            //20200115 komla 子线程访问主线程中的DispatcherObjector
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,(ThreadStart)delegate()
            {
                lblHello.Content = "欢迎你光临WPF的世界,Dispatcher";
            });
        }

        private void btnThd_Click(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(ModifyUI);
            thread.IsBackground = true;
            thread.Start();
        }
    }
}
