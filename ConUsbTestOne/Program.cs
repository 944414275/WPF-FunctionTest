using LibUsbDotNet;
using LibUsbDotNet.Main;
using System; 
using LibUsbDotNet.Info;
using System.Collections.ObjectModel;
using LibUsbDotNet.LibUsb;

namespace ConUsbTestOne
{
    class Program
    {
        //public static IDeviceNotifier UsbDeviceNotifier=DeviceNo
        const int myPID = 0x0c2e;  //产品ID
        const int myVID = 0x0901;  //供应商ID
        public static UsbDevice MyUsbDevice;//USB设备 
        public static UsbEndpointWriter writer = null;
        public static UsbEndpointReader reader = null;

        static void Main(string[] args)
        {
            PrintUsbInfo();

            // Wait for user input..
            Console.ReadKey();
        }

        public static void PrintUsbInfo()
        {
            UsbDevice usbDevice = null;
            UsbRegDeviceList allDevices = UsbDevice.AllDevices;

            Console.WriteLine("Found {0} devices", allDevices.Count);

            foreach (UsbRegistry usbRegistry in allDevices)
            {
                Console.WriteLine("Got device: {0}\r\n", usbRegistry.FullName);

                if (usbRegistry.Open(out usbDevice))
                {
                    Console.WriteLine("Device Information\r\n------------------");

                    Console.WriteLine("{0}", usbDevice.Info.ToString());

                    Console.WriteLine("VID & PID: {0} {1}", usbDevice.Info.Descriptor.VendorID, usbDevice.Info.Descriptor.ProductID);

                    Console.WriteLine("\r\nDevice configuration\r\n--------------------");
                    foreach (UsbConfigInfo usbConfigInfo in usbDevice.Configs)
                    {
                        Console.WriteLine("{0}", usbConfigInfo.ToString());

                        Console.WriteLine("\r\nDevice interface list\r\n---------------------");
                        ReadOnlyCollection<UsbInterfaceInfo> interfaceList = usbConfigInfo.InterfaceInfoList;
                        foreach (UsbInterfaceInfo usbInterfaceInfo in interfaceList)
                        {
                            Console.WriteLine("{0}", usbInterfaceInfo.ToString());

                            Console.WriteLine("\r\nDevice endpoint list\r\n--------------------");
                            ReadOnlyCollection<UsbEndpointInfo> endpointList = usbInterfaceInfo.EndpointInfoList;
                            foreach (UsbEndpointInfo usbEndpointInfo in endpointList)
                            {
                                Console.WriteLine("{0}", usbEndpointInfo.ToString());
                            }
                        }
                    }
                    usbDevice.Close();
                }
                Console.WriteLine("\r\n----- Device information finished -----\r\n");
            }
        }

        
    }
}
