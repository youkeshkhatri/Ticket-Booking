using System;
using System.ComponentModel.DataAnnotations;

namespace Ticketing_System.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Passwords doesn't match !")]
        public string Confirm_Password { get; set; }

    }
}