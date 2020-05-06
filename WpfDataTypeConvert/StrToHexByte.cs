using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDataTypeConvert
{
    public class StrToHexByte
    { 
        /// <summary>
        /// 16进制byte[]转16进制字符串
        /// </summary>
        /// <param name="temp">传入字节数组</param>
        /// <param name="hexOutput">返回的字符串</param>
        /// <returns></returns>
        public static string hexByteToStr(byte[] temp)
        { 
            byte[] message1 = new byte[2] { 0x01, 0x02 };
            byte[] message2 = temp;
            string hexOutput = string.Empty;
            for (int i = 0; i < message1.Length; i++)
            {
                hexOutput += string.Format("{0:X2}", message1[i]) + " ";
            } 
            return hexOutput;
        }
        /// <summary>
        /// 16进制字符串转16进制byte[]
        /// </summary>
        /// <param name="hexString">传入字符串</param>
        /// <param name="returnBytes">返回字节数组</param>
        /// <returns></returns>
        public static byte[] strToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        #region 字符串和字节数组相互转换 
        /// <summary>
        /// 将字符串转换成16进制字节数组,并自动在字节数组后面加cs校验和
        /// </summary>
        /// <param name="_data">要转换的string</param>
        /// <param name="byteArray">转换之后的字节数组</param>
        /// <returns>为空则表示执行正确，否则，表示错误信息</returns>
        public static string ChangeHexStringToByteArray(string _data, out byte[] byteArray)
        {
            string sResult = "";
            byteArray = null;//转换后的字节数组
            if (_data == "")
            {
                sResult = "请不要传入空值。函数：ChangeHexStringToByteArray。";
            }
            else if (_data.Length % 2 != 0)
            {
                sResult = "请传入正确的十六进制字符串，字符串的个数必须是偶数个。函数：ChangeHexStringToByteArray。";
            }
            else
            {
                try
                {
                    _data = _data.Replace(" ", "");
                    int sendLength = _data.Length / 2;
                    byteArray = new byte[sendLength];//转换后的字节数组
                    string hexstring = "";
                    int k = 0;
                    for (int i = 0; i < _data.Length;)
                    {
                        hexstring = _data.Substring(i, 2);
                        int j;
                        j = int.Parse(hexstring, System.Globalization.NumberStyles.HexNumber);
                        byteArray[k] = (byte)j; 
                        i += 2; 
                        k++; 
                    } 
                } 
                catch (Exception ex) 
                { 
                    sResult = ex.Message; 
                } 
            } 
            return sResult; 
        } 
        
        /// <summary> 
        /// 将字节数组转换成十六进制字符串 
        /// </summary> 
        /// <param name="data">要转换的字节数组</param> 
        /// <param name="HexString">转换后的字符串</param> 
        /// <returns>为空则表示执行正确，否则，表示错误信息</returns> 
        public static string ChangeByteArrayToHexString(byte[] data, out string HexString) 
        { 
            string sResult = ""; 
            HexString = ""; 
            StringBuilder sb = new StringBuilder(data.Length * 3); 
            try 
            { 
                foreach (byte b in data) 
                    sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' ')); 
            } 
            catch (Exception ex) 
            { 
                sResult = "将字节数组转换成字符串时出错，函数：ChangeByteArrayToHexString。异常信息：" + ex.Message;
            } 
            HexString = sb.ToString().ToUpper(); 
            return sResult; 
        } 
        #endregion

        #region 计算校验和 
        /// <summary> 
        /// 对16进制数字的进行CRC16校验，然后把校验值返回 
        /// </summary> 
        /// <param name="str">要进行校验的数据</param>  
        /// <param name="CRC16Result">校验值（校验结果）</param> 
        /// <returns>为空则表示执行正确，否则，表示错误信息</returns> 
        public string GetCRC16ResultByHexString(string str, string CRC16Result) 
        { 
            ushort crcResult; 
            byte[] byArr; 
            string sResult = ""; 
            CRC16Result = ""; 
            try 
            { 
                sResult = ChangeHexStringToByteArray(str, out byArr); 
                if ((sResult == "") && (byArr != null)) 
                { 
                    sResult = CalculateCRC(byArr, byArr.Length, out crcResult); 
                    if (sResult == "") 
                    { 
                        ushort h = (byte)(crcResult & 0xFF); 
                        ushort l = (byte)((crcResult & 0xFF00) >> 8); 
                        string height = Convert.ToString(h, 16); 
                        string low = Convert.ToString(l, 16); 
                        if (height.Length == 1) 
                            height = height.Insert(0, "0"); 
                        if (low.Length == 1) 
                            low = low.Insert(0, "0"); 
                        CRC16Result = low.ToUpper() + height.ToUpper(); 
                    } 
                } 
            } 
            catch (Exception ex) 
            { 
                sResult += ex.Message; 
            } 
            return sResult; 
        } 
        /// <summary> 
        /// crc16算法 
        /// </summary> 
        /// <param name="pByte">要校验的字节数组</param> 
        /// <param name="nNumberOfBytes">字节个数</param> 
        /// <param name="pChecksum">校验结果</param> 
        /// <returns>为空则表示执行正确，否则，表示错误信息</returns> 
        public static string CalculateCRC(byte[] pByte, int nNumberOfBytes, out ushort pChecksum)
        { 
            int nBit; 
            ushort nShiftedBit; 
            pChecksum = 0xFFFF; 
            string sResult = ""; 
            if (nNumberOfBytes > pByte.Length) 
            { 
                sResult = "传入的参数nNumberOfBytes（字节个数）大于pByte（要校验的字节数组）的长度";  
            } 
            else 
            { 
                try 
                { 
                    for (int nByte = 0; nByte < nNumberOfBytes; nByte++) 

                    { 
                        pChecksum ^= pByte[nByte]; 
                        for (nBit = 0; nBit < 8; nBit++) 
                        { 
                            if ((pChecksum & 0x1) == 1) 
                            { 
                                nShiftedBit = 1; 
                            } 
                            else 
                            { 
                                nShiftedBit = 0; 
                            } 
                            pChecksum >>= 1; 
                            if (nShiftedBit != 0) 
                            { 
                                pChecksum ^= 0xA001; 
                            } 
                        } 
                    } 
                } 
                catch (Exception ex) 
                { 
                    sResult = ex.Message; 
                } 
            } 
            return sResult; 
        } 
        /// <summary> 
        /// 计算累加校验和 
        /// </summary> 
        /// <param name="SendByteArray">要校验的字节数组</param> 
        /// <returns>对字节数组进行校验所得到的校验和</returns> 
        public static byte GetSumCS(byte[] SendByteArray) 
        { 
            byte rst = 0x00; 
            //计算累加和 
            for (int j = 2; j < SendByteArray.Length; j++) 
            { 
                unchecked 
                { 
                    rst += SendByteArray[j]; 
                } 
            } 
            //累加和计算结束 
            return rst; 
        } 
        #endregion
    }
}
