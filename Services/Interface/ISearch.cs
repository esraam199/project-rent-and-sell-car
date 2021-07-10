using CarsApi.Models;
using CarsApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.Services.Interface
{
    public interface ISearch
    {
        List<SearchViewModel> GetAll(decimal minprice, decimal maxprice, int brand, int model, int body, int year);
        List<SearchViewModel> GetRent(decimal minprice, decimal maxprice, int brand, int model, int body, int year);
        List<SearchViewModel> SearchViewModel(int Brand);
        List<SearchViewModel> SearchViewModel();
    }
}
