using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace kr6
{
    class Program
    {
        static bool EndWork;
        static ConsoleColor def_bg;
        static ConsoleColor def_fg;
        static TextReader def_in;
        static TextWriter def_out;
        static TextReader new_in;
        static TextWriter new_out;
        // Каталог дисков/песен с двумя уровнями вложенности
        // В основной хэш-таблице дисков в значении value для ключей имен дисков
        // записаны ссылки на хэш-таблицы песен, в которых для ключей имен песен
        // записаны наименования их исполнителей
        static Hashtable MusicCatalog;

        static void ToDefColor()
        {
            Console.BackgroundColor = def_bg;
            Console.ForegroundColor = def_fg;
        }

        static void ToRedColor()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Red;
        }

        static void ToInvertRedColor()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void ToRedOnBlackColor()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
        }

        static void ToDarkRedOnBlackColor()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkRed;
        }

        static void ToGreenColor()
        {
            Console.BackgroundColor = def_bg;
            Console.ForegroundColor = ConsoleColor.Green;
        }

        static void ToBlueColor()
        {
            Console.BackgroundColor = def_bg;
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        // Создать полное имя файла данных
        // Параметры
        // DataFileName: Имя файла данных без информации о path
        // Возвращает
        // Имя файла данных с информацией о path
        // (все файлы данных находятся в поддиректории \data директории, из которой запущен скрипт)
        static string GetDataPath(string DataFileName)
        {
            // Может выполняться под vs-хостом, поэтому, если есть, удаляется .vshost
            string script_path = Process.GetCurrentProcess().MainModule.FileName.Replace(".vshost", "");
            return Path.GetDirectoryName(script_path) + @"\data\" + DataFileName;
        }

        static void RedirectIn(string FileName)
        {
            if (new_in != null)
            {
                new_in.Close();
                new_in.Dispose();
                new_in = null;
            }
            new_in = new StreamReader(GetDataPath(FileName));
            Console.SetIn(new_in);
        }

        static void RedirectOut(string FileName)
        {
            if (new_out != null)
            {
                new_out.Flush();
                new_out.Close();
                new_out.Dispose();
                new_out = null;
            }
            new_out = new StreamWriter(GetDataPath(FileName));
            Console.SetOut(new_out);
        }

        static void ToDefInOut()
        {
            if (new_in != null)
            {
                new_in.Close();
                new_in.Dispose();
                new_in = null;
            }
            if (new_out != null)
            {
                new_out.Flush();
                new_out.Close();
                new_out.Dispose();
                new_out = null;
            }
            Console.SetIn(def_in);
            Console.SetOut(def_out);
        }

        static bool LaunchTask(string TaskId)
        {
            if (TaskId.Trim() == "1.1")
            {
                return Task_1_1();
            }
            else if (TaskId.Trim() == "1.2")
            {
                return Task_1_2();
            }
            else if (TaskId.Trim() == "1.3")
            {
                return Task_1_3();
            }
            else if (TaskId.Trim() == "1.4")
            {
                return Task_1_4();
            }
            else if (TaskId.Trim() == "2.1")
            {
                return Task_2_1();
            }
            else if (TaskId.Trim() == "2.2")
            {
                return Task_2_2();
            }
            else if (TaskId.Trim() == "2.3")
            {
                return Task_2_3();
            }
            else if (TaskId.Trim() == "2.4")
            {
                return Task_2_4();
            }
            else if (TaskId.Trim() == "3.1")
            {
                return Task_3_1();
            }
            else if (TaskId.Trim() == "3.2")
            {
                return Task_3_2();
            }
            else if (TaskId.Trim() == "3.3")
            {
                return Task_3_3();
            }
            else if (TaskId.Trim() == "3.4")
            {
                return Task_3_4();
            }
            else if (TaskId.Trim() == "4.1")
            {
                return Task_4_1();
            }
            else if (TaskId.Trim() == "5")
            {
                EndWork = true;
                return EndWork;
            }

            Console.WriteLine("");
            ToInvertRedColor();
            Console.WriteLine("ЗАДАН НЕПРАВИЛЬНЫЙ НОМЕР ЗАДАЧИ!");
            ToDefColor();
            Console.WriteLine("");
            return false;
        }

        static bool Task_1_1()
        {
            ToDefInOut();
            ToDefColor();
            try
            {
                try
                {
                    Console.WriteLine("");
                    ToRedOnBlackColor();
                    Console.WriteLine("Запуск задачи 1.1");
                    ToDefColor();
                    RedirectIn("task1_1.txt");
                    Stack<string> st = new Stack<string>();
                    string line = Console.ReadLine();
                    while (!string.IsNullOrWhiteSpace(line))
                    {
                        st.Push(line);
                        line = Console.ReadLine();
                    }
                    ToDefInOut();
                    Console.WriteLine("Содержимое входного файла task1_1.txt:");

                    // Все стандартные функции работы со Stack всегда просматривают его от конца к началу!
                    // Поэтому, только для распечатки:
                    string[] st_array = st.ToArray();
                    int st_len = st_array.Length - 1;
                    for (int i = st_len; i >= 0; --i)
                    {
                        string s = st_array[i];
                        Console.WriteLine(s);
                    }

                    RedirectOut("task1_1_out.txt");
                    while (st.Count > 0)
                    {
                        string s = st.Pop();
                        Console.WriteLine(s);
                    }
                    ToDefInOut();
                    ToGreenColor();
                    Console.WriteLine("Задача 1.1 выполнена");
                    ToDefColor();
                    Console.WriteLine("Содержимое выходного файла task1_1_out.txt:");
                    RedirectIn("task1_1_out.txt");
                    List<string> result = new List<string>();
                    line = Console.ReadLine();
                    while (!string.IsNullOrWhiteSpace(line))
                    {
                        result.Add(line);
                        line = Console.ReadLine();
                    }
                    ToDefInOut();
                    foreach (string s in result)
                    {
                        Console.WriteLine(s);
                    }
                    Console.WriteLine("");
                }
                // В любом случае восстановить консоль
                finally
                {
                    ToDefInOut();
                    ToDefColor();
                }
            }
            catch (IOException e)
            {
                TextWriter errorWriter = Console.Error;
                Console.WriteLine("");
                ToInvertRedColor();
                errorWriter.WriteLine("Ошибка ввода/вывода выполнения задачи 1.1 [{0}]", e.Message);
                ToDefColor();
                Console.WriteLine("");
                return false;
            }
            catch (Exception e)
            {
                TextWriter errorWriter = Console.Error;
                Console.WriteLine("");
                ToInvertRedColor();
                errorWriter.WriteLine("Ошибка выполнения задачи 1.1 [{0}]", e.Message);
                ToDefColor();
                Console.WriteLine("");
                return false;
            }

            return true;
        }

        static bool Task_1_2()
        {
            ToDefInOut();
            ToDefColor();

            string ru_vowels = "АЕЁИЙОУЫЭЮЯ"; 

            try
            {
                try
                {
                    Console.WriteLine("");
                    ToRedOnBlackColor();
                    Console.WriteLine("Запуск задачи 1.2");
                    ToDefColor();

                    Console.WriteLine("Введите содержимое входного файла task1_2.txt в виде отдельных строк.");
                    Console.WriteLine("Для окончания ввода задайте пустую строку:");
                    Console.WriteLine("");
                    string user_input = null;
                    List<string> in_text = new List<string>();
                    while (!string.IsNullOrWhiteSpace(user_input = Console.ReadLine()))
                    {
                        in_text.Add(user_input);
                    }
                    RedirectOut("task1_2.txt");
                    foreach (string s in in_text)
                    {
                        Console.WriteLine(s);
                    }
                    ToDefInOut();
                    RedirectIn("task1_2.txt");
                    Stack<char> st = new Stack<char>();
                    string line = null;
                    while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
                    {
                        for (int i = 0; i < line.Length; ++i)
                        {
                            string bukva = (new string(line[i], 1)).ToUpper();
                            if (ru_vowels.IndexOf(bukva) > -1)
                            {
                                st.Push(line[i]);
                            }
                        }
                    }
                    ToDefInOut();
                    while (st.Count > 0)
                    {
                        string s = new string(st.Pop(), 1);
                        Console.WriteLine(s);
                    }
                    ToGreenColor();
                    Console.WriteLine("Задача 1.2 выполнена");
                    ToDefColor();
                    Console.WriteLine("");
                }
                // В любом случае восстановить консоль
                finally
                {
                    ToDefInOut();
                    ToDefColor();
                }
            }
            catch (IOException e)
            {
                TextWriter errorWriter = Console.Error;
                Console.WriteLine("");
                ToInvertRedColor();
                errorWriter.WriteLine("Ошибка ввода/вывода выполнения задачи 1.2 [{0}]", e.Message);
                ToDefColor();
                Console.WriteLine("");
                return false;
            }
            catch (Exception e)
            {
                TextWriter errorWriter = Console.Error;
                Console.WriteLine("");
                ToInvertRedColor();
                errorWriter.WriteLine("Ошибка выполнения задачи 1.2 [{0}]", e.Message);
                ToDefColor();
                Console.WriteLine("");
                return false;
            }

            return true;
        }

        static bool Task_1_3()
        {
            ToDefInOut();
            ToDefColor();

            try
            {
                try
                {
                    Console.WriteLine("");
                    ToRedOnBlackColor();
                    Console.WriteLine("Запуск задачи 1.3");
                    ToDefColor();

                    Console.WriteLine(@"Введите имя входного файла без пути (файл д.б. в подкаталоге \data).");
                    Console.WriteLine("По-умолчанию, если задано пустое имя, то имя файла task1_3.txt:");
                    Console.WriteLine("");
                    ToRedColor();
                    string user_input = Console.ReadLine();
                    ToDefColor();
                    if (string.IsNullOrWhiteSpace(user_input))
                        user_input = "task1_3.txt";
                    RedirectIn(user_input);
                    List<Stack<char>> lst = new List<Stack<char>>();
                    string line = null;
                    while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
                    {
                        Stack<char> st_2 = new Stack<char>();
                        for (int i = 0; i < line.Length; ++i)
                        {
                            st_2.Push(line[i]);
                        }
                        lst.Add(st_2);
                    }
                    ToDefInOut();
                    foreach (Stack<char> st_2 in lst)
                    {
                        while (st_2.Count > 0)
                        {
                            Console.Write(st_2.Pop());
                        }
                        Console.Write(Environment.NewLine);
                    }
                    ToGreenColor();
                    Console.WriteLine("Задача 1.3 выполнена");
                    ToDefColor();
                    Console.WriteLine("");
                }
                // В любом случае восстановить консоль
                finally
                {
                    ToDefInOut();
                    ToDefColor();
                }
            }
            catch (IOException e)
            {
                TextWriter errorWriter = Console.Error;
                Console.WriteLine("");
                ToInvertRedColor();
                errorWriter.WriteLine("Ошибка ввода/вывода выполнения задачи 1.3 [{0}]", e.Message);
                ToDefColor();
                Console.WriteLine("");
                return false;
            }
            catch (Exception e)
            {
                TextWriter errorWriter = Console.Error;
                Console.WriteLine("");
                ToInvertRedColor();
                errorWriter.WriteLine("Ошибка выполнения задачи 1.3 [{0}]", e.Message);
                ToDefColor();
                Console.WriteLine("");
                return false;
            }

            return true;
        }

        static bool Task_1_4()
        {
            ToDefInOut();
            ToDefColor();

            try
            {
                try
                {
                    Console.WriteLine("");
                    ToRedOnBlackColor();
                    Console.WriteLine("Запуск задачи 1.4");
                    ToDefColor();
                    Console.WriteLine("Введите две строки s1 и s2:");
                    Console.WriteLine("");
                    ToRedColor();
                    string user_input1 = Console.ReadLine();
                    string user_input2 = Console.ReadLine();
                    ToDefColor();

                    bool is_revers = user_input1.Length == user_input2.Length;
                    if (is_revers)
                    {
                        Stack<char> st = new Stack<char>();
                        for (int i = 0; i < user_input1.Length; ++i)
                        {
                            st.Push(user_input1[i]);
                        }

                        int j = 0;
                        while (st.Count > 0)
                        {
                            if (st.Pop() != user_input2[j])
                            {
                                is_revers = false;
                                break;
                            }
                            ++j;
                        }
                    }

                    Console.WriteLine("");
                    if (is_revers)
                    {
                        ToBlueColor();
                        Console.WriteLine("Строка s2 обратна s1");
                    }
                    else
                    {
                        ToRedOnBlackColor();
                        Console.WriteLine("Строка s2 НЕ обратна s1");
                    }
                    Console.WriteLine("");
                    ToGreenColor();
                    Console.WriteLine("Задача 1.4 выполнена");
                    ToDefColor();
                    Console.WriteLine("");
                }
                // В любом случае восстановить консоль
                finally
                {
                    ToDefInOut();
                    ToDefColor();
                }
            }
            catch (IOException e)
            {
                TextWriter errorWriter = Console.Error;
                Console.WriteLine("");
                ToInvertRedColor();
                errorWriter.WriteLine("Ошибка ввода/вывода выполнения задачи 1.4 [{0}]", e.Message);
                ToDefColor();
                Console.WriteLine("");
                return false;
            }
            catch (Exception e)
            {
                TextWriter errorWriter = Console.Error;
                Console.WriteLine("");
                ToInvertRedColor();
                errorWriter.WriteLine("Ошибка выполнения задачи 1.4 [{0}]", e.Message);
                ToDefColor();
                Console.WriteLine("");
                return false;
            }

            return true;
        }

        static bool Task_2_1()
        {
            ToDefInOut();
            ToDefColor();

            try
            {
                try
                {
                    Console.WriteLine("");
                    ToRedOnBlackColor();
                    Console.WriteLine("Запуск задачи 2.1");
                    ToDefColor();

                    Console.WriteLine("Входной файл: task2_1.txt.");
                    Console.WriteLine("");

                    RedirectIn("task2_1.txt");
                    Queue<string> que_low = new Queue<string>();
                    Queue<string> que_high = new Queue<string>();
                    string[] stringSeparators = new string[] { "," };
                    string line = null;
                    while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
                    {
                        string[] fields = line.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                        if (int.Parse(fields[5]) < 10000)
                        {
                            que_low.Enqueue(line);
                        }
                        else
                        {
                            que_high.Enqueue(line);
                        }
                    }
                    ToDefInOut();

                    Console.WriteLine("Список сотрудников:");
                    while (que_low.Count > 0)
                    {
                        Console.WriteLine(que_low.Dequeue());
                    }
                    while (que_high.Count > 0)
                    {
                        Console.WriteLine(que_high.Dequeue());
                    }

                    Console.WriteLine("");
                    ToGreenColor();
                    Console.WriteLine("Задача 2.1 выполнена");
                    ToDefColor();
                    Console.WriteLine("");
                }
                // В любом случае восстановить консоль
                finally
                {
                    ToDefInOut();
                    ToDefColor();
                }
            }
            catch (IOException e)
            {
                TextWriter errorWriter = Console.Error;
                Console.WriteLine("");
                ToInvertRedColor();
                errorWriter.WriteLine("Ошибка ввода/вывода выполнения задачи 2.1 [{0}]", e.Message);
                ToDefColor();
                Console.WriteLine("");
                return false;
            }
            catch (Exception e)
            {
                TextWriter errorWriter = Console.Error;
                Console.WriteLine("");
                ToInvertRedColor();
                errorWriter.WriteLine("Ошибка выполнения задачи 2.1 [{0}]", e.Message);
                ToDefColor();
                Console.WriteLine("");
                return false;
            }

            return true;
        }

        static bool Task_2_2()
        {
            ToDefInOut();
            ToDefColor();

            try
            {
                try
                {
                    Console.WriteLine("");
                    ToRedOnBlackColor();
                    Console.WriteLine("Запуск задачи 2.2");
                    ToDefColor();

                    Console.WriteLine("Входной файл: task2_2.txt.");
                    Console.WriteLine("");

                    RedirectIn("task2_2.txt");
                    Queue<string> que_low = new Queue<string>();
                    Queue<string> que_high = new Queue<string>();
                    string[] stringSeparators = new string[] { "," };
                    string line = null;
                    while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
                    {
                        string[] fields = line.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                        if (int.Parse(fields[4]) < 30)
                        {
                            que_low.Enqueue(line);
                        }
                        else
                        {
                            que_high.Enqueue(line);
                        }
                    }
                    ToDefInOut();

                    Console.WriteLine("Список сотрудников:");
                    while (que_low.Count > 0)
                    {
                        Console.WriteLine(que_low.Dequeue());
                    }
                    while (que_high.Count > 0)
                    {
                        Console.WriteLine(que_high.Dequeue());
                    }

                    Console.WriteLine("");
                    ToGreenColor();
                    Console.WriteLine("Задача 2.2 выполнена");
                    ToDefColor();
                    Console.WriteLine("");
                }
                // В любом случае восстановить консоль
                finally
                {
                    ToDefInOut();
                    ToDefColor();
                }
            }
            catch (IOException e)
            {
                TextWriter errorWriter = Console.Error;
                Console.WriteLine("");
                ToInvertRedColor();
                errorWriter.WriteLine("Ошибка ввода/вывода выполнения задачи 2.2 [{0}]", e.Message);
                ToDefColor();
                Console.WriteLine("");
                return false;
            }
            catch (Exception e)
            {
                TextWriter errorWriter = Console.Error;
                Console.WriteLine("");
                ToInvertRedColor();
                errorWriter.WriteLine("Ошибка выполнения задачи 2.2 [{0}]", e.Message);
                ToDefColor();
                Console.WriteLine("");
                return false;
            }

            return true;
        }

        static bool Task_2_3()
        {
            ToDefInOut();
            ToDefColor();

            try
            {
                try
                {
                    Console.WriteLine("");
                    ToRedOnBlackColor();
                    Console.WriteLine("Запуск задачи 2.3");
                    ToDefColor();

                    Console.WriteLine("Входной файл: task2_3.txt.");
                    Console.WriteLine("");

                    RedirectIn("task2_3.txt");
                    Queue<string> que_low = new Queue<string>();
                    Queue<string> que_high = new Queue<string>();
                    string[] stringSeparators = new string[] { "," };
                    string line = null;
                    while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
                    {
                        string[] fields = line.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                        if ((int.Parse(fields[4]) < 3) || (int.Parse(fields[5]) < 3) || (int.Parse(fields[6]) < 3))
                        {
                            que_high.Enqueue(line);
                        }
                        else
                        {
                            que_low.Enqueue(line);
                        }
                    }
                    ToDefInOut();

                    Console.WriteLine("Список студентов:");
                    while (que_low.Count > 0)
                    {
                        Console.WriteLine(que_low.Dequeue());
                    }
                    while (que_high.Count > 0)
                    {
                        Console.WriteLine(que_high.Dequeue());
                    }

                    Console.WriteLine("");
                    ToGreenColor();
                    Console.WriteLine("Задача 2.3 выполнена");
                    ToDefColor();
                    Console.WriteLine("");
                }
                // В любом случае восстановить консоль
                finally
                {
                    ToDefInOut();
                    ToDefColor();
                }
            }
            catch (IOException e)
            {
                TextWriter errorWriter = Console.Error;
                Console.WriteLine("");
                ToInvertRedColor();
                errorWriter.WriteLine("Ошибка ввода/вывода выполнения задачи 2.3 [{0}]", e.Message);
                ToDefColor();
                Console.WriteLine("");
                return false;
            }
            catch (Exception e)
            {
                TextWriter errorWriter = Console.Error;
                Console.WriteLine("");
                ToInvertRedColor();
                errorWriter.WriteLine("Ошибка выполнения задачи 2.3 [{0}]", e.Message);
                ToDefColor();
                Console.WriteLine("");
                return false;
            }

            return true;
        }

        static bool Task_2_4()
        {
            ToDefInOut();
            ToDefColor();

            try
            {
                try
                {
                    Console.WriteLine("");
                    ToRedOnBlackColor();
                    Console.WriteLine("Запуск задачи 2.4");
                    ToDefColor();

                    Console.WriteLine("Входной файл: task2_4.txt.");
                    Console.WriteLine("");

                    RedirectIn("task2_4.txt");
                    Queue<string> que_low = new Queue<string>();
                    Queue<string> que_high = new Queue<string>();
                    string[] stringSeparators = new string[] { "," };
                    string line = null;
                    while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
                    {
                        string[] fields = line.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                        if ((int.Parse(fields[4]) < 4) || (int.Parse(fields[5]) < 4) || (int.Parse(fields[6]) < 4))
                        {
                            que_high.Enqueue(line);
                        }
                        else
                        {
                            que_low.Enqueue(line);
                        }
                    }
                    ToDefInOut();

                    Console.WriteLine("Список студентов:");
                    while (que_low.Count > 0)
                    {
                        Console.WriteLine(que_low.Dequeue());
                    }
                    while (que_high.Count > 0)
                    {
                        Console.WriteLine(que_high.Dequeue());
                    }

                    Console.WriteLine("");
                    ToGreenColor();
                    Console.WriteLine("Задача 2.4 выполнена");
                    ToDefColor();
                    Console.WriteLine("");
                }
                // В любом случае восстановить консоль
                finally
                {
                    ToDefInOut();
                    ToDefColor();
                }
            }
            catch (IOException e)
            {
                TextWriter errorWriter = Console.Error;
                Console.WriteLine("");
                ToInvertRedColor();
                errorWriter.WriteLine("Ошибка ввода/вывода выполнения задачи 2.4 [{0}]", e.Message);
                ToDefColor();
                Console.WriteLine("");
                return false;
            }
            catch (Exception e)
            {
                TextWriter errorWriter = Console.Error;
                Console.WriteLine("");
                ToInvertRedColor();
                errorWriter.WriteLine("Ошибка выполнения задачи 2.4 [{0}]", e.Message);
                ToDefColor();
                Console.WriteLine("");
                return false;
            }

            return true;
        }

        static bool Task_3_1()
        {
            ToDefInOut();
            ToDefColor();

            try
            {
                try
                {
                    Console.WriteLine("");
                    ToRedOnBlackColor();
                    Console.WriteLine("Запуск задачи 3.1");
                    ToDefColor();

                    Console.WriteLine("Входной файл: task3_1.txt.");
                    Console.WriteLine("");

                    RedirectIn("task3_1.txt");
                    ArrayList onelist = new ArrayList();
                    string[] stringSeparators = new string[] { "," };
                    string line = null;
                    int low = 0;
                    while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
                    {
                        string[] fields = line.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                        if (int.Parse(fields[5]) < 10000)
                        {
                            onelist.Insert(low, line);
                            ++low;
                        }
                        else
                        {
                            onelist.Add(line);
                        }
                    }
                    ToDefInOut();

                    Console.WriteLine("Список сотрудников:");
                    foreach (string s in onelist)
                    {
                        Console.WriteLine(s);
                    }
                    Console.WriteLine("");
                    ToGreenColor();
                    Console.WriteLine("Задача 3.1 выполнена");
                    ToDefColor();
                    Console.WriteLine("");
                }
                // В любом случае восстановить консоль
                finally
                {
                    ToDefInOut();
                    ToDefColor();
                }
            }
            catch (IOException e)
            {
                TextWriter errorWriter = Console.Error;
                Console.WriteLine("");
                ToInvertRedColor();
                errorWriter.WriteLine("Ошибка ввода/вывода выполнения задачи 3.1 [{0}]", e.Message);
                ToDefColor();
                Console.WriteLine("");
                return false;
            }
            catch (Exception e)
            {
                TextWriter errorWriter = Console.Error;
                Console.WriteLine("");
                ToInvertRedColor();
                errorWriter.WriteLine("Ошибка выполнения задачи 3.1 [{0}]", e.Message);
                ToDefColor();
                Console.WriteLine("");
                return false;
            }

            return true;
        }

        static bool Task_3_2()
        {
            ToDefInOut();
            ToDefColor();

            try
            {
                try
                {
                    Console.WriteLine("");
                    ToRedOnBlackColor();
                    Console.WriteLine("Запуск задачи 3.2");
                    ToDefColor();

                    Console.WriteLine("Входной файл: task3_2.txt.");
                    Console.WriteLine("");

                    RedirectIn("task3_2.txt");

                    ArrayList onelist = new ArrayList();
                    string[] stringSeparators = new string[] { "," };
                    string line = null;
                    int low = 0;
                    while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
                    {
                        string[] fields = line.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                        if (int.Parse(fields[4]) < 30)
                        {
                            onelist.Insert(low, line);
                            ++low;
                        }
                        else
                        {
                            onelist.Add(line);
                        }
                    }

                    ToDefInOut();
                    Console.WriteLine("Список сотрудников:");
                    foreach (string s in onelist)
                    {
                        Console.WriteLine(s);
                    }

                    Console.WriteLine("");
                    ToGreenColor();
                    Console.WriteLine("Задача 3.2 выполнена");
                    ToDefColor();
                    Console.WriteLine("");
                }
                // В любом случае восстановить консоль
                finally
                {
                    ToDefInOut();
                    ToDefColor();
                }
            }
            catch (IOException e)
            {
                TextWriter errorWriter = Console.Error;
                Console.WriteLine("");
                ToInvertRedColor();
                errorWriter.WriteLine("Ошибка ввода/вывода выполнения задачи 3.2 [{0}]", e.Message);
                ToDefColor();
                Console.WriteLine("");
                return false;
            }
            catch (Exception e)
            {
                TextWriter errorWriter = Console.Error;
                Console.WriteLine("");
                ToInvertRedColor();
                errorWriter.WriteLine("Ошибка выполнения задачи 3.2 [{0}]", e.Message);
                ToDefColor();
                Console.WriteLine("");
                return false;
            }

            return true;
        }

        static bool Task_3_3()
        {
            ToDefInOut();
            ToDefColor();

            try
            {
                try
                {
                    Console.WriteLine("");
                    ToRedOnBlackColor();
                    Console.WriteLine("Запуск задачи 3.3");
                    ToDefColor();

                    Console.WriteLine("Входной файл: task3_3.txt.");
                    Console.WriteLine("");

                    RedirectIn("task3_3.txt");

                    ArrayList onelist = new ArrayList();
                    string[] stringSeparators = new string[] { "," };
                    string line = null;
                    int high = 0;
                    while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
                    {
                        string[] fields = line.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                        if ((int.Parse(fields[4]) > 2) && (int.Parse(fields[5]) > 2) && (int.Parse(fields[6]) > 2))
                        {
                            onelist.Insert(high, line);
                            ++high;
                        }
                        else
                        {
                            onelist.Add(line);
                        }
                    }

                    ToDefInOut();

                    Console.WriteLine("Список студентов:");
                    foreach (string s in onelist)
                    {
                        Console.WriteLine(s);
                    }
                    Console.WriteLine("");

                    ToGreenColor();
                    Console.WriteLine("Задача 3.3 выполнена");
                    ToDefColor();
                    Console.WriteLine("");
                }
                // В любом случае восстановить консоль
                finally
                {
                    ToDefInOut();
                    ToDefColor();
                }
            }
            catch (IOException e)
            {
                TextWriter errorWriter = Console.Error;
                Console.WriteLine("");
                ToInvertRedColor();
                errorWriter.WriteLine("Ошибка ввода/вывода выполнения задачи 3.3 [{0}]", e.Message);
                ToDefColor();
                Console.WriteLine("");
                return false;
            }
            catch (Exception e)
            {
                TextWriter errorWriter = Console.Error;
                Console.WriteLine("");
                ToInvertRedColor();
                errorWriter.WriteLine("Ошибка выполнения задачи 3.3 [{0}]", e.Message);
                ToDefColor();
                Console.WriteLine("");
                return false;
            }

            return true;
        }

        static bool Task_3_4()
        {
            ToDefInOut();
            ToDefColor();

            try
            {
                try
                {
                    Console.WriteLine("");
                    ToRedOnBlackColor();
                    Console.WriteLine("Запуск задачи 3.4");
                    ToDefColor();

                    Console.WriteLine("Входной файл: task3_4.txt.");
                    Console.WriteLine("");

                    RedirectIn("task3_4.txt");

                    ArrayList onelist = new ArrayList();
                    string[] stringSeparators = new string[] { "," };
                    string line = null;
                    int high = 0;
                    while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
                    {
                        string[] fields = line.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                        if ((int.Parse(fields[4]) > 3) && (int.Parse(fields[5]) > 3) && (int.Parse(fields[6]) > 3))
                        {
                            onelist.Insert(high, line);
                            ++high;
                        }
                        else
                        {
                            onelist.Add(line);
                        }
                    }

                    ToDefInOut();

                    Console.WriteLine("Список студентов:");
                    foreach (string s in onelist)
                    {
                        Console.WriteLine(s);
                    }

                    Console.WriteLine("");
                    ToGreenColor();
                    Console.WriteLine("Задача 3.4 выполнена");
                    ToDefColor();
                    Console.WriteLine("");
                }
                // В любом случае восстановить консоль
                finally
                {
                    ToDefInOut();
                    ToDefColor();
                }
            }
            catch (IOException e)
            {
                TextWriter errorWriter = Console.Error;
                Console.WriteLine("");
                ToInvertRedColor();
                errorWriter.WriteLine("Ошибка ввода/вывода выполнения задачи 3.4 [{0}]", e.Message);
                ToDefColor();
                Console.WriteLine("");
                return false;
            }
            catch (Exception e)
            {
                TextWriter errorWriter = Console.Error;
                Console.WriteLine("");
                ToInvertRedColor();
                errorWriter.WriteLine("Ошибка выполнения задачи 3.4 [{0}]", e.Message);
                ToDefColor();
                Console.WriteLine("");
                return false;
            }

            return true;
        }

        static bool Disk(Hashtable node)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("");
                Console.WriteLine("СПИСОК ДИСКОВ:");
                Console.WriteLine("");
                int i = 0;
                foreach (DictionaryEntry de in node)
                {
                    ++i;
                    Console.WriteLine("{0}. {1}", i, (string)de.Key);
                }
                Console.WriteLine("");

                Console.WriteLine("Задачи для работы с дисками:");
                Console.WriteLine("1. Список всех дисков/песен");
                Console.WriteLine("2. Список всех песен исполнителя");
                Console.WriteLine("3. Работа со списком песен диска");
                Console.WriteLine("4. Добавить диск");
                Console.WriteLine("5. Удалить диск");
                Console.WriteLine("6. Выйти из списка дисков");
                Console.WriteLine("");

                Console.WriteLine("Введите номер задачи для выполнения в виде <№ пункта> (например 1) и нажмите Enter :");
                string user_input = Console.ReadLine();

                if (user_input.Trim() == "1")
                {
                    if (node.Count == 0)
                    {
                        Console.WriteLine("");
                        ToDarkRedOnBlackColor();
                        Console.WriteLine("Каталог дисков не заполнен. Создайте записи каталога!");
                        ToDefColor();
                        continue;
                    }

                    Console.WriteLine("");
                    Console.WriteLine("Список всех дисков/песен:");

                    i = 0;
                    foreach (DictionaryEntry de in node)
                    {
                        ++i;
                        Console.WriteLine("{0}. {1}", i, (string)de.Key);
                        Hashtable disk = (Hashtable)de.Value;
                        int j = 0;
                        foreach (DictionaryEntry de2 in disk)
                        {
                            ++j;
                            Console.WriteLine("\t{0}. {1}; исполнитель: {2}", j, (string)de2.Key, (string)de2.Value);
                        }
                    }

                }
                if (user_input.Trim() == "2")
                {
                    if (node.Count == 0)
                    {
                        Console.WriteLine("");
                        ToDarkRedOnBlackColor();
                        Console.WriteLine("Каталог дисков не заполнен. Создайте записи каталога!");
                        ToDefColor();
                        continue;
                    }

                    Console.WriteLine("");
                    Console.WriteLine("Введите наименование исполнителя и нажмите Enter :");
                    string user_input2 = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(user_input2)) continue;

                    Console.WriteLine("");
                    Console.WriteLine("Список всех песен исполнителя [{0}]:", user_input2);
                    i = 0;
                    foreach (DictionaryEntry de in node)
                    {
                        ++i;
                        Hashtable disk = (Hashtable)de.Value;
                        int j = 0;
                        foreach (DictionaryEntry de2 in disk)
                        {
                            ++j;
                            if (((string)de2.Value).Trim() == user_input2.Trim())
                            {
                                Console.WriteLine(
                                    "Диск: {0}; Песня: {1}; Исполнитель: {2}", 
                                    (string)de.Key, (string)de2.Key, (string)de2.Value);
                            }
                        }
                    }
                }
                if (user_input.Trim() == "3")
                {
                    if (node.Count == 0)
                    {
                        Console.WriteLine("");
                        ToDarkRedOnBlackColor();
                        Console.WriteLine("Каталог дисков не заполнен. Создайте записи каталога!");
                        ToDefColor();
                        continue;
                    }

                    Console.WriteLine("");
                    // Хэш-таблица не сортированная, имена дисков могут быть очень длинными.
                    // Поэтому, удобнее ссылаться на диск по его текущему физическому номеру в хэш-таблице.
                    // (этот номер отображается слева от имени диска в списке дисков)
                    Console.WriteLine("Введите порядковый номер (например 1) диска и нажмите Enter :");
                    string user_input3 = Console.ReadLine();

                    i = 0;
                    foreach (DictionaryEntry de in node)
                    {
                        ++i;
                        if (i.ToString() == user_input3.Trim())
                        {
                            Song((string)de.Key, (Hashtable)de.Value);
                        }
                    }
                }
                if (user_input.Trim() == "4")
                {
                    Console.WriteLine("");
                    Console.WriteLine("Введите наименование НОВОГО диска и нажмите Enter :");
                    string user_input4 = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(user_input4)) continue;

                    if (!string.IsNullOrWhiteSpace(user_input4.Trim()))
                    {
                        try
                        {
                            node.Add(user_input4.Trim(), new Hashtable());
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                if (user_input.Trim() == "5")
                {
                    if (node.Count == 0)
                    {
                        Console.WriteLine("");
                        ToDarkRedOnBlackColor();
                        Console.WriteLine("Каталог дисков не заполнен. Создайте записи каталога!");
                        ToDefColor();
                        continue;
                    }

                    Console.WriteLine("");
                    Console.WriteLine("Введите порядковый номер (например 1) диска ДЛЯ УДАЛЕНИЯ и нажмите Enter :");
                    string user_input5 = Console.ReadLine();

                    string disk_key = null;
                    i = 0;
                    foreach (DictionaryEntry de in node)
                    {
                        ++i;
                        if (i.ToString() == user_input5.Trim())
                        {
                            disk_key = (string)de.Key;
                        }
                    }

                    if (string.IsNullOrWhiteSpace(disk_key))
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Диск №{0} не найден.", user_input5.Trim());
                        continue;
                    }

                    Console.WriteLine("");
                    Console.WriteLine("Диск [{0}] и все его песни будут удалены. УДАЛИТЬ ДИСК?", disk_key);
                    Console.WriteLine("Для удаления диска введите 'Да' или любую строку для отмены удаления.");
                    string user_input6 = Console.ReadLine();
                    if (user_input6.Trim().ToUpper() == "ДА")
                    {
                        node.Remove(disk_key);
                    }
                }
                if (user_input.Trim() == "6")
                {
                    exit = true;
                }
            }
            return true;
        }

        static bool Song(string DiskName, Hashtable node)
        {
            bool exit = false;
            while (!exit)
            {

                Console.WriteLine("");
                Console.WriteLine("СПИСОК ПЕСЕН ДИСКА [{0}]:", DiskName);
                Console.WriteLine("");
                int i = 0;
                foreach (DictionaryEntry de in node)
                {
                    ++i;
                    Console.WriteLine("{0}. {1}; исполнитель: {2}", i, (string)de.Key, (string)de.Value);
                }
                Console.WriteLine("");

                Console.WriteLine("Задачи для работы с песнями:");
                Console.WriteLine("1. Добавить песню");
                Console.WriteLine("2. Удалить песню");
                Console.WriteLine("3. Выйти из списка песен");

                Console.WriteLine("Введите номер задачи для выполнения в виде <№ пункта> (например 1) и нажмите Enter :");
                string user_input = Console.ReadLine();

                if (user_input.Trim() == "1")
                {
                    Console.WriteLine("");
                    Console.WriteLine("Введите наименование НОВОЙ песни и нажмите Enter :");
                    string user_input1 = Console.ReadLine();
                    Console.WriteLine("Введите наименование исполнителя нажмите Enter :");
                    string user_input2 = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(user_input1.Trim()) ||
                        string.IsNullOrWhiteSpace(user_input2.Trim())) continue;

                    try
                    {
                        node.Add(user_input1.Trim(), user_input2.Trim());
                    }
                    catch (Exception ex)
                    {
                    }
                }
                if (user_input.Trim() == "2")
                {
                    if (node.Count == 0)
                    {
                        Console.WriteLine("");
                        ToDarkRedOnBlackColor();
                        Console.WriteLine("Каталог песен не заполнен. Создайте записи каталога!");
                        ToDefColor();
                        continue;
                    }

                    Console.WriteLine("");
                    Console.WriteLine("Введите порядковый номер (например 1) песни ДЛЯ УДАЛЕНИЯ и нажмите Enter :");
                    string user_input3 = Console.ReadLine();

                    string song_key = null;
                    i = 0;
                    foreach (DictionaryEntry de in node)
                    {
                        ++i;
                        if (i.ToString() == user_input3.Trim())
                        {
                            song_key = (string)de.Key;
                        }
                    }

                    if (string.IsNullOrWhiteSpace(song_key))
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Песня №{0} не найдена.", user_input3.Trim());
                        continue;
                    }

                    Console.WriteLine("");
                    Console.WriteLine("Песня [{0}] будет удалена. УДАЛИТЬ ПЕСНЮ?", song_key);
                    Console.WriteLine("Для удаления песни введите 'Да' или любую строку для отмены удаления.");
                    string user_input4 = Console.ReadLine();
                    if (user_input4.Trim().ToUpper() == "ДА")
                    {
                        node.Remove(song_key);
                    }
                }
                if (user_input.Trim() == "3")
                {
                    exit = true;
                }
            }

            return true;
        }

        static bool Task_4_1()
        {
            ToDefInOut();
            ToDefColor();

            try
            {
                try
                {
                    Console.WriteLine("");
                    ToRedOnBlackColor();
                    Console.WriteLine("Запуск задачи 4.1");
                    ToDefColor();

                    if (MusicCatalog == null)
                        MusicCatalog = new Hashtable();
                    Disk(MusicCatalog);

                    Console.WriteLine("");
                    ToGreenColor();
                    Console.WriteLine("Задача 4.1 выполнена");
                    ToDefColor();
                    Console.WriteLine("");
                }
                // В любом случае восстановить консоль
                finally
                {
                    ToDefInOut();
                    ToDefColor();
                }
            }
            catch (IOException e)
            {
                TextWriter errorWriter = Console.Error;
                Console.WriteLine("");
                ToInvertRedColor();
                errorWriter.WriteLine("Ошибка ввода/вывода выполнения задачи 4.1 [{0}]", e.Message);
                ToDefColor();
                Console.WriteLine("");
                return false;
            }
            catch (Exception e)
            {
                TextWriter errorWriter = Console.Error;
                Console.WriteLine("");
                ToInvertRedColor();
                errorWriter.WriteLine("Ошибка выполнения задачи 4.1 [{0}]", e.Message);
                ToDefColor();
                Console.WriteLine("");
                return false;
            }

            return true;
        }

        static void Main(string[] args)
        {
            def_in = Console.In;
            def_out = Console.Out;
            def_bg = Console.BackgroundColor;
            def_fg = Console.ForegroundColor;

            Console.SetBufferSize(120, Int16.MaxValue - 1);
            Console.SetWindowSize(120, 60);
            Console.Title = "Лр. №6";

            Console.WriteLine("");
            ToRedColor();
            Console.WriteLine("КОЛЛЕКЦИИ ОБЩЕГО НАЗНАЧЕНИЯ: СТЕК, ОЧЕРЕДЬ, ДИНАМИЧЕСКИЙ МАССИВ, ХЕШ-ТАБЛИЦА");
            ToDefColor();
            Console.WriteLine("");

            while (!EndWork)
            {
                ToGreenColor();
                Console.WriteLine("1. Решить следующие задачи с использованием класса Stack:");
                ToDefColor();
                Console.WriteLine("\t1. Дан файл, в котором записан набор чисел. Переписать в другой файл все числа в обратном порядке.");
                Console.WriteLine("\t2. Создать текстовый файл. Распечатать гласные буквы этого файла в обратном порядке.");
                Console.WriteLine("\t3. Напечатать содержимое текстового файла t, выписывая литеры каждой его строки в обратном порядке.");
                Console.WriteLine("\t4. Даны 2 строки s1 и s2. Из каждой можно читать по одному символу. Выяснить, является ли строка s2 обратной s1.");
                ToGreenColor();
                Console.WriteLine("2. Решить следующие задачи с использованием класса Queue:");
                ToDefColor();
                Console.WriteLine("\t1. Дан файл, содержащий информацию о сотрудниках фирмы:");
                Console.WriteLine("\t   фамилия, имя, отчество, пол, возраст, размер зарплаты.");
                Console.WriteLine("\t   За один просмотр файла напечатать элементы файла в следующем порядке:");
                Console.WriteLine("\t   сначала все данные о сотрудниках, зарплата которых меньше 10000, потом данные об остальных сотрудниках,");
                Console.WriteLine("\t   сохраняя исходный порядок в каждой группе сотрудников.");
                Console.WriteLine("\t2. Дан файл, содержащий информацию о сотрудниках фирмы:");
                Console.WriteLine("\t   фамилия, имя, отчество, пол, возраст, размер зарплаты.");
                Console.WriteLine("\t   За один просмотр файла напечатать элементы файла в следующем порядке:");
                Console.WriteLine("\t   сначала все данные о сотрудниках младше 30 лет, потом данные об остальных сотрудниках,");
                Console.WriteLine("\t   сохраняя исходный порядок в каждой группе сотрудников.");
                Console.WriteLine("\t3. Дан файл, содержащий информацию о студентах:");
                Console.WriteLine("\t   фамилия, имя, отчество, номер группы, оценки по трем предметам текущей сессии.");
                Console.WriteLine("\t   За один просмотр файла напечатать элементы файла в следующем порядке:");
                Console.WriteLine("\t   сначала все данные о студентах, успешно сдавших сессию, потом данные об остальных студентах,");
                Console.WriteLine("\t   сохраняя исходный порядок в каждой группе сотрудников.");
                Console.WriteLine("\t4. Дан файл, содержащий информацию о студентах:");
                Console.WriteLine("\t   фамилия, имя, отчество, номер группы, оценки по трем предметам текущей сессии.");
                Console.WriteLine("\t   За один просмотр файла напечатать элементы файла в следующем порядке:");
                Console.WriteLine("\t   сначала все данные о студентах, успешно обучающихся на 4 и 5, потом данные об остальных студентах,");
                Console.WriteLine("\t   сохраняя исходный порядок в каждой группе сотрудников.");
                ToGreenColor();
                Console.WriteLine("3. Решить предыдущие задачи п 2. используя класс ArrayList:");
                ToDefColor();
                Console.WriteLine("\t1. Дан файл, содержащий информацию о сотрудниках фирмы:");
                Console.WriteLine("\t   фамилия, имя, отчество, пол, возраст, размер зарплаты.");
                Console.WriteLine("\t   За один просмотр файла напечатать элементы файла в следующем порядке:");
                Console.WriteLine("\t   сначала все данные о сотрудниках, зарплата которых меньше 10000,");
                Console.WriteLine("\t   потом данные об остальных сотрудниках, сохраняя исходный порядок в каждой группе сотрудников.");
                Console.WriteLine("\t2. Дан файл, содержащий информацию о сотрудниках фирмы:");
                Console.WriteLine("\t   фамилия, имя, отчество, пол, возраст, размер зарплаты.");
                Console.WriteLine("\t   За один просмотр файла напечатать элементы файла в следующем порядке:");
                Console.WriteLine("\t   сначала все данные о сотрудниках младше 30 лет, потом данные об остальных сотрудниках,");
                Console.WriteLine("\t   сохраняя исходный порядок в каждой группе сотрудников.");
                Console.WriteLine("\t3. Дан файл, содержащий информацию о студентах:");
                Console.WriteLine("\t   фамилия, имя, отчество, номер группы, оценки по трем предметам текущей сессии.");
                Console.WriteLine("\t   За один просмотр файла напечатать элементы файла в следующем порядке:");
                Console.WriteLine("\t   сначала все данные о студентах, успешно сдавших сессию, потом данные об остальных студентах,");
                Console.WriteLine("\t   сохраняя исходный порядок в каждой группе сотрудников.");
                Console.WriteLine("\t4. Дан файл, содержащий информацию о студентах:");
                Console.WriteLine("\t   фамилия, имя, отчество, номер группы, оценки по трем предметам текущей сессии.");
                Console.WriteLine("\t   За один просмотр файла напечатать элементы файла в следующем порядке:");
                Console.WriteLine("\t   сначала все данные о студентах, успешно обучающихся на 4 и 5, потом данные об остальных студентах,");
                Console.WriteLine("\t   сохраняя исходный порядок в каждой группе сотрудников.");
                ToGreenColor();
                Console.WriteLine("4. Решить задачу, используя класс HashTable:");
                ToDefColor();
                Console.WriteLine("\t1. Каталог музыкальных компакт-дисков.");
                ToGreenColor();
                Console.WriteLine("5. Завершить работу.");
                ToDefColor();

                ToDefColor();
                Console.WriteLine("");
                ToBlueColor();
                Console.WriteLine("Введите номер задачи для выполнения в виде <пункт.подпункт> (например 1.1) и нажмите Enter :");
                ToRedColor();
                string user_input = Console.ReadLine();
                ToDefColor();
                LaunchTask(user_input);
                if (EndWork) break;

                Console.WriteLine("Для продолжения нажмите Enter...");
                Console.ReadLine();
            }
        }
    }
}
