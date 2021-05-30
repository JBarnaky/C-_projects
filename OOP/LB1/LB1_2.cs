using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB11
{
    class Class2
    {
        static int[] Input()
        {
            Console.WriteLine("Введите размерность массива: ");
            int n = int.Parse(Console.ReadLine());
            int[] a = new int[n];
            for (int i = 0; i < n; ++i)
            {
                Console.Write("Введите элемент №[{0}]= ", i);
                a[i] = int.Parse(Console.ReadLine());
            }
            return a;
        }

        static void Print(int[] a)
        {
            for (int i = 0; i < a.Length; ++i) Console.Write("{0} ", a[i]);
            Console.WriteLine();
        }

        static void Min_Max(int[] a)
        {
            int min = a[0];
            int max = a[0];

            for (int i = 1; i < a.Length; i++)
                if (a[i] > max) max = a[i];
                else if (a[i] < min) min = a[i];

            for (int i = 0; i < a.Length; i++)
                if (a[i] == min) a[i] = max;

            for (int i = a.Length-1; i > 0; i--)
                if (a[i] == max) a[i] = min;
        }

        static void Main()
        {
            int[] myArray = Input();
            Console.WriteLine("Исходный массив:");
            Print(myArray);
            Min_Max(myArray);
            Console.WriteLine("Измененный массив:");
            Print(myArray);
            Console.ReadKey(true);
        }
    }
}