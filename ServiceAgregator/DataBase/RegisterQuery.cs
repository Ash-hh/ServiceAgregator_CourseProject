using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ServiceAgregator.DataBase
{
    class RegisterQuery
    {
        public bool Succes = false;
        public RegisterQuery(Models.Users NewUser)
        {
            using (DataBase.DBContext db = new DBContext())
            {
                
                GenerateUserId(db);
                NewUser.Date_Registration = DateTime.Now;
                NewUser.Date_LastLogin = DateTime.Now;                
                NewUser.User_Type = 1;
                NewUser.User_ID = GenerateUserId(db);
                try
                {
                    if(IsUserLoginValid(db,NewUser))
                    {
                        db.Users.Add(NewUser);

                        db.SaveChanges();
                        Succes = true;
                        MessageBox.Show("New User Succesfully register");
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    e.EntityValidationErrors.ToList();
                }


            }
           
        }

        bool IsUserLoginValid(DBContext db, Models.Users NewUser)
        {
            var user = db.Users.FirstOrDefault(p => p.Login == NewUser.Login);
            if(user == null)
            {
                return true;
            }
            else
            {
                MessageBox.Show($"User with this login is already exist {user.Login}");
                return false;
            }
        }

        int GenerateUserId(DBContext db)
        {
            var id = db.Users.OrderByDescending(p => p.User_ID).Select(p => p.User_ID).Take(1);

            foreach(int f in id)
            {
                return f + 1;
            }
            return 1;
        }
    }
}
