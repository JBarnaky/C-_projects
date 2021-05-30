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

namespace taxCalc
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Closing += new System.ComponentModel.CancelEventHandler(MainWindow_Closing);
        }

        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
                App.Current.Shutdown();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            // запрос на завершение работы
            if (MessageBox.Show("Завершить работу приложения?", this.Title, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                App.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // запрос на завершение работы
            if (MessageBox.Show("Завершить работу приложения?", this.Title, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                App.Current.Shutdown();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            wKind wNK = new wKind();

            wNK.Show();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            wTax wtx = new wTax();

            wtx.Show();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            wInspection win = new wInspection();

            win.Show();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            wOrg wor = new wOrg();

            wor.Show();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            wTaxPay wtp = new wTaxPay();

            wtp.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            wTaxPay wtp = new wTaxPay();

            wtp.Show();
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            tTool.Text = btExit.ToolTip.ToString();
        }

        private void btExit_MouseLeave(object sender, MouseEventArgs e)
        {
            tTool.Text = "";
        }

        private void Image_MouseEnter_1(object sender, MouseEventArgs e)
        {
            tTool.Text = btNalog.ToolTip.ToString();
        }

        private void btNalog_MouseLeave(object sender, MouseEventArgs e)
        {
            tTool.Text = "";
        }

        private void Image_MouseEnter_2(object sender, MouseEventArgs e)
        {
            tTool.Text = btGraf.ToolTip.ToString();
        }

        private void btGraf_MouseLeave(object sender, MouseEventArgs e)
        {
            tTool.Text = "";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            wGraf wgr = new wGraf();

            wgr.Show();
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            wGraf wgr = new wGraf();

            wgr.Show();
        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {
            AboutBox1 a = new AboutBox1();
            a.Show();
        }

        private void MenuItem_Click_8(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.Help.ShowHelp(null, @"TaxCalcHelp.chm");
        }

        public void Executed_New(object sender, ExecutedRoutedEventArgs e)
        {
            wTaxPay wtp = new wTaxPay();

            wtp.Show();
        }

        public void CanExecute_New(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
