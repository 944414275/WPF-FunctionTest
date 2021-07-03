using LibUsbDotNet;
using LibUsbDotNet.Main;
using System;
using System.Text;

namespace ConUsbTestTwo
{
    class Program
    {
        public static UsbDevice MyUsbDevice;
        //设置USB的Vendor Product ID
        public static UsbDeviceFinder MyUsbFinder = new UsbDeviceFinder(0x0c2e, 0x0901);

        static void Main(string[] args)
        {
            ErrorCode ec = ErrorCode.None;
            try
            {
                //找到并打开USB设备
                MyUsbDevice = UsbDevice.OpenUsbDevice(MyUsbFinder);

                if (MyUsbDevice == null)
                {
                    throw new Exception("Device Not Found");
                }
                //如果设备打开and ready

                //libusb-win32是"whole"USB device，为IUsbDevice interface,不是（WinUSB）,则变量
                //wholeUSBDevice变量为null，是device interface,不需要configuration 和 interface
                //as is 判断两个变量是否相等，is 返回TRUE/FALSE; as 相同返回结果，不同返回null
                IUsbDevice wholeUsbDevice = MyUsbDevice as IUsbDevice;
                if (!ReferenceEquals(wholeUsbDevice, null))
                {
                    //这是个"whole"USB device,使用前选择configuration interface
                    //选中配置1
                    wholeUsbDevice.SetConfiguration(1);
                    wholeUsbDevice.ClaimInterface(0);
                }

                //打开并读取 read endpoint
                UsbEndpointReader reader = MyUsbDevice.OpenEndpointReader(ReadEndpointID.Ep02);

                byte[] readBuffer = new byte[1024];
                //如果5秒内设备没有发生数据，发生timeout error(ec=IoTimedOut)
                while (ec == ErrorCode.None) //一直在读取
                {
                    int bytesRead;
                    ec = reader.Read(readBuffer, 5000000, out bytesRead);

                    if (bytesRead == 0) throw new Exception(string.Format("{0}:No more bytes", ec));
                    Console.WriteLine("{0} bytes read", bytesRead);

                    //将结果输出到控制台上
                    Console.Write(Encoding.Default.GetString(readBuffer, 0, bytesRead));
                }
                //Console.WriteLine("\r\n Done! \r\n");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine((ec != ErrorCode.None ? ec + ":" : string.Empty) + ex.Message);
            }
            //读取数据后执行
            finally
            {
                if (MyUsbDevice != null)
                {
                    if (MyUsbDevice.IsOpen)
                    {
                        IUsbDevice wholeUsbDevice = MyUsbDevice as IUsbDevice;
                        if (!ReferenceEquals(wholeUsbDevice, null))
                        {
                            //释放interface 0
                            wholeUsbDevice.ReleaseInterface(0);
                        }
                        MyUsbDevice.Close();
                    }
                    MyUsbDevice = null;
                    //释放usb资源
                    UsbDevice.Exit();
                }

                Console.ReadKey();
            }
        }
    }
}
