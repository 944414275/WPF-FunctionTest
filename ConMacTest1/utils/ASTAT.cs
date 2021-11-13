using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConMacTest1.utils
{
    [StructLayout(LayoutKind.Auto)]
    public struct ASTAT
    {
        public ADAPTER_STATUS adapt;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)NCBCONST.NUM_NAMEBUF)]
        public NAME_BUFFER[] NameBuff;
    }
}
