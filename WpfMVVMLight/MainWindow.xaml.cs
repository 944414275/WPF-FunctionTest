using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace WpfMVVMLight
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window 
    {

        public MainWindow()
        {
            MainViewModel mainViewModel = new MainViewModel();
            InitializeComponent();
            this.DataContext = mainViewModel;
            mainViewModel.inidata();
           



        } 
    }

    public class MainViewModel:ViewModelBase
    {
        private ObservableCollection<string> logList = new ObservableCollection<string>();
        private int selectIndex_listBox = 7;
        public event PropertyChangedEventHandler PropertyChanged;
        int addNum = 20;

        private RelayCommand addCommand;
        //开始实验按钮
        public RelayCommand AddBtn
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand(new Action(addMethod), true);
                }
                return addCommand;
            }
        }

        public void addMethod()
        {
            LogListProp.Add(addNum.ToString());
            addNum++;
        }

        public ObservableCollection<string> LogListProp
        {
            get { return logList; }
            set
            {
                logList = value;
                SelectIndex_listBoxProp = logList.Count - 1;
                if (PropertyChanged != null)
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("LogListProp"));
            }
        }
        public int SelectIndex_listBoxProp
        {
            get { return selectIndex_listBox; }
            set
            {
                selectIndex_listBox = value;
                if (PropertyChanged != null)
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SelectIndex_listBoxProp"));
            }
        }

        public void inidata()
        {
            for (int i = 0; i < 10; i++)
            {
                LogListProp.Add(i.ToString());
                Thread.Sleep(500);
            }
        }

        string s = "AA";
        public string S
        {
            get { return s; }
            set { s = value;
                RaisePropertyChanged();
            }
        }
    }

    public class ScrollingListBox : ListBox
    {
        protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)//此处需要判空
            {
                int newItemCount = e.NewItems.Count;

                if (newItemCount > 0)
                    this.ScrollIntoView(e.NewItems[newItemCount - 1]);

                base.OnItemsChanged(e);
            }
        }
    }
}
