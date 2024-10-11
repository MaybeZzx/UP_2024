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
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;
using MySqlX.XDevAPI;

namespace service
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        private string m_login;
        
        DataTable dt = new DataTable();

        public MainMenu(string login)
        {
            InitializeComponent();
            m_login = login;
            if (m_login != "admin")
            {
                btnAdd.Visibility = Visibility.Hidden;
                btnUsers.Visibility = Visibility.Hidden;
            }
        }

        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            string connectionString = "server=127.0.0.1; port=3306; userid=admin; password=Qwerty2006; database=autoservice; sslmode=none; AllowPublicKeyRetrieval=true;";
            string sql = "SELECT * FROM autoparts; ";
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

        private void txtBox_Search_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtBox_Search.Text == "Поиск")
            {
                txtBox_Search.Text = "";
                txtBox_Search.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF000000");
            }
        }

        private void txtBox_Search_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtBox_Search.Text == "")
            {
                txtBox_Search.Text = "Поиск";
                txtBox_Search.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF565656");
            }
        }

        public bool CheckRows(string tableName, string select, string prompt)
        {
            bool hasRows = false;
            string connectionString = "server=127.0.0.1; port=3306; userid=admin; password=Qwerty2006; database=autoservice; sslmode=none; AllowPublicKeyRetrieval=true;";
            MySqlConnection connection = new MySqlConnection(connectionString);

            using (MySqlCommand command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT {select} FROM {tableName} WHERE {prompt}";
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    hasRows = true;
                }

                reader.Close();
            }

            connection.Close();
            return hasRows;
        }

        private void Update()
        {
            string connectionString = "server=127.0.0.1; port=3306; userid=admin; password=Qwerty2006; database=autoservice; sslmode=none; AllowPublicKeyRetrieval=true;";
            string sql = "SELECT * FROM autoparts";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    dt.Clear();
                    da.Fill(dt);
                    dataGrid1.DataContext = dt;
                    dataGrid1.ItemsSource = dt.DefaultView;
                }
                connection.Close();
            }
        }
        public void UpdateIf(string tableName, string prompt) //prompt = "SET example = example - 1 WHERE name = \"example\" "
        {
            string connectionString = "server=127.0.0.1; port=3306; userid=admin; password=Qwerty2006; database=autoservice; sslmode=none; AllowPublicKeyRetrieval=true;";
            string sql = $"UPDATE {tableName} SET {prompt}; ";
            //MessageBox.Show(sql);
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
            Update();
        }
        public string getUserName(string login)
        {
            string userName = "";

            string connectionString= "server=127.0.0.1; port=3306; userid=admin; password=Qwerty2006; database=autoservice; sslmode=none; AllowPublicKeyRetrieval=true;";
            string sql = $"SELECT * FROM employes WHERE login = \"{login}\"";
            //MessageBox.Show(sql);
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {

                if (CheckRows("employes", "name, login", $"login = \"{login}\""))
                {

                    using (MySqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = sql;
                        connection.Open();
                        MySqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            reader.Read();
                            userName = reader[0].ToString();
                        }

                        reader.Close();
                    }

                    connection.Close();
                }
            }
            return userName;
        }
        public void AddLog(string login, string action, string itemName, int count)
        {
            string name = getUserName(login);
            if (name != "")
            {
                string connectionString = "server=127.0.0.1; port=3306; userid=admin; password=Qwerty2006; database=autoservice; sslmode=none; AllowPublicKeyRetrieval=true;";
                string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                MessageBox.Show(currentTime);
                string sql = $"INSERT logs(who, action, count, what, time) VALUES (\"{name}\", \"{action}\", {count}, \"{itemName}\", \"{currentTime}\") ";
                //MessageBox.Show(sql);
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

        private void btnTake_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = dataGrid1.SelectedItem;
            if (selectedItem != null)
            {
                string name = dt.DefaultView[dataGrid1.SelectedIndex][1].ToString();

                UpdateIf("autoparts", $"count = count - 1 WHERE name = \"{name}\"");
                AddLog(m_login, "взял", name, 1);
            }
        }
        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            ReturnWindow returnWindow = new ReturnWindow(m_login);
            returnWindow.Owner = this;
            returnWindow.ShowDialog();
        }

        private void btnLogs_Click(object sender, RoutedEventArgs e)
        {
            History history = new History();
            history.Show();
        }
    }
}

