using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.ViewModels
{

    public class SearchOutputViewModel
    {
        public bool IsSuccess { get; set; }
        public List<SearchViewModel> SearchResults = new List<SearchViewModel>();
    }

    public class SearchViewModel
    {
        public decimal price { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Img { get; set; }
        public int modelclassId { get; set; }
        public int modelId { get; set; }
        public string ClassName { get; set; }
        public int CarDetailsId { get; set; }
        public string UserEmail { get; set; }
    }
}
