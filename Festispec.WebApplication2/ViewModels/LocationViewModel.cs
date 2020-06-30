using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Festispec.WebApplication.ViewModels
{
    public class LocationViewModel
    {
        [Required(ErrorMessage = "Gelieve een postcode in te vullen")]
        [StringLength(6)]
        [RegularExpression(@"[0-9]{4}[a-zA-Z]{2}")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Gelieve een nummer in te vullen")]
        public string Number { get; set; }

        public bool ShowMessage { get; set; }
    }
}
