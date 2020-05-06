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

namespace WpfCommanfTest1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeCommand();
        }

        private RoutedCommand routedCommand = new RoutedCommand("clear",typeof(MainWindow));
        private void InitializeCommand()
        {
            this.btn1.Command = routedCommand;
            this.btn1.CommandParameter = this.btn1.Name.ToString();
            this.routedCommand.InputGestures.Add(new KeyGesture( Key.C,ModifierKeys.Alt));
            this.btn1.CommandTarget = txt1;

            CommandBinding commandBinding = new CommandBinding();
            commandBinding.Command = routedCommand;
            commandBinding.CanExecute += new CanExecuteRoutedEventHandler(cb_CanExecute);
            commandBinding.Executed += new ExecutedRoutedEventHandler(cb_Execute);

            this.sp1.CommandBindings.Add(commandBinding); 
        }

        private void cb_Execute(object sender,ExecutedRoutedEventArgs e)
        {
            txt1.Clear();
            //避免事件继续向上传递而降低程序性能
            e.Handled = true;
        }
        //当探测命令是否可执行的时候该方法会被调用
        private void cb_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            string UIName = e.ToString();

            if(string.IsNullOrEmpty(txt1.Text))
            {
                e.CanExecute = false;
            }
            else
            {
                //验证指令来自哪方将军，我方就执行
                if (e.Parameter.ToString() == "btn1")
                    e.CanExecute = true;
            }
            //降低事件继续向上传递而降低程序性能
            e.Handled = true;
        }
    } 
}
