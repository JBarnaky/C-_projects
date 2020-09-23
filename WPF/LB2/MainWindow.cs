using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Lab213052017
{
  class MainWindow : Window
    {
        
      
      
     

        Button b1 = new Button(); //клик кнопки

        TextBox tbResult = new TextBox(); //поле для ввода текста

        TextBox tbX = new TextBox(); //поле для ввода текста
        TextBox tbi = new TextBox(); //поле для ввода текста

        Label labelX = new Label(); //надпись текст для ввода возле
        Label labeli = new Label(); //надпись текст для ввода возле
        Label sin = new Label(); //надпись текст для ввода возле      
        Label cos = new Label(); //надпись текст для ввода возле
        Label tg = new Label(); //надпись текст для ввода возле
        Label min = new Label(); //надпись текст для ввода возле
        Label max = new Label(); //надпись текст для ввода возле

        RadioButton radiobat = new RadioButton();// точка для выбора из вариантов
        RadioButton radiobat1 = new RadioButton();
        RadioButton radiobat2 = new RadioButton();
        
        GroupBox groupBox = new GroupBox(){Header = "f(x)"};  // полувидимая черта

        CheckBox chex1 = new CheckBox();// это где выбираем птичку клик мин макс
        CheckBox chex2 = new CheckBox();

        double Nmin = double.MinValue;
        double Nmax = double.MaxValue;

        public MainWindow()
        
        {
            Title = "Lab2-1. Яцынович И.И. 60321-1";
            tbX.Width = 50; //размер окна
          //  tbX.Text = "";
            tbi.Width = 50;


            Canvas canvas = new Canvas();
            this.Content = canvas;


            b1.Content = "калькулятор";
            Canvas.SetTop(b1, 300);
            Canvas.SetLeft(b1, 50);
            canvas.Children.Add(b1);

            groupBox.Width = 100;
            groupBox.Height = 200;
            Canvas.SetTop(groupBox,50);
            Canvas.SetLeft(groupBox,300);
            canvas.Children.Add(groupBox);

            radiobat.Width = 110; 
            radiobat.Height = 210;
            Canvas.SetTop(radiobat,100);
            Canvas.SetLeft(radiobat,320);
            canvas.Children.Add(radiobat);

            radiobat1.Width = 120;
            radiobat1.Height = 220;
            Canvas.SetTop(radiobat1,150);
            Canvas.SetLeft(radiobat1,320);
            canvas.Children.Add(radiobat1);

            radiobat2.Width = 130;
            radiobat2.Height = 230;
            Canvas.SetTop(radiobat2,200);
            Canvas.SetLeft(radiobat2,320);
            canvas.Children.Add(radiobat2);

            chex1.Width = 210;
            chex1.Height = 220;
            Canvas.SetTop(chex1, 100);
            Canvas.SetLeft(chex1,450);
            canvas.Children.Add(chex1);

            chex2.Width = 230;
            chex2.Height = 240;
            Canvas.SetTop(chex2, 150);
            Canvas.SetLeft(chex2, 450);
            canvas.Children.Add(chex2);



           // labelX.Width = 200;
            labelX.Content = "X=";
            canvas.Children.Add(labelX);
            Canvas.SetTop(labelX, 60);
            Canvas.SetLeft(labelX, 20);

            labeli.Content = "i=";
            canvas.Children.Add(labeli);
            Canvas.SetTop(labeli, 100);
            Canvas.SetLeft(labeli, 20);


            sin.Content = "sin(x)";
            canvas.Children.Add(sin);
            Canvas.SetTop(sin, 100);
            Canvas.SetLeft(sin, 340);

            cos.Content = "cos(x)";
            canvas.Children.Add(cos);
            Canvas.SetTop(cos, 150);
            Canvas.SetLeft(cos, 340);

            tg.Content = "tg(x)";
            canvas.Children.Add(tg);
            Canvas.SetTop(tg, 200);
            Canvas.SetLeft(tg,340);

            min.Content = "min";
            canvas.Children.Add(min);
            Canvas.SetTop(min, 90);
            Canvas.SetLeft(min,480);
            //chbMax.Checked += ChbMax_Checked;


            max.Content = "max";
            canvas.Children.Add(max);
            Canvas.SetTop(max, 130);
            Canvas.SetLeft(max,480);
           
                   
            Canvas.SetTop(tbX, 60);
            Canvas.SetLeft(tbX, 60);
            canvas.Children.Add(tbX);

            Canvas.SetTop(tbi, 100);
            Canvas.SetLeft(tbi, 60);
            canvas.Children.Add(tbi); 

                     
            tbResult.Width = 200;
            tbResult.Height = 100;
            Canvas.SetTop(tbResult, 300);
            Canvas.SetLeft(tbResult, 300);
            canvas.Children.Add(tbResult);



            radiobat.Checked += Sin_Checked;
            radiobat1.Checked += Cos_Checked;
            radiobat2.Checked += Tg_Checked;
            chex1.Checked += Min_Checked;
            chex2.Checked += Max_Checked;


         
            b1.Click += new RoutedEventHandler(b1_Click);

        }

        void Sin_Checked(object sender, RoutedEventArgs a)
        {
        }
        void Cos_Checked(object sender, RoutedEventArgs a)
        {
        }
        void Tg_Checked(object sender, RoutedEventArgs a)
        {
        }
        void Min_Checked(object sender, RoutedEventArgs a)
        {
        }
        void Max_Checked(object sender, RoutedEventArgs a)
        {
        }

        void b1_Click(object sender, RoutedEventArgs a)
        {
           
            double i = double.Parse(tbi.Text);
            double x = double.Parse(tbX.Text);
            double fu = 0;
            double e = 0;
            string branch = "Ветка не выбрана";

            if (radiobat.IsChecked == true)
            {
                fu = Math.Sin(x);
            }
            else 
            if (radiobat1.IsChecked == true)
            {
                fu = Math.Cos(x);
            }
            else

                if (radiobat2.IsChecked == true)
            {
                fu = Math.Tan(x);
            }


            //Ветвление
             if (!(i%2==0) && x>0 )//i нечётное число, X>0
            {
                e = i*(Math.Sqrt(fu));
                branch = "Ветвь №1: i нечётное число, X>0";
            }
             else
            if ((i%2==0) && x<0)   //i чётное число, X<0

            {
                e = 1/2* (Math.Sqrt( Math.Abs(fu)));
                branch = "Ветвь №2: i чётное число, X<0";
            }

            else
           {
                e = Math.Sqrt(i*fu);
                branch = "Ветвь №3: без условия";
            }


            if (Nmin > e)
            {
                Nmin = e;
            }
            if (Nmax < e)
            {
                Nmax = e;
            }
            
            tbResult.Text = string.Format("e={0:0000e00}", e) + Environment.NewLine;
            tbResult.Text += branch + Environment.NewLine;

            if (chex1.IsChecked == true)
                tbResult.Text += string.Format("Min={0:0000e00}", Nmin) + Environment.NewLine;
            if (chex1.IsChecked == true)
                tbResult.Text += string.Format("Max={0:0000e00}", Nmax);




        }
    }
}