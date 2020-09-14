using System;
using System.Collections.Generic;
using System.Linq;

namespace ConByteArrayTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<byte[]> listByteArray = new List<byte[]>();
            byte[] vs = new byte[] { 0,1,2,3,4,5,6,7,8,8,10,11};
            //byte[] vs1;
            //vs1= vs.Skip(0).Take(2).ToArray();


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
