using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.ViewModels
{
    public class SearchFormViewModel
    {
        public int Year { get; set; }
        public int ModelId { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }

    }
}
