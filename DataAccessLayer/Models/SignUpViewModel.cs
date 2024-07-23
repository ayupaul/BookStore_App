using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class SignUpViewModel
    {
       
        [Display(Name = "User Name")]
       
        public string UserName { get; set; }
     
        public string Email { get; set; }
       
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password Mismatched")]
        public string ConfirmPassword { get; set; }
    }
}

