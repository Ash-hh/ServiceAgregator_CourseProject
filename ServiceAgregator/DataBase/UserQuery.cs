using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceAgregator.Models;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using ServiceAgregator.Command;


namespace ServiceAgregator.DataBase
{
    class UserQuery
    {
        public Users GetUserById(int id)
        {          
            using (DBContext db = new DBContext())
            {
                var DBUser = db.Users.Include("ReviewsRecepient").
                                        Include("ReviewsRecepient.UsersSender").
                                        Include("Services").
                                        Include("Services.Orders").Where(p => p.User_ID == id).FirstOrDefault();
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
                var bufflist = db.Users.
                                    Include("Orders").
                                    Include("Services").
                                    Include("Services.Orders").Where(p=>p.User_ID != Changer.CurrentUserID).
                                    ToList();

                return users.FromListToObservableCollection(bufflist);
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
                    User.Rating = user.Rating;
                    User.User_Type = user.User_Type;
                    User.Active = user.Active;
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

        public void SendReview(Reviews review)
        {
            using (DBContext db = new DBContext())
            {
                db.Reviews.Attach(review);
                db.Reviews.Add(review);
                db.SaveChanges();
            }
        }

    } 

}
