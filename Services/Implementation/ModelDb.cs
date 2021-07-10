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
    public class ModelDb : IModel
    {
        private CarsContext _db;
        public ModelDb(CarsContext db)
        {
            _db = db;
        }

        public MessageResponseViewModel AddModel(Model model)
        {
            try
            {
                _db.Add(model);
                _db.SaveChanges();
                return new MessageResponseViewModel
                {
                    IsSuccess = true,
                    Message = $"Model {model.Name} is added successfully"
                };
            }
            catch (Exception)
            {
                return new MessageResponseViewModel
                {
                    IsSuccess = false,
                    Message = "failed to add model"
                };
            }
        }

        public MessageResponseViewModel EditModel(int modelId, Model newModelData)
        {
            if (IsFilled(newModelData))
            {
                var model = GetModelById(modelId);
                if (model == null)
                    return new MessageResponseViewModel
                    {
                        IsSuccess = false,
                        Message = "Model is not found"
                    };

                try
                {
                    model.Name = newModelData.Name;
                    model.Year = newModelData.Year;
                    model.BrandId = newModelData.BrandId;
                    model.TypeId = newModelData.TypeId;

                    _db.SaveChanges();
                    return new MessageResponseViewModel
                    {
                        IsSuccess = true,
                        Message = "Model Updated Successfully"
                    };
                }
                catch (Exception)
                {
                    return new MessageResponseViewModel
                    {
                        IsSuccess = false,
                        Message = "Failed to Update"
                    };
                }
            }
            else
            {
                return new MessageResponseViewModel
                {
                    IsSuccess = false,
                    Message = "fields are empty"
                };
            }
        }

        public List<Model> GetAllModels()
        {
            return _db.Models.ToList();
        }

        public List<ModelViewModel> GetAllModelsInOneBrand(int brandId, int year)
        {
            List<ModelViewModel> ReturnedModels = new List<ModelViewModel>();
            var models = _db.Models.Where(w => w.Brand.Id == brandId).ToList();

            if (year != 0)
                models = models.Where(w => w.Year == year).ToList();

            foreach (var model in models)
            {
                ModelViewModel modelView = new ModelViewModel
                {
                    Id = model.Id,
                    Name = model.Name + " - " + model.Year
                };
                ReturnedModels.Add(modelView);
            }
            return ReturnedModels;
        }

        public List<ModelViewModel> GetAllModelsVM()
        {
            List<ModelViewModel> ReturnedModels = new List<ModelViewModel>();
            var models = GetAllModels();

            foreach (var model in models)
            {
                ModelViewModel modelView = new ModelViewModel
                {
                    Id = model.Id,
                    Name = model.Name + " - " + model.Year
                };
                ReturnedModels.Add(modelView);
            }

            return ReturnedModels;
        }

        public Model GetModelById(int modelId)
        {
            return _db.Models.Include(i => i.ModelClasses).FirstOrDefault(f => f.Id == modelId);
        }

        private bool IsFilled(Model model)
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
