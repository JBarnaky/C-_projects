using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB14
{
    class Class
    {
        static void Main()
        {
            Console.WriteLine("Введите строку: ");
            String a = (Console.ReadLine());
            Console.WriteLine("Исходная строка: " + a);
            char[] ch = a.ToCharArray();
            var count = ch.Where((n) => n >= '0' && n <= '9').Count();
            Console.WriteLine("Количество цифр в строке: " + count);
            Console.ReadKey();
        }
    }
}
