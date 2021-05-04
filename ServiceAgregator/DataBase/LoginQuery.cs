using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAgregator.DataBase
{
    class LoginQuery
    {
        Models.Login login;

        public LoginQuery(Models.Login login)
        {
            this.login = login;
        }

        public bool Login()
        {
            using (DataBase.DBContext db = new DataBase.DBContext())
            {
                var User = db.Users.FirstOrDefault(p => p.Login == login._Login && p.Password == login._Password);
                if (User != null)
                {
                    return true;
                }
            }
            return false;
        }

        public Models.Users ReturnCurrenUser()
        {
            using (DataBase.DBContext db = new DataBase.DBContext())
            {
                var User = db.Users.FirstOrDefault(p => p.Login == login._Login && p.Password == login._Password);
                if (User != null)
                {
                    return (Models.Users)User;
                }
                else
                {
                    return new Models.Users();
                }
                
            }
            
        }
    }
}
