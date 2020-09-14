using System.Collections.ObjectModel;
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
using System.Drawing;
using System.Windows.Threading;
using System.IO;
using System.Drawing.Imaging;


namespace WpfPictureTest1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        byte[] vs = new byte[] { 0,1,2,3,4,5,6,7,8,9};
        BitmapImage bitmapImage = new BitmapImage();


        public MainWindow()
        {
            InitializeComponent();
            bitmapImage = ToImage(vs);
            this.ssss.Source = bitmapImage;
        }


        /// <summary>
        /// byte[]转为BitmapImage
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        BitmapImage ToImage(byte[] byteArray)
        {
            BitmapImage bmpImage = null;
           
            try
            {   
                bmpImage = new BitmapImage();
                bmpImage.BeginInit();
                bmpImage.StreamSource = new MemoryStream(byteArray);
                bmpImage.EndInit();
            }
            catch
            {
                bmpImage = null;
            } 
            return bmpImage;
        } 
    }
}
