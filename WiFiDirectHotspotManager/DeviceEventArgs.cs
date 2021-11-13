using System;
using Windows.Devices.WiFiDirect;

namespace WiFiDirectHotspotManager
{
    public class DeviceEventArgs : EventArgs
    {
        public DeviceEventArgs(WiFiDirectDevice device, string message)
        {
            Device = device;
            Message = message;
        }

        public WiFiDirectDevice Device { get; }

        public string Message { get; }
    }
}
