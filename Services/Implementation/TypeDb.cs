using CarsApi.Models;
using CarsApi.Services.Interface;
using CarsApi.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Type = CarsApi.Models.Type;

namespace CarsApi.Services.Implementation
{
    public class TypeDb : IType
    {
        private CarsContext _db;
        public TypeDb(CarsContext db)
        {
            _db = db;
        }

        public MessageResponseViewModel AddType(Type type)
        {
            try
            {
                _db.Type.Add(type);
                _db.SaveChanges();
                return new MessageResponseViewModel
                {
                    IsSuccess = true,
                    Message = $"Type ${type.Name} is added successfully"
                };
            }
            catch (Exception)
            {
                return new MessageResponseViewModel
                {
                    IsSuccess = false,
                    Message = "faild to add type"
                };
            }
        }

        public MessageResponseViewModel EditType(int id, Type newtype)
        {
            if (IsFilled(newtype))
            {
                var type = GetTypeById(id);

                if (type == null)
                    return new MessageResponseViewModel
                    {
                        IsSuccess = false,
                        Message = "Type is not found"
                    };

                try
                {
                    type.Name = newtype.Name;
                    _db.SaveChanges();
                    return new MessageResponseViewModel
                    {
                        IsSuccess = true,
                        Message = "Type Updated Successfully"
                    };
                }
                catch (Exception)
                {

                    return new MessageResponseViewModel
                    {
                        IsSuccess = false,
                        Message = "Update faild"
                    };
                }
            }

            return new MessageResponseViewModel
            {
                IsSuccess = false,
                Message = "All Fields Are Empty!!"
            };
        }

        public List<Type> GetAllTypes()
        {
            return _db.Type.ToList();
        }

        public Type GetTypeById(int id)
        {
            return _db.Type.Include(i => i.Models).FirstOrDefault(f => f.Id == id);
        }

        public List<Type> GetTypesForPreSearch()
        {
            return _db.Type.Where(w => w.Icon != null).ToList();
        }

        public List<Type> GetTypesInBrand(int brandId, int year, int modelId)
        {
            List<Model> types = _db.Models
                .Include(i => i.Brand)
                .Include(i => i.Type)
                .ToList();

            List<Type> Types = new List<Type>();

            if (brandId != 0)
                types = types.Where(w => w.Brand.Id == brandId).ToList();

            if (year != 0)
                types = types.Where(w => w.Year == year).ToList();

            if (modelId != 0)
                types = types.Where(w => w.Id == modelId).ToList();



            Types = types.Select(s => s.Type).Distinct().ToList();
            Types.ForEach(f => f.Models = null);

            return Types;
        }

        private bool IsFilled(Type type)
        {
            PropertyInfo[] props = type.GetType().GetProperties();

            foreach (var prop in props)
            {
                if (prop.PropertyType == typeof(string))
                {
                    string val = (string)prop.GetValue(type);
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
