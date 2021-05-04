using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAgregator.Models
{
    class Login
    {
        public string _Login { set; get; }

        public string _Password { set; get; }

        public Login(string login, string password)
        {
            _Login = login;
            _Password = password;
        }
        public Login()
        {

        }
    }
}
