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

namespace WpfTextBlockBinding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window,INotifyPropertyChanged
    {
        int i = 1;
        private string receivedTxtBlock="1";
        public string ReceivedTxtBlock
        {
            set 
            { 
                if (receivedTxtBlock != value) 
                {
                    receivedTxtBlock = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ReceivedTxtBlock"));
                } 
            } 
            get
            { 
                return receivedTxtBlock; 
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        { 
            ReceivedTxtBlock = i.ToString(); 
            i++;
        } 
        public event PropertyChangedEventHandler PropertyChanged;
    } 
}
