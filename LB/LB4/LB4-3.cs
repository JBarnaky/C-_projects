using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApplication1
{
    class FileDir
    {
        public string Name;
        public int DirFile;
        public FileDir(string Name, int DirFile)
        {
            this.Name = Name;
            this.DirFile = DirFile;
        }
    }
    class Program
    {
        static void POISKKOREN(string file, string Dir, ref List<FileDir> allList)
        {
            try
            {
                string[] MyDir1 = Directory.GetDirectories(@Dir, @"*", SearchOption.TopDirectoryOnly);
                string[] MyDir = Directory.GetDirectories(@Dir, @file, SearchOption.TopDirectoryOnly);
                string[] MyFile = Directory.GetFiles(@Dir, @file, SearchOption.TopDirectoryOnly);
                foreach (string b in MyFile)
                {
                    allList.Add(new FileDir(b, -2));
                }
                foreach (string a in MyDir1)
                {
                    string[] MyDirDir1 = Directory.GetDirectories(a, @file, SearchOption.AllDirectories);

                    string[] MyDirDir = Directory.GetFiles(a, @file, SearchOption.AllDirectories);
                    foreach (string b in MyDirDir)
                    {
                        allList.Add(new FileDir(b, -2));
                    }
                }
                foreach (string a in MyDir1)
                {
                    string[] MyDirDir1 = Directory.GetDirectories(a, @file, SearchOption.AllDirectories);
                    foreach (string b in MyDirDir1)
                    {
                        allList.Add(new FileDir(b, -1));
                    }
                }
                foreach (string b in MyDir)
                {
                    allList.Add(new FileDir(b, -1));
                }
                if (allList.Count == 0 && MyFile.Length == 0) { throw new Exception(); }

            }
            catch (FormatException) { Console.WriteLine("Некорректный ввод параметров поиска "); }
            catch (Exception)
            {
                Console.WriteLine("Файл не найден ");
                Console.WriteLine("Проверьте правильность параметров поиска ");
            }
        }
        static void Vivod(List<FileDir> mass)
        {
            Console.WriteLine("------------------");
            int i = 0;
            foreach (FileDir b in mass)
            {
                string h = b.Name;
                Console.WriteLine(++i + "." + h);
            }
            Console.WriteLine("------------------");
        }

        static void deleteArray(ref List<string> allListmain, int nomer, int collichestvo)
        {
            allListmain.RemoveRange(nomer, collichestvo);
        }
        static void Delete(ref List<FileDir> allListmain, int nomer)
        {
            FileDir b = allListmain[nomer - 1];
            int h = b.DirFile;
            if (h == -1)
            {
                Directory.Delete(b.Name, true);
            }
            else
            { File.Delete(b.Name); }
            allListmain.RemoveRange(nomer - 1, 1);
        }

        static void Delete(ref List<FileDir> allListmain, int First, int Last)
        {
            for (int i = First; i <= Last; i++)
            {
                FileDir b = allListmain[i - 1];
                int h = b.DirFile;
                if (h == -1)
                {
                    Directory.Delete(b.Name, true);
                }
                else
                {
                    File.Delete(b.Name);
                }
            }

            allListmain.RemoveRange(First - 1, Last - First + 1);
        }
        static void Ex1()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Неверный формат ввода! ");
            Console.WriteLine();
            Console.WriteLine();
        }
        static void Ex2()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Значения введены некорректно! ");
            Console.WriteLine();
            Console.WriteLine();
        }

        static void Ex3()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Файлы/директории удалены ");
            Console.WriteLine();
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            int YESNO = 1;
            while (YESNO == 1)
            {
                try
                {
                    List<FileDir> allListmain = new List<FileDir>(1000);
                    Console.Write("Введите маску: ");
                    string file = Console.ReadLine();
                    Console.Write("Введите директорию поиска: ");
                    string Dir = Console.ReadLine();
                    POISKKOREN(file, Dir, ref allListmain);
                    Console.WriteLine("Результаты поиска: ");
                    Console.WriteLine();
                    Vivod(allListmain);

                    while (true)
                    {
                        try
                        {
                            Console.WriteLine("1.Новый поиск ");
                            Console.WriteLine("2.Удалить файл/директорию ");
                            Console.WriteLine("3.Удалить диапазон файлов/директорий ");
                            Console.WriteLine("4.Удалить все ");
                            int t = int.Parse(Console.ReadLine());
                            if (t == 1)
                            {
                                break;
                            }
                            else
                            {
                                if (t == 2)
                                {
                                    try
                                    {
                                        Console.WriteLine("Введите номер файла/директории ");
                                        int nomer = int.Parse(Console.ReadLine());
                                        Delete(ref allListmain, nomer);
                                        Ex3();
                                        Vivod(allListmain);

                                    }
                                    catch (FormatException)
                                    {
                                        Ex1();
                                    }
                                    catch (ArgumentOutOfRangeException)
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine();
                                        Console.WriteLine("Номер введенного файла/директории не найден ");
                                    }
                                }
                                else
                                {
                                    if (t == 3)
                                    {
                                        try
                                        {
                                            Console.WriteLine("Введите номер с которого начать удаление ");
                                            int first = int.Parse(Console.ReadLine());
                                            Console.WriteLine("Введите номер до которого удалять ");
                                            int Last = int.Parse(Console.ReadLine());
                                            if (first < 1 || first > allListmain.Count || Last < first || Last > allListmain.Count) { throw new Exception(); }

                                            Delete(ref allListmain, first, Last);

                                            Vivod(allListmain);
                                            Ex3();
                                        }

                                        catch (FormatException)
                                        {
                                            Ex1();
                                        }
                                        catch (Exception)
                                        {
                                            Ex2();
                                        }
                                    }
                                    else
                                    {
                                        if (t == 4)
                                        {
                                            Delete(ref allListmain, 1, allListmain.Count);
                                            Ex3();
                                        }
                                        else { if (t < 0 || t > 4) { throw new Exception(); } }
                                    }
                                }
                            }
                        }
                        catch (Exception) { Ex1(); }
                    }
                }
                catch (FormatException) { Ex1(); }
            }
        }
    }
}