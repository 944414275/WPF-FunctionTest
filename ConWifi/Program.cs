using System;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;

namespace ConWifi
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string strInput = "netsh wlan show hosted";
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();

            p.StandardInput.WriteLine(strInput + "&exit");
            p.StandardInput.AutoFlush = true;
            string strOuput = p.StandardOutput.ReadToEnd();
            strOuput.IndexOf("");
            p.WaitForExit();
            p.Close();
            Console.WriteLine(strOuput);
            Console.ReadKey();

            //string mac = "";
            //foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            //{

            //    if (nic.OperationalStatus == OperationalStatus.Up &&
            //       (!nic.Description.Contains("Virtual") && !nic.Description.Contains("Pseudo")))
            //    {
            //        if (nic.GetPhysicalAddress().ToString() != "")
            //        {
            //            mac = nic.GetPhysicalAddress().ToString();
            //        }
            //    }
            //}
            //Console.WriteLine(mac);

            //NetworkInterface[] nfaces = NetworkInterface.GetAllNetworkInterfaces();
            //var nface = nfaces.First();
            ////var nface = nfaces.First(x => x.Name == "WLAN 2");
            //if (nface == null)
            //{
            //    Console.WriteLine("WLAN2 - Wifi未连接."); 
            //    return;
            //}
            //var ipProperties = nface.GetIPProperties();
            //// 获取默认网关
            //var defualtGateway = ipProperties.GatewayAddresses[0];
            //Ping ping = new Ping();
            //var treplay = ping.SendPingAsync(defualtGateway.Address);
            //var replay = treplay.Result;
            //Console.WriteLine(replay?.Status == IPStatus.Success
            //                ? $"WLAN2 - Wifi已连接. [Ping {defualtGateway.Address} Status: {replay?.Status}]"
            //                : $"WLAN2 - Wifi未连接. [Ping {defualtGateway.Address} Status: {replay?.Status}]"); 
        }
    }
}
