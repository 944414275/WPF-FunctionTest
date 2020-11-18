using System;
using System.Collections.Generic;
using System.Linq;

namespace ConByteArrayTest
{
    class Program
    {
        static void Main(string[] args)
        {

            //20200930 KOMLA 
            byte[] rawData = new byte[] { 0X27, 0X06, 0X17, 0X43,0X0A,0X32,0XF8,0X42 };
            double S = BitConverter.ToSingle(rawData,0);

            //ystem.Windows.Point _point = new System.Windows.Point(BitConverter.ToDouble(rawData, DACList.Count - 8), BitConverter.ToDouble(rawData, DACList.Count - 4));




            List<byte[]> listByteArray = new List<byte[]>();
            byte[] vs = new byte[] { 0,1,2,3,4,5,6,7,8,8,10,11};
            //byte[] vs1;
            //vs1= vs.Skip(0).Take(2).ToArray();

            float s = Convert.ToSingle("0.00");
            byte[] by = new byte[10];
            for(int i=0;i<10;i++)
            {
                by.Append(vs[2]);
            }
            


            byte[] vs1= vs.Skip(0).Take(2).ToArray();
            byte[] vs2 = vs.Skip(2).Take(2).ToArray();

            //listByteArray[0] = vs.Skip(0).Take(2).ToArray();
            //listByteArray[1] = vs.Skip(2).Take(2).ToArray();

            Console.WriteLine("Hello World!");
        }
    }
}
