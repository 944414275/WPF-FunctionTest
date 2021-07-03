using LibUsbDotNet;
using LibUsbDotNet.LibUsb;
using LibUsbDotNet.Main;
using System;
using System.Text;

namespace ConUsbTestThree
{
    class Program
    {
        const int myPID = 0x0c2e;   // 产品ID
        const int myVID = 0x0901;   // 供应商ID
        public static UsbDeviceFinder MyUsbFinder = new UsbDeviceFinder(0x0c2e, 0x0901);
        public static UsbDevice MyUsbDevice; // USB设备
        public static UsbEndpointReader reader = null;
        public static Boolean EnbaleInt = true; // 是否使用中断接收 
        static void Main(string[] args)
        {
            //FindAndOpenUSB(myPID, myVID);
            MyUsbDevice = UsbDevice.OpenUsbDevice(MyUsbFinder);
            reader = MyUsbDevice.OpenEndpointReader(ReadEndpointID.Ep02);

            if (EnbaleInt == true)
            {
                reader.DataReceived += (OnRxEndPointData);
                reader.DataReceivedEnabled = true;
            }
        }
        
        // USB中断接收函数
        private static void OnRxEndPointData(object sender, EndpointDataEventArgs e)
        {
            Console.WriteLine(e.Buffer.ToString(), 0, e.Count);
            // txtReadInt.Text = Encoding.Default.GetString(e.Buffer, 0, e.Count);
            // MessageBox.Show(Encoding.Default.GetString(e.Buffer, 0, e.Count));
            //SetText(Encoding.Default.GetString(e.Buffer, 0, e.Count));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="VID"></param>
        //private static void FindAndOpenUSB(int PID, int VID)
        //{
        //    UsbDeviceFinder MyUsbFinder = new UsbDeviceFinder(PID, VID);

        //    UsbRegistry myUsbRegistry = UsbGlobals.AllDevices.Find(MyUsbFinder);

        //    if (MyUsbDevice == null)
        //    {
        //        throw new Exception("Device Not Found");
        //    }

        //    IUsbDevice wholeUsbDevice = MyUsbDevice as IUsbDevice;
        //    if (!ReferenceEquals(wholeUsbDevice, null))
        //    {
        //        //这是个"whole"USB device,使用前选择configuration interface
        //        //选中配置1
        //        wholeUsbDevice.SetConfiguration(1);
        //        wholeUsbDevice.ClaimInterface(0);
        //    }
        //}
    }
}
