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
        public RegisterQuery(Models.Users NewUser)
        {
            using (DataBase.DBContext db = new DBContext())
            {
                NewUser.Date_Registration = DateTime.Now;
                NewUser.Date_LastLogin = DateTime.Now;
                NewUser.User_ID = 6;
                NewUser.User_Type = 0;
                try
                {
                    db.Users.Add(NewUser);

                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    // MessageBox.Show();
                    e.EntityValidationErrors.ToList();
                }


            }
            MessageBox.Show("Новый пользователь успешно добавлен");

        }
    }
}
