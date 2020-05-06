using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfEvent
{
    public delegate void Mydelegate(float c);



    class Program
    {
        static void Main(string[] args)
        {


        }
    }

    class Boilter
    {
        float c;
        int min;
        public static event Mydelegate TempAlar;

        public void Boil()
        {
            while (c < 100)
            {
                min++;
                c = min * 10;
                if (TempAlar != null)
                {
                    TempAlar(c);
                }
            }
        }

        


    }
}
