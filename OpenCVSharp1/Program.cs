using OpenCvSharp;
using System;

namespace OpenCVSharp1
{
    /// <summary>
    /// 20211102 komla using...
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            bool StopFlag = false;
            var capture = new VideoCapture("rtmp://221.2.36.238:6062/live");
            
            //此处参考网上的读取方法
            //int sleepTime = (int)Math.Round(1000 / capture.Fps);
            var win = new Window("fengbi");
            Mat image = new Mat();
            while (!StopFlag)
            {
                if (capture == null || capture.IsDisposed)
                { 
                    continue;
                }
               
                bool res = capture.Read(image);
                if(!res || image.Empty())
                {
                    capture.Open("rtmp://221.2.36.238:6062/live");
                    continue;
                }
                win.ShowImage(image);

                //image.Release();
                Cv2.WaitKey(5);
            }
            //using (var window = new Window("capture"))
            //{
            //    // 进入读取视频每帧的循环
            //    while (true)
            //    {
            //        // 声明实例 Mat类
            //        Mat image = new Mat();
            //        capture.Read(image);
            //        //判断是否还有没有视频图像 
            //        if (image.Empty())
            //        {
            //            Console.WriteLine("没有图像！");
            //            break;
            //        }
                        
            //        // 在picturebox中播放视频， 需要先转换成bitmap格式
            //        //picturebox1.image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(image);

            //        // 在Window窗口中播放视频
            //        window.ShowImage(image);
            //        //image.Dispose();
            //        window.Dispose();
            //        Cv2.WaitKey(sleepTime);
            //    }
            //}
        }
    }
}
