using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1sr4
{
    class Program
    {
        static void Main(string[] args)
        {
            One ob = new One();
            ob.two();
            Console.ReadKey();
        }
    }
    class One
    {
        int a = 1, b = 1;
        public void two()
        {
            Console.WriteLine("Введите n: ");
            int x = Convert.ToInt32(Console.ReadLine());
            rec(x);
        }
        public void rec(int x)
        {
            for (int i = 1; i < x; i++)
            {
                Console.Write(a);
                for (int j = x - i; j > 1; j--)
                {
                    Console.Write("+1");
                }
                Console.Write("+" + b);
                Console.WriteLine();
                b++;
            }
            b = a + 1;
            a++;
            if (x > 1)
                rec(x - 1);
        }
    }
}