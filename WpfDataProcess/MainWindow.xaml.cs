using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace WpfDataProcess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string wakeCode = "FEFEFEFE";
        private string writeCode = "6888888888888868140E6E1212373533333322D22240";
        private string endBits = "16";

        private string _currentValue = null;
        public string CurrentValue
        {
            get
            {
                if (_currentValue != null)
                    return _currentValue;
                else return "300.0";
            } 
        }
        public MainWindow()
        {
            InitializeComponent();
            //string s = "6333";//6333 3433A789
            //string ss = fnDecodingData(s);//01007456
            //double getTime = Convert.ToDouble(ss);

            //byte[] bt = StrHex2HexByte(s);
            //int a = 2;

            //1倒置
            _currentValue = AgainstData("7000");
            //2组成数据域进行校验
            //2.1电流值进行+33
            _currentValue = fnEncodingData(_currentValue);
            //2.2组包
            string dataCS = writeCode + _currentValue;
            //2.3校验
            byte bt = fnGetCS(dataCS);
            string strCS = bt.ToString("X2");
            wakeCode += dataCS+ strCS + endBits;
            //2.4dataCS转换成byte数组
            byte[] btArray = fnString2HEX(wakeCode);

            int a = 1;
        }


        public static byte[] StrHex2HexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        /// <summary>
        /// 字符串转16进制字节数组(方法2)
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] StrHexToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        /// <summary>
        /// 编码+33
        /// </summary>
        /// <param name="strValue">待编码数据</param>
        /// <returns></returns>
        public string fnEncodingData(string strValue)
        {
            byte[] bTmp = new byte[strValue.Length / 2];
            bTmp = fnString2HEX(strValue);
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
        public string fnDecodingData(string strValue)
        {
            byte[] bTmp = new byte[strValue.Length / 2];
            bTmp = fnString2HEX(strValue);
            for (int i = 0; i < bTmp.Length; i++)
            {
                byte b = bTmp[i];
                b -= 0x33;
                bTmp[i] = b;
            }
            return bytes2StringHex(bTmp);
        }

        /// <summary>
        /// 智能转换：HEX码字符串到byte数组，遇到0~F以及空格以外的字符马上退出
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public static byte[] fnString2HEX(string strInput)
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

        public static string bytes2StringHex(byte[] bValues)
        {
            if (bValues == null)
                return "";
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bValues)
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        /// <summary>
        /// 数据域颠倒
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string AgainstData(string strValue)
        {
            string strRet = "";
            for (int i = 0; i < strValue.Length; i = i + 2)
            {
                strRet = strValue.Substring(i, 2) + strRet;
            }
            return strRet;
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
            byte[] bDatas = fnString2HEX(strTmp);
            unchecked
            {
                foreach (byte b in bDatas)
                    bCS += b;
            }
            return bCS;
        }
    }
}
