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
using System.Windows.Shapes;
using control_lib;
using System.Reflection;

namespace lab4_paint
{
    /// <summary>
    /// Interaction logic for Window_Properties.xaml
    /// </summary>

    public partial class Window_Properties : Window
    {
        Props prop = new Props();

        public Props Props
        {
            get
            {
                return prop;
            }
        }

        public Window_Properties()
        {
            InitializeComponent();
            DataContext = prop;

            List<SolidColorBrush> brList = new List<SolidColorBrush>();
            foreach (PropertyInfo pi in typeof(Colors).GetProperties())
            {
                Color c = (Color)pi.GetValue(null);
                brList.Add(new SolidColorBrush(c));
            }
            cbLineColor.ItemsSource = brList;
            cbFillColor.ItemsSource = brList;

        }

        private void cbLineColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            prop.LineColor = cbLineColor.SelectedItem.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void cbFillColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            prop.FillColor = cbFillColor.SelectedItem.ToString();
        }
    }
}
