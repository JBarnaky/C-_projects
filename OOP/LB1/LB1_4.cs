using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB13
{
    class Class
    {
        static int[][] Input()
        {
            Console.WriteLine("введите размерность массива");
            Console.Write("n = ");
            int n = int.Parse(Console.ReadLine());
            int[][] a = new int[n][];
            for (int i = 0; i < n; ++i)
            {
                a[i] = new int[n];
                for (int j = 0; j < n; ++j)
                {
                    Console.Write("a[{0},{1}]= ", i, j);
                    a[i][j] = int.Parse(Console.ReadLine());
                }
            }
            return a;
        }

        static void Print1(int[] a)
        {
            for (int i = 0; i < a.Length; ++i)
                Console.Write("{0,5} ", a[i]);
        }

        static void Print2(int[][] a)
        {
            for (int i = 0; i < a.Length; ++i, Console.WriteLine())
                for (int j = 0; j < a[i].Length; ++j)
                    Console.Write("{0,5} ", a[i][j]);
        }

        static int Min(int[] a)
        {
            int min = a[0];
            for (int i = 1; i < a.Length; ++i)
            {
                if (a[i] < 0)
                {
                    min = a[i];
                    break;
                }
                Console.WriteLine("Первый отрицательный элемент строки: {0} ", min);
            }
            return min;
        }

        static void Main()
        {
            int[][] myArray = Input();
            Console.WriteLine("Исходный массив:");
            Print2(myArray);
            int[] rez = new int[myArray.Length];
            for (int i = 0; i < myArray.Length; ++i)
                rez[i] = Min(myArray[i]);
            Console.WriteLine("Новый массив:");
            Print1(rez);
            Console.ReadKey(true);
        }
    }
}