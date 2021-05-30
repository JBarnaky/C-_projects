using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1sr1
{
    class Program
    {
        static void Main(string[] args)
        {
            Work w = new Work(10);
            w.show("До:");
            Console.Write("Цифра для поиска: ");
            int num = Convert.ToInt32(Console.ReadLine());
            Console.Write("Число для добавления: ");
            int val = Convert.ToInt32(Console.ReadLine());
            w.addElementAfter(num, val);
            w.show("После добавления: ");
            Console.ReadKey();
        }
    }

    class Work
    {
        private int[] mass;

        public Work(int n)
        {
            Random rnd = new Random();
            mass = new int[n];
            for (int i = 0; i < n; i++)
            {
                mass[i] = rnd.Next(-n * 2, n * 2);
            }
        }

        public void addElementAfter(int num, int val)
        {
            for (int i = 0; i < mass.Length; i++)
            {
                if (mass[i] == val)
                    continue;
                if (mass[i] == num)
                {
                    Array.Resize(ref mass, mass.Length + 1);
                    for (int j = mass.Length - 1; j >= i + 1; j--)
                    {
                        mass[j] = mass[j - 1];
                    }
                    mass[i + 1] = val;
                }
            }
        }

        public void show(string msg)
        {
            Console.WriteLine(msg);
            for (int i = 0; i < mass.Length; i++)
                Console.Write(mass[i] + " ");
            Console.WriteLine();
        }
    }
}