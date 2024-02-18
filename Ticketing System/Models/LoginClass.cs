using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Ticketing_System.Models
{
    public class LoginClass
    {
        [Required(ErrorMessage ="Enter Username")]
        [Display(Name ="UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsLoggedIn { get; set; }
    }
}