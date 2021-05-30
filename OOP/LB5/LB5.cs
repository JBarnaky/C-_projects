using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp3

{
    public static class Program
    {
        private static void Main()
        {
            // Читаем из файла
            Console.WriteLine("Читаем из файла...");
            string[] allLines = File.ReadAllLines("input.txt", Encoding.Default);
            // Преобразуем в массив студентов
            Student[] students = new Student[allLines.Length];
            for (int index = 0; index < allLines.Length; index++)
            {
                string line = allLines[index];
                string[] fields = line.Split(';');
                Student student = new Student(fields[0], Convert.ToInt32(fields[1]), Convert.ToInt32(fields[2]), Convert.ToInt32(fields[3]), Convert.ToInt32(fields[4]));
                students[index] = student;
            }
            // Сортируем
            Console.WriteLine("Сортируем по группе...");
            try
            {
                Array.Sort(students);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception 1 caught", e);
            }
            // Выводим данные
            Console.WriteLine("______________________________");
            try
            {
                foreach (Student student in students)
                {
                    Console.WriteLine(student);
                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception 2 caught", e);
            }
            Console.WriteLine("______________________________");
            // Преобразуем в удобный для записи вид
            string[] linesToSave = new string[students.Length];
            for (int i = 0; i < students.Length; i++)
            {
                Student student = students[i];
                linesToSave[i] = student.ToString();
            }
            // Сохраняем в файл
            Console.WriteLine("Сохраняем в файл...");
            try
            {
                foreach (Student student in students)
                {
                    if ((student.Mark1 >= 4) && (student.Mark2 >= 4) && (student.Mark3 >= 4))
                    {
                        File.AppendAllText("output.txt", String.Format("ФИО: {0}; Группа: {1}; Результаты сдачи 3х экзаменов: {2}; {3}; {4}; {5}",
                        student.Name, student.Group, student.Mark1, student.Mark2, student.Mark3, Environment.NewLine));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception 3 caught", e);
            }
            Console.ReadKey();
        }
    }

    public struct Student : IComparable
    {
        public Student(string fio, int num, int res1, int res2, int res3)
            : this()
        {
            Name = fio;
            Group = num;
            Mark1 = res1;
            Mark2 = res2;
            Mark3 = res3;
        }

        // ФИО
        public string Name { get; private set; }
        //Номер группы
        public int Group { get; private set; }
        //результаты сдачи 3х экзаменов
        public int Mark1 { get; private set; }
        public int Mark2 { get; private set; }
        public int Mark3 { get; private set; }

        public int CompareTo(object obj)
        {
            return Group.CompareTo(((Student)obj).Group);
        }

        public override string ToString()
        {
            return string.Format("{0}; {1}; {2}; {3}; {4}", Name, Group, Mark1, Mark2, Mark3);
        }
    }
}