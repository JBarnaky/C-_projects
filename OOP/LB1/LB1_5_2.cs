using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB14
{
    class Class2
    {
        static void Main()
        {
            Console.WriteLine("Введите строку: ");
            string str = Console.ReadLine();
            var words = str.Split(@" .,!:;?-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine("Самое короткое слово: " + words.First(x => x.Length == words.Select(y => y.Length).Min()));
            Console.ReadKey();
        }
    }
}