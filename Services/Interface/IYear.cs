using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.Services.Interface
{
    public interface IYear
    {
        List<string> GetYearsOfModelsInBrand(int brandId);
        List<string> GetAllYears();
    }
}
