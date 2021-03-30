using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfStream
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


    }

    class Bq
    {
        public string Input { get; set; }

        public byte[] Byte { get; set; }

        public MemoryStream MemStream { get; set; }

        public byte[] GetByte()
        {
            return Encoding.ASCII.GetBytes(Input);
        }

        public MemoryStream GetMemoryStream()
        {
            byte[] byteArray = Byte;
            return new MemoryStream(byteArray);
        }

        public string GetString()
        {
            StreamReader reader = new StreamReader(MemStream);
            return reader.ReadToEnd();
        }
    } 
}
