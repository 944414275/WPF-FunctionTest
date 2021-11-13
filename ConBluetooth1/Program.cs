using InTheHand.Net.Bluetooth;
using System;
using System.Collections.Generic; 

namespace ConBluetooth1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello world!");
            BluetoothRadio radio = BluetoothRadio.PrimaryRadio;//获取蓝牙适配器
            if (radio == null)
            {
                Console.WriteLine("没有找到本机蓝牙设备!");
            }
            else
            {
                Console.WriteLine("ClassOfDevice: " + radio.ClassOfDevice);
                Console.WriteLine("HardwareStatus: " + radio.HardwareStatus);
                Console.WriteLine("HciRevision: " + radio.HciRevision);
                Console.WriteLine("HciVersion: " + radio.HciVersion);
                Console.WriteLine("LmpSubversion: " + radio.LmpSubversion);
                Console.WriteLine("LmpVersion: " + radio.LmpVersion);
                Console.WriteLine("LocalAddress: " + radio.LocalAddress);
                Console.WriteLine("Manufacturer: " + radio.Manufacturer);
                Console.WriteLine("Mode: " + radio.Mode);
                Console.WriteLine("Name: " + radio.Name);
                Console.WriteLine("Remote:" + radio.Remote);
                Console.WriteLine("SoftwareManufacturer: " + radio.SoftwareManufacturer);
                Console.WriteLine("StackFactory: " + radio.StackFactory);
            }
            //Console.ReadKey();

            List<LanYa> lanYaList = new List<LanYa>(); //搜索到的蓝牙的集合
            BluetoothClient client = new BluetoothClient();
            radio.Mode = RadioMode.Connectable;
            BluetoothDeviceInfo[] devices = client.DiscoverDevices();//搜索蓝牙 10秒钟
            int count = 0;
            foreach (var item in devices)
            {
                count++;
                Console.WriteLine("===========蓝牙设备" + count + "================");
                Console.WriteLine("device name:" + item.DeviceName);//输出每个蓝牙设备的名字
                Console.WriteLine("device address:" + item.DeviceAddress);//输出每个蓝牙设备的名字
                Console.WriteLine("ClassOfDevice:" + item.ClassOfDevice);
                Console.WriteLine("Authenticated:" + item.Authenticated);
                Console.WriteLine("Remembered:" + item.Remembered);
                Console.WriteLine("LastSeen:" + item.LastSeen);
                Console.WriteLine("LastUsed:" + item.LastUsed);
                lanYaList.Add(new LanYa { blueName = item.DeviceName, blueAddress = item.DeviceAddress, blueClassOfDevice = item.ClassOfDevice, IsBlueAuth = item.Authenticated, IsBlueRemembered = item.Remembered, blueLastSeen = item.LastSeen, blueLastUsed = item.LastUsed });//把搜索到的蓝牙添加到集合中
            }
            Console.WriteLine("device count:" + devices.Length);//输出搜索到的蓝牙设备个数

            ////蓝牙的配对
            //BluetoothClient blueclient = new BluetoothClient();
            //Guid mGUID1 = BluetoothService.Handsfree; //蓝牙服务的uuid

            //blueclient.Connect(s.blueAddress, mGUID) //开始配对 蓝牙4.0不需要setpin

            BluetoothDeviceInfo dev = devices[0];

            //客户端
            BluetoothClient bl = new BluetoothClient();//
            Guid mGUID = Guid.Parse("0000fff4-0000-1000-8000-00805F9B34FB");//蓝牙串口服务的uuiid

            //byte[] def = { 0x13, 0x00, 0x05, 0x15, 0x11, 0x08, 0x01, 0x10, 0x02, 0x01, 0x12, 0x14 };

            //string s = "D05FB81A21CE";
            //byte[] def = Encoding.Default.GetBytes(s);
            //string str2 = BitConverter.ToString(def);
            //Console.WriteLine(str2);

            //BluetoothAddress abc = new BluetoothAddress(def);

            try
            {
                //bl.Connect(abc, mGUID);
                bl.Connect(dev.DeviceAddress, mGUID);
                Console.WriteLine("连接成功");
                //"连接成功";
            }
            catch (Exception x)
            {
                Console.WriteLine("连接异常");
                //异常
            }

            //var v = bl.GetStream();
            //byte[] sendData = Encoding.Default.GetBytes(“人生苦短，我用python”);
            //v.Write(sendData, 0, sendData.Length); //发送

            //服务器端
            BluetoothListener bluetoothListener = new BluetoothListener(mGUID);
            bluetoothListener.Start();//开始监听

            bl = bluetoothListener.AcceptBluetoothClient();//接收

            while (true)
            {
                Console.WriteLine("111111111");
                byte[] buffer = new byte[100];
                Stream peerStream = bl.GetStream();

                peerStream.Read(buffer, 0, buffer.Length);

                string data = Encoding.UTF8.GetString(buffer).ToString().Replace("\0", "");//去掉后面的\0字节
            }
            Console.ReadKey();
        }
    }

    class LanYa
    {
        public string blueName { get; set; } //蓝牙名字
        public BluetoothAddress blueAddress { get; set; } //蓝牙的唯一标识符
        public ClassOfDevice blueClassOfDevice { get; set; } //蓝牙是何种类型
        public bool IsBlueAuth { get; set; } //指定设备通过验证
        public bool IsBlueRemembered { get; set; } //记住设备
        public DateTime blueLastSeen { get; set; }
        public DateTime blueLastUsed { get; set; }
    }
}
