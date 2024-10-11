using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service
{
    internal class User
    {
        private string m_login;
        private string m_password;

        public User(string login, string password) 
        {
            m_login = login;
            m_password = password;
        }

        public string getLogin()
        {
            return m_login;
        }
        public string getPassword()
        {
            return m_password;
        }
    }
}
