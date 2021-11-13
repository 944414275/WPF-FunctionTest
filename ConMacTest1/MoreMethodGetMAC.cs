using System; 
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text; 
using static ConMacTest1.Program;

namespace ConMacTest1
{
    public class MoreMethodGetMAC
    {

        public string GetMacAddress()
        {
            if (!GetMacAddressByWMI().Equals(""))
            {
                return GetMacAddressByWMI();
            }
            //if (!GetMacAddressByNetBios().Equals(""))
            //{
            //    return GetMacAddressByNetBios();
            //}
            //if (!GetMacAddressBySendARP().Equals(""))
            //{
            //    return GetMacAddressBySendARP();
            //}
            //if (!GetMacAddressByAdapter().Equals(""))
            //{
            //    return GetMacAddressByAdapter();
            //}
            //if (!GetMacAddressByDos().Equals(""))
            //{
            //    return GetMacAddressByDos();
            //}
            return "";
        }

        /// <summary>
        /// 通过WMI获得电脑的mac地址
        /// </summary>
        /// <returns></returns>
        public string GetMacAddressByWMI()
        {
            string mac = "";
            try
            {
                //ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 'TRUE'");
                ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapter WHERE ((MACAddress Is Not NULL)");
                //SELECT * FROM Win32_NetworkAdapter WHERE ((MACAddress Is Not NULL) AND (Manufacturer <> 'Microsoft'))
                ManagementObjectCollection queryCollection = query.Get();

                foreach (ManagementObject mo in queryCollection)
                {
                    //if (mo["IPEnabled"].ToString() == "True")
                    //{
                        mac = mo["MacAddress"].ToString();
                        //break;
                    //}

                }
            }
            catch (Exception ex)
            {
            }
            return mac;
        }

        [DllImport("Iphlpapi.dll")]
        static extern int SendARP(Int32 DestIP, Int32 SrcIP, ref Int64 MacAddr, ref Int32 PhyAddrLen);
        /// <summary>
        /// SendArp获取MAC地址
        /// </summary>
        /// <returns></returns>
        public string GetMacAddressBySendARP()
        {
            StringBuilder strReturn = new StringBuilder();
            try
            {
                System.Net.IPHostEntry Tempaddr = (System.Net.IPHostEntry)Dns.GetHostByName(Dns.GetHostName());
                System.Net.IPAddress[] TempAd = Tempaddr.AddressList;
                Int32 remote = (int)TempAd[0].Address;
                Int64 macinfo = new Int64();
                Int32 length = 6;
                SendARP(remote, 0, ref macinfo, ref length);

                string temp = System.Convert.ToString(macinfo, 16).PadLeft(12, '0').ToUpper();

                int x = 12;
                for (int i = 0; i < 6; i++)
                {
                    if (i == 5) { strReturn.Append(temp.Substring(x - 2, 2)); }
                    else { strReturn.Append(temp.Substring(x - 2, 2) + ":"); }
                    x -= 2;
                }

                return strReturn.ToString();
            }
            catch
            {
                return "";
            }
        }
         
        [DllImport("Iphlpapi.dll")]
        public static extern uint GetAdaptersAddresses(uint Family, uint flags, IntPtr Reserved,
            IntPtr PAdaptersAddresses, ref uint pOutBufLen);

        /// <summary>
        /// 通过适配器信息获取MAC地址
        /// </summary>
        /// <returns></returns>
        //public string GetMacAddressByAdapter()
        //{
        //    string macAddress = "";
        //    try
        //    {
        //        IntPtr PAdaptersAddresses = new IntPtr();

        //        uint pOutLen = 100;
        //        PAdaptersAddresses = Marshal.AllocHGlobal(100);

        //        uint ret =
        //            GetAdaptersAddresses(0, 0, (IntPtr)0, PAdaptersAddresses, ref pOutLen);

        //        if (ret == 111)
        //        {
        //            Marshal.FreeHGlobal(PAdaptersAddresses);
        //            PAdaptersAddresses = Marshal.AllocHGlobal((int)pOutLen);
        //            ret = GetAdaptersAddresses(0, 0, (IntPtr)0, PAdaptersAddresses, ref pOutLen);
        //        }

        //        IP_Adapter_Addresses adds = new IP_Adapter_Addresses();

        //        IntPtr pTemp = PAdaptersAddresses;

        //        while (pTemp != (IntPtr)0)
        //        {
        //            Marshal.PtrToStructure(pTemp, adds);
        //            string adapterName = Marshal.PtrToStringAnsi(adds.AdapterName);
        //            string FriendlyName = Marshal.PtrToStringAuto(adds.FriendlyName);
        //            string tmpString = string.Empty;

        //            for (int i = 0; i < 6; i++)
        //            {
        //                tmpString += string.Format("{0:X2}", adds.PhysicalAddress[i]);

        //                if (i < 5)
        //                {
        //                    tmpString += ":";
        //                }
        //            }


        //            RegistryKey theLocalMachine = Registry.LocalMachine;

        //            RegistryKey theSystem
        //                = theLocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces");
        //            RegistryKey theInterfaceKey = theSystem.OpenSubKey(adapterName);

        //            if (theInterfaceKey != null)
        //            {
        //                macAddress = tmpString;
        //                break;
        //            }

        //            pTemp = adds.Next;
        //        }
        //    }
        //    catch
        //    { }
        //    return macAddress;
        //}

        /// <summary>
        /// 通过NetBios获取MAC地址
        /// </summary>
        /// <returns></returns>
        public string GetMacAddressByNetBios()
        {
            string macAddress = "";
            try
            {
                string addr = "";
                int cb;
                ASTAT adapter;
                NCB Ncb = new NCB();
                char uRetCode;
                LANA_ENUM lenum;

                Ncb.ncb_command = (byte)NCBCONST.NCBENUM;
                cb = Marshal.SizeOf(typeof(LANA_ENUM));
                Ncb.ncb_buffer = Marshal.AllocHGlobal(cb);
                Ncb.ncb_length = (ushort)cb;
                uRetCode = Win32API.Netbios(ref Ncb);
                lenum = (LANA_ENUM)Marshal.PtrToStructure(Ncb.ncb_buffer, typeof(LANA_ENUM));
                Marshal.FreeHGlobal(Ncb.ncb_buffer);
                if (uRetCode != (short)NCBCONST.NRC_GOODRET)
                    return "";

                for (int i = 0; i < lenum.length; i++)
                {
                    Ncb.ncb_command = (byte)NCBCONST.NCBRESET;
                    Ncb.ncb_lana_num = lenum.lana[i];
                    uRetCode = Win32API.Netbios(ref Ncb);
                    if (uRetCode != (short)NCBCONST.NRC_GOODRET)
                        return "";

                    Ncb.ncb_command = (byte)NCBCONST.NCBASTAT;
                    Ncb.ncb_lana_num = lenum.lana[i];
                    Ncb.ncb_callname[0] = (byte)'*';
                    cb = Marshal.SizeOf(typeof(ADAPTER_STATUS)) + Marshal.SizeOf(typeof(NAME_BUFFER)) * (int)NCBCONST.NUM_NAMEBUF;
                    Ncb.ncb_buffer = Marshal.AllocHGlobal(cb);
                    Ncb.ncb_length = (ushort)cb;
                    uRetCode = Win32API.Netbios(ref Ncb);
                    adapter.adapt = (ADAPTER_STATUS)Marshal.PtrToStructure(Ncb.ncb_buffer, typeof(ADAPTER_STATUS));
                    Marshal.FreeHGlobal(Ncb.ncb_buffer);

                    if (uRetCode == (short)NCBCONST.NRC_GOODRET)
                    {
                        if (i > 0)
                            addr += ":";
                        addr = string.Format("{0,2:X}:{1,2:X}:{2,2:X}:{3,2:X}:{4,2:X}:{5,2:X}",
                              adapter.adapt.adapter_address[0],
                              adapter.adapt.adapter_address[1],
                              adapter.adapt.adapter_address[2],
                              adapter.adapt.adapter_address[3],
                              adapter.adapt.adapter_address[4],
                              adapter.adapt.adapter_address[5]);
                    }
                }
                macAddress = addr.Replace(' ', '0'); 
            }
            catch {} 
            return macAddress;
        }

        /// <summary>
        /// 通过DOS命令获得MAC地址
        /// </summary>
        /// <returns></returns>
        public string GetMacAddressByDos()
        {
            string macAddress = "";
            Process p = null;
            StreamReader reader = null;
            try
            {
                ProcessStartInfo start = new ProcessStartInfo("cmd.exe");

                start.FileName = "ipconfig";
                start.Arguments = "/all";

                start.CreateNoWindow = true;

                start.RedirectStandardOutput = true;

                start.RedirectStandardInput = true;

                start.UseShellExecute = false;

                p = Process.Start(start);

                reader = p.StandardOutput;

                string line = reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    if (line.ToLower().IndexOf("physical address") > 0 || line.ToLower().IndexOf("物理地址") > 0)
                    {
                        int index = line.IndexOf(":");
                        index += 2;
                        macAddress = line.Substring(index);
                        macAddress = macAddress.Replace('-', ':');
                        break;
                    }
                    line = reader.ReadLine();
                }
            }
            catch
            {

            }
            finally
            {
                if (p != null)
                {
                    p.WaitForExit();
                    p.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return macAddress;
        }

        /// <summary>
        /// 通过网络适配器获取MAC地址
        /// </summary>
        /// <returns></returns>
        public string GetMacAddressByNetworkInformation()
        {
            string macAddress = "";
            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface adapter in nics)
                {
                    if (!adapter.GetPhysicalAddress().ToString().Equals(""))
                    {
                        macAddress = adapter.GetPhysicalAddress().ToString();
                        for (int i = 1; i < 6; i++)
                        {
                            macAddress = macAddress.Insert(3 * i - 1, ":");
                        }
                        break;
                    }
                }

            }
            catch
            {
            }
            return macAddress;
        }
    }
}
