using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace service
{
    /// <summary>
    /// Логика взаимодействия для History.xaml
    /// </summary>
    public partial class History : Window
    {
        public History()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string connectionString = "server=127.0.0.1; port=3306; userid=admin; password=Qwerty2006; database=autoservice; sslmode=none; AllowPublicKeyRetrieval=true;";
            string sql = "SELECT * FROM logs; ";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    dataGrid1.DataContext = dt;
                    dataGrid1.ItemsSource = dt.DefaultView;
                }
                connection.Close();
            }
        }
        private void Update()
        {
            string connectionString = "server=127.0.0.1; port=3306; userid=admin; password=Qwerty2006; database=autoservice; sslmode=none; AllowPublicKeyRetrieval=true;";
            string sql = "SELECT * FROM logs; ";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    dataGrid1.DataContext = dt;
                    dataGrid1.ItemsSource = dt.DefaultView;
                }
                connection.Close();
            }
        }
    }
}
