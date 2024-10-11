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

namespace service
{
    /// <summary>
    /// Логика взаимодействия для ReturnWindow.xaml
    /// </summary>
    public partial class ReturnWindow : Window
    {
        private string m_login;
        public ReturnWindow(string login)
        {
            InitializeComponent();
            m_login = login;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            string name = txtBox_name.Text;
            int count;
            bool success = int.TryParse(txtBox_count.Text, out count);
            count = success ? count : -1;
            if (count == -1)
            {
                MessageBox.Show("Введено неверное кол-во!");
                txtBox_count.Text = "0";
            }
            else
            {
                if ((this.Owner as MainMenu).CheckRows("autoparts", "name, count", $"name = \"{name}\" AND count >= 0"))
                {
                    (this.Owner as MainMenu).UpdateIf("autoparts", $"count = count + {count} WHERE name = \"{name}\"");
                    (this.Owner as MainMenu).AddLog(m_login, "вернул", name, count);
                    Close();
                }
                else
                {
                    MessageBox.Show("Такой позиции нет в списке!");
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
