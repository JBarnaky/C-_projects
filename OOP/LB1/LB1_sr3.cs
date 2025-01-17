﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1sr3
{
    class SR_3
    {
        static readonly int MaxDeep = 10000;
        static int[] Decomposition = new int[MaxDeep];
        static void Decompose(int multiplier, int n, int deep)
        {
            int i, mlim = 0;
            if (n == 1)
            {
                for (i = 0; i < deep - 1; i++)
                    Console.Write(Decomposition[i] + "*");
                if (deep > 0)
                    Console.WriteLine(Decomposition[deep - 1]);
                return;
            }
            if (deep == 0)
                mlim = n - 1;
            else
                mlim = n;
            for (Decomposition[deep] = multiplier; Decomposition[deep] <= mlim; Decomposition[deep]++)
                if ((n % Decomposition[deep]) == 0)
                    Decompose(Decomposition[deep], n / Decomposition[deep], deep + 1);
        }
        static void Main(string[] args)
        {
            int n;
            Console.WriteLine("Разложение числа на множители");
            Console.Write("Введите число: ");
            n = int.Parse(Console.ReadLine());
            SR_3.Decompose(2, n, 0);
            Console.ReadKey();
        }
    }
}