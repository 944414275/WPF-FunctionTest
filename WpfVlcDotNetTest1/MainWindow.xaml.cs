using System; 
using System.IO; 
using System.Windows;
using Vlc.DotNet.Core;

namespace WpfVlcDotNetTest1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //VLC播放器的安装位置，我的VLC播放器安装在D:\Program Files (x86)\VideoLAN\VLC文件夹下。
            string currentDirectory = @"D:\Program Files (x86)\VideoLAN\VLC";
            var vlcLibDirectory = new DirectoryInfo(currentDirectory);

            var options = new string[]
            {
                //添加日志
                "--file-logging", "-vvv", "--logfile=Logs.log"
                // VLC options can be given here. Please refer to the VLC command line documentation.
            };
            //初始化播放器
            vlcPlayer.SourceProvider.CreatePlayer(vlcLibDirectory, options);

            //加载libvlc库并初始化内容。在调用此方法之前，要设置好选项和lib目录
            //设置播放源
            this.vlcPlayer.SourceProvider.MediaPlayer.Play(new Uri(@"F:\Media\测试\d259fd2b16067469395e16a31a385df5.mp4"));//本地文件。
            #region 播放网络流
            //this.vlcPlayer.SourceProvider.MediaPlayer.Play(new Uri("rtsp://184.72.239.149/vod/mp4://BigBuckBunny_175k.mov"));//Rtsp流文件。
            // this.vlcPlayer.SourceProvider.MediaPlayer.Play(new Uri("rtmp://10.160.64.244:1935/live/room"));
            #endregion
            //暂停
            this.vlcPlayer.SourceProvider.MediaPlayer.Pause();
            //音量控制
            this.vlcPlayer.SourceProvider.MediaPlayer.Audio.IsMute = false;
            //播放速度
            this.vlcPlayer.SourceProvider.MediaPlayer.Rate = float.Parse("2");
            //
            this.vlcPlayer.SourceProvider.MediaPlayer.Playing += new EventHandler<Vlc.DotNet.Core.VlcMediaPlayerPlayingEventArgs>(PlayingEvent);
            this.vlcPlayer.SourceProvider.MediaPlayer.Paused += new EventHandler<Vlc.DotNet.Core.VlcMediaPlayerPausedEventArgs>(PausedEvent);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //暂停
            if (tag)
            {
                this.vlcPlayer.SourceProvider.MediaPlayer.Pause();
                tag = !tag;
            }
            else
            {
                this.vlcPlayer.SourceProvider.MediaPlayer.Play();
                tag = !tag;
            }
        }

        private void PlayingEvent(object sender, VlcMediaPlayerPlayingEventArgs e)
        {
            MessageBox.Show("播放视频！");
        }

        bool tag = true;
        private void PausedEvent(object sender, Vlc.DotNet.Core.VlcMediaPlayerPausedEventArgs args)
        {
            MessageBox.Show("视频暂停了！");
        }
    }
}
