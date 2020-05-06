using System;
using System.Collections.Generic;
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
using System.Threading;

namespace WpfThread
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Thread> threadList = new List<Thread>();
        Thread threadA = null;
        Thread threadB = null;
        Thread threadC = null;
        Thread threadD = null;

        public MainWindow()
        {
            InitializeComponent(); 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(threadA!=null)
            {
                threadA.Abort();
                threadA = null;
            }
            if (threadB != null)
            {
                threadB.Abort();
                threadB = null;
            }
            if (threadC != null)
            {
                threadC.Abort();
                threadC = null;
            }

            threadA = new Thread(FunA);
            if(!threadList.Contains(threadA))
            {
                threadList.Add(threadA);
            } 
            threadA.IsBackground = true;
            threadA.Start();

            threadB = new Thread(FunB);
            if(!threadList.Contains(threadB))
            {
                threadList.Add(threadB);
            } 
            threadB.IsBackground = true;
            threadB.Start();

            threadC = new Thread(FunC);
            if(!threadList.Contains(threadC))
            {
                threadList.Add(threadC);
            } 
            threadC.IsBackground = true;
            threadC.Start(); 
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            foreach(Thread item in threadList)
            {
                if(item!= threadB)
                {
                    item.Abort();
                    Thread thread = item;
                    thread = null;
                }
            }
        }

        public void FunA()
        {  }
        public void FunB()
        { }
        public void FunC()
        { }
        public void FunD()
        { }
    }
}
