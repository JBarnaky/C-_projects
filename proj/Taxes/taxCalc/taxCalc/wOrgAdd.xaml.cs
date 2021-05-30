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
    /// Логика взаимодействия для wOrgAdd.xaml
    /// </summary>
    public partial class wOrgAdd : Window
    {
        SqlDataAdapter daKind;
        DataTable tbKind;
        SqlDataAdapter daNalog;
        DataTable tbNalog;

        public int id_org = 0;

        public wOrgAdd()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // подготовка комбо-боксов и привязка к ним данных

            SqlConnection cn = new SqlConnection(Properties.Settings.Default.taxConnectionString);
            SqlCommand cmKind = new SqlCommand("SELECT id_kn, knName FROM kind", cn);
            SqlCommand cmNalog = new SqlCommand("SELECT id_in, inName FROM inspection", cn);
            cmKind.CommandType = CommandType.Text;
            cmNalog.CommandType = CommandType.Text;

            daKind = new SqlDataAdapter(cmKind);
            daNalog = new SqlDataAdapter(cmNalog);
            tbKind = new DataTable();
            tbNalog = new DataTable();

            try
            {
                cn.Open();
                daKind.Fill(tbKind);
                daNalog.Fill(tbNalog);

                cbKind.ItemsSource = tbKind.DefaultView;
                cbNalog.ItemsSource = tbNalog.DefaultView;

                cbKind.DisplayMemberPath = "knName";
                cbKind.SelectedValuePath = "id_kn";

                cbNalog.DisplayMemberPath = "inName";
                cbNalog.SelectedValuePath = "id_in";
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

        
        private void txUNP_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // проверка на ввод только цифр
            int r;
            if (!Int32.TryParse(e.Text, out r))
                e.Handled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // внести новую запись в таблицу БД
            SqlConnection cn = new SqlConnection(Properties.Settings.Default.taxConnectionString);
            SqlCommand cmd = new SqlCommand("org_INS", cn);
            
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@orgName", txName.Text);
            cmd.Parameters.AddWithValue("@orgPhone", txPhone.Text);
            cmd.Parameters.AddWithValue("@orgAdr", txAdr.Text);
            cmd.Parameters.AddWithValue("@orgRCnt", txRCnt.Text);
            cmd.Parameters.AddWithValue("@orgUNP", Convert.ToInt32(txUNP.Text));
            cmd.Parameters.AddWithValue("@id_kn", cbKind.SelectedValue);
            cmd.Parameters.AddWithValue("@id_in", cbNalog.SelectedValue);
            SqlParameter pr =  cmd.Parameters.Add(new SqlParameter("@id_org", SqlDbType.Int));
            pr.Direction = ParameterDirection.Output;

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();

                id_org = (int)pr.Value;
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
            }

            if (DialogResult == true) Close();
        }
    }
}
