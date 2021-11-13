using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConMacTest1.utils
{
    [StructLayout(LayoutKind.Sequential)]
    public struct NAME_BUFFER
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)NCBCONST.NCBNAMSZ)]
        public byte[] name;
        public byte name_num;
        public byte name_flags;
    }
}
