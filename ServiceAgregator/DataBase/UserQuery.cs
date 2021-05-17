﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceAgregator.Models;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;


namespace ServiceAgregator.DataBase
{
    class UserQuery
    {
        public Users GetUserById(int id)
        {          
            using (DBContext db = new DBContext())
            {
                var DBUser = db.Users.Include("ReviewsRecepient").Include("ReviewsRecepient.UsersSender").Where(p => p.User_ID == id).FirstOrDefault();
                if(DBUser!=null)
                {
                    return DBUser;
                }
                else
                {
                    return new Users();
                }
            }
        }

        public Users GetUserByLogin(Login login)
        {
            Users User = new Users();
            using (DBContext db = new DBContext())
            {
                var DBUser = db.Users.Where(p => p.Login == login._Login).FirstOrDefault();
                if (DBUser != null)
                {
                    return DBUser;
                }
                else
                {
                    return new Users();
                }
            }               
        }

        public ObservableCollection<Users> GetAllUsers()
        {
            using (DBContext db = new DBContext())
            {
                ObservableCollection<Users> users = new ObservableCollection<Users>();
                var bufflist = db.Users.ToList<Users>();
                foreach (Users buff in bufflist)
                {
                    users.Add(buff);
                }
                return users;
            }
        }

        public void UserUpdate(Users user)
        {
            using (DBContext db = new DBContext())
            {
                try
                {
                    var User = db.Users.Where(p => p.User_ID == user.User_ID).FirstOrDefault();
                    User.User_Image = user.User_Image;
                    User.Login = user.Login;
                    User.Password = user.Password;
                    User.User_Name = user.User_Name;
                    User.Date_LastLogin = user.Date_LastLogin;
                    db.SaveChanges();
                }
                catch(DbEntityValidationException ex)
                {
                    foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                    {
                        Console.WriteLine("Object: " + validationError.Entry.Entity.ToString());
                        Console.WriteLine("");
                        foreach (DbValidationError err in validationError.ValidationErrors)
                        {
                            Console.WriteLine(err.ErrorMessage + "");
                        }
                    }
                }               
            }
        }
    }
}
