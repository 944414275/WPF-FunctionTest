using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
 
namespace WpfTextBox
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

        //private void textBox_KeyDown(object sender, KeyEventArgs e)
        //{
        //    string strMessage = "textBox_KeyDown Event:" + e.RoutedEvent + "  " + "Key:" + e.Key;
        //    if (listBox != null)
        //    {
        //        listBox.Items.Add(strMessage);
        //    }
        //}

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string strMessage= this.textBox.Text;

            //string strMessage = "textBox_TextChanged Event:" + e.RoutedEvent+ this.textBox.Text;
            
            
            if (listBox != null)
            {
                listBox.Text="";
                listBox.Text = strMessage;

                //listBox.Items.Clear();
                //listBox.Items.Add(strMessage);
            }
        }

        private void listBox_MouseRightButtonDown(object sender, MouseButtonEventArgs e)//右击弹出菜单
        {
            ContextMenu aMenu = new ContextMenu();
            MenuItem deleteMenu = new MenuItem();
            deleteMenu.Header = "清空";
            deleteMenu.Click += btDel_Click;
            aMenu.Items.Add(deleteMenu);
            listBox.ContextMenu = aMenu;
        }
        private void btDel_Click(object sender, RoutedEventArgs e)
        {
            //listBox.Items.Clear();
        }
    }
}
