using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfRegex
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string temp = "";
        string invertStr = "";
        string hexString = ""; 

        public MainWindow()
        {
            InitializeComponent();
            int c = 0;
            string[] strDecArray = new string[4];
            StringBuilder strDecMinus33 = new StringBuilder();
            string[] strTemp = new string[4] { "34", "33", "A7", "89" };
            foreach (string temp in strTemp)
            {
                strDecArray[c] = (Convert.ToInt64(temp) - 33).ToString();
                strDecMinus33.Append(strDecArray[c]);
                c++;
            }
















            byte _byte = fnGetCS("68 88 88 88 88 88 88 68 14 0E 6E 12 12 37 35 33 33 33 22 D2 22 40 33 7B");
            byte[] btArray = strToToHexByte("fe fe ff ff ff");
            int lengh = btArray.Length;
            //string hexStr = _byte.ToString("X2");
            //FE FE FE FE 68 88 88 88 88 88 88 68 14 0E 6E 12 12 37 35 33 33 33 22 D2 22 40 33 7B BD 16
            //68 88 88 88 88 88 88 68 14 0E 6E 12 12 37 35 33 33 33 22 D2 22 40 33 7B/189-BD
            //68 88 88 88 88 88 88 68 14 0E 6E 12 12 37 35 33 33 33 22 D2 22 40 33 63/165-A5
            int INA = 1;


            //hexString = fnEncodingData("90");//期望值是7B
            //string s=InvertCurrentValue("680.1");//22 65
            //string ss = fnEncodingData("6801");

            temp = Regex.Replace(this.testTxt.Text, @"[^\d]*", "");
            string[] strArray=new string[temp.Length/2];
            int j = 0;
            for (int i=0;i<temp.Length;i=i+2)
            { 
                strArray[j] = (Convert.ToInt32(temp.Substring(i, 2)) + 33).ToString();
                invertStr = temp.Substring(i, 2) + invertStr;
                j++;
            }
            string strHex=Convert.ToString(123,16).Trim().ToUpper(); ; 
            hexString = Convert.ToInt64("123").ToString("x");//7B  "x"这个很重要，没有就是Int型了。 
            hexString = String2HEXString("68");//00360038 
            //byte[] _byte=String2HexByte(invertStr);
            int a = 1; 
            hexString =GetHexStringFromString(invertStr);
            this.showTextboxText.Text = hexString;
            //int a = 1;
        }

        #region 十六进制字符串转字节型
        /// <summary>
        /// 字符串转16进制字节数组(方法2)
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] strToToHexByte(string hexString)
        {

            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
        #endregion
        /// <summary>
        /// 把十六进制字符串转换成字节型(方法1)
        /// </summary>
        /// <param name="InString"></param>
        /// <returns></returns>
        public static byte[] StringToByte(string InString)
        {
            string[] ByteStrings;
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






        private string InvertCurrentValue(string currentValue)
        {
            //去掉字符串中的小数点
            string _currentValue = Regex.Replace(currentValue, @"[^\d]*", "");
            int index = _currentValue.Length / 2;
            string invertedStr = "";
            string[] strByte = new string[index];
            string[] strHexByte = new string[index];
            StringBuilder strHexAdded33 = new StringBuilder();
            int j = 0;
            int c = 0;
              
            for (int i = 0; i < _currentValue.Length; i = i + 2)
            { 
                strByte[j] = _currentValue.Substring(i, 2);
                j++;
            }
            //转换为Hex String 
            foreach (string temp in strByte)
            { 
                strHexByte[c] = (Convert.ToInt64(temp) + 33).ToString("X2");
                strHexAdded33.Append(strHexByte[c]);
                c++;
            } 
            string str=strHexAdded33.ToString();

            for (int i = 0; i < str.Length; i = i + 2)
            {
                invertedStr = str.Substring(i, 2) + invertedStr; 
            }
            return invertedStr;
        }



        /// <summary>
        /// 编码+33
        /// </summary>
        /// <param name="strValue">待编码数据</param>
        /// <returns></returns>
        public string fnEncodingData(string strValue)
        {
            byte[] bTmp = new byte[strValue.Length / 2];
            bTmp = String2HexByte(strValue);
            for (int i = 0; i < bTmp.Length; i++)
            {
                byte b = 0x33;
                b += bTmp[i];
                bTmp[i] = b;
            }
            return bytes2String(bTmp);
        }
        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public byte fnGetCS(string strValue)
        {
            byte bCS = 0;
            string strTmp = strValue.Replace(" ", "");
            byte[] bDatas = String2HexByte(strTmp);
            unchecked
            {
                foreach (byte b in bDatas)
                    bCS += b;
            }
            return bCS;
        }
        /// <summary>
        /// 智能转换：HEX码字符串到byte数组，遇到0~F以及空格以外的字符马上退出
        /// </summary>
        /// <param name="strInput">普通字符串</param>
        /// <returns></returns>
        public static byte[] String2HexByte(string strInput)
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
            return fnGetPartData(result, 0, iPtr);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bValues"></param>
        /// <returns></returns>
        public static string bytes2String(byte[] bValues)
        {
            if (bValues == null)
                return "";
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bValues)
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        } 
        
         
        public static byte[] fnGetPartData(byte[] input, int iStart, int iLen)
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
        /// String2HEXString
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string String2HEXString(string str)
        {
            string strReturn = "";//  存储转换后的编码
            foreach (short shortx in str.ToCharArray())
            {
                strReturn += shortx.ToString("X2");//X4
            }
            return strReturn;
        }
        /// <summary>
        /// GetHexStringFromString
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetHexStringFromString(string s)
        {
            if ((s.Length % 2) != 0)
            {
                s += " ";//空格
                         //throw new ArgumentException("s is not valid chinese string!");
            }

            System.Text.Encoding chs = System.Text.Encoding.GetEncoding("gb2312");

            byte[] bytes = chs.GetBytes(s);

            string str = "";

            for (int i = 0; i < bytes.Length; i++)
            {
                str += string.Format("{0:X}", bytes[i]);
            }

            return str;
        }
    }
}
