using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.ViewModels
{
    public class UserProfileViewModel
    {
        public bool IsSuccess { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
    }
}
