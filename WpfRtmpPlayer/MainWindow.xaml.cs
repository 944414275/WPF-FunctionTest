using System;
using System.Collections.Generic;
using System.IO; 
using System.Reflection; 
using System.Windows;


namespace WpfRtmpPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //初始化配置，指定引用库
            //myControl.SourceProvider.MediaPlayer.VlcLibDirectoryNeeded += OnVlcControlNeedsLibDirectory;
            //myControl.MediaPlayer.VlcLibDirectoryNeeded += OnVlcControlNeedsLibDirectory;
            //myControl.MediaPlayer.EndInit();
        }

        private void OnVlcControlNeedsLibDirectory(object sender, Vlc.DotNet.Forms.VlcLibDirectoryNeededEventArgs e)
        {
            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            if (currentDirectory == null)
                return;
            if (AssemblyName.GetAssemblyName(currentAssembly.Location).ProcessorArchitecture == ProcessorArchitecture.X86)
                e.VlcLibDirectory = new DirectoryInfo(System.IO.Path.Combine(currentDirectory, @"..\..\..\lib\x86\"));
            else
                e.VlcLibDirectory = new DirectoryInfo(System.IO.Path.Combine(currentDirectory, @"..\..\..\lib\x64\"));
        }

        private void OnPlayButtonClick(object sender, RoutedEventArgs e)
        {
            //播放网络视频
            //myControl.MediaPlayer.Play(new Uri("http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_surround-fix.avi"));
            //播放FM广播失败
            //myControl.MediaPlayer.Play(new Uri("mms://radiolive.jnnc.com/news"));

            //播放本地视频
            //string filename = @"F:\MyDocument\视频\COOLUI理念篇.mp4";
            //myControl.MediaPlayer.Play(new FileInfo(filename));
        }
        private void OnForwardButtonClick(object sender, RoutedEventArgs e)
        {
            //myControl.MediaPlayer.Rate = 2;
        }

        private void GetLength_Click(object sender, RoutedEventArgs e)
        {
            //GetLength.Content = myControl.MediaPlayer.Length + " ms";
        }
        private void GetCurrentTime_Click(object sender, RoutedEventArgs e)
        {
            //GetCurrentTime.Content = myControl.MediaPlayer.Time + " ms";
        }
        private void SetCurrentTime_Click(object sender, RoutedEventArgs e)
        {
            //myControl.MediaPlayer.Time = 5000;
            //SetCurrentTime.Content = myControl.MediaPlayer.Time + " ms";
        }

    }
}
//————————————————
//版权声明：本文为CSDN博主「天马3798」的原创文章，遵循CC 4.0 BY-SA版权协议，转载请附上原文出处链接及本声明。
//原文链接：https://blog.csdn.net/u011127019/article/details/52734195
