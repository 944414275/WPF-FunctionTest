using System;
using System.Collections.Generic;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Foundation;
using Windows.Security.Cryptography;

namespace BleCoreLib
{
    public class BleCore
    {
        private bool asyncLock = false;

        /// <summary>
        /// 当前连接的服务
        /// </summary>
        public GattDeviceService CurrentService { get; private set; }

        /// <summary>
        /// 当前连接的蓝牙设备
        /// </summary>
        public BluetoothLEDevice CurrentDevice { get; private set; }

        /// <summary>
        /// 写特征对象
        /// </summary>
        public GattCharacteristic CurrentWriteCharacteristic { get; private set; }

        /// <summary>
        /// 通知特征对象
        /// </summary>
        public GattCharacteristic CurrentNotifyCharacteristic { get; private set; }

        /// <summary>
        /// 存储检测到的设备
        /// </summary>
        public List<BluetoothLEDevice> DeviceList { get; private set; }

        /// <summary>
        /// 特性通知类型通知启用
        /// </summary>
        private const GattClientCharacteristicConfigurationDescriptorValue CHARACTERISTIC_NOTIFICATION_TYPE = GattClientCharacteristicConfigurationDescriptorValue.Notify;

        /// <summary>
        /// 定义搜索蓝牙设备委托
        /// </summary>
        public delegate void DeviceWatcherChangedEvent(BluetoothLEDevice bluetoothLEDevice);

        /// <summary>
        /// 搜索蓝牙事件
        /// </summary>
        public event DeviceWatcherChangedEvent DeviceWatcherChanged;

        /// <summary>
        /// 获取服务委托
        /// </summary>
        public delegate void CharacteristicFinishEvent(int size);

        /// <summary>
        /// 获取服务事件
        /// </summary>
        public event CharacteristicFinishEvent CharacteristicFinish;

        /// <summary>
        /// 获取特征委托
        /// </summary>
        public delegate void CharacteristicAddedEvent(GattCharacteristic gattCharacteristic);

        /// <summary>
        /// 获取特征事件
        /// </summary>
        public event CharacteristicAddedEvent CharacteristicAdded;

        /// <summary>
        /// 接受数据委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        public delegate void RecDataEvent(GattCharacteristic sender, byte[] data);

        /// <summary>
        /// 接受数据事件
        /// </summary>
        public event RecDataEvent Recdate;

        /// <summary>
        /// 当前连接的蓝牙Mac
        /// </summary>
        private string CurrentDeviceMAC { get; set; }

        private BluetoothLEAdvertisementWatcher Watcher = null;

        public BleCore()
        {
            DeviceList = new List<BluetoothLEDevice>();
        }

        /// <summary>
        /// 搜索蓝牙设备
        /// </summary>
        public void StartBleDeviceWatcher()
        {
            Watcher = new BluetoothLEAdvertisementWatcher();

            Watcher.ScanningMode = BluetoothLEScanningMode.Active;

            // only activate the watcher when we're recieving values >= -80
            Watcher.SignalStrengthFilter.InRangeThresholdInDBm = -80;

            // stop watching if the value drops below -90 (user walked away)
            Watcher.SignalStrengthFilter.OutOfRangeThresholdInDBm = -90;

            // register callback for when we see an advertisements
            Watcher.Received += OnAdvertisementReceived;

            // wait 5 seconds to make sure the device is really out of range
            Watcher.SignalStrengthFilter.OutOfRangeTimeout = TimeSpan.FromMilliseconds(5000);
            Watcher.SignalStrengthFilter.SamplingInterval = TimeSpan.FromMilliseconds(2000);

            // starting watching for advertisements
            Watcher.Start();

            Console.WriteLine("自动发现设备中..");
        }

        /// <summary>
        /// 停止搜索蓝牙
        /// </summary>
        public void StopBleDeviceWatcher()
        {
            if (Watcher != null)
                this.Watcher.Stop();
        }

        /// <summary>
        /// 主动断开连接
        /// </summary>
        /// <returns></returns>
        public void Dispose()
        {
            CurrentDeviceMAC = null;
            CurrentService?.Dispose();
            CurrentDevice?.Dispose();
            CurrentDevice = null;
            CurrentService = null;
            CurrentWriteCharacteristic = null;
            CurrentNotifyCharacteristic = null;
            Console.WriteLine("主动断开连接");
        }

        /// <summary>
        /// 匹配
        /// </summary>
        /// <param name="Device"></param>
        public void StartMatching(BluetoothLEDevice Device)
        {
            this.CurrentDevice = Device;
        }

