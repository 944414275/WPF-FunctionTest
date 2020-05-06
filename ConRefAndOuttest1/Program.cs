using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//首先：两者都是按地址传递的，使用后都将改变原来参数的数值。
//其次：ref可以把参数的数值传递进函数，但是out是要把参数清空，就是说你无法把一个数值从out传递进去的，out进去后，参数的数值为空，所以你必须初始化一次。
//这个就是两个的区别，或者说就像有的网友说的，ref是有进有出，out是只出不进。
namespace ConRefAndOuttest1
{
    //2、调用前赋值
    //ref作为参数的函数在调用前，实参必须赋初始值。所以就可以要求函数中可以不对参数进行赋值操作，
    //out作为参数的函数在调用前，实参可以不赋初始值。所以就要求在函数中必须赋值

    //3、函数中，参数赋值
    //在调用函数中，out引入的参数必须赋值；
    //而ref引入的参数在返回前可以不赋值；
    class Program
    {
        static void Main(string[] args)
        {
            #region 2、调用前赋值
            //int valRef; //valRef必须赋初始值，不然会报错：Use of unassigned local variable 'valRef' ，使用了没定义的valRef，这是因为，ref的设计就是将值类型当成引用类型来使用，所以，引用类型嘛，就得使用前实例化
            int valRef = 1;
            int valOut;//不需要为valOut赋初始值，没什么意义，因为out在带进MethodOut里面时会把这个值clear，也就是经典的only out，弄come

            string strRef = "";
            string strOut = "";
            #endregion



            MethodRef(ref valRef); 
            MethodOut(out valOut);

            MethodRef(ref strRef);
            MethodOut(out strOut);
        }

        #region 3、函数中，参数赋值
        //ref 关键字使参数按引用传递。其效果是，当控制权传递回调用方法时，在方法中对参数的任何更改都将反映在该变量中。若要使用 ref 参数，则方法定义和调用方法都必须显式使用 ref 关键字。
        static void MethodRef(ref int i)
        {
            //int tempRef = i;
            //i = 44;
        }
        static void MethodOut(out int i)
        {
            //int tempOut = i;
            i = 42;
        }

        //引用类型
        static void MethodRef(ref string i)
        {
            i = "refString";
        }
        static void MethodOut(out string i)
        {
            //int tempOut = i;
            i = "outString";
        }
        #endregion

    }
    //总结(20200506 komla)：第一使用out或者ref后不需要在调用函数中进行返回，直接可以对当前函数中的变量进行操作，接着用，这是其一
    //第二点也是ref和out的设计初衷，就是将值类型按引用类型进行传递，这样就可以不改变变量，将值的地址传递过去，
    //如果按第一条说的，将值直接传递过去，值的变化不会对原来的值有什么变化，因为传递的是值的copy
}
