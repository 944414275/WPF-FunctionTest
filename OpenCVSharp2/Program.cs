using OpenCvSharp;
using System;

namespace OpenCVSharp2
{
    class Program
    {
        private static VideoCapture Capture;
        private static VideoWriter VideoWriter; 
        private static bool StopFlag = false;
        private bool SaveFlag = false;

        static void Main(string[] args)
        { 
            //Capture = new VideoCapture("rtmp://221.2.36.238:6062/live");
            Capture = new VideoCapture("rtmp://221.2.36.238:6062/live");
            if (!Capture.IsOpened())
                return;
            int time = (int)Math.Round(1000 / Capture.Fps);
            
            var window = new Window("capture");
            var image = new Mat();
            // When the movie playback reaches end, Mat.data becomes NULL.
            while (true)
            {
                Capture.Read(image); // same as cvQueryFrame
                if (image.Empty())
                    continue;

                window.ShowImage(image);
                Cv2.WaitKey(time);
                image.Dispose();
            }
            //while (!StopFlag)
            //{
            //    if (Capture == null || Capture.IsDisposed)
            //    {
            //        continue;
            //    }

            //    Mat mat = new Mat();
            //    bool res = Capture.Read(mat);//会阻塞线程
            //    if (!res || mat.Empty())
            //    {
            //        continue;
            //    }
            //    window.ShowImage(mat);

            //    Cv2.WaitKey(time);

            //    mat.Dispose();

            //}
        }
    }
}
