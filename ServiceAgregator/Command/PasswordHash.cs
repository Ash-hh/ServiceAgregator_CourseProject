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

        public bool TryGetAccess(Users User, string password)
        {
            if(User.Password == GetStrHash(password))
            {
                User.Date_LastLogin = DateTime.Now;
                new UserQuery().UserUpdate(User);

                return true;
            }
            return false;
        }

    }
}
