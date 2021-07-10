using CarsApi.Models;
using CarsApi.Services.Interface;
using CarsApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.Services.Implementation
{
    public class GenralDb : IGeneral
    {
        private CarsContext _db;

        public GenralDb(CarsContext db)
        {
            _db = db;
        }
        public GeneralInfoViewModel GetGeneralInfo()
        {
            var brandsNo = _db.Brand.Count();
            var modelsNo = _db.Models.Count();
            var carsNo = _db.CarDetails.Where(w => w.IsFromSystem == true).Count();

            return new GeneralInfoViewModel
            {
                BrandNo = brandsNo,
                CarsNo = carsNo,
                ModelNo = modelsNo
            };
        }
    }
}
