using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Festispec.WebApplication.ViewModels
{
    public class UserViewModel
    {
        public string Name { get; set; }

        [Required(ErrorMessage = "Gelieve een email in te vullen")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Gelieve een wachtwoord in te vullen")]
        [StringLength(255, ErrorMessage = "Wachtwoord moet tussen 5 en 255 karakters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Gelieve een wachtwoord in te vullen")]
        [StringLength(255, ErrorMessage = "Wachtwoord moet tussen 5 en 255 karakters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
