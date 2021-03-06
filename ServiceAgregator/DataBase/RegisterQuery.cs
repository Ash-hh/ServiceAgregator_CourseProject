using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ServiceAgregator.Models;

namespace ServiceAgregator.DataBase
{
    class RegisterQuery
    {
        public bool Succes = false;
        public RegisterQuery(Users NewUser)
        {          

            using (DBContext db = new DBContext())
            {                

                
                try
                {
                    if(IsUserLoginValid(db,NewUser))
                    {
                        NewUser.Password = new Command.PasswordHash().GetStrHash(NewUser.Password);
                        NewUser.Date_Registration = DateTime.Now;
                        NewUser.Date_LastLogin = DateTime.Now;
                        NewUser.User_Type = 1;
                        NewUser.Rating = 5;
                        NewUser.Active = true;
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
       
    }
}
