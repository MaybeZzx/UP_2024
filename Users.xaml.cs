using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace service
{
    /// <summary>
    /// Логика взаимодействия для Users.xaml
    /// </summary>
    public partial class Users : Window
    {
        public Users()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
            string Connect = "server=127.0.0.1; port=3306; userid=admin; password=Qwerty2006; database=autoservice; sslmode=none; AllowPublicKeyRetrieval=true;";
            MySqlConnection connection = new MySqlConnection(Connect);

            List<User> my_list = new List<User>();
            string sql = "SELECT * from users;";
            MySqlCommand command1 = new MySqlCommand(sql, connection);
            connection.Open();

            MySqlDataReader reader = command1.ExecuteReader();

            int number = 1;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    User my = new User(reader["login"].ToString(), reader["password"].ToString());
                    my_list.Add(my);
                }

                txtBox.ScrollToEnd();

                foreach (var emp in my_list)
                {
                    txtBox.Text = txtBox.Text + "#" + number++ + "\n   Login:  " + emp.getLogin() +
                    "\n   Password:  " + emp.getPassword() + "\n";
                };

            }

            reader.Close();
            connection.Close();

        }
    }
}
