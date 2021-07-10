using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }

        [StringLength(14, MinimumLength = 14)]
        public string NationalId { get; set; }

        public string PersonalLicenceNo { get; set; }

        public virtual ICollection<UserCar> UserCars { get; set; }
        public virtual ICollection<UserPhone> UserPhones { get; set; }
    }
}
