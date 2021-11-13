using FFmpeg.AutoGen;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;

namespace ConFFmpeg1
{
    class Program
    {
        [DllImport("Project1.dll", CallingConvention = CallingConvention.Cdecl)]
        extern static int ffmpegVideo(byte[] rgbvalues, int Width, int Height);

        static void Main(string[] args)
        {
            Console.WriteLine("Current directory: " + Environment.CurrentDirectory);
            Console.WriteLine("Running in {0}-bit mode.", Environment.Is64BitProcess ? "64" : "32");

            FFmpegBinariesHelper.RegisterFFmpegBinaries();

            Console.WriteLine($"FFmpeg version info: {ffmpeg.av_version_info()}");

            ConfigureHWDecoder(out var deviceType);

            Console.WriteLine("Decoding...");
            DecodeAllFramesToImages(deviceType); 
        }

        private static void ConfigureHWDecoder(out AVHWDeviceType HWtype)
        {
            HWtype = AVHWDeviceType.AV_HWDEVICE_TYPE_NONE;
            Console.WriteLine("Use hardware acceleration for decoding?[n]");
            var key = Console.ReadLine();
            var availableHWDecoders = new Dictionary<int, AVHWDeviceType>();

            if (key == "y")
            {
                Console.WriteLine("Select hardware decoder:");
                var type = AVHWDeviceType.AV_HWDEVICE_TYPE_NONE;
                var number = 0;

                while ((type = ffmpeg.av_hwdevice_iterate_types(type)) != AVHWDeviceType.AV_HWDEVICE_TYPE_NONE)
                {
                    Console.WriteLine($"{++number}. {type}");
                    availableHWDecoders.Add(number, type);
                }

                if (availableHWDecoders.Count == 0)
                {
                    Console.WriteLine("Your system have no hardware decoders.");
                    HWtype = AVHWDeviceType.AV_HWDEVICE_TYPE_NONE;
                    return;
                }

                var decoderNumber = availableHWDecoders
                    .SingleOrDefault(t => t.Value == AVHWDeviceType.AV_HWDEVICE_TYPE_DXVA2).Key;
                if (decoderNumber == 0)
                    decoderNumber = availableHWDecoders.First().Key;
                Console.WriteLine($"Selected [{decoderNumber}]");
                int.TryParse(Console.ReadLine(), out var inputDecoderNumber);
                availableHWDecoders.TryGetValue(inputDecoderNumber == 0 ? decoderNumber : inputDecoderNumber,
                    out HWtype);
            }
        }

        /// <summary>
        /// 20211026 komla 将视频解码成图片
        /// </summary>
        /// <param name="HWDevice"></param>
        private static unsafe void DecodeAllFramesToImages(AVHWDeviceType HWDevice)
        {
            // decode all frames from url, please not it might local resorce, e.g. string url = "../../sample_mpeg4.mp4";
            //var url = "http://clips.vorwaerts-gmbh.de/big_buck_bunny.mp4"; // be advised this file holds 1440 frames
            var url = "rtmp://221.2.36.238:6062/live";
            using var vsd = new VideoStreamDecoder(url, HWDevice);

            Console.WriteLine($"codec name: {vsd.CodecName}");

            var info = vsd.GetContextInfo();
            info.ToList().ForEach(x => Console.WriteLine($"{x.Key} = {x.Value}"));

            var sourceSize = vsd.FrameSize;
            var sourcePixelFormat = HWDevice == AVHWDeviceType.AV_HWDEVICE_TYPE_NONE
                ? vsd.PixelFormat
                : GetHWPixelFormat(HWDevice);
            var destinationSize = sourceSize;
            var destinationPixelFormat = AVPixelFormat.AV_PIX_FMT_BGR24;
            using var vfc =
                new VideoFrameConverter(sourceSize, sourcePixelFormat, destinationSize, destinationPixelFormat);

            var frameNumber = 0;
            var win = new Window("fengbi");
            Mat image = new Mat();
             
            while (vsd.TryDecodeNextFrame(out var frame))
            {
                var convertedFrame = vfc.Convert(frame);

                using (var bitmap = new Bitmap(convertedFrame.width,
                    convertedFrame.height,
                    convertedFrame.linesize[0],
                    PixelFormat.Format24bppRgb,
                    (IntPtr)convertedFrame.data[0]))
                {
                    image = BitmapConverter.ToMat(bitmap);
                    win.ShowImage(image);
                    Cv2.WaitKey(5);
                    //byte[] rgbvalues = GetBitmapData(bitmap);
                    //ffmpegVideo(rgbvalues, bitmap.Width, bitmap.Height);
                }
                    //bitmap.Save($"frame.{frameNumber:D8}.jpg", ImageFormat.Jpeg);
                    //byte[] rgbvalues = GetBitmapData(bitmap);
                //Console.WriteLine($"frame: {frameNumber}");
                //frameNumber++;
            }
        }

        private static AVPixelFormat GetHWPixelFormat(AVHWDeviceType hWDevice)
        {
            return hWDevice switch
            {
                AVHWDeviceType.AV_HWDEVICE_TYPE_NONE => AVPixelFormat.AV_PIX_FMT_NONE,
                AVHWDeviceType.AV_HWDEVICE_TYPE_VDPAU => AVPixelFormat.AV_PIX_FMT_VDPAU,
                AVHWDeviceType.AV_HWDEVICE_TYPE_CUDA => AVPixelFormat.AV_PIX_FMT_CUDA,
                AVHWDeviceType.AV_HWDEVICE_TYPE_VAAPI => AVPixelFormat.AV_PIX_FMT_VAAPI,
                AVHWDeviceType.AV_HWDEVICE_TYPE_DXVA2 => AVPixelFormat.AV_PIX_FMT_NV12,
                AVHWDeviceType.AV_HWDEVICE_TYPE_QSV => AVPixelFormat.AV_PIX_FMT_QSV,
                AVHWDeviceType.AV_HWDEVICE_TYPE_VIDEOTOOLBOX => AVPixelFormat.AV_PIX_FMT_VIDEOTOOLBOX,
                AVHWDeviceType.AV_HWDEVICE_TYPE_D3D11VA => AVPixelFormat.AV_PIX_FMT_NV12,
                AVHWDeviceType.AV_HWDEVICE_TYPE_DRM => AVPixelFormat.AV_PIX_FMT_DRM_PRIME,
                AVHWDeviceType.AV_HWDEVICE_TYPE_OPENCL => AVPixelFormat.AV_PIX_FMT_OPENCL,
                AVHWDeviceType.AV_HWDEVICE_TYPE_MEDIACODEC => AVPixelFormat.AV_PIX_FMT_MEDIACODEC,
                _ => AVPixelFormat.AV_PIX_FMT_NONE
            };
        }

        //private static byte[] GetBitmapData(Bitmap frameBitmap)
        //{
        //    var bitmapData = frameBitmap.LockBits(new Rectangle(Point.Empty, frameBitmap.Size),
        //        ImageLockMode.ReadOnly,
        //        PixelFormat.Format24bppRgb);

        //    try
        //    {
        //        var length = bitmapData.Stride * bitmapData.Height;
        //        var data = new byte[length];
        //        Marshal.Copy(bitmapData.Scan0, data, 0, length);
        //        return data;
        //    }
        //    finally
        //    {
        //        frameBitmap.UnlockBits(bitmapData);
        //    }
        //}
    }
}