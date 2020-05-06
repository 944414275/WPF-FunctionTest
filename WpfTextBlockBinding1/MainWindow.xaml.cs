using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WpfTextBlockBinding1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window,INotifyPropertyChanged
    {
        //Student stu = new Student();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            //this.txtName.SetBinding(TextBox.TextProperty, new Binding("Name"));
            //this.txtAge.SetBinding(TextBox.TextProperty, new Binding("Age"));

        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }

        private int age;
        public int Age
        {
            get { return age; }
            set
            {
                age = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Age"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void btn1(object sender, RoutedEventArgs e)
        {
            this.Name += "testName";
        }

        private void btn2(object sender, RoutedEventArgs e)
        {
            this.Age += 10;
        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    stu.Age += 10;
        //    stu.Name += "testName";
        //}
    }

    //class Student : INotifyPropertyChanged
    //{
    //    public event PropertyChangedEventHandler PropertyChanged;

    //    private string name;
    //    public string Name
    //    {
    //        get { return name; }
    //        set
    //        {
    //            name = value;
    //            if (this.PropertyChanged != null)
    //            {
    //                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Name"));
    //            }
    //        }
    //    }

    //    private int age;
    //    public int Age
    //    {
    //        get { return age; }
    //        set
    //        {
    //            age = value;
    //            if (this.PropertyChanged != null)
    //            {
    //                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Age"));
    //            }
    //        }
    //    }
    //}
}
