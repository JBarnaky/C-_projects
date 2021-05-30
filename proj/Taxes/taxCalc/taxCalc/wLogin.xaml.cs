using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for wLogin.xaml
    /// </summary>
    public partial class wLogin : Window
    {
        public wLogin()
        {
            InitializeComponent();
            this.Closing += new System.ComponentModel.CancelEventHandler(wLogin_Closing);
        }

        void wLogin_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Current.Shutdown();
        }

        class Login
        {
            //decalre properties 
            public string Username { get; set; }
            public string Userpassword { get; set; }

            //intialise  
            public Login(string user, string pass)
            {
                this.Username = user;
                this.Userpassword = pass;
            }
            //validate string 
            private bool StringValidator(string input)
            {
                string pattern = "[^a-zA-Z]";
                if (Regex.IsMatch(input, pattern))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            //validate integer 
            private bool IntegerValidator(string input)
            {
                string pattern = "[^0-9]";
                if (Regex.IsMatch(input, pattern))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            //clear user inputs 
            private void ClearTexts(string user, string pass)
            {
                user = String.Empty;
                pass = String.Empty;
            }
            //method to check if eligible to be logged in 
            internal bool IsLoggedIn(string user, string pass)
            {
                //check user name empty 
                if (string.IsNullOrEmpty(user))
                {
                    MessageBox.Show("Введите имя пользователя!");
                    return false;

                }
                //check user name is valid type 
                else if (StringValidator(user) == true)
                {
                    MessageBox.Show("Имя пользователя должно состоять только из букв латиницей");
                    ClearTexts(user, pass);
                    return false;
                }
                //check user name is correct 
                else
                {
                    if (string.IsNullOrEmpty(pass))
                    {
                        MessageBox.Show("Введите пароль!");
                        return false;
                    }
                    //check password is valid 
                    else if (IntegerValidator(pass) == true)
                    {
                        MessageBox.Show("Пароль может состоять только из цифр");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }

        //Enter code here for your version of username and userpassword 
        Login login = new Login("admin", "12345");


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            //define local variables from the user inputs 
            string user = l01.Text;
            string pass = l02.Text;

            SqlConnection sqlcon = new SqlConnection(@"Data Source=YATSK-PC;Initial Catalog=LoginDB;Integrated Security=True");
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                    sqlcon.Open();
                String query = "SELECT COUNT(1) FROM Table_Login WHERE Login=@Login AND Password=@Password";
                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.CommandType = CommandType.Text;
                sqlcmd.Parameters.AddWithValue("@Login", l01.Text);
                sqlcmd.Parameters.AddWithValue("@Password", l02.Text);

                int count = Convert.ToInt32(sqlcmd.ExecuteScalar());

                if (login.IsLoggedIn(user, pass))
                {
                    MessageBox.Show("Вы успешно вошли в систему");
                    this.Hide();
                    MainWindow mwnd = new MainWindow();
                    mwnd.ShowDialog();
                }
                else if (count == 1)
                {
                    MessageBox.Show("Вы успешно вошли в систему");
                    this.Hide();
                    MainWindow mwnd = new MainWindow();
                    mwnd.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Ошибка входа - указано неверное Имя или Пароль!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlcon.Close();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            l03.Content = "admin";
            l04.Content = "12345";
        }
    }
}