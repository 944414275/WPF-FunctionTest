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
using System.Data;
using System.Windows;
using System.Collections;
using System.ComponentModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TransmissionSW.Common.Model;

namespace WpfBoolToStringConvert
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region fields
        int count = 2;
        private bool state = false;
        private string coilStatement = "断闸";
        private RelayCommand<string> closeCoilCommand = null;
        DataConvert _DataConvert = new DataConvert();
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Property
        public RelayCommand<string> CloseCoilCommand
        {
            get
            {
                if (closeCoilCommand == null)
                {
                    //closeCoilCommand = new RelayCommand<string>(new Action<string>(CoilOperateMethod()),true );
                    //closeCoilCommand = new RelayCommand<string>((p)=>CoilOperateMethod(p));
                }
                return closeCoilCommand;
            }
        }

        public bool StateProp
        {
            get
            {
                return state;
            }
            set
            {
                if (state == value) return;
                state = value;
                if (PropertyChanged != null)
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("StateProp"));
            }
        }
        
        public string CoilStatementProp
        {
            get { return coilStatement; }
            set
            {
                if (coilStatement != value)
                {
                    coilStatement = value;
                }
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("CoilStatementProp"));
            }
        }
        #endregion
         
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;//必须要 
        }
         
        private void CoilOperateMethod(string s)
        {
            if(s=="合闸")
            {
                //MessageBox.Show(s);
                CoilStatementProp = "断闸";
                StateProp = false;
            }
            else if(s=="断闸")
            {
                //MessageBox.Show(s);
                CoilStatementProp ="合闸";
                StateProp = true;
            } 
        }

        private void ChangeBackGround()
        { 
            if (count % 2 == 0)
                StateProp = true;
            else StateProp = false;
            count++; 
        }
    }
}
