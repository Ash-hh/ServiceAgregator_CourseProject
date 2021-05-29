using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAgregator.Models
{
    class Login
    {
        [Required(ErrorMessage = "Недопустимая длинна логина")]
        //[StringLength(50)]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Недопустимая длинна логина")]
        public string _Login { set; get; }

        [Required(ErrorMessage = "Недопустимая длинна пароля")]
        [StringLength(95, MinimumLength = 3, ErrorMessage = "Недопустимая длинна пароля")]
        public string _Password { set; get; }

        public Login(string login, string password)
        {
            _Login = login;
            _Password = password;
        }
        public Login()
        {

        }
    }
}
