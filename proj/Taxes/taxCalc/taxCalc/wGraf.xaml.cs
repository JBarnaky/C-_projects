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
    /// Логика взаимодействия для wGraf.xaml
    /// </summary>
    public partial class wGraf : Window
    {
        SqlDataAdapter daOrg;
        DataTable tbOrg;
       
        public wGraf()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int dd;
            if (!Int32.TryParse(e.Text, out dd))
                e.Handled = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txYear.Text = DateTime.Now.Year.ToString();

            SqlConnection cn = new SqlConnection(Properties.Settings.Default.taxConnectionString);
            SqlCommand cmOrg = new SqlCommand("SELECT id_org, orgName FROM organization", cn);
            cmOrg.CommandType = CommandType.Text;

            try
            {
                daOrg = new SqlDataAdapter(cmOrg);
                tbOrg = new DataTable();

                cn.Open();
                daOrg.Fill(tbOrg);

                cbOrg.ItemsSource = tbOrg.DefaultView;
                cbOrg.DisplayMemberPath = "orgName";
                cbOrg.SelectedValuePath = "id_org";
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection cn = new SqlConnection(Properties.Settings.Default.taxConnectionString);
            SqlCommand cm = new SqlCommand("gr_SEL", cn);
            


            try
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@id_org", (int)cbOrg.SelectedValue);
                cm.Parameters.AddWithValue("@pYear", Convert.ToInt32(txYear.Text));
            }
            catch
            {
                MessageBox.Show("Вы не выбрали организацию");
            }

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable tb = new DataTable();

                cn.Open();
                da.Fill(tb);

                double[] tv = { 0,0,0,0,0,0,0,0,0,0,0,0 };
                int[] hv = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                double maxVal = 0;

                for(int i=0; i<tb.Rows.Count; i++)
                {
                    int dd = Convert.ToInt32(tb.Rows[i].ItemArray[0]);
                    double d2 = Convert.ToDouble(tb.Rows[i].ItemArray[1]);

                    tv[dd-1] = d2;
                    maxVal = maxVal < d2 ? d2 : maxVal;
                }

                m01.Height = 0;
                Canvas.SetTop(m01, 200);

                m02.Height = 0;
                Canvas.SetTop(m02, 200);

                m03.Height = 0;
                Canvas.SetTop(m03, 200);

                m04.Height = 0;
                Canvas.SetTop(m04, 200);

                m05.Height = 0;
                Canvas.SetTop(m05, 200);

                m06.Height = 0;
                Canvas.SetTop(m06, 200);

                m07.Height = 0;
                Canvas.SetTop(m07, 200);

                m08.Height = 0;
                Canvas.SetTop(m08, 200);

                m09.Height = 0;
                Canvas.SetTop(m09, 200);

                m10.Height = 0;
                Canvas.SetTop(m10, 200);

                m11.Height = 0;
                Canvas.SetTop(m11, 200);

                m12.Height = 0;
                Canvas.SetTop(m12, 200);

                if (maxVal != 0)
                {
                    double sum = 0;
                    for (int i = 0; i < tv.Length; i++)
                        {
                            sum += tv[i];
                        }

                    t13.Text = sum.ToString();

                    t01.Text = tv[0].ToString();
                    m01.Height = ((int)(tv[0] / maxVal * 200));
                    Canvas.SetTop(m01, 200 - m01.Height);

                    t02.Text = tv[1].ToString();
                    m02.Height = ((int)(tv[1] / maxVal * 200));
                    Canvas.SetTop(m02, 200 - m02.Height);

                    t03.Text = tv[2].ToString();
                    m03.Height = ((int)(tv[2] / maxVal * 200));
                    Canvas.SetTop(m03, 200 - m03.Height);

                    t04.Text = tv[3].ToString();
                    m04.Height = ((int)(tv[3] / maxVal * 200));
                    Canvas.SetTop(m04, 200 - m04.Height);

                    t05.Text = tv[4].ToString();
                    m05.Height = ((int)(tv[4] / maxVal * 200));
                    Canvas.SetTop(m05, 200 - m05.Height);

                    t06.Text = tv[5].ToString();
                    m06.Height = ((int)(tv[5] / maxVal * 200));
                    Canvas.SetTop(m06, 200 - m06.Height);

                    t07.Text = tv[6].ToString();
                    m07.Height = ((int)(tv[6] / maxVal * 200));
                    Canvas.SetTop(m07, 200 - m07.Height);

                    t08.Text = tv[7].ToString();
                    m08.Height = ((int)(tv[7] / maxVal * 200));
                    Canvas.SetTop(m08, 200 - m08.Height);

                    t09.Text = tv[8].ToString();
                    m09.Height = ((int)(tv[8] / maxVal * 200));
                    Canvas.SetTop(m09, 200 - m09.Height);

                    t10.Text = tv[9].ToString();
                    m10.Height = ((int)(tv[9] / maxVal * 200));
                    Canvas.SetTop(m10, 200 - m10.Height);

                    t11.Text = tv[10].ToString();
                    m11.Height = ((int)(tv[10] / maxVal * 200));
                    Canvas.SetTop(m11, 200 - m11.Height);

                    t12.Text = tv[11].ToString();
                    m12.Height = ((int)(tv[11] / maxVal * 200));
                    Canvas.SetTop(m12, 200 - m12.Height);

                    double q1 = (tv[0]+ tv[1]+ tv[2]);
                    t14.Text = q1.ToString();

                    double q2 = (tv[3] + tv[4] + tv[5]);
                    t15.Text = q2.ToString();

                    double q3 = (tv[6] + tv[7] + tv[8]);
                    t16.Text = q3.ToString();

                    double q4 = (tv[9] + tv[10] + tv[11]);
                    t17.Text = q4.ToString();
                }
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
    }
}
