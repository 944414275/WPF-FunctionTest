using System;
using System.Runtime.InteropServices;

namespace ConMacTest1
{
    class Program
    {
        public enum NCBCONST
        {
            NCBNAMSZ = 16,
            MAX_LANA = 254,
            NCBENUM = 0x37,
            NRC_GOODRET = 0x00,
            NCBRESET = 0x32,
            NCBASTAT = 0x33,
            NUM_NAMEBUF = 30,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ADAPTER_STATUS
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] adapter_address;
            public byte rev_major;
            public byte reserved0;
            public byte adapter_type;
            public byte rev_minor;
            public ushort duration;
            public ushort frmr_recv;
            public ushort frmr_xmit;
            public ushort iframe_recv_err;
            public ushort xmit_aborts;
            public uint xmit_success;
            public uint recv_success;
            public ushort iframe_xmit_err;
            public ushort recv_buff_unavail;
            public ushort t1_timeouts;
            public ushort ti_timeouts;
            public uint reserved1;
            public ushort free_ncbs;
            public ushort max_cfg_ncbs;
            public ushort max_ncbs;
            public ushort xmit_buf_unavail;
            public ushort max_dgram_size;
            public ushort pending_sess;
            public ushort max_cfg_sess;
            public ushort max_sess;
            public ushort max_sess_pkt_size;
            public ushort name_count;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NAME_BUFFER
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)NCBCONST.NCBNAMSZ)]
            public byte[] name;
            public byte name_num;
            public byte name_flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NCB
        {
            public byte ncb_command;
            public byte ncb_retcode;
            public byte ncb_lsn;
            public byte ncb_num;
            public IntPtr ncb_buffer;
            public ushort ncb_length;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)NCBCONST.NCBNAMSZ)]
            public byte[] ncb_callname;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)NCBCONST.NCBNAMSZ)]
            public byte[] ncb_name;
            public byte ncb_rto;
            public byte ncb_sto;
            public IntPtr ncb_post;
            public byte ncb_lana_num;
            public byte ncb_cmd_cplt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] ncb_reserve;
            public IntPtr ncb_event;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct LANA_ENUM
        {
            public byte length;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)NCBCONST.MAX_LANA)]
            public byte[] lana;
        }

        [StructLayout(LayoutKind.Auto)]
        public struct ASTAT
        {
            public ADAPTER_STATUS adapt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)NCBCONST.NUM_NAMEBUF)]
            public NAME_BUFFER[] NameBuff;
        }
        public class Win32API
        {
            [DllImport("NETAPI32.DLL")]
            public static extern char Netbios(ref NCB ncb);
        }

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

         
        static void Main(string[] args)
        {
            MoreMethodGetMAC moreMethodGetMAC = new MoreMethodGetMAC();
            string macStr = moreMethodGetMAC.GetMacAddress();
            Console.WriteLine(macStr);
            Console.ReadLine();
        } 
    }
}
