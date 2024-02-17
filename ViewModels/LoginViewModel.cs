using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroopWebApp.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage ="Email is required")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}