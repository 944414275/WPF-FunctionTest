using OpenCvSharp;
using System; 

namespace OpenCvSharp3
{
    class Program
    {
        static void Main(string[] args)
        {
            // 视频地址
            string videoPath = "rtmp://221.2.36.238:6062/live";
            // 视频帧率
            //int interval = 25;
            using (Window win = new Window("Capture Video :"))
            using (VideoCapture video = VideoCapture.FromFile(videoPath))
            {
                int delayTime = 1000 / (int)video.Fps;
                while (Window.WaitKey(delayTime) < 0)
                {
                    //win.Image = video.QueryFrame();
                }
            }
        }
    }
}
