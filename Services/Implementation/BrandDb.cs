using CarsApi.Models;
using CarsApi.Services.Interface;
using CarsApi.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CarsApi.Services.Implementation
{
    public class BrandDb : IBrand
    {
        private CarsContext _db;
        public BrandDb(CarsContext db)
        {
            _db = db;
        }

        public MessageResponseViewModel AddBrand(Brand brand)
        {
            try
            {
                _db.Brand.Add(brand);
                _db.SaveChanges();
                return new MessageResponseViewModel
                {
                    IsSuccess = true,
                    Message = $"Brand {brand.Name} added successfully"
                };
            }
            catch (Exception)
            {

                return new MessageResponseViewModel
                {
                    IsSuccess = false,
                    Message = "failed to add brand"
                };
            }
        }

        public MessageResponseViewModel EditBrand(int brandId, UpdateBrandViewModel newBrandData)
        {
            if (IsFilled(newBrandData))
            {
                Brand brand = GetBrandByID(brandId);

                if (brand == null)
                    return new MessageResponseViewModel
                    {
                        IsSuccess = false,
                        Message = "Brand Is not found"
                    };

                try
                {
                    brand.Name = newBrandData.Name;
                    brand.Nationality = newBrandData.Nationality;
                    brand.Brief = newBrandData.Brief;

                    _db.SaveChanges();
                    return new MessageResponseViewModel
                    {
                        IsSuccess = true,
                        Message = "Brand Updated Successfully"
                    };
                }
                catch (Exception)
                {
                    return new MessageResponseViewModel
                    {
                        IsSuccess = false,
                        Message = "Update failed!"
                    };
                }
            }
            else
            {
                return new MessageResponseViewModel
                {
                    IsSuccess = false,
                    Message = "All Fields Are Empty!!"
                };
            }

        }

        public List<Brand> GetAllBrands()
        {
            return _db.Brand.OrderBy(o=>o.Name).ToList();
        }

        public Brand GetBrandByID(int brandID)
        {
            return _db.Brand.Include(i => i.Models).FirstOrDefault(f => f.Id == brandID);

        }

        private bool IsFilled(UpdateBrandViewModel model)
        {
            PropertyInfo[] props = model.GetType().GetProperties();

            foreach (var prop in props)
            {
                if (prop.PropertyType == typeof(string))
                {
                    string val = (string)prop.GetValue(model);
                    if (!string.IsNullOrEmpty(val))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
