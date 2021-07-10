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
    public class HomeDb : IHome
    {
        private CarsContext _db;

        public HomeDb(CarsContext db)
        {
            _db = db;
        }

        public HomeCarsList GetCarsDate()
        {
            HomeCarsList CarsList = new HomeCarsList();
            List<int> Ids = GenerateIds();

            try
            {
                var Cars = _db.CarDetails
            .Include(i => i.Performances)
            .Include(i => i.ModelClass.Model.Type)
            .Include(i => i.ModelClass.Model)
            .Include(i => i.ModelClass)
            .Include(i => i.ModelClass.Model.Brand)
            .Include(i => i.CarPhotos)
            .Where(w => Ids.Contains(w.Id)).ToList();

                foreach (var car in Cars)
                {
                    HomeCarViewModel CarModel = new HomeCarViewModel
                    {
                        CarDetailsID = car.Id,
                        CarType = car.ModelClass.Model.Type.Name,
                        Cylinders = car.Performances.Select(s=>s.Cylinders).FirstOrDefault(),
                        Fuel = car.Performances.Select(s=>s.FuelType).FirstOrDefault(),
                        ImageName = car.CarPhotos.Select(s=>s.PhotoName).FirstOrDefault(),
                        Price = car.Price,
                        Year = car.ModelClass.Model.Year,
                        CarName = car.ModelClass.Model.Brand.Name + " " + car.ModelClass.Model.Name,
                        ClassName = car.ModelClass.ClassName,
                        TransmissionType = car.ModelClass.ClassName.Contains("A/T") ? "Automatic" : "Manual"
                    };
                    CarsList.homeCars.Add(CarModel);
                }

                CarsList.IsSuccess = true;
                return CarsList;

            }
            catch (Exception)
            {

                return new HomeCarsList
                {
                    IsSuccess = false
                };
            }

        }

        private List<int> GenerateIds()
        {
            List<int> Ids = new List<int>();
            List<int> BrandIds = new List<int>();
            Random rnd = new Random();
            int id;
            int modelId;
            for (int i = 0; i < 9; i++)
            {
                do
                {
                    id = rnd.Next(1, 591);
                    modelId = GetCarBrandId(id).Value;
                } while (Ids.Contains(id) || BrandIds.Contains(modelId));
                Ids.Add(id);
                BrandIds.Add(modelId);
            }
            return Ids;
        }

        private int? GetCarBrandId(int carDetailsId)
        {
            var model = _db.CarDetails
                .Include(i => i.ModelClass.Model.Brand)
                .FirstOrDefault(f => f.Id == carDetailsId);
            return model.ModelClass.Model.Brand.Id;
        }
    }
}
