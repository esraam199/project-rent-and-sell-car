using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        public string PhoneNo { get; set; }

        [Required]
        [NotMapped]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password and Confirm Password not match")]
        public string ConfirmPassword { get; set; }


        [StringLength(14, MinimumLength = 14)]
        public string NationalId { get; set; }

        public string PersonalLicenceNo { get; set; }
    }
}
