using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.Models
{
    public class IsbnValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool result = true;
            if(value.ToString().Length < 13)
            {
                result = false;
            }

            return result;
        }
    }
}
