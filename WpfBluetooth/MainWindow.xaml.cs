using System.Windows;
using InTheHand.Net.Sockets;
using System.Threading;
using System;
using System.Text;

namespace WpfBluetooth
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BluetoothClient bluetoothClient;
        BluetoothListener bluetoothListener;
        Thread listenThread;
        bool isConnected;

        public MainWindow()
        {
            InitializeComponent();
            listenThread = new Thread(ReceiveData);
            listenThread.Start();
        }

        private void ReceiveData()
        {
            try
            {
                Guid mGUID = Guid.Parse("00001101-0000-1000-8000-00805F9B34FB");
                bluetoothListener = new BluetoothListener(mGUID);
                bluetoothListener.Start();
                bluetoothClient = bluetoothListener.AcceptBluetoothClient();
                isConnected = true;
            }
            catch (Exception)
            {
                isConnected = false;
            }
            while (isConnected)
            {
                string receive = string.Empty;
                if (bluetoothClient == null)
                {
                    break;
                }
                try
                {
                    var peerStream = bluetoothClient.GetStream();
                    byte[] buffer = new byte[6];
                    peerStream.Read(buffer, 0, 6);
                    receive = Encoding.UTF8.GetString(buffer).ToString();
                }
                catch (System.Exception)
                {
                }
                Thread.Sleep(100);
            }
        } 
    }
}
