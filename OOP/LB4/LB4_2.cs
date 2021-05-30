using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LB4
{
        class Search
        {
            static void Main()
            {
                Console.Write("Введите путь к каталогу: ");
                string Path = Console.ReadLine();
                Console.Write("Введите маску для файлов: ");
                string Mask = Console.ReadLine();
                Console.Write("Введите текст для поиска в файлах: ");
                string Text = Console.ReadLine();

                // Дописываем слэш (в случае его отсутствия)
                if (Path[Path.Length - 1] != '\\')
                    Path += '\\';

                DirectoryInfo di = new DirectoryInfo(Path);

                if (!di.Exists)
                {
                    Console.WriteLine("Некорректный путь!!!");
                    return;
                }

                Mask = Mask.Replace(".", @"\." /* (".", "\\.") */);
                // Заменяем ? на .
                Mask = Mask.Replace("?", ".");
                // Заменяем * на .*
                Mask = Mask.Replace("*", ".*");
                // Указываем, что требуется найти точное соответствие маске
                Mask = "^" + Mask + "$";

                Regex regMask = new Regex(Mask, RegexOptions.IgnoreCase);

                // Экранируем спецсимволы во введенном тексте
                Text = Regex.Escape(Text);

                Regex regText = new Regex(Text, RegexOptions.IgnoreCase);

                ulong Count = FindTextInFiles(di, regText, regMask);
                Console.WriteLine("Всего обработано файлов: {0}.", Count);
                Console.ReadKey();
            }

            static ulong FindTextInFiles(DirectoryInfo di, Regex regText, Regex regMask)
            {
                StreamReader sr = null;
                MatchCollection mc = null;

                ulong CountOfMatchFiles = 0;

                FileInfo[] fi = null;
                try
                {
                    fi = di.GetFiles();
                }
                catch
                {
                    return CountOfMatchFiles;
                }

                foreach (FileInfo f in fi)
                {
                    if (regMask.IsMatch(f.Name))
                    {
                        ++CountOfMatchFiles;
                        Console.WriteLine("Файл " + f.Name + ":");

                        sr = new StreamReader(di.FullName + @"\" + f.Name,
                            Encoding.Default);
                        string Content = sr.ReadToEnd();
                        sr.Close();
                        mc = regText.Matches(Content);
                        foreach (Match m in mc)
                        {
                            Console.WriteLine("Текст найден в позиции {0}.", m.Index);
                        }
                        if (mc.Count == 0)
                        {
                            Console.WriteLine("В данном файле запрошенный текст не найден.");
                        }
                    }
                }

                Console.WriteLine("Количество обработанных файлов в каталоге {0} {1}",
                        di.FullName, CountOfMatchFiles);

                DirectoryInfo[] diSub = di.GetDirectories();
                foreach (DirectoryInfo diSubDir in diSub)
                    CountOfMatchFiles += FindTextInFiles(diSubDir, regText, regMask);

                return CountOfMatchFiles;
            }
        }
    }