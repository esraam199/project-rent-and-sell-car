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
    public class Search : ISearch
    {
        private CarsContext _db;
        public Search(CarsContext db)
        {
            _db = db;
        }

       public List<SearchViewModel> GetAll(decimal minprice, decimal maxprice, int brand, int model, int body, int year)
        {
            List<SearchViewModel> modelPrices = new List<SearchViewModel>();
            List<CarDetails> Cars = _db.CarDetails.Include(a => a.ModelClass).Include(a => a.ModelClass.Model).Include(a => a.CarPhotos)
                .Include(a => a.ModelClass.Model.Type).Include(a=>a.ModelClass.Model.Brand).Include(a => a.ModelClass.Class)
                .Where(a=>a.IsFromSystem==true)
                .ToList();

            if (minprice != 0)
                Cars = FilterByMinPrice(Cars, minprice);
            if (maxprice != 0)
                Cars = FilterByMaxPrice(Cars, maxprice);
            if (brand != 0)
                Cars = FilterByBrand(Cars, brand);
            if (model != 0)
                Cars = FilterByModel(Cars, model);
            if (body != 0)
                Cars = FilterByBody(Cars, body);
            if (year != 0)
                Cars = FilterByYear(Cars, year);

            foreach (var M in Cars)
            {
                SearchViewModel model_Price = new SearchViewModel
                {
                    Model = M.ModelClass.Model.Name,
                    price = (decimal)M.Price,
                    Brand = M.ModelClass.Model.Brand.Name,
                    Img = M.CarPhotos.Select(a => a.PhotoName).FirstOrDefault(),
                    ClassName=M.ModelClass.ClassName,
                    modelclassId = M.ModelClass.Id,
                    modelId = M.ModelClass.Model.Id,
                    CarDetailsId = M.Id
                };

                modelPrices.Add(model_Price);
            }
            return modelPrices;
            
        }

        public List<SearchViewModel> SearchViewModel(int Brand)
        {
            List<SearchViewModel> modelPrices = new List<SearchViewModel>();
            var cardetails = _db.CarDetails.Include(a => a.ModelClass).Include(a => a.ModelClass.Model).Include(a => a.CarPhotos)
                .Where(a => a.ModelClass.Model.Brand.Id == Brand && a.ModelClass.ClassId == 1)
                .Select(x => new { x.ModelClass.Model, x.Price, x.CarPhotos, x.ModelClass.Model.Brand, x.ModelClass }).ToList();
            foreach (var M in cardetails)
            {
                SearchViewModel model_Price = new SearchViewModel
                {
                    Model = M.Model.Name,
                    price = (decimal)M.Price,
                    Brand = M.Brand.Name,
                    Img = M.CarPhotos.Select(a => a.PhotoName).FirstOrDefault(),
                    modelclassId = M.ModelClass.Id,
                    modelId=M.ModelClass.Model.Id
                };
                modelPrices.Add(model_Price);
            }
            return modelPrices;
        }

        public List<SearchViewModel> GetRent(decimal minprice, decimal maxprice, int brand, int model, int body, int year)
        {
            List<SearchViewModel> modelPrices = new List<SearchViewModel>();
            List<CarDetails> Cars = _db.CarDetails.Include(a => a.ModelClass).Include(a => a.ModelClass.Model).Include(a => a.CarPhotos)
                .Include(a => a.ModelClass.Model.Type).Include(a => a.ModelClass.Model.Brand).Include(a => a.ModelClass.Class)
                .Where(a=>a.IsFromSystem==false)
                .ToList();

            if (minprice != 0)
                Cars = FilterByMinPrice(Cars, minprice);
            if (maxprice != 0)
                Cars = FilterByMaxPrice(Cars, maxprice);
            if (brand != 0)
                Cars = FilterByBrand(Cars, brand);
            if (model != 0)
                Cars = FilterByModel(Cars, model);
            if (body != 0)
                Cars = FilterByBody(Cars, body);
            if (year != 0)
                Cars = FilterByYear(Cars, year);

            foreach (var M in Cars)
            {
                SearchViewModel model_Price = new SearchViewModel
                {
                    Model = M.ModelClass.Model.Name,
                    price = (decimal)M.Price,
                    Brand = M.ModelClass.Model.Brand.Name,
                    Img = M.CarPhotos.Select(a => a.PhotoName).FirstOrDefault(),
                    ClassName = M.ModelClass.ClassName,
                    modelclassId = M.ModelClass.Id,
                    modelId = M.ModelClass.Model.Id
                };

                modelPrices.Add(model_Price);
            }
            return modelPrices;

        }

        public List<SearchViewModel> SearchViewModel()
        {
            List<SearchViewModel> AllRent = new List<SearchViewModel>();
            List<CarDetails> Cars = _db.CarDetails
                .Include(a => a.ModelClass).Include(a => a.ModelClass.Model).Include(a => a.CarPhotos)
                .Include(a => a.ModelClass.Model.Type).Include(a => a.ModelClass.Model.Brand).Include(a => a.ModelClass.Class)
                .Where(a => a.IsFromSystem==false).ToList();
            foreach (var M in Cars)
            {
                SearchViewModel model_Price = new SearchViewModel
                {
                    Model = M.ModelClass.Model.Name,
                    price = (decimal)M.Price,
                    Brand = M.ModelClass.Model.Brand.Name,
                    Img = M.CarPhotos.Select(a => a.PhotoName).FirstOrDefault(),
                    ClassName = M.ModelClass.ClassName,
                    modelclassId = M.ModelClass.Id,
                    modelId = M.ModelClass.Model.Id
                };

                AllRent.Add(model_Price);
            }
            return AllRent;

        }



        private List<CarDetails> FilterByMinPrice(List<CarDetails> myCars, decimal minp)
        {
            return myCars.Where(w => w.Price >= minp).ToList();
        }
        private List<CarDetails> FilterByMaxPrice(List<CarDetails> myCars, decimal maxp)
        {
            return myCars.Where(w => w.Price <= maxp).ToList();
        }
        private List<CarDetails> FilterByBrand(List<CarDetails> myCars, int br)
        {
            return myCars.Where(w => w.ModelClass.Model.Brand.Id == br).ToList();
        }

        private List<CarDetails> FilterByModel(List<CarDetails> myCars, int Md)
        {
            return myCars.Where(w => w.ModelClass.Model.Id == Md).ToList();
        }
        private List<CarDetails> FilterByBody(List<CarDetails> myCars, int body)
        {
            return myCars.Where(w => w.ModelClass.Model.Type.Id == body).ToList();
        }
        private List<CarDetails> FilterByYear(List<CarDetails> myCars, int year)
        {
            return myCars.Where(w => w.ModelClass.Model.Year == year).ToList();
        }
    }
}