        /// <summary>
        /// 发送数据接口
        /// </summary>
        /// <returns></returns>
        public void Write(byte[] data)
        {
            if (CurrentWriteCharacteristic != null)
            {
                CurrentWriteCharacteristic.WriteValueAsync(CryptographicBuffer.CreateFromByteArray(data), GattWriteOption.WriteWithResponse).Completed = (asyncInfo, asyncStatus) =>
                {
                    if (asyncStatus == AsyncStatus.Completed)
                    {
                        GattCommunicationStatus a = asyncInfo.GetResults();
                        Console.WriteLine("发送数据：" + BitConverter.ToString(data) + " State : " + a);
                    }
                };
            }

        }

        /// 获取蓝牙服务
        /// </summary>
        public void FindService()
        {
            this.CurrentDevice.GetGattServicesAsync().Completed = (asyncInfo, asyncStatus) =>
            {
                if (asyncStatus == AsyncStatus.Completed)
                {
                    var services = asyncInfo.GetResults().Services;
                    Console.WriteLine("GattServices size=" + services.Count);
                    foreach (GattDeviceService ser in services)
                    {
                        FindCharacteristic(ser);
                    }
                    CharacteristicFinish?.Invoke(services.Count);
                }
            };

        }

        /// <summary>
        /// 按MAC地址直接组装设备ID查找设备
        /// </summary>
        public void SelectDeviceFromIdAsync(string MAC)
        {
            CurrentDeviceMAC = MAC;
            CurrentDevice = null;
            BluetoothAdapter.GetDefaultAsync().Completed = (asyncInfo, asyncStatus) =>
            {
                if (asyncStatus == AsyncStatus.Completed)
                {
                    BluetoothAdapter mBluetoothAdapter = asyncInfo.GetResults();
                    byte[] _Bytes1 = BitConverter.GetBytes(mBluetoothAdapter.BluetoothAddress);//ulong转换为byte数组
                    Array.Reverse(_Bytes1);
                    string macAddress = BitConverter.ToString(_Bytes1, 2, 6).Replace('-', ':').ToLower();
                    string Id = "BluetoothLE#BluetoothLE" + macAddress + "-" + MAC;
                    Matching(Id);
                }
            };
        }

        /// <summary>
        /// 获取操作
        /// </summary>
        /// <returns></returns>
        public void SetOpteron(GattCharacteristic gattCharacteristic)
        {
            byte[] _Bytes1 = BitConverter.GetBytes(this.CurrentDevice.BluetoothAddress);
            Array.Reverse(_Bytes1);
            this.CurrentDeviceMAC = BitConverter.ToString(_Bytes1, 2, 6).Replace('-', ':').ToLower();

            string msg = "正在连接设备<" + this.CurrentDeviceMAC + ">..";
            Console.WriteLine(msg);

            if (gattCharacteristic.CharacteristicProperties == GattCharacteristicProperties.Write)
            {
                this.CurrentWriteCharacteristic = gattCharacteristic;
            }
            if (gattCharacteristic.CharacteristicProperties == GattCharacteristicProperties.Notify)
            {
                this.CurrentNotifyCharacteristic = gattCharacteristic;
            }
            if ((uint)gattCharacteristic.CharacteristicProperties == 26)
            {

            }

            if (gattCharacteristic.CharacteristicProperties == (GattCharacteristicProperties.Write | GattCharacteristicProperties.Notify))
            {
                this.CurrentWriteCharacteristic = gattCharacteristic;
                this.CurrentNotifyCharacteristic = gattCharacteristic;
                this.CurrentNotifyCharacteristic.ProtectionLevel = GattProtectionLevel.Plain;
                this.CurrentNotifyCharacteristic.ValueChanged += Characteristic_ValueChanged;
                this.CurrentDevice.ConnectionStatusChanged += this.CurrentDevice_ConnectionStatusChanged;
                this.EnableNotifications(CurrentNotifyCharacteristic);
            }

        }

