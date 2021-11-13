using System;
using System.Collections.Generic; 
using Windows.Devices.Bluetooth; 
using Windows.Devices.Bluetooth.GenericAttributeProfile;

namespace ConBle
{
    class Program
    {
        private static BleCore bleCore = null;

        private static List<GattCharacteristic> characteristics = new List<GattCharacteristic>();
        static void Main(string[] args)
        {
            bleCore = new BleCore();
            bleCore.DeviceWatcherChanged += DeviceWatcherChanged;
            bleCore.CharacteristicAdded += CharacteristicAdded;
            bleCore.CharacteristicFinish += CharacteristicFinish;
            bleCore.Recdate += Recdata;
            bleCore.StartBleDeviceWatcher();

            Console.ReadKey(true);

            bleCore.Dispose();
            bleCore = null;

            // Start the program
            //var program = new Program();

            // Close on key press
            Console.ReadLine();
        }

        private static void CharacteristicFinish(int size)
        {
            if (size <= 0)
            {
                Console.WriteLine("设备未连上");
                return;
            }
        }

        private static void Recdata(GattCharacteristic sender, byte[] data)
        {
            string str = BitConverter.ToString(data);
            Console.WriteLine(sender.Uuid + "             " + str);
        }

        private static void CharacteristicAdded(GattCharacteristic gatt)
        {
            Console.WriteLine(
                "handle:[0x{0}]  char properties:[{1}]  UUID:[{2}]",
                gatt.AttributeHandle.ToString("X4"),
                gatt.CharacteristicProperties.ToString(),
                gatt.Uuid);
            characteristics.Add(gatt);
        }

        private static void DeviceWatcherChanged(BluetoothLEDevice currentDevice)
        {
            byte[] _Bytes1 = BitConverter.GetBytes(currentDevice.BluetoothAddress);
            Array.Reverse(_Bytes1);
            string address = BitConverter.ToString(_Bytes1, 2, 6).Replace('-', ':').ToLower();
            Console.WriteLine("发现设备：<" + currentDevice.Name + ">  address:<" + address + ">");

            //指定一个对象，使用下面方法去连接设备
            //ConnectDevice(currentDevice);
        }

        //private static void ConnectDevice(BluetoothLEDevice Device)
        //{
        //    characteristics.Clear();
        //    bleCore.StopBleDeviceWatcher();
        //    bleCore.StartMatching(Device);
        //    bleCore.FindService();
        //}

        //public Program()
        //{
        //    // Create Bluetooth Listener
        //    var watcher = new BluetoothLEAdvertisementWatcher();

        //    watcher.ScanningMode = BluetoothLEScanningMode.Active;

        //    // Only activate the watcher when we're recieving values >= -80
        //    watcher.SignalStrengthFilter.InRangeThresholdInDBm = -80;

        //    // Stop watching if the value drops below -90 (user walked away)
        //    watcher.SignalStrengthFilter.OutOfRangeThresholdInDBm = -90;

        //    // Register callback for when we see an advertisements
        //    watcher.Received += OnAdvertisementReceived;

        //    // Wait 5 seconds to make sure the device is really out of range
        //    watcher.SignalStrengthFilter.OutOfRangeTimeout = TimeSpan.FromMilliseconds(5000);
        //    watcher.SignalStrengthFilter.SamplingInterval = TimeSpan.FromMilliseconds(2000);

        //    // Starting watching for advertisements
        //    watcher.Start();
        //}

        //private void OnAdvertisementReceived(BluetoothLEAdvertisementWatcher watcher, BluetoothLEAdvertisementReceivedEventArgs eventArgs)
        //{
        //    // Tell the user we see an advertisement and print some properties
        //    Console.WriteLine(String.Format("Advertisement:"));
        //    Console.WriteLine(String.Format("  BT_ADDR: {0}", eventArgs.BluetoothAddress));
        //    Console.WriteLine(String.Format("  FR_NAME: {0}", eventArgs.Advertisement.LocalName));
        //    Console.WriteLine();
        //}
    }
}
