using CarsApi.Models;
using CarsApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.Services.Interface
{
    public interface IBrand
    {
        Brand GetBrandByID(int brandID);
        MessageResponseViewModel EditBrand(int brandId, UpdateBrandViewModel newBrandData);
        List<Brand> GetAllBrands();
        MessageResponseViewModel AddBrand(Brand brand);
    }
}
