using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConMacTest1.utils
{
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
}
