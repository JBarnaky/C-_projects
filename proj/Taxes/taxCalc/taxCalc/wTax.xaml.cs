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
    /// Логика взаимодействия для wTax.xaml
    /// </summary>
    public partial class wTax : Window
    {
        SqlDataAdapter da;
        DataTable tb;

        public wTax()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            // подготовка набора данных

            SqlConnection cn = new SqlConnection(Properties.Settings.Default.taxConnectionString);
            SqlCommand cm = new SqlCommand("taxes_SEL", cn);
            cm.CommandType = CommandType.StoredProcedure;

            try
            {
                da = new SqlDataAdapter(cm);
                tb = new DataTable("taxesTbl");
                da.InsertCommand = new SqlCommand("taxes_INS");
                da.InsertCommand.CommandType = CommandType.StoredProcedure;
                da.InsertCommand.Parameters.Add(new SqlParameter("@taxName", SqlDbType.VarChar, 20, "taxName"));
                da.InsertCommand.Parameters.Add(new SqlParameter("@taxPerc", SqlDbType.Int, 0, "taxPerc"));
                SqlParameter npar = da.InsertCommand.Parameters.Add("@id_tax", SqlDbType.Int, 0, "id_tax");
                npar.Direction = ParameterDirection.Output;

                cn.Open();
                da.Fill(tb);
                grTaxes.ItemsSource = tb.DefaultView;
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

        private void UpdateDB()
        {
            // обновление сделанных изменений на сервере
            SqlCommandBuilder cbl = new SqlCommandBuilder(da);
            da.Update(tb);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены что хотите удалить этот налог?", this.Title, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            // удаление записи в наборе
            if (grTaxes.SelectedItems != null)
            {
                for (int i = 0; i < grTaxes.SelectedItems.Count; i++)
                {
                    DataRowView dw = grTaxes.SelectedItems[i] as DataRowView;
                    if (dw != null)
                    {
                        DataRow dr = (DataRow)dw.Row;
                        dw.Delete();
                    }
                }
                // отправить обновления на сервер
                UpdateDB();

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UpdateDB();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
