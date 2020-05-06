using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransmissionSW.Common.Model
{
    public class DataConvert
    {
        #region DataInvertion
        
        #region byteToString
        /// <summary>
        /// 把字节型转换成十六进制字符串
        /// </summary>
        /// <param name="InBytes"></param>
        /// <returns></returns>
        public static string ByteToString(byte[] InBytes)
        {
            string StringOut = "";
            foreach (byte InByte in InBytes)
            {
                StringOut = StringOut + String.Format("{0:X2} ", InByte);
            }
            return StringOut;
        }
         
        /// <summary>
        /// 字节数组转16进制字符串
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
        /// 字节转16进制字符串
        /// </summary>
        /// <param name="bValues"></param>
        /// <returns></returns>
        public static string bytes2StringHex(byte[] bValues)
        {
            if (bValues == null)
                return "";
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bValues)
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
        #endregion


        #region StringToByte
        /// <summary>
        /// 普通字符串转换成16进制字节型(方法1)
        /// </summary>
        /// <param name="InString">普通字符串</param>
        /// <returns></returns>
        public byte[] StringToByte(string InString)
        {
            string[] ByteStrings;
            //var _char = " ".ToCharArray();
            ByteStrings = InString.Split(" ".ToCharArray());
            byte[] ByteOut;
            ByteOut = new byte[ByteStrings.Length];
            for (int i = 0; i <= ByteStrings.Length - 1; i++)
            {
                //ByteOut[i] = System.Text.Encoding.ASCII.GetBytes(ByteStrings[i]);
                ByteOut[i] = Byte.Parse(ByteStrings[i], System.Globalization.NumberStyles.HexNumber);
                //ByteOut[i] =Convert.ToByte("0x" + ByteStrings[i]);
            }
            return ByteOut;
        }

        /// <summary>
        /// 16进制字符串转16进制字节数组(方法2)
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] strToToHexByte(string hexString)
        {
            int a = 0X3A;

            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = System.Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        /// <summary>
        /// 智能转换：HEX码字符串到byte数组，遇到0~F以及空格以外的字符马上退出
        /// </summary>
        /// <param name="strInput">hex</param>
        /// <returns></returns>
        public byte[] HexString2HexByte(string strInput)
        {
            strInput = strInput.Trim().ToUpper();
            byte[] result = new byte[strInput.Length / 2];

            int iPtr = 0; bool bCouple = true; byte b = 0;
            foreach (char c in strInput)
            {
                //首先验证范围
                if ((c >= (char)'0' && c <= (char)'9') || (c >= (char)'A' && c <= (char)'F'))
                {
                    b = byte.Parse(c.ToString(), System.Globalization.NumberStyles.HexNumber);
                    if (bCouple)
                        b *= (byte)0x10;
                    result[iPtr] += b;
                    bCouple = !bCouple;

                    if (bCouple)
                        iPtr++;
                }
                else if (c == ' ')
                    continue;
                else
                    break;
            }
            return GetPartData(result, 0, iPtr);
        }
        
        
        #endregion

         
        /// <summary>
        /// 数据域颠倒
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string DataAgainst(string strValue)//20191105 Kmola存在问题，不能根据数据类型要求进行补全，可使用hex类型进行补全
        {
            string strRet = "";
            for (int i = 0; i < strValue.Length; i = i + 2)
            {
                strRet = strValue.Substring(i, 2) + strRet;
            }
            return strRet;
        }

        /// <summary>
        /// 进行校验
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public byte GetCS(string strValue)
        {
            byte bCS = 0;
            string strTmp = strValue.Replace(" ", "");
            byte[] bDatas = HexString2HexByte(strTmp);
            unchecked
            {
                foreach (byte b in bDatas)
                    bCS += b;
            }
            return bCS;
        }

         
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="iStart"></param>
        /// <param name="iLen"></param>
        /// <returns></returns>
        public byte[] GetPartData(byte[] input, int iStart, int iLen)
        {
            //起始长度直接超出范围了
            if (input.Length < iStart)
                return new byte[] { };

            //从0开始。。。
            if (iStart == 0 && iLen > input.Length)
                return (byte[])input.Clone();

            int iRealLen = iLen;
            if (iStart + iLen > input.Length)
                iRealLen = input.Length - iStart;

            byte[] result = new byte[iRealLen];

            for (int i = 0; i < iRealLen; i++)
                result[i] = input[i + iStart];

            return result;
        }

        /// <summary>
        /// 编码+33
        /// </summary>
        /// <param name="strValue">待编码数据</param>
        /// <returns></returns>
        public string EncodingAdd33Data(string strValue)
        {
            byte[] bTmp = new byte[strValue.Length / 2];
            bTmp = HexString2HexByte(strValue);
            for (int i = 0; i < bTmp.Length; i++)
            {
                byte b = 0x33;
                b += bTmp[i];
                bTmp[i] = b;
            }
            return bytes2StringHex(bTmp);
        }

        /// <summary>
        /// 解码-33
        /// </summary>
        /// <param name="strValue">待解码数据</param>
        /// <returns></returns>
        public string DecodingSub33Data(string strValue)
        {
            byte[] bTmp = new byte[strValue.Length / 2];
            bTmp = HexString2HexByte(strValue);
            for (int i = 0; i < bTmp.Length; i++)
            {
                byte b = bTmp[i];
                b -= 0x33;
                bTmp[i] = b;
            }
            return bytes2StringHex(bTmp);
        }
         
        #endregion
    }
}
