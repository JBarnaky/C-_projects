using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB3
{
    abstract class Stud //основной абстрактный класс
    {
        string name;
        DateTime birthDay;

        public DateTime BirthDay //свойство
        {
            get { return birthDay; }
            set { if (birthDay.Year > 1920 && birthDay.Year < 2000) birthDay = value; }
        }

        public string Name //свойство
        {
            get { return name; }
            set { name = value; }
        }

        public Stud(string name, DateTime birthDay) //конструктор
        {
            this.name = name;
            this.birthDay = birthDay;
        }

        public abstract void write_inf(); //вывод


        public int Age { get { return DateTime.Now.Year - birthDay.Year; } }


    }

    class Entrant : Stud //производный класс "Абитуриент"
    {
        string faculty;

        public string Faculty
        {
            get { return faculty; }
            set { if (faculty == "Tech" || faculty == "Programming") faculty = value; }
        }

        public Entrant(string name, DateTime birthDay, string faculty)
            : base(name, birthDay)
        {
            this.faculty = faculty;
        }

        public override void write_inf()
        {
            Console.WriteLine("Имя - {0}, Возраст - {1}, Факультет - {2}", Name, this.Age, faculty);
        }
    }

    class Student : Stud //производный класс "Студент"
    {
        string faculty;
        int kourse;

        public string Faculty
        {
            get { return faculty; }
            set { if (faculty == "Tech" || faculty == "Programming") faculty = value; }
        }

        public int Kourse
        {
            get { return kourse; }
            set { if (kourse > 0 && kourse < 7) kourse = value; }
        }

        public Student(string name, DateTime birthDay, string faculty, int kourse)
            : base(name, birthDay)
        {
            this.faculty = faculty;
            this.kourse = kourse;
        }

        public override void write_inf()
        {
            Console.WriteLine("Имя - {0}, Возраст - {1}, Факультет - {2}, Курс - {3}", Name, this.Age, faculty, kourse);
        }
    }

    class Teacher : Stud //производный класс "Преподаватель"
    {

        public Teacher(string name, DateTime birthDay) : base(name, birthDay) { }


        public override void write_inf()
        {
            Console.WriteLine("Имя - {0}, Возраст - {1}", Name, this.Age);
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            List<Stud> persons = new List<Stud>();
            persons.Add(new Student("Иван", new DateTime(1992, 2, 23), "Tech", 4));
            persons.Add(new Entrant("Петр", new DateTime(1996, 2, 23), "Programming"));
            persons.Add(new Teacher("Василий", new DateTime(1980, 2, 23)));
            foreach (Stud p in persons)
            {
                if (p is Student)
                    Console.Write("Студент: ");
                else if (p is Entrant)
                    Console.Write("Абитуриент: ");
                else
                    Console.Write("Преподаватель: ");
                p.write_inf();
            }

            Console.WriteLine("Лица старше 18 лет ");

            foreach (Stud p in persons)
            {
                if (p.Age > 18)
                    p.write_inf();
            }

            Console.ReadLine();

        }
    }
}