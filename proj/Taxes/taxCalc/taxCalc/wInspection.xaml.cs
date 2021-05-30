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
    /// Логика взаимодействия для wInspection.xaml
    /// </summary>
    public partial class wInspection : Window
    {
        SqlDataAdapter da;
        DataTable tb;

        public wInspection()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // подготовка локального представления данных
            SqlConnection cn = new SqlConnection(Properties.Settings.Default.taxConnectionString);
            SqlCommand cm = new SqlCommand("inspetion_SEL", cn);
            cm.CommandType = CommandType.StoredProcedure;

            try
            {
                da = new SqlDataAdapter(cm);
                tb = new DataTable("taxesTbl");
                da.InsertCommand = new SqlCommand("inspetion_INS");
                da.InsertCommand.CommandType = CommandType.StoredProcedure;
                da.InsertCommand.Parameters.Add(new SqlParameter("@inName", SqlDbType.VarChar, 50, "inName"));
                da.InsertCommand.Parameters.Add(new SqlParameter("@inPhone", SqlDbType.VarChar, 20, "inPhone"));
                da.InsertCommand.Parameters.Add(new SqlParameter("@inAdr", SqlDbType.VarChar, 150, "inAdr"));
                SqlParameter npar = da.InsertCommand.Parameters.Add("@id_in", SqlDbType.Int, 0, "id_in");
                npar.Direction = ParameterDirection.Output;

                cn.Open();
                da.Fill(tb);
                grInsp.ItemsSource = tb.DefaultView;
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
            // обновление данных на сервере
            SqlCommandBuilder cbl = new SqlCommandBuilder(da);
            da.Update(tb);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UpdateDB();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены что хотите удалить данную инспекцию?", this.Title, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            // удаление записи из локального набора 
            if (grInsp.SelectedItems != null)
            {
                for (int i = 0; i < grInsp.SelectedItems.Count; i++)
                {
                    DataRowView dw = grInsp.SelectedItems[i] as DataRowView;
                    if (dw != null)
                    {
                        DataRow dr = (DataRow)dw.Row;
                        dw.Delete();
                    }
                }
                // обновить данные на сервере
                UpdateDB();
            }
        }
    }
}
