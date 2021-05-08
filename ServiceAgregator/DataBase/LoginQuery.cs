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
            using (DBContext db = new DBContext())
            {
                var User = db.Users.FirstOrDefault(p => p.Login == login._Login);
                if (User != null)
                {
                    user = User;
                    return new Command.PasswordHash().TryGetAccess(User, login._Password,db);
                }
                else
                    return false;
            }            
        }

        public bool LoginPass()
        {
           
           
            
            //byte[] DBpass = new UTF8Encoding().GetBytes(user.Password);
            byte[] Inputpass = new UTF8Encoding().GetBytes(login._Password);

            
          
            using (SHA256 mySHA256 = SHA256.Create())
            {
                string pass = BitConverter.ToString(mySHA256.ComputeHash(Inputpass));
                if (user.Password == pass)
                {
                    using (DBContext db = new DBContext())
                    {
                        try 
                        {
                            var User = db.Users.Where(p => p.Login == login._Login).FirstOrDefault();
                            User.Date_LastLogin = DateTime.Now;
                            //User.Password = BitConverter.ToString(mySHA256.ComputeHash(Inputpass));
                            db.SaveChanges();
                        }
                        catch (DbEntityValidationException ex)
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
                    return true;
                }
                else
                {
                    MessageBox.Show("!!!");
                    return false;
                }
            }            
        }
    }
}
