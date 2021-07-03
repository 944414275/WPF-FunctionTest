using FFmpeg.AutoGen;
using System;
using System.Diagnostics;
using System.IO;

namespace ConFFmpegTest1
{
    class Program
    { 
        private static Process process = new Process();
        private static string ffmpegPath = AppDomain.CurrentDomain.BaseDirectory + "\\ffmpeg\\ffmpeg.exe";
        static void Main(string[] args)
        {
            string rtmpUrl = "rtmp://172.16.18.73:1935/live";
            StartRtmpPush(ffmpegPath, rtmpUrl);
        }

        /// <summary>
        /// 功能: 开始录制
        /// </summary>
        public static void Start(string audioDevice, string outFilePath)
        {
            if (File.Exists(outFilePath))
            {
                File.Delete(outFilePath);
            }

            /*转码，视频录制设备：gdigrab；录制对象：桌面；
             * 音频录制方式：dshow；
             * 视频编码格式：h.264；*/
            ProcessStartInfo startInfo = new ProcessStartInfo(ffmpegPath);
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;

            var parastr = string.Format("-f gdigrab -framerate 15 -i desktop -f dshow -i audio=\"{0}\" -vcodec libx264 -preset:v ultrafast -tune:v zerolatency -acodec libmp3lame \"{1}\"", audioDevice, outFilePath);
            startInfo.Arguments = parastr;
            process.StartInfo = startInfo;

            process.Start();
        }

        /// <summary>
        /// 功能: 开始推流
        /// </summary>
        public static void StartPush(string audioDevice, string pushUrl)
        {
            /*转码，视频录制设备：gdigrab；录制对象：桌面；
             * 音频录制方式：dshow；
             * 视频编码格式：h.264；*/
            ProcessStartInfo startInfo = new ProcessStartInfo(ffmpegPath);
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            var parastr = string.Format("-f gdigrab -framerate 15 -i desktop -f dshow -i audio=\"{0}\" -filter:v scale=w=trunc(oh*a/2)*2:h=720 -vcodec libx264 -preset:v ultrafast -acodec libmp3lame -maxrate 1000k -pix_fmt yuv422p -f flv \"{1}\"", audioDevice, pushUrl);
            startInfo.Arguments = parastr;
            process.StartInfo = startInfo;
            // Console.WriteLine("parastr:" + parastr);
            process.Start();
        }

        /// <summary>
        /// 功能: 停止录制
        /// </summary>
        public static void Stop()
        {
            process.Kill();
            process.StartInfo.Arguments = "";
        }

        /// <summary>
        /// 20210610 komla
        /// </summary>
        /// <param name="ffmpegPath"></param>
        /// <param name="rtmpUrl"></param>
        public static void StartRtmpPush(string ffmpegPath,string rtmpUrl)
        { 
            //var parastr = string.Format("-f gdigrab -framerate 15 -i desktop -f flv \"{0}\"", rtmpUrl);
            var parastr = string.Format("-f gdigrab -framerate 15 -max_delay 100 -g 5 -b 700000 -i desktop -f flv \"{0}\"", rtmpUrl);

            /*转码，视频录制设备：gdigrab；录制对象：桌面；
             * 音频录制方式：dshow；
             * 视频编码格式：h.264；*/
            ProcessStartInfo startInfo = new ProcessStartInfo(ffmpegPath);
            //startInfo.FileName = ffmpegPath;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.UseShellExecute = false; 
            startInfo.Arguments = parastr;
            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;

            startInfo.RedirectStandardError = true;

            process.StartInfo = startInfo;
            process.ErrorDataReceived+= new DataReceivedEventHandler(p_ErrorDataReceived);
            process.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);

            // Console.WriteLine("parastr:" + parastr);
            process.Start();
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
