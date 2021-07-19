using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogLabModels.Account
{
    public class ApplicationUserCreate : ApplicationUserLogin
    {
        [MinLength(10, ErrorMessage = "Must be at least 4 characters")]
        [MaxLength(30, ErrorMessage = "Must be at most 30 characters")]
        public string Fullname{ get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email format")]
        [MaxLength(30, ErrorMessage = "Must be at most 30 characters")]
        public string Email { get; set; }

    }
}
