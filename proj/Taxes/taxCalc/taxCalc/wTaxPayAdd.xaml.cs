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
//using System.Collections.Generic;

namespace taxCalc
{
    /// <summary>
    /// Логика взаимодействия для wTaxPayAdd.xaml
    /// </summary>
    public partial class wTaxPayAdd : Window
    {
        SqlDataAdapter daTax;
        DataTable tbTax;
        SqlDataAdapter daOrg;
        DataTable tbOrg;

        public wTaxPayAdd()
        {
            InitializeComponent();
        }

        private void Button_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void txSum_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            decimal dd;

            if(!decimal.TryParse(txSum.Text, out dd))
            {
                MessageBox.Show("Неверный формат облагаемой суммы!");
                return;
            }

            if (!decimal.TryParse(txTax.Text, out dd))
            {
                MessageBox.Show("Неверный формат уплаченной суммы!");
                return;
            }

            SqlConnection cn = new SqlConnection(Properties.Settings.Default.taxConnectionString);
            SqlCommand cm = new SqlCommand("taxPay_INS", cn);
            cm.CommandType = CommandType.StoredProcedure;

            cm.Parameters.AddWithValue("@tpDate", dtTax.SelectedDate.Value.Date);
            cm.Parameters.AddWithValue("@tpSum", Convert.ToDecimal(txSum.Text));
            cm.Parameters.AddWithValue("@tpTax", Convert.ToDecimal(txTax.Text));
            cm.Parameters.AddWithValue("@id_tax", (int)cbTax.SelectedValue);
            cm.Parameters.AddWithValue("@id_org", (int)cbOrg.SelectedValue);

            try
            {
                cn.Open();
                cm.ExecuteNonQuery();
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

        private void txTax_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection cn = new SqlConnection(Properties.Settings.Default.taxConnectionString);
            SqlCommand cmTax = new SqlCommand("SELECT id_tax, taxName FROM taxes", cn);
            cmTax.CommandType = CommandType.Text;
            SqlCommand cmOrg = new SqlCommand("SELECT id_org, orgName FROM organization", cn);
            cmOrg.CommandType = CommandType.Text;

            try
            {

                daOrg = new SqlDataAdapter(cmOrg);
                tbOrg = new DataTable();
                daTax = new SqlDataAdapter(cmTax);
                tbTax = new DataTable();

                cn.Open();
                daTax.Fill(tbTax);
                daOrg.Fill(tbOrg);

                cbOrg.ItemsSource = tbOrg.DefaultView;
                cbTax.ItemsSource = tbTax.DefaultView;

                cbOrg.DisplayMemberPath = "orgName";
                cbOrg.SelectedValuePath = "id_org";

                cbTax.DisplayMemberPath = "taxName";
                cbTax.SelectedValuePath = "id_tax";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
            }

            dtTax.SelectedDate = DateTime.Now.Date;
        }
    }
}
