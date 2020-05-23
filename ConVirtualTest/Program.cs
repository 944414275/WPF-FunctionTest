using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*基类baseclass ,包含虚方法ABC， 类A、B、C都继承了baseclass，如果AB修改了方法AB,
 * 那么类C默认使用AB方法的话，使用的是baseclass的方法吧 */
namespace ConVirtualTest
{
    
    class Program
    {
        static void Main(string[] args)
        {
            double r = 3.0, h = 5.0;
            Shape c = new Circle(r);
            Shape s = new Sphere(r);
            Shape l = new Cylinder(r, h);
            // Display results.
            Console.WriteLine("Area of Circle   = {0:F2}", c.Area());
            Console.WriteLine("Area of Sphere   = {0:F2}", s.Area());
            Console.WriteLine("Area of Cylinder = {0:F2}", l.Area());//没有重写父类中的虚方法，那么会直接使用父类的方法，是15
            Console.ReadKey();
        }
    }

    /// <summary>
    /// 20200523 komla 基类
    /// </summary>
    public class Shape
    {
        public const double PI = Math.PI;
        protected double x, y;

        public Shape()
        {
        }

        public Shape(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        //可被重写
        public virtual double Area()
        {
            return x * y;
        }
    }

    /// <summary>
    /// 类A
    /// </summary>
    public class Circle : Shape
    {
        public Circle(double r) : base(r, 0)//通过在方法后面继承
        {
        }

        //直接重写
        public override double Area()
        {
            return PI * x * x;
        }
    }

    /// <summary>
    /// 类B
    /// </summary>
    class Sphere : Shape
    {
        public Sphere(double r) : base(r, 0)
        {
        }

        public override double Area()
        {
            return 4 * PI * x * x;
        }
    }

    /// <summary>
    /// 类C
    /// </summary>
    class Cylinder : Shape
    {
        public Cylinder(double r, double h) : base(r, h)
        {
        }

        //public override double Area()
        //{
        //    return 2 * PI * x * x + 2 * PI * x * y;
        //}
    }
}
