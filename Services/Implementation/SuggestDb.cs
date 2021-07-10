using CarsApi.Models;
using CarsApi.Services.Interface;
using CarsApi.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.Services.Implementation
{
    public class SuggestDb : ISuggest
    {
        private CarsContext _db;
        private List<CarDetails> Cars;

        public SuggestDb(CarsContext db)
        {
            _db = db;
        }

        public SearchOutputViewModel SuggestCar(SuggestionViewModel suggestForm)
        {
            SearchOutputViewModel result = new SearchOutputViewModel();

            Cars = _db.CarDetails
               .Include(i => i.Dimensions)
               .Include(i => i.Safeties)
               .Include(i => i.Performances)
               .Include(i => i.Interiors)
               .Include(i => i.Exteriors)
               .Include(i => i.Multimedia)
               .Include(i => i.ModelClass)
               .Include(i => i.ModelClass.Model)
               .Include(i => i.ModelClass.Model.Brand)
               .Include(i => i.ModelClass.Model.Type)
               .Include(i => i.CarPhotos)
               .ToList();

            FilterByPricing(suggestForm.PriceRange);
            FilterByState(suggestForm.CarState);
            FilterByCriteria(suggestForm.CarCriteria);
            FilterByUsage(suggestForm.CarUsage);
            FilterByWays(suggestForm.WaysType);

            foreach (var car in Cars.Take(5))
            {
                SearchViewModel output = new SearchViewModel
                {
                    Brand = car.ModelClass.Model.Brand.Name,
                    Model = car.ModelClass.Model.Name,
                    ClassName = car.ModelClass.ClassName,
                    Img = car.CarPhotos.Select(s => s.PhotoName).FirstOrDefault(),
                    modelclassId = car.ModelClass.Id,
                    modelId = car.ModelClass.Model.Id,
                    price = car.Price.Value,
                    CarDetailsId = car.Id
                };
                result.SearchResults.Add(output);
            }

            if (result.SearchResults.Count == 0)
                result.IsSuccess = false;

            result.IsSuccess = true;

            return result;

        }

        private void FilterByPricing(int value)
        {
            List<CarDetails> newCarList = new List<CarDetails>();
            switch (value)
            {
                case 1:
                    newCarList = Cars.Where(w => w.Price <= 100000).ToList();
                    break;
                case 2:
                    newCarList = Cars.Where(w => w.Price >= 100000 && w.Price <= 200000).ToList();
                    break;
                case 3:
                    newCarList = Cars.Where(w => w.Price >= 200000 && w.Price <= 300000).ToList();
                    break;
                case 4:
                    newCarList = Cars.Where(w => w.Price >= 300000 && w.Price <= 400000).ToList();
                    break;
                case 5:
                    newCarList = Cars.Where(w => w.Price >= 400000 && w.Price <= 500000).ToList();
                    break;
                case 6:
                    newCarList = Cars.Where(w => w.Price >= 500000 && w.Price <= 600000).ToList();
                    break;
                case 7:
                    newCarList = Cars.Where(w => w.Price >= 600000 && w.Price <= 700000).ToList();
                    break;
                case 8:
                    newCarList = Cars.Where(w => w.Price >= 700000 && w.Price <= 1000000).ToList();
                    break;
                case 9:
                    newCarList = Cars.Where(w => w.Price >= 1000000).ToList();
                    break;
                default:
                    break;
            }

            CheckAndAssign(newCarList);
        }

        private void FilterByState(int value)
        {
            List<CarDetails> newCarList = new List<CarDetails>();
            if (value == 1)
            {
                newCarList = Cars.Where(w => w.IsFromSystem == true).ToList();
            }
            else if (value == 1)
            {
                newCarList = Cars.Where(w => w.IsFromSystem == false).ToList();
            }

            CheckAndAssign(newCarList);
        }

        private void FilterByUsage(int value)
        {
            List<CarDetails> newCarList = new List<CarDetails>();

            if (value == 1)
            {
                newCarList = Cars
                    .Where(w => w.Performances.FirstOrDefault().FuelConsumption <= 7)
                    .ToList();

                newCarList = Cars.Where(w => w.ModelClass.Model.Id == 35 || w.ModelClass.Model.Id == 242 || w.ModelClass.Model.Id == 243 || w.ModelClass.Model.Id == 127 || w.ModelClass.Model.Brand.Id == 18).ToList();

            }
            else if (value == 2)
            {
                newCarList = Cars
                    .Where(w => w.Interiors.FirstOrDefault().NumberOfSeats >= 5 && w.Dimensions.FirstOrDefault().LuggageBoxCapacity >= 430).ToList();
                newCarList = Cars.Where(w => w.Safeties.FirstOrDefault().ElectroncStabilityControl == true && w.Safeties.FirstOrDefault().ElectronicBrakeForceDistribution == true && w.Safeties.FirstOrDefault().AntiLockBrakingSystem == true && w.Safeties.FirstOrDefault().Airbags >= 2).ToList();

            }
            else if (value == 3)
            {
                newCarList = Cars.Where(w => w.Performances.FirstOrDefault().Cylinders >= 4 && w.Performances.FirstOrDefault().Acceleration <= 9.5).ToList();
            }

            CheckAndAssign(newCarList);
        }

        private void FilterByCriteria(int value)
        {
            List<CarDetails> newCarList = new List<CarDetails>();
            switch (value)
            {
                case 1:
                    newCarList = Cars.Where(w => w.Performances.FirstOrDefault().FuelConsumption <= 7).ToList();
                    break;
                case 2:
                    newCarList = Cars
                        .OrderBy(o => o.Performances.FirstOrDefault().Acceleration)
                        .ThenBy(th => th.Performances.FirstOrDefault().MaxSpeed).ToList();
                    break;
                default:
                    break;
            }

            CheckAndAssign(newCarList);
        }

        private void FilterByWays(int value)
        {
            List<CarDetails> newCarList = new List<CarDetails>();
            switch (value)
            {
                case 2:
                    newCarList = Cars
                        .Where(w => w.ModelClass.Model.Type.Id == 4 || w.ModelClass.Model.Type.Id == 6)
                        .ToList();
                    break;
                case 3:
                    newCarList = Cars
                        .Where(w => w.Safeties.FirstOrDefault().ElectroncStabilityControl == true && w.Safeties.FirstOrDefault().ElectronicBrakeForceDistribution == true && w.Safeties.FirstOrDefault().AntiLockBrakingSystem == true)
                        .ToList();
                    break;
                case 4:
                    newCarList = Cars
                        .Where(w => w.ModelClass.Model.Type.Id != 1 || w.ModelClass.Model.Type.Id != 3)
                        .ToList();
                    break;
                default:
                    break;
            }

            CheckAndAssign(newCarList);
        }

        private void CheckAndAssign(List<CarDetails> newCarsList)
        {
            if (newCarsList.Count != 0)
                Cars = newCarsList;
        }
    }
}
