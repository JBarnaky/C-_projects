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
    /// Логика взаимодействия для wOrg.xaml
    /// </summary>
    public partial class wOrg : Window
    {
        SqlDataAdapter da;
        DataTable tb;

        public wOrg()
        {
            InitializeComponent();
        }

        private void UpdateData()
        {
            // обновление набора данных 

            SqlConnection cn = new SqlConnection(Properties.Settings.Default.taxConnectionString);
            SqlCommand cm = new SqlCommand("org_SEL", cn);
            cm.CommandType = CommandType.StoredProcedure;

            try
            {
                
                da = new SqlDataAdapter(cm);
                tb = new DataTable();
                cn.Open();
                da.Fill(tb);
                grOrg.ItemsSource = tb.DefaultView;
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

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            wOrgAdd woa = new wOrgAdd();

            woa.ShowDialog();
            // обновить данные 
            if (woa.DialogResult == true)
                UpdateData();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены что хотите удалить плательщика?", this.Title, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            // выход если нет записей
            if (grOrg.SelectedItems.Count == 0) return;

            // удаление записи с помощью хранимой процедуры
            SqlConnection cn = new SqlConnection(Properties.Settings.Default.taxConnectionString);
            SqlCommand cm = new SqlCommand("org_DEL", cn);
            try
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@id_org", ((DataRowView)(grOrg.SelectedItems[0])).Row["id_org"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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
                UpdateData();
            }
        }
    }
}
