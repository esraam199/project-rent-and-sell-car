using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.ViewModels
{
    public class SuggestionViewModel
    {
        public int PriceRange { get; set; }
        public int CarState { get; set; }
        public int CarUsage { get; set; }
        public int CarCriteria { get; set; }
        public int WaysType { get; set; }
    }
}
