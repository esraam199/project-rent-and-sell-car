using CarsApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarsApi.Models;
using Type = CarsApi.Models.Type;

namespace CarsApi.Services.Interface
{
    public interface IType
    {
        Type GetTypeById(int id);
        MessageResponseViewModel EditType(int id, Type type);
        List<Type> GetAllTypes();
        MessageResponseViewModel AddType(Type type);
        List<Type> GetTypesInBrand(int brandId, int year, int modelId);
        List<Type> GetTypesForPreSearch();
    }
}
