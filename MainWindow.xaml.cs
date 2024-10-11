using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using MySql.Data.MySqlClient;
using MySql.Data;

namespace service
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void txtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            lblAlert.Visibility = Visibility.Hidden;
            TextBox tmp = (TextBox)sender;
            if (tmp.Text == "Логин" || tmp.Text == "Пароль")
            {
                tmp.Text = "";
            }
        }


       
        private void txtBox_login_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtBox_login.Text == "")
            {
                txtBox_login.Text = "Логин";
            }
        }


        private void passBox_GotFocus(object sender, RoutedEventArgs e)
        {
            lblAlert.Visibility = Visibility.Hidden;
            PasswordBox tmp = (PasswordBox)sender;
            if (tmp.Password == "***")
            {
                tmp.Password = "";
            }
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            btn.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF4A0F0F");
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            btn.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF6D1717");
        }

        private bool CheckAuthorization(string login, string password)
        {
            string connectionString = "server=127.0.0.1; port=3306; userid=admin; password=Qwerty2006; database=autoservice; sslmode=none; AllowPublicKeyRetrieval=true;";
            MySqlConnection connection = new MySqlConnection(connectionString);

            bool isAuthenticated = false;
            const string defaultPassword = "***";
            using (MySqlCommand command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT * FROM users WHERE login = \"{((login == "Логин")? "login" : login)}\" AND password = \"{((password == defaultPassword)? "1": password)}\"";
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    isAuthenticated = true;
                }

                reader.Close();
            }

            connection.Close();

            return isAuthenticated;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = txtBox_login.Text;
            string password = passBox.Password;
            if (CheckAuthorization(login, password))
            {
                MainMenu mainMenu = new MainMenu(login);
                mainMenu.Show();
                this.Close();
            }
            else
            {
                lblAlert.Visibility = Visibility.Visible;
            }
        }
    }
}
