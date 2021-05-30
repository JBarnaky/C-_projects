using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB4
{
    class Program
    {
            static void Main(string[] args)
        {
            Console.Write("Введите путь к каталогу: ");
            string path = Console.ReadLine();
            Console.Write("Введите маску для файлов: ");
            string mask = Console.ReadLine();
            if (Directory.Exists (path))
            {
                string[] files = Directory.GetFiles(path, mask, SearchOption.AllDirectories);
                for (int i = 0; files.Length > i; i++)
                {
                    FileStream fs = new FileStream("log.txt", FileMode.Append); // Projects\LB4\LB4\bin\Debug
                    StreamWriter sw = new StreamWriter(fs);
                    DateTime lastChange = File.GetLastWriteTime(files[i]);
                    Console.WriteLine(files[i] + " Был создан " + lastChange + ", ");
                    sw.WriteLine(files[i] + " Был создан " + lastChange + ", ");
                    sw.Close();
                }
                Console.WriteLine("log.txt создан ");
            }
            else
            {
                Console.WriteLine("Нет такой директории");
                Console.ReadKey();
            }
            Console.ReadKey();
            }
        }
    }