        private void OnAdvertisementReceived(BluetoothLEAdvertisementWatcher watcher, BluetoothLEAdvertisementReceivedEventArgs eventArgs)
        {
            BluetoothLEDevice.FromBluetoothAddressAsync(eventArgs.BluetoothAddress).Completed = (asyncInfo, asyncStatus) =>
            {
                if (asyncStatus == AsyncStatus.Completed)
                {
                    if (asyncInfo.GetResults() == null)
                    {
                        //Console.WriteLine("没有得到结果集");
                    }
                    else
                    {
                        BluetoothLEDevice currentDevice = asyncInfo.GetResults();

                        if (DeviceList.FindIndex((x) => { return x.Name.Equals(currentDevice.Name); }) < 0)
                        {
                            this.DeviceList.Add(currentDevice);
                            DeviceWatcherChanged?.Invoke(currentDevice);
                        }

                    }

                }
            };
        }

        /// <summary>
        /// 获取特性
        /// </summary>
        private void FindCharacteristic(GattDeviceService gattDeviceService)
        {
            this.CurrentService = gattDeviceService;
            this.CurrentService.GetCharacteristicsAsync().Completed = (asyncInfo, asyncStatus) =>
            {
                if (asyncStatus == AsyncStatus.Completed)
                {
                    var services = asyncInfo.GetResults().Characteristics;
                    foreach (var c in services)
                    {
                        this.CharacteristicAdded?.Invoke(c);
                    }

                }
            };
        }

        /// <summary>
        /// 搜索到的蓝牙设备
        /// </summary>
        /// <returns></returns>
        private void Matching(string Id)
        {
            try
            {
                BluetoothLEDevice.FromIdAsync(Id).Completed = (asyncInfo, asyncStatus) =>
                {
                    if (asyncStatus == AsyncStatus.Completed)
                    {
                        BluetoothLEDevice bleDevice = asyncInfo.GetResults();
                        this.DeviceList.Add(bleDevice);
                        Console.WriteLine(bleDevice);
                    }

                    if (asyncStatus == AsyncStatus.Started)
                    {
                        Console.WriteLine(asyncStatus.ToString());
                    }
                    if (asyncStatus == AsyncStatus.Canceled)
                    {
                        Console.WriteLine(asyncStatus.ToString());
                    }
                    if (asyncStatus == AsyncStatus.Error)
                    {
                        Console.WriteLine(asyncStatus.ToString());
                    }
                };
            }
            catch (Exception e)
            {
                string msg = "没有发现设备" + e.ToString();
                Console.WriteLine(msg);
                this.StartBleDeviceWatcher();
            }
        }

        private void CurrentDevice_ConnectionStatusChanged(BluetoothLEDevice sender, object args)
        {
            if (sender.ConnectionStatus == BluetoothConnectionStatus.Disconnected && CurrentDeviceMAC != null)
            {
                if (!asyncLock)
                {
                    asyncLock = true;
                    Console.WriteLine("设备已断开");
                    //this.CurrentDevice?.Dispose();
                    //this.CurrentDevice = null;
                    //CurrentService = null;
                    //CurrentWriteCharacteristic = null;
                    //CurrentNotifyCharacteristic = null;
                    //SelectDeviceFromIdAsync(CurrentDeviceMAC);
                }
            }
            else
            {
                if (!asyncLock)
                {
                    asyncLock = true;
                    Console.WriteLine("设备已连接");
                }
            }
        }

        /// <summary>
        /// 设置特征对象为接收通知对象
        /// </summary>
        /// <param name="characteristic"></param>
        /// <returns></returns>
        private void EnableNotifications(GattCharacteristic characteristic)
        {
            Console.WriteLine("收通知对象=" + CurrentDevice.Name + ":" + CurrentDevice.ConnectionStatus);
            characteristic.WriteClientCharacteristicConfigurationDescriptorAsync(CHARACTERISTIC_NOTIFICATION_TYPE).Completed = (asyncInfo, asyncStatus) =>
            {
                if (asyncStatus == AsyncStatus.Completed)
                {
                    GattCommunicationStatus status = asyncInfo.GetResults();
                    if (status == GattCommunicationStatus.Unreachable)
                    {
                        Console.WriteLine("设备不可用");
                        if (CurrentNotifyCharacteristic != null && !asyncLock)
                        {
                            this.EnableNotifications(CurrentNotifyCharacteristic);
                        }
                        return;
                    }
                    asyncLock = false;
                    Console.WriteLine("设备连接状态" + status);
                }
            };
        }

        /// <summary>
        /// 接受到蓝牙数据
        /// </summary>
        private void Characteristic_ValueChanged(GattCharacteristic sender, GattValueChangedEventArgs args)
        {
            byte[] data;
            CryptographicBuffer.CopyToByteArray(args.CharacteristicValue, out data);
            Recdate?.Invoke(sender, data);
        }

    }
}
