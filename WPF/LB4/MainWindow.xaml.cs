using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using control_lib;

namespace lab4_paint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static List<Props> lstProps = new List<Props>();

        public MainWindow()
        {
            InitializeComponent();
            this.MouseMove += MainWindow_MouseMove;
            this.MouseDown += MainWindow_MouseDown;
            CommandBinding saveBinding = new CommandBinding(ApplicationCommands.Save);
            saveBinding.Executed += SaveBinding_Executed;
            saveBinding.CanExecute += SaveBinding_CanExecute;
            this.CommandBindings.Add(saveBinding);
    }

        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            double x = e.GetPosition(canvas_status).X;
            double y = e.GetPosition(canvas_status).Y;

            if (x >= 0 && y >= 0)
                status_bar.Content = $"Координаты: x={x:0} y={y:0}";
            else
                status_bar.Content = "";
        }

        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            double x = e.GetPosition(canvas_status).X;
            double y = e.GetPosition(canvas_status).Y;
            UserControl1 uc1 = new UserControl1();

            if (status_bar_LC.Content != null)
            {
                uc1.WBrush = new SolidColorBrush((Color)new System.Windows.Media.ColorConverter().ConvertFrom(status_bar_LC.Content));
            }

            if (status_bar_FC.Content != null)
            {
                uc1.WBrush_Fill = new SolidColorBrush((Color)new System.Windows.Media.ColorConverter().ConvertFrom(status_bar_FC.Content));
            }

            if (status_bar_LT.Content == null)
            {
                uc1.WCross = 1;
            }

            Canvas.SetLeft(uc1, x);
            Canvas.SetTop(uc1, y);
            canvas_status.Children.Add(uc1);

            string lineColor = Convert.ToString(uc1.WBrush);
            string fillColor = Convert.ToString(uc1.WBrush_Fill);
            int lineThickness = Convert.ToInt32(uc1.WCross);

            Props infProps = new Props(lineColor, fillColor, lineThickness, x, y);
            lstProps.Add(infProps);
        }

        private void SaveBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = canvas_status.Children.Count > 0;
        }

        private void SaveBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            
            foreach (Props WriteParams in lstProps)
            {
                StreamWriter ConsumptionWriter = new StreamWriter(saveFileDialog.FileName, true, Encoding.Default);
                ConsumptionWriter.Write("{0}|", WriteParams.LineColor);
                ConsumptionWriter.Write("{0}|", WriteParams.FillColor);
                ConsumptionWriter.Write("{0}|", WriteParams.LineThickness);
                ConsumptionWriter.Write("{0}|", WriteParams.X);
                ConsumptionWriter.WriteLine("{0};", WriteParams.Y);
                ConsumptionWriter.Close();
            }
        }


        private void MenuItem_New(object sender, RoutedEventArgs e)
        {
            canvas_status.Children.Clear();
            lstProps.Clear();
        }

        private void MenuItem_Open(object sender, RoutedEventArgs e)
        {
            
            lstProps.Clear();
            canvas_status.Children.Clear();

            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                ofd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                ofd.InitialDirectory = @"d:\work\";

                string[] allLines = File.ReadAllLines(ofd.FileName, Encoding.Default);

                for (int i = 0; i < allLines.Length; i++)
                {
                    string line = allLines[i];
                    string[] inf = line.Split('|', ';');
                    string lineColor = inf[0];
                    string fillColor = inf[1];
                    int lineThickness = Convert.ToInt32(inf[2]);
                    double x = Convert.ToDouble(inf[3]);
                    double y = Convert.ToDouble(inf[4]);


                    {
                        UserControl1 uc1 = new UserControl1();
                        uc1.WCross = lineThickness;

                        uc1.WBrush = new SolidColorBrush((Color)new System.Windows.Media.ColorConverter().ConvertFrom(lineColor));
                        uc1.WBrush_Fill = new SolidColorBrush((Color)new System.Windows.Media.ColorConverter().ConvertFrom(fillColor));

                        Canvas.SetLeft(uc1, x);
                        Canvas.SetTop(uc1, y);
                        canvas_status.Children.Add(uc1);
                    }

                }
                this.Title = Convert.ToString(ofd.FileName);
            }
        }

        private void MenuItem_Save(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItem_About(object sender, RoutedEventArgs e)
        {
            WindowAbout wa = new WindowAbout();
            wa.ShowDialog();
        }

        private void MenuItem_Properties(object sender, RoutedEventArgs e)
        {
            Window_Properties dialog = new Window_Properties();
            if (dialog.ShowDialog() == true)
            {
                Props prop = dialog.Props;
                status_bar_LT.Content = prop.LineThickness;
                status_bar_LC.Content = prop.LineColor;
                status_bar_FC.Content = prop.FillColor;
            }
        }
    }
}
