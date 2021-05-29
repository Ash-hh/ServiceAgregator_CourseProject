using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAgregator.Command
{
    class Validation<T>
    {           

        public bool IsValid(T obj, out string ErrorMessage)
        {
            ErrorMessage = "";
            var results = new List<ValidationResult>();
            var context = new ValidationContext(obj);
            if (!Validator.TryValidateObject(obj, context, results, true))
            {
                foreach (var error in results)
                {
                    ErrorMessage+=(error.ErrorMessage)+"\n";
                }
                return false;
            }
            else
            {              
                return true;
            }
       
        }
    }
}
