using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB2
{
    class Triangle
    {
        int a, b, c;
        public int A
        {
            get
            {
                return a;
            }
            set
            {
                a = value;
            }
        }
        public int B
        {
            get
            {
                return b;
            }
            set
            {
                b = value;
            }
        }
        public int C
        {
            get
            {
                return c;
            }
            set
            {
                c = value;
            }
        }
        public bool isTriangle
        {
            get
            {
                if (a + b > c && a + c > b && b + c > a)
                {
                    return true;
                }
                return false;
            }
        }
        //Конструктор
        public Triangle(int a_, int b_, int c_)
        {
            //Проверка на положительность сторон треугольника
            isCorrect(a_, b_, c_);
            A = a_;
            B = b_;
            C = c_;
        }
        public void PrintSides()
        {
            Console.WriteLine("a = {0}, b = {1}, c = {2}", a, b, c);
        }
        public int Perimetr()
        {
            return a + b + c;
        }
        public double Square()
        {
            return Math.Sqrt(halfper(this) * (halfper(this) - a) * (halfper(this) - b)
                * (halfper(this) - c));
        }
        static double halfper(Triangle Ob)
        {
            return Ob.Perimetr() / 2.0;
        }
        static void isCorrect(int a, int b, int c)
        {
            if (a < 0 || b < 0 || c < 0)
                throw new Exception("Стороны треугольника не могут быть отрицательными");
        }
        public static Triangle operator ++(Triangle Ob)
        {
            return new Triangle(++Ob.a, ++Ob.b, ++Ob.c);
        }
        public static Triangle operator --(Triangle Ob)
        {
            return new Triangle(--Ob.a, --Ob.b, --Ob.c);
        }
        public static Triangle operator *(Triangle Ob, int mult)
        {
            return new Triangle(Ob.a * mult, Ob.b * mult, Ob.c * mult);
        }
        //Перевод в строку
        public override string ToString()
        {
            return (string)"Стороны треугольника: " + "a = " + A + " b = " + B + " c = " + C;
        }
        //Индексатор
        public int this[int idx]
        {
            get
            {
                if (idx == 1)
                    return a;
                else if (idx == 2)
                    return b;
                else if (idx == 3)
                    return c;
                else
                    throw new Exception("Индекс может быть только 1, 2 или 3");
            }
            //Если idx=1 устанавливаем a
            //если idx=2 устанавливаем b
            //если idx=3 устанавливаем c
            set
            {
                if (idx == 1)
                    a = value;
                else if (idx == 2)
                    b = value;
                else if (idx == 3)
                    c = value;
                else
                    throw new Exception("Индекс может быть только 1, 2 или 3");
            }
        }
        public static bool operator true(Triangle t)
        {
            return t.isTriangle;
        }
        public static bool operator false(Triangle t)
        {
            return t.isTriangle;
        }
    };

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Triangle Ob = new Triangle(3, 3, 3);
                if (Ob)
                    Ob.PrintSides();
                else
                    throw new Exception("Треугольника с такими сторонами не может быть");
                Console.WriteLine("Периметр={0}", Ob.Perimetr());
                Console.WriteLine("Площадь={0}", Ob.Square());
                Ob++;
                Ob.PrintSides();
                --Ob;
                Ob.PrintSides();
                Ob *= 5;
                Ob.PrintSides();
                Console.WriteLine(Ob.ToString());
                Console.WriteLine(Ob[1]);
                Ob[2] = 30;
                Console.WriteLine(Ob);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}