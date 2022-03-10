using System;
using System.Collections.Generic;
namespace shape
{
    public abstract class Shape
    {
        static void Main(string[] args)
        {
            double sum = 0;
            for (int i = 0; i < 10; i++)
            {
                Shape shape = ShapeFactory.GetShape((int)GetRandomNumber() % 3);
                Console.Write("Type:{0}, SideLength:{1}, Area:{2}\n\n", shape.Type(), shape.SideLength(), shape.Area());
                sum += shape.Area();

            }
            Console.WriteLine("Sum:{0}\n\n", sum);
        }
        public abstract double Area();
        public virtual bool Legal() => true;
        public abstract void Random();

        public abstract string Type();

        public abstract string SideLength();
        public static Random random = new Random();
        public static double GetRandomNumber()
        {
            return random.Next(1, 100);
        }
    }
    public class ShapeFactory
    {
        public static readonly int Triangle = 0;
        public static readonly int Rectangle = 1;
        public static readonly int Square = 2;

        private static readonly Dictionary<int, Func<Shape>> shapeDic;

        static ShapeFactory()
        {
            shapeDic = new Dictionary<int, Func<Shape>>
            {
                { Triangle, () => new Triangle() },
                { Rectangle, () => new Rectangle() },
                { Square, () => new Square() }
            };
        }

        public static Shape GetShape(int arg)
        {
            return shapeDic[arg]();
        }

    }
    public class Triangle : Shape
    {
        private double a;
        private double b;
        private double c;

        public double A { get => a; set => a = value; }
        public double B { get => b; set => b = value; }
        public double C { get => c; set => c = value; }

        public Triangle(double a, double b, double c)
        {
            this.a = a; this.b = b; this.c = c;
        }
        public Triangle()
        {
            Random();
        }
        public override double Area()
        {
            double p = (a + b + c) / 2;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }

        public override bool Legal()
        {
            if (a <= 0 || b <= 0 || c <= 0) return false;
            if (a + b <= c) return false;
            if (b + c <= a) return false;
            if (c + a <= b) return false;
            return true;
        }

        public override void Random()
        {
            a = GetRandomNumber();
            b = GetRandomNumber();
            c = GetRandomNumber();
            while (Legal() == false)
            {
                a = GetRandomNumber();
                b = GetRandomNumber();
                c = GetRandomNumber();
            }
        }

        public override string Type()
        {
            return "Triangle";
        }

        public override string SideLength()
        {
            return a + "、" + b + "、" + c;
        }
    }
    public class Rectangle : Shape
    {
        protected double a;
        protected double b;

        public virtual double A { get => a; set => a = value; }
        public virtual double B { get => b; set => b = value; }
        public Rectangle(double a, double b)
        {
            this.a = a; this.b = b;
        }
        public Rectangle()
        {
            Random();
        }
        public override double Area()
        {
            return a * b;
        }

        public override bool Legal()
        {
            return a > 0 && b > 0;
        }
        public override void Random()
        {
            a = GetRandomNumber();
            b = GetRandomNumber();
        }

        public override string Type()
        {
            return "Rectangle";
        }

        public override string SideLength()
        {
            return a + "、" + b;
        }
    }
    public class Square : Rectangle
    {
        public override double A { get => a; set => a = value; }
        public override double B { get => b; set => b = value; }
        public override double Area()
        {
            if (Legal() == false) throw new ArgumentException("边长不相等");
            return a * a;
        }

        public Square(double a)
        {
            this.a = this.b = a;
        }

        public Square()
        {
            Random();
        }
        public override bool Legal()
        {
            return base.Legal() && a == b;
        }
        public override void Random()
        {
            a = b = GetRandomNumber();
        }

        public override string Type()
        {
            return "Square";
        }

        public override string SideLength()
        {
            return a.ToString();
        }
    }
}