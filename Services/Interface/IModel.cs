using CarsApi.Models;
using CarsApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.Services.Interface
{
    public interface IModel
    {
        Model GetModelById(int modelId);
        MessageResponseViewModel EditModel(int modelId, Model newModelData);
        List<Model> GetAllModels();
        MessageResponseViewModel AddModel(Model model);
        List<ModelViewModel> GetAllModelsInOneBrand(int brandId, int year);
        List<ModelViewModel> GetAllModelsVM();

    }
}
