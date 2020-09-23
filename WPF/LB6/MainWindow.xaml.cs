using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading.Tasks;

namespace DefiniteIntegral
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IProgress<int>
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Report(int value)
        {
            Dispatcher.Invoke(() => progressBar.Value = value);
        }

        private void inputDialog_Click(object sender, RoutedEventArgs e)
        {
            InputDialog input_dialog = new InputDialog();

            if (input_dialog.ShowDialog() == true)
            {
                aValue.Content = input_dialog.AVal;
                bValue.Content = input_dialog.BVal;
                NValue.Content = input_dialog.NVal;
            }
        }

        private async void Calculate_Click(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            CalcBtn.IsEnabled = false;
            double resIntegral = await IntegralCalculate.Integral4CPUAsync(double.Parse(aValue.Content.ToString()),
                double.Parse(bValue.Content.ToString()), int.Parse(NValue.Content.ToString()), this);
            ResultValue.Content = resIntegral.ToString();
            Cursor = Cursors.Arrow;
            CalcBtn.IsEnabled = true;
            progressBar.Value = 0;
        }
    }
}
