using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.ViewModels
{
    public class SellingDataViewModel
    {
        public decimal Km { get; set; }
        public bool Maintainance { get; set; }
        public bool Guarntee { get; set; }
        public bool Fabrique { get; set; }
        public decimal Price { get; set; }
        public string Notes { get; set; }
    }
}
