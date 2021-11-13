using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConMacTest1.utils
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class IP_Adapter_Addresses
    {
        public uint Length;
        public uint IfIndex;
        public IntPtr Next;

        public IntPtr AdapterName;
        public IntPtr FirstUnicastAddress;
        public IntPtr FirstAnycastAddress;
        public IntPtr FirstMulticastAddress;
        public IntPtr FirstDnsServerAddress;

        public IntPtr DnsSuffix;
        public IntPtr Description;

        public IntPtr FriendlyName;

        [MarshalAs(UnmanagedType.ByValArray,
             SizeConst = 8)]
        public Byte[] PhysicalAddress;

        public uint PhysicalAddressLength;
        public uint flags;
        public uint Mtu;
        public uint IfType;

        public uint OperStatus;

        public uint Ipv6IfIndex;
        public uint ZoneIndices;

        public IntPtr FirstPrefix;
    }
}
