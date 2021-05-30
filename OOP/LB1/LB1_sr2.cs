using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1sr2
{
    class Program
    {
        static Random r = new Random();

        static int[,] Input()
        {
            Console.WriteLine(" Введите размерность массива:");
            Console.Write(" n строк = ");
            int n = int.Parse(Console.ReadLine());
            Console.Write(" m столбцов = ");
            int m = int.Parse(Console.ReadLine());
            int[,] a = new int[n, 2 * m];

            for (int i = 0; i < n; ++i)
                for (int j = 0; j < m; ++j)
                    a[i, j] = r.Next(-10, 10);
            return a;
        }

        static void AddColumn(int[,] a, int column)
        {
            for (int j = a.GetLength(1) - 1; j >= column + 1; j--)
            {
                for (int i = 0; i < a.GetLength(0); i++)
                {
                    a[i, j] = a[i, j - 1];
                }
            }
            for (int i = 0; i < a.GetLength(0); i++)
            {
                a[i, column] = 99;
            }
        }

        static void Print(int[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write(arr[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            int[,] a = Input();
            Print(a);

            Console.WriteLine(" - Вставить новый столбец перед всеми столбцами, в которых встречается заданное число");
            Console.Write("Задайте цифру x: ");
            int x = int.Parse(Console.ReadLine());
            for (int j = 0; j < a.GetLength(1); j++)
            {
                for (int i = 0; i < a.GetLength(0); i++)
                {
                    if (a[i, j] == x)
                    {
                        AddColumn(a, j);
                        j++;
                        break;
                    }
                }
            }

            Console.WriteLine("\n Массив с добавленными столбцами: "); //не лажа
            Print(a);
            Console.ReadKey();
        }
    }
}