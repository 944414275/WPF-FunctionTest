using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConMacTest1.utils
{
    public class Win32API
    {
        [DllImport("NETAPI32.DLL")]
        public static extern char Netbios(ref NCB ncb);
    }
}
