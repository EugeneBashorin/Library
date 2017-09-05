using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryProject.Models
{
    public class Register
    {   
        //public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage ="Passwords are different")]
        public string ConfirmPassword { get; set; }

        [Required]
        public int Age { get; set; }
    }
}