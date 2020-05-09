using System;

namespace ConTypeTest1
{
    class Program
    {
        static void Main(string[] args)
        {
            Car car=new Car();
            Type type=car.GetType();

            string s = "1";
            Type sType=s.GetType();

            int i = 1;
            Type iType = i.GetType();

            bool b = true;
            Type bType = typeof(bool);

            Console.WriteLine("Type is {0} --- Type'name is {1} --- Type'full name is {2}", type, type.Name, type.FullName);
            Console.WriteLine("Type is {0} --- Type'name is {1} --- Type'full name is {2}", sType, sType.Name, sType.FullName);
            Console.WriteLine("Type is {0} --- Type'name is {1} --- Type'full name is {2}", iType,iType.Name, iType.FullName);
            Console.WriteLine("Type is {0} --- Type'name is {1} --- Type'full name is {2}", bType, bType.Name, bType.FullName);

            Console.ReadKey();
        }
    }

    public class Car
    {
        string Name { set; get; }
    }

    /*
     https://www.cnblogs.com/xcsn/p/9052330.html
     c#基础之Type
     官方文档：https://msdn.microsoft.com/zh-cn/library/system.type%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396

C#中通过Type类可以访问任意数据类型信息。 

system.Type类以前把Type看作一个类,但它实际上是一个抽象的基类。

只要实例化了一个Type对象,实际上就实例化了Typc的一个派生类。

尽管一般情况下派生类只提供各种Type方法和属性的不同重载,但是这些方法和属性返回对应数据类型的正确数据,Type有与每种数据类型对应的派生类。

Type是许多反射功能的入口 。注意,可用的属性都是只读的:可以使用Type确定数据的类型,但不能使用它修改该类型

 

1.获取Type

　有3种方式：
  a.使用typeof运算符，如Type t = typeof(int);
  b.使用GetType()方法，如int i;Type t = i.GetType();
  c.使用Type类的静态方法GetType()，如Type t =Type.GetType("System.Double");


2.Type的属性：
  Name：数据类型名；
  FullName：数据类型的完全限定名，包括命名空间；
  Namespace：数据类型的命名空间；
  BaseType：直接基本类型；
  UnderlyingSystemType：映射类型；


3.Type的方法：
  GetMethod()：返回一个方法的信息；
  GetMethods()：返回所有方法的信息。

GetMember()和GetMembers()方法返回数据类型的任何成员或所有成员的详细信息,不管这些成员是构造函数、属性和方法等。
public static void Main()
        {
            //基本数据类型
            Type intType = typeof(int);
            
            //属性
            Console.WriteLine("intType.Name = " + intType.Name);
            Console.WriteLine("intType.FullName = " + intType.FullName);
            Console.WriteLine("intType.Namespace = " + intType.Namespace);
            Console.WriteLine("intType.IsAbstract = " + intType.IsAbstract);
            Console.WriteLine("intType.IsClass = " + intType.IsClass);
            Console.WriteLine("intType.IsEnum = " + intType.IsEnum);
            Console.WriteLine("intType.IsPrimitive = " + intType.IsPrimitive);
            Console.WriteLine("intType.IsValueType = " + intType.IsValueType);

            //方法
            MethodInfo[] methods = intType.GetMethods();
            foreach (MethodInfo method in methods)
            {
                Console.WriteLine(method.DeclaringType + " " + method.MemberType + " " + method.Name);
            }
        }
        参考文档：

https://www.cnblogs.com/kingdom_0/articles/2040855.html

http://www.knowsky.com/604653.html
     */
}
