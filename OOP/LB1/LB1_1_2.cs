using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1
{
    class Class1
    {
        static int[,] Input(out int n, out int m)
        {
            Console.WriteLine("Введите размерность массива: ");
            Console.Write("n = ");
            n = int.Parse(Console.ReadLine());
            Console.Write("m = ");
            m = int.Parse(Console.ReadLine());
            int[,] a = new int[n, m];
            for (int i = 0; i < n; ++i)
                for (int j = 0; j < m; ++j)
                {
                    Console.Write("Введите элемент [{0},{1}]= ", i, j);
                    a[i, j] = int.Parse(Console.ReadLine());
                }
            return a;
        }

        static void Print(int[,] a)
        {
            for (int i = 0; i < a.GetLength(0); ++i, Console.WriteLine())
                for (int j = 0; j < a.GetLength(1); ++j)
                    Console.Write("{0,5} ", a[i, j]);
        }

        static void Change(int[,] a)
        {
            Console.WriteLine("Введите пороговое значение: ");
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine("Числа больше введенного: ");

            for (int i = 0; i < a.GetLength(0); ++i)
                for (int j = 0; j < a.GetLength(1); ++j)
                    if (a[i, j] > n) Console.WriteLine("a[{0},{1}]={2} ", i, j, a[i, j]);
        }

        static void Main()
        {
            int n, m;
            int[,] myArray = Input(out n, out m);
            Console.WriteLine("Исходный массив:");
            Print(myArray);
            Change(myArray);
            Console.ReadKey(true);
        }
    }
}