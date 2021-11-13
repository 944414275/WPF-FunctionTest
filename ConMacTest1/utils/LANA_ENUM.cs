using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConMacTest1.utils
{
    [StructLayout(LayoutKind.Sequential)]
    public struct LANA_ENUM
    {
        public byte length;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)NCBCONST.MAX_LANA)]
        public byte[] lana;
    }
}
