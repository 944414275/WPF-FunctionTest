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

namespace WpfStringToHexAndBCD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string numbeString;
        byte[] verSetSerNum=new byte[6];

        public MainWindow()
        {
            InitializeComponent();
             
            //日期格式判断 
        }



        public void Start()
        {
            //年 2019
            numbeString = CRCYear.Text;
            if (JudgeData(numbeString, 1, 4, 0))//判断输入的字符串最短一位，最长4位  数值0-9
            {
                verSetSerNum[0] = (string_to_BCD(numbeString)[1]);//转换成BCD码
                verSetSerNum[1] = (string_to_BCD(numbeString)[0]);
            }
            else
            {
                MessageBox.Show("年的格式不正确", "错误");
            }

            //月
            numbeString = CRCMonth.Text;
            if (JudgeData(numbeString, 1, 2, 0))//判断输入的字符串最短一位，最长2位   数值0-9
            {
                int[] num = new int[2];
                //字符串分割  2个字符串平均分成2个字符串
                string[] subString = new string[2];
                for (int i = 0; i < 2; i++)
                {
                    subString[i] = numbeString.Substring(i, 1);//字符串分割
                    num[i] = int.Parse(subString[i], System.Globalization.NumberStyles.HexNumber);//字符串转int类型
                }
                //先发送低位数据再发送高位数据

                verSetSerNum[2] = (byte)num[1];
                verSetSerNum[3] = (byte)num[0];
            }
            else
            {
                MessageBox.Show("月的格式不正确", "错误");
            }

            //日
            numbeString = CRCDay.Text;
            if (JudgeData(numbeString, 1, 2, 0))//判断输入的字符串最短一位，最长2位   数值0-9
            {
                int[] num = new int[2];
                //字符串分割  2个字符串平均分成2个字符串
                string[] subString = new string[2];
                for (int i = 0; i < 2; i++)
                {
                    subString[i] = numbeString.Substring(i, 1);//字符串分割
                    num[i] = int.Parse(subString[i], System.Globalization.NumberStyles.HexNumber);//字符串转int类型
                }
                //先发送低位数据再发送高位数据

                verSetSerNum[4] = (byte)num[1];
                verSetSerNum[5] = (byte)num[0];
            }
            else
            {
                MessageBox.Show("日的格式不正确", "错误");
            }
        }

        //日期格式判断
        public bool JudgeData(string strMessage, int iMinLong, int iMaxLong, int f)
        {
            bool bResult = false;
            //if (f == 1)
            //     number_string += strMessage;
            //开头匹配一个字母或数字+匹配两个十进制数字+匹配一个字母或数字+匹配两个相同格式的的（-加数字）+已字母或数字结尾  

            string pattern = @"^[0-9]*$"; //输入的字符串为十进制数
            if (strMessage.Length >= iMinLong && strMessage.Length <= iMaxLong)//判断字符串长度是否在要求范围内
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(strMessage, pattern))//匹配所有字符都在字母和数字之间
                {
                    bResult = true;
                }
                else
                {
                    bResult = false;
                }
                //字符串的位数不满iMaxLong位时，左侧用0补齐
                if (strMessage.Length < iMaxLong)
                {
                    numbeString = numbeString.PadLeft(iMaxLong, '0');//字符串固定为iMaxLong为，不足左侧补0
                }
            } 
            return bResult;
        }

        //字符串转BCD码方法
        private static Byte[] string_to_BCD(string strTemp)
        {
            try
            {
                if (Convert.ToBoolean(strTemp.Length & 1))//若字符串的长度为奇数，则变为偶数 
                {
                    strTemp = "0" + strTemp;//数位为奇数时前面补0  
                }
                Byte[] aryTemp = new Byte[strTemp.Length / 2];
                for (int i = 0; i < (strTemp.Length / 2); i++)
                {
                    aryTemp[i] = (Byte)(((strTemp[i * 2] - '0') << 4) | (strTemp[i * 2 + 1] - '0'));//两个字节的数组成一个字节的BCD码0-9
                                                                                                    // aryTemp[i] = (Byte)(((strTemp[i * 2])<< 4) | (strTemp[i * 2 + 1]));
                }
                return aryTemp;//高位在前  
            }
            catch
            {
                MessageBox.Show("set failed!");
                return null;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Start();
        }
    }
}
