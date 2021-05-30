using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1
{
    class Class
    {
        static int[] Input()
        {
            Console.Write("Введите размерность массива: ");
            int n = int.Parse(Console.ReadLine());
            int[] a = new int[n];
            for (int i = 0; i < n; ++i)
            {
                Console.Write("Введите элемент №[{0}]: ", i+1);
                a[i] = int.Parse(Console.ReadLine());
            }
            return a;
        }

        static void Print(int[] a)
        {
            for (int i = 0; i < a.Length; ++i) Console.Write("{0} ", a[i]);
            Console.WriteLine();
        }

        static void Change(int[] a)
        {
            Console.Write("Введите пороговое значение: ");
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine("Числа больше введенного: ");
            for (int i = 0; i < a.Length; ++i)
                if (a[i] > n) Console.WriteLine("a[{0}]={1} ", i, a[i]);
        }

        static void Main()
        {
            int[] myArray = Input();
            Console.WriteLine("Исходный массив:");
            Print(myArray);
            Change(myArray);
            Console.ReadKey(true);
        }
    }
}