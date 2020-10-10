 using System.Windows; 
using System.Windows.Input;
using System.ComponentModel;
using System;

namespace WpfCommandTest2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window,INotifyPropertyChanged
    {
        
        float flo;
        public string FloProp 
        {
            get { return flo.ToString(); }
            set
            {
                flo = Convert.ToSingle(value);
                if (PropertyChanged != null)
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("FloProp"));
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public static RoutedCommand rouutedCommand = new RoutedCommand("Clear", typeof(MainWindow));

        public event PropertyChangedEventHandler PropertyChanged;

        private void cb_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            
            FloProp = "10.9999";

            //txt1.Clear();
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
