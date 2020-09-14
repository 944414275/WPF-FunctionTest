using System;
using System.Collections.Generic;
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

namespace WpfPictureTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        int intCounter = 0;
        System.Drawing.Point[] _point = new System.Drawing.Point[10];
        
        
        BitmapImage bitmapImage = new BitmapImage();
        DispatcherTimer Timer = new DispatcherTimer();
        public event PropertyChangedEventHandler PropertyChanged;
        
        ObservableCollection<Bitmap> bitmapObc = new ObservableCollection<Bitmap>();
        ObservableCollection<BitmapImage> bitmapImages = new ObservableCollection<BitmapImage>(); 
        


        public ObservableCollection<Bitmap> BitmapObcProp
        {
            get { return bitmapObc; }
            set
            {
                bitmapObc = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("BitmapObcProp"));
            }
        }
        public BitmapImage BitmapImageProp
        {
            get { return bitmapImage; }
            set
            {
                bitmapImage = value;
                if (PropertyChanged != null)
                    this.PropertyChanged.Invoke(this,new PropertyChangedEventArgs("BitmapImageProp"));
            }
        }
        //int index = 0;  //记录索引
        //bool isRendering = false;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            CreatPoints();
             
            DrawingBrush drawingBrush = new DrawingBrush();
             
            CreateBmp();
            //定时
            Timer.Tick += delegate (object sender, EventArgs e)
            {
                intCounter ++;
                ShowBmpMethod(intCounter);
                if (intCounter > 2)
                    Timer.Stop(); 
            };
            Timer.Interval = new TimeSpan(2000);
            Timer.Start();
             

            //InitList();

            //CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);
            //BackgroundWorker bw = new BackgroundWorker();
            //bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            //bw.RunWorkerAsync();
        }

        //void bw_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    while (true)
        //    {
        //        isRendering = true;
        //        System.Threading.Thread.Sleep(50); //停1秒
        //    }
        //}

        //public void InitList()
        //{ 
        //    for (int i = 1; i < 4; i++)
        //    {
        //        BitmapImage bmImg = new BitmapImage(new Uri(System.Environment.CurrentDirectory + "\\" + i.ToString() + ".jpg"));
        //        bitmapObc.Add(bmImg);
        //        //Bitmap bitmap = new Bitmap();
        //    }
        //}

        //void CompositionTarget_Rendering(object sender, EventArgs e)
        //{
        //    if (isRendering)
        //    {
        //        if (index < bitmapObc.Count)
        //        {
        //            this.imgViewer.Source = bitmapObc[index];
        //            this.imgViewer.Width = this.imgViewer.Source.Width;
        //            this.imgViewer.Height = this.imgViewer.Source.Height;

        //            index++;
        //        }
        //        else
        //        {
        //            index = 0;
        //        }
        //        isRendering = false;
        //    }
        //} 
        

        void CreatPoints()
        {
            for(int i=0;i<10;i++)
            {
                _point[i].X = i;
                _point[i].Y = i;
            }
        }

        void CreateBmp()
        {
            for(int i=0;i<3;i++)
            {
                Bitmap bmp = new Bitmap(700, 260);
                Graphics g = Graphics.FromImage(bmp);



                BitmapImage image = new BitmapImage();

                System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.White, 1);
                System.Drawing.Pen dashPen = new System.Drawing.Pen(System.Drawing.Color.Gray, 1);
                dashPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                for (int j = 0; j < 5; j++)
                {
                    g.DrawLine(dashPen, 15.0f, j * 45 + 5.0f, 451.92f + 15f, j * 45 + 5.0f);
                    g.DrawLine(dashPen, (j + 1) * 90.3846f + 15f, 0 + 5f, (j + 1) * 90.3846f + 15, 225 + 5f);
                }

                image = BitmapToBitmapImage(bmp);
                bitmapImages.Add(image); 
            } 
        }
        
        void ShowBmpMethod(int count)
        { 
            BitmapImageProp = bitmapImages[count - 1]; 
        }

        // Bitmap --> BitmapImage
        BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png); // 坑点：格式选Bmp时，不带透明度 
                stream.Position = 0;

                BitmapImage result = new BitmapImage();
                result.BeginInit();
                // According to MSDN, "The default OnDemand cache option retains access to the stream until the image is needed."
                // Force the bitmap to load right now so we can dispose the stream.
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.StreamSource = stream;
                result.EndInit();
                result.Freeze();
                return result;
            }
        } 
    }
}
