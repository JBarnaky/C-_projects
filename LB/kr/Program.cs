using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{    
    public struct tarif
    {
        public string naim;
        public long cena;
    }//тип тариф
    public struct operac
    {
        public string fio;
        public string passport;
        public string tarif_naim;
        public string skidka;
        public long cena_it;
    }//тип операции

    
    class Tpassport
    {
        public int zz;//индекс элемента для изменения
        public long chi;//сумма на сколько изменить
      
        public static Tpassport operator -(Tpassport obj1, long obj2)//перегружаемый оператор
        {
            Tpassport arr = new Tpassport();
            arr = obj1;//делаем полную копию элемента            
            arr.dan_t[obj1.zz].cena = obj1.dan_t[obj1.zz].cena - obj2;//уменьшаем стоимость на заданное значение 
            return arr;
        }
        public void f7(int n2)//процедура для запроса параметров для уменьшения стоимости тарифа
        {
            Console.Write("\nДанные по тарифам:");
            Print(n2, true);//выводим список тарифов
            do
            {
                Console.Write("Введите № тарифа по которому хотите уменьшить стоимость = "); zz = Convert.ToInt32(Console.ReadLine());
                if ((zz < 1) || (zz > n2)) { Console.WriteLine("Нет такого тарифа!!! Повторите ввод"); }
                else { break; };
            } while (true);//пользователь вводит № тарифа до тех пор, пока не введет №, который существует
            Console.Write("Введите сумму на которую хотите ее уменьшить = "); chi = Convert.ToInt64(Console.ReadLine());                        
        }     

        public operac[] dan_o = new operac[100];
        public tarif[] dan_t = new tarif[20];        

        public void Input(int n_o, int n_t, Boolean p)//процедура ввода данных
        {
            long ch = 0;
            long zn = 0;
            long ntt = 0;
            Console.Clear();
            if (p == false)//вводим данные по операции
            {
                if (n_t == 0)
                {
                    Console.WriteLine("Заведите сначала тарифы");
                }
                else
                {
                    Console.Write("\nДанные по тарифам:");
                    Print(n_t, true);
                    Console.WriteLine("");
                    Console.WriteLine("\nВведите данные по операции № " + n_o);
                    Console.Write("ФИО= "); dan_o[n_o].fio = Console.ReadLine();
                    Console.Write("ПАСПОРТ= "); dan_o[n_o].passport = Console.ReadLine();
                    Console.Write("ТАРИФ= (Введите его №)"); ntt = Convert.ToInt32(Console.ReadLine()); dan_o[n_o].tarif_naim = dan_t[ntt].naim;
                    Console.Write("Вы выбрали ТАРИФ=" + dan_o[n_o].tarif_naim);
                    Console.Write("\nСКИДКА (Пример: 100 или 5%)= "); dan_o[n_o].skidka = Console.ReadLine();
                    zn = dan_t[ntt].cena;
                    if (zn < 0) { zn = 0; };
                    int pos = dan_o[n_o].skidka.IndexOf("%");
                    if (pos > -1) { ch = Convert.ToInt64(dan_o[n_o].skidka.Replace("%", "")); dan_o[n_o].cena_it = zn - ((zn * ch) / 100); }
                    else { ch = Convert.ToInt64(dan_o[n_o].skidka); dan_o[n_o].cena_it = zn - ch; }
                    Console.Write("ЦЕНА= " + dan_o[n_o].cena_it);
                    Console.WriteLine("\nДля продолжения нажмите любую клавишу..."); Console.ReadKey();
                }
            }
            if (p == true)//вводим данные по тарифам
            {
                Console.WriteLine("\nВведите данные по тарифу № " + n_o);
                do
                {
                    Console.Write("НАИМЕНОВАНИЕ= "); dan_t[n_o].naim = Console.ReadLine();
                    if (dan_t[n_o].naim.Length < 10) { Console.WriteLine("Длина строки < 10 символов!!! Повторите ввод"); }
                    else { break; };
                } while (true);
                Console.Write("ЦЕНА= "); dan_t[n_o].cena = Convert.ToInt64(Console.ReadLine());
            }
        }
        public void Print(int n, Boolean p)//процедура вывода
        {
            if (p == false)//выводим данные по операции
            {
                for (int i = 1; i <= n; i++)
                {
                    Console.Write("\nДанные по операции № " + i);
                    Console.Write("\nФИО= " + dan_o[i].fio);
                    Console.Write(" | ПАСПОРТ= " + dan_o[i].passport);
                    Console.Write(" | ТАРИФ= " + dan_o[i].tarif_naim);
                    Console.Write(" | СКИДКА (Пример: 100 или 5%)= " + dan_o[i].skidka);
                    Console.Write(" | ЦЕНА= " + Convert.ToString(dan_o[i].cena_it));
                }
            }
            if (p == true)//выводим данные по тарифам
            {
                for (int i = 1; i <= n; i++)
                {
                    Console.Write("\nДанные по тарифу № " + i);
                    Console.Write("\nНАИМЕНОВАНИЕ= " + dan_t[i].naim);
                    Console.Write(" | ЦЕНА= " + dan_t[i].cena);
                }
            }
            Console.WriteLine("\nДля продолжения нажмите любую клавишу..."); Console.ReadKey();
        }
        public void Srzn(int n)//процедура для рассчета средней стоимости проданных билетов
        {
            long sm = 0;
            long sr = 0;
            for (int i = 1; i <= n; i++) { sm = sm + Convert.ToInt64(dan_o[i].cena_it); }
            sr = sm / n;
            Console.WriteLine("\nСредняя стоимость проданных билетов = " + Convert.ToString(sr));
            Console.WriteLine("\nДля продолжения нажмите любую клавишу..."); Console.ReadKey();
        }
        public void Spb(int n, int n2)//процедура для рассчета суммы проданных билетов с учетом скидок
        {
            long sm = 0;
            int t = 0;
            Console.Write("\nДанные по тарифам:");
            Print(n2, true);
            do
            {
                Console.Write("Введите № тарифа по которому хотите сумму проданных билетов с учетом скидок = "); t = Convert.ToInt32(Console.ReadLine());
                if ((t < 1) || (t > n2)) { Console.WriteLine("Нет такого тарифа!!! Повторите ввод"); }
                else { break; };
            } while (true);//пользователь вводит № тарифа до тех пор, пока не введет №, который существует
            for (int i = 1; i <= n; i++) { sm = sm + Convert.ToInt64(dan_o[i].cena_it); }
            Console.WriteLine("\nСумма проданных билетов с учетом скидок = " + Convert.ToString(sm));
            Console.WriteLine("\nДля продолжения нажмите любую клавишу..."); Console.ReadKey();
        }        
    }

    class Program
    {
        static void Main(string[] args)
        {
            const string PressAnyKey = "\nДля продолжения нажмите любую клавишу...";
            Tpassport ff = new Tpassport();
            
            int n_o, n_t;
            int item;
            n_o = 0; n_t = 0;            
            do//меню пользователя
            {
                do
                {
                    Console.Clear();
                    Console.Write("\nВыберите одно из следующих действий:");
                    Console.Write("\n 1. Вводить данные о тарифах");
                    Console.Write("\n 2. Вводить паспортные данные пассажира и регистрировать покупку билета");
                    Console.Write("\n 3. Вывести данные о тарифах");
                    Console.Write("\n 4. Вывести паспортные данные пассажира и данные покупки билета");
                    Console.Write("\n 5. Рассчитать среднюю стоимость проданных билетов");
                    Console.Write("\n 6. По введенному наименованию направления высчитать сумму проданных билетов с учетом предоставленных скидок");
                    Console.Write("\n 7. Изменить цену тарифа");
                    Console.Write("\n 8. Выход в Windows");
                    Console.Write("\n Введите номер выбранного действия: ");
                    item = 0;
                    try
                    { item = Convert.ToInt16(Console.ReadLine()); }
                    catch (FormatException ex)//проверка на ошибки
                    { Console.WriteLine(ex.Message + "\nНеобходимо вводить целые числа от 1 до 7 " + PressAnyKey); Console.ReadKey(); }

                } while ((item < 1) || (item >8));
                switch (item)//обработка команд пользователя
                {
                    case 1: { n_t = n_t + 1; ff.Input(n_t, n_t, true); break; }
                    case 2: { n_o = n_o + 1; ff.Input(n_o, n_t, false); break; }
                    case 3: { ff.Print(n_t, true); break; }
                    case 4: { ff.Print(n_o, false); break; }
                    case 5: { ff.Srzn(n_o); break; }
                    case 6: { ff.Spb(n_o, n_t); break; }
                    case 7: { ff.f7(n_t); ff = ff - ff.chi; break; }
                    case 8: { item = 0; break; }
                }
                if (item == 0) break;
            } while (true);
        }
    }
}
