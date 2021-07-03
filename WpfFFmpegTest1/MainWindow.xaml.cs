using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace WpfFFmpegTest1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Process process;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (process != null)
            {
                MessageBox.Show("请先停止录屏");
                return;
            }

            string path = AppDomain.CurrentDomain.BaseDirectory;

            process = new Process();

            process.StartInfo.FileName = path + "ffmpeg.exe";

            process.StartInfo.Arguments = " -f gdigrab -framerate 15 -i desktop -f flv rtmp://172.16.18.73:1935/live";    //执行参数

            process.StartInfo.UseShellExecute = false;  ////不使用系统外壳程序启动进程
            process.StartInfo.CreateNoWindow = true;  //不显示dos程序窗口

            process.StartInfo.RedirectStandardInput = true;

            process.StartInfo.RedirectStandardOutput = true;

            process.StartInfo.RedirectStandardError = true;//把外部程序错误输出写到StandardError流中

            process.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);

            process.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);

            //process.StartInfo.UseShellExecute = false;

            process.Start();

            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            process.BeginErrorReadLine();//开始异步读取
        }

        private void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            //AddText(e.Data + "\r\n");
            //Console.WriteLine(e.Data);
        }

        private void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            //AddText(e.Data);
            //Console.WriteLine(e.Data + "\r\n");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (process != null)
            {
                process.Kill();
                process.Close();
                process.Dispose();
                process = null;
            }
        }
    }
}
