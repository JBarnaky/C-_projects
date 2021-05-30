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
    /// Логика взаимодействия для wKind.xaml
    /// </summary>
    public partial class wKind : Window
    {

        SqlDataAdapter da;
        DataTable tb;

        public wKind()
        {
            InitializeComponent();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // подготовка отображения данных, подготовка добавления записей в базу
            SqlConnection cn = new SqlConnection(Properties.Settings.Default.taxConnectionString);
            SqlCommand cm = new SqlCommand("SELECT id_kn, knName FROM kind", cn);
            cm.CommandType = CommandType.Text;

            try
            {
                da = new SqlDataAdapter(cm);
                tb = new DataTable("kindTbl");
                da.InsertCommand = new SqlCommand("kind_INS");
                da.InsertCommand.CommandType = CommandType.StoredProcedure;
                da.InsertCommand.Parameters.Add(new SqlParameter("@knName", SqlDbType.VarChar, 50, "knName"));
                SqlParameter npar = da.InsertCommand.Parameters.Add("@id_kn", SqlDbType.Int, 0, "id_kn");
                npar.Direction = ParameterDirection.Output;

                cn.Open();
                da.Fill(tb);
                grKing.ItemsSource = tb.DefaultView;
            }
            catch(Exception ex)
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
            if (MessageBox.Show("Вы уверены что хотите удалить этот вид деятельности?", this.Title, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            // удаление записи в локальном наборе
            if (grKing.SelectedItems != null)
            {
                for(int i=0; i<grKing.SelectedItems.Count; i++)
                {
                    DataRowView dw = grKing.SelectedItems[i] as DataRowView;
                    if(dw != null){
                        DataRow dr = (DataRow)dw.Row;
                        dw.Delete();
                    }
                }
                // обновленеи данных на сервере
                UpdateDB();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            UpdateDB();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
