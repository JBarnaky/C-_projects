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
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace taxCalc
{
    /// <summary>
    /// Логика взаимодействия для wTaxPay.xaml
    /// </summary>
    public partial class wTaxPay : Window
    {
        SqlDataAdapter da;
        DataTable tb;

        public wTaxPay()
        {
            InitializeComponent();
        }

        private void txYear_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // ввод только цифр
            int yy;

            if (!Int32.TryParse(e.Text, out yy))
            {
                e.Handled = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // установка начального периода
            cbMonth.SelectedIndex = DateTime.Now.Month - 1;
            txYear.Text = DateTime.Now.Year.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // выборка данных по условию
            SqlConnection cn = new SqlConnection(Properties.Settings.Default.taxConnectionString);
            SqlCommand cm = new SqlCommand("taxPay_SEL", cn);
            cm.CommandType = CommandType.StoredProcedure;

            cm.Parameters.AddWithValue("@pMonth", (int)(cbMonth.SelectedIndex + 1));
            cm.Parameters.AddWithValue("@pYear", Convert.ToInt32(txYear.Text));
            

            try
            {

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable tb = new DataTable();

                cn.Open();
                da.Fill(tb);
                grTax.ItemsSource = tb.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // добавление записи
            wTaxPayAdd wta = new wTaxPayAdd();

            wta.ShowDialog();
            // если запись добавлена - обновить выборку
            if (wta.DialogResult == true)
                Button_Click(sender, e);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены что хотите удалить этот платеж?", this.Title, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            // выход если нет записей
            if (grTax.SelectedItems.Count == 0) return;

            //удаление записи
            SqlConnection cn = new SqlConnection(Properties.Settings.Default.taxConnectionString);
            SqlCommand cm = new SqlCommand("taxPay_DEL", cn);
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.AddWithValue("@id_tp", ((DataRowView)(grTax.SelectedItems[0])).Row["id_tp"]);

            try
            {
                cn.Open();
                cm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
                Button_Click(sender, e);
            }
        }
    }
}
