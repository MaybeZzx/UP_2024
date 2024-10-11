using MySql.Data.MySqlClient;
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
using static System.Collections.Specialized.BitVector32;
using System.Xml.Linq;
using System.Globalization;
using System.Threading;

namespace service
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Window
    {
        public Add()
        {
            InitializeComponent();
            LoadCategories();
        }
        private void LoadCategories()
        {
            string Connect = "server=127.0.0.1; port=3306; userid=admin; password=Qwerty2006; database=autoservice; sslmode=none; AllowPublicKeyRetrieval=true;";
            MySqlConnection connection = new MySqlConnection(Connect);

            List<string> my_list = new List<string>();
            string sql = "SELECT * from categories;";
            MySqlCommand command1 = new MySqlCommand(sql, connection);
            connection.Open();

            MySqlDataReader reader = command1.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    my_list.Add(reader["name"].ToString());
                    cmbBox_category.ItemsSource = my_list;
                }
            }
            reader.Close();
            connection.Close();
        }
        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            int price = 0;
            string name = txtBox_name.Text;
            string category = cmbBox_category.SelectedItem.ToString();

            string Connect = "server=127.0.0.1; port=3306; userid=admin; password=Qwerty2006; database=autoservice; sslmode=none; AllowPublicKeyRetrieval=true;";
            string sql = $"INSERT autoparts(categoryName, name, count, price) VALUES(\"{category}\", \"{name}\", {count}, {price});";

            MySqlConnection connection = new MySqlConnection(Connect);
            MySqlCommand command1 = new MySqlCommand(sql, connection);
            connection.Open();
            if (int.TryParse(txtBox_count.Text, out count) && int.TryParse(txtBox_price.Text, out price))
            {
                command1.ExecuteNonQuery();
            }
            else
            {
                MessageBox.Show("Ошибка в формате записи!");
                return;
            }
            connection.Close();
            (this.Owner as MainMenu).UpdateP();
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
