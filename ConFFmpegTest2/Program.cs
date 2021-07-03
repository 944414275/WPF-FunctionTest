using System;
using System.Diagnostics;

/// <summary>
/// 可用
/// </summary>
namespace ConFFmpegTest2
{
    class Program
    {
        private static Process process;
        static void Main(string[] args)
        {
            if (process != null)
            {
                Console.WriteLine("请先停止录屏!");
                return;
            }

            string path = AppDomain.CurrentDomain.BaseDirectory;

            process = new Process();

            process.StartInfo.FileName = path + "ffmpeg.exe";

            process.StartInfo.Arguments = " -r 30 -f gdigrab -framerate 15 -i desktop -max_delay 100 -g 5 -b 700000 -f flv rtmp://172.16.18.73:1935/live";    //执行参数

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

        private static void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            //AddText(e.Data + "\r\n");
            //Console.WriteLine(e.Data);
        }

        private static void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            //AddText(e.Data);
            //Console.WriteLine(e.Data + "\r\n");
        }
    }
}
