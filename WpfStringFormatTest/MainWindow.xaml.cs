using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace WpfStringFormatTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            
        }

        private int count = 1;
        private ObservableCollection<string> logList = new ObservableCollection<string>();
        
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> LogListProp
        {
            get
            { 
                return logList;
            }
            set
            {
                logList = value;
                if (PropertyChanged != null)
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("LogListProp"));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (count < 20)
            {
                LogListProp.Add(string.Format("{0}{1}", string.Format("{0:G}", System.DateTime.Now),count));
            }
            else count = 1;
        }
    }
}
