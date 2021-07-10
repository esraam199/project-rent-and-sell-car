using CarsApi.Models;
using CarsApi.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.Services.Implementation
{
    public class YearDb : IYear
    {
        private CarsContext _db;

        public YearDb(CarsContext db)
        {
            _db = db;
        }

        public List<string> GetAllYears()
        {
            List<string> ReturnedYears = new List<string>();
            var years = _db.Models
                .Select(s => s.Year)
                .Distinct()
                .ToList();

            foreach (var year in years)
            {
                ReturnedYears.Add(year.ToString());
            }

            return ReturnedYears;
        }

        public List<string> GetYearsOfModelsInBrand(int brandId)
        {
            List<string> ReturnedYears = new List<string>();

            var years = _db.Models
                .Include(i => i.Brand)
                .Where(w => w.Brand.Id == brandId)
                .Select(s => s.Year.Value)
                .Distinct()
                .ToList();

            foreach (var year in years)
            {
                ReturnedYears.Add(year.ToString());
            }

            return ReturnedYears;
        }
    }
}
