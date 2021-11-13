using FFmpeg.AutoGen;
using System; 
using System.Windows;  
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace WpfFFmpeg1
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FFmpegBinariesHelper.RegisterFFmpegBinaries();
            MessageBox.Show("FFmpeg version info:"+ ffmpeg.av_version_info());
            //Console.WriteLine($"FFmpeg version info: {ffmpeg.av_version_info()}");
            DecodeAllFramesToImages();

        }

        private unsafe void DecodeAllFramesToImages()
        {
            var url = "http://clips.vorwaerts-gmbh.de/big_buck_bunny.mp4";
            using var vsd = new VideoStreamDecoder(url);

            var sourceSize = vsd.FrameSize;
            var sourcePixelFormat = vsd.PixelFormat;
            var destinationSize = sourceSize;
            var destinationPixelFormat = AVPixelFormat.AV_PIX_FMT_BGR24;
            using var vfc =
                new VideoFrameConverter(sourceSize, sourcePixelFormat, destinationSize, destinationPixelFormat);

            var frameNumber = 0;

            while (vsd.TryDecodeNextFrame(out var frame))
            {
                var convertedFrame = vfc.Convert(frame);

                using (var bitmap = new Bitmap(convertedFrame.width,
                    convertedFrame.height,
                    convertedFrame.linesize[0],
                    PixelFormat.Format24bppRgb,
                    (IntPtr)convertedFrame.data[0]));
                frameNumber++;

                //bitmap.Save($"frame.{frameNumber:D8}.jpg", ImageFormat.Jpeg);

                //{
                //    //bitmap.Save($"frame.{frameNumber:D8}.jpg", ImageFormat.Jpeg);
                //    //videoImage.Source = bitmap;
                //    MemoryStream ms = new MemoryStream();
                //    bitmap.Save(ms, ImageFormat.Bmp);
                //    byte[] bytes = ms.GetBuffer();  //byte[]   bytes=   ms.ToArray(); 这两句都可以
                //    ms.Close();
                //    //Convert it to BitmapImage
                //    BitmapImage image = new BitmapImage();
                //    image.BeginInit();
                //    image.StreamSource = new MemoryStream(bytes);
                //    image.EndInit();
                //    videoImage.Source = image;
                //    //https://blog.csdn.net/u013139930/article/details/51785687
                //    //Console.WriteLine($"frame: {frameNumber}");
                //    //frameNumber++;
                //}
            }
        }
    }
}
