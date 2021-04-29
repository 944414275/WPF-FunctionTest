using System;
using System.IO;
using System.Text;

namespace ConStream
{
    class Program
    {
        static void Main(string[] args)
        {
            //读取本地文件流
            FileStream fileStream = new FileStream("F:\\data\\rujia\\citanji\\20210331\\132616544640166034", FileMode.OpenOrCreate, System.IO.FileAccess.Read);
            //FileStream ss=File.Open(@"F:\data\rujia\citanji\20210330\132615418799099802",FileMode.Open);
            //byte[] bytes1 = new byte[5*1024*1024];
            //int num = fileStream.Read(bytes1, 0, bytes1.Length);
            //fileStream.Close();

            StreamReader reader = new StreamReader(fileStream);
            string picture = reader.ReadToEnd();
            reader.Close();

            #region 20210330 
            Bq objBq = new Bq();
            objBq.Input = "Hello Insus.NET";

            byte[] Bytes = objBq.GetByte();

            objBq.Byte = Bytes;
            MemoryStream ms = objBq.GetMemoryStream();

            objBq.MemStream = ms;
            string output = objBq.GetString();
            #endregion


            Console.WriteLine(output);
        }
    }

    class Bq
    {
        /// <summary>
        /// 输入的字符串
        /// </summary>
        public string Input { get; set; }

        /// <summary>
        /// byte[]
        /// </summary>
        public byte[] Byte { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MemoryStream MemStream { get; set; }

        /// <summary>
        /// 转换为Byte[]
        /// </summary>
        /// <returns></returns>
        public byte[] GetByte()
        {
            return Encoding.ASCII.GetBytes(Input);
        }

        /// <summary>
        /// 转换为MemoryStream
        /// </summary>
        /// <returns></returns>
        public MemoryStream GetMemoryStream()
        {
            byte[] byteArray = Byte;
            return new MemoryStream(byteArray);
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <returns></returns>
        public string GetString()
        {
            StreamReader reader = new StreamReader(MemStream);
            return reader.ReadToEnd();
        }
    } 
}
