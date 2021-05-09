using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows;
using ServiceAgregator.Models;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace ServiceAgregator.DataBase
{
    class LoginQuery
    {
       Login login;

        public Users user { set; get; }

        public LoginQuery(Login login)
        {
            this.login = login;
        }

        public bool Login()
        {
            user = new UserQuery().GetUserByLogin(login);
            return new Command.PasswordHash().TryGetAccess(user, login._Password);                                 
        }
       
    }
}
