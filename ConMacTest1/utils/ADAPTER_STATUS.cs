using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConMacTest1.utils
{
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
}
