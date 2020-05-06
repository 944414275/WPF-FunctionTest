using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfByteTypeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string strMessage = "ff ff ff ff ff ff 12 02 30 00 20 ";
            byte[] byteSendMassge = strHexToToHexByte(strMessage);
            string strGetMassge = byteToHexStr(byteSendMassge);

            double d = 300;
            //string str = d.ToString("D4");
            string str = d.ToString().PadRight(4,'0');
            byte[] _byte = {0xFF,2};
            int int1 = 128021;
            int int2 = 0x80;
            try
            {
                byte value1 = (byte)int1;
                Console.WriteLine(value1);
            }
            catch (OverflowException)
            {
                Console.WriteLine("{0} is out of range of a byte.", int1);
            }

            double dbl2 = 3.997;
            try
            {
                byte value2 = (byte)dbl2;
                Console.WriteLine(value2);
            }
            catch (OverflowException)
            {
                Console.WriteLine("{0} is out of range of a byte.", dbl2);
            }
            // The example displays the following output:
            //       128
            //        3


            string string1 = "244";
            try
            {
                byte byte1 = Byte.Parse(string1);
                Console.WriteLine(byte1);
            }
            catch (OverflowException)
            {
                Console.WriteLine("'{0}' is out of range of a byte.", string1);
            }
            catch (FormatException)
            {
                Console.WriteLine("'{0}' is out of range of a byte.", string1);
            }

            string string2 = "F9";
            try
            {
                byte byte2 = Byte.Parse(string2,
                                        System.Globalization.NumberStyles.HexNumber);
                Console.WriteLine(byte2);
            }
            catch (OverflowException)
            {
                Console.WriteLine("'{0}' is out of range of a byte.", string2);
            }
            catch (FormatException)
            {
                Console.WriteLine("'{0}' is out of range of a byte.", string2);
            }
            // The example displays the following output:
            //       244
            //       249 
        }

        /// <summary>
        /// 16进制字节数组转换成16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }

        /// <summary>
        /// 16进制字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        private static byte[] strHexToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
    }
}
