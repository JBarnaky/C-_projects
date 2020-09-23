
using System;
using System.Windows;
using System.Windows.Controls;

namespace ConsoleApplication1
{
    class MainWindow : Window
    {
        Button b1 = new Button(); //клик кнопки

         TextBox tbResult = new TextBox();

        TextBox tbX = new TextBox(); //писать xyz
        TextBox tbY = new TextBox();
        TextBox tbZ = new TextBox();

        Label labelX = new Label(); //для написания цифр ячейки
        Label labelY = new Label();
        Label labelZ = new Label();


        public MainWindow()
        {

            tbX.Text = "2,444";
            tbY.Text = "0,00869";
            tbZ.Text = "-130,0";

            Canvas canvas = new Canvas();
            this.Content = canvas;


            b1.Content = "калькулятор";
            Canvas.SetTop(b1, 160);
            Canvas.SetLeft(b1, 40);
            canvas.Children.Add(b1);


           // labelY.Width = 40;
            labelY.Content = "Y=";
            canvas.Children.Add(labelY);
            Canvas.SetTop(labelY, 100);
            Canvas.SetLeft(labelY, 20);


           // labelX.Width = 60;
            labelX.Content = "X=";
            canvas.Children.Add(labelX);
            Canvas.SetTop(labelX, 60);
            Canvas.SetLeft(labelX, 20);

           // labelZ.Width = 80;
            labelZ.Content = "Z=";
            canvas.Children.Add(labelZ);
            Canvas.SetTop(labelZ, 20);
            Canvas.SetLeft(labelZ, 20);

            Canvas.SetTop(tbX, 60);
            Canvas.SetLeft(tbX, 60);
            canvas.Children.Add(tbX);

            Canvas.SetTop(tbY, 100);
            Canvas.SetLeft(tbY, 60);
            canvas.Children.Add(tbY);

            Canvas.SetTop(tbZ, 20);
            Canvas.SetLeft(tbZ, 60);
            canvas.Children.Add(tbZ);

            Canvas.SetTop(tbResult, 20);
            Canvas.SetLeft(tbResult, 180);
            canvas.Children.Add(tbResult);


                    
            b1.Click += new RoutedEventHandler(b1_Click);

        }

        void b1_Click(object sender, RoutedEventArgs e)
        {
            double x = double.Parse(tbX.Text);
            double y = double.Parse(tbY.Text);
            double z = double.Parse(tbZ.Text);
            double a = Math.Pow(x, (y + 1)) + Math.Pow(Math.E,(y - 1));
            double b = 1 + x * Math.Abs(y - Math.Tan(z));
            double c = 1 + Math.Abs(y - x);
            double d = Math.Pow(Math.Abs(y - x), 2) / 2 - Math.Pow(Math.Abs(y - x), 3) / 3;
            double h = a / b * c + d;
            tbResult.Text = "Lab1" + Environment.NewLine;
            tbResult.Text = string.Format("h={0:00000e000}", h);


        }
    }
}


//namespace ConsoleApplication3
//{
//    class MainWindow : Window
//    {
//        Button b1 = new Button();
//        TextBox tb1 = new TextBox();

//        public MainWindow()
//        {
//            Canvas canvas = new Canvas();
//            this.Content = canvas;


//            b1.Content = "b1";
//            Canvas.SetLeft(b1, 100);
//            canvas.Children.Add(b1);

//            tb1.Width = 80;
//            canvas.Children.Add(tb1);
//            Canvas.SetTop(tb1, 120);

//            b1.Click += new RoutedEventHandler(b1_Click);

//        }

//        void b1_Click(object sender, RoutedEventArgs e)
//        {
//            tb1.Text = "hello";
//        }
//    }
//}
