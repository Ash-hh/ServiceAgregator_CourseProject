using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using ServiceAgregator.DataBase;
using ServiceAgregator.Models;
using System.Data.Entity.Validation;

namespace ServiceAgregator.Command
{
    class PasswordHash
    {
        public PasswordHash()
        {

        }

        public string GetStrHash(string str)
        {
            byte[] ByteStr = new UTF8Encoding().GetBytes(str);
            using (SHA256 mySHA256 = SHA256.Create())
            {
                return BitConverter.ToString(mySHA256.ComputeHash(ByteStr));
            }
        }

        public bool TryGetAccess(Users User, string password,DBContext db)
        {
            if(User.Password == GetStrHash(password))
            {
               
                    try
                    {
                        var DBUser = db.Users.Where(p => p.Login == User.Login).FirstOrDefault();
                        DBUser.Date_LastLogin = DateTime.Now;
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
                
                return true;
            }
            return false;
        }

    }
}
