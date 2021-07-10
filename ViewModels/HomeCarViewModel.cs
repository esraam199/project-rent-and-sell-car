using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.ViewModels
{

    public class HomeCarsList
    {
        public bool IsSuccess { get; set; }
        public List<HomeCarViewModel> homeCars = new List<HomeCarViewModel>();
    }


    public class HomeCarViewModel
    {
        public int CarDetailsID { get; set; }
        public int? Fuel { get; set; }
        public string TransmissionType { get; set; }
        public string CarName { get; set; }
        public string ClassName { get; set; }
        public string CarType { get; set; }
        public int? Cylinders { get; set; }
        public decimal? Price { get; set; }
        public int? Year { get; set; }
        public string ImageName { get; set; }
    }
}
