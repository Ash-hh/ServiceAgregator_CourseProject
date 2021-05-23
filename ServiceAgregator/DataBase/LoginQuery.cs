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
            IsFirstRun();
            this.login = login;
        }

        public bool Login()
        {           
            user = new UserQuery().GetUserByLogin(login);
            return new Command.PasswordHash().TryGetAccess(user, login._Password);                                 
        }

        public void IsFirstRun()
        {
            using (DBContext db = new DBContext())
            {
                if(db.Tags.Count() == 0)
                {
                    db.Tags.Add(new Tags { Tag = "Auto" });
                    db.Tags.Add(new Tags { Tag = "Education" });
                    db.Tags.Add(new Tags { Tag = "Home" });
                    db.Tags.Add(new Tags { Tag = "Other" });
                    db.SaveChanges();
                }
            }
        }
       
    }
}
