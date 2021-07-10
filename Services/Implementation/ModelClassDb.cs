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
    public class ModelClassDb : IModelClass
    {
        private CarsContext _db;

        public ModelClassDb(CarsContext db)
        {
            _db = db;
        }

        public ChooseCarViewModel GetModelClassByModelAndClass(int modelId, int classId)
        {
            var modelClass = _db.ModelClass
                .Include(i => i.Model)
                .Include(i => i.Model.Brand)
                .Include(i=>i.CarDetails.Where(w=>w.IsFromSystem == true))
                .ThenInclude(ti=>ti.CarPhotos)
                .Where(w => w.Model.Id == modelId && w.Class.Id == classId)
                .FirstOrDefault();

            if (modelClass == null)
                return new ChooseCarViewModel
                {
                    IsSuccess = false
                };

            return new ChooseCarViewModel
            {
                IsSuccess = true,
                CarName = modelClass.Model.Brand.Name + " " + modelClass.Model.Name + " " + modelClass.Model.Year,
                ClassName = modelClass.ClassName,
                ImgName = modelClass.CarDetails.FirstOrDefault().CarPhotos.Select(s=>s.PhotoName).FirstOrDefault(),
                ModelClassId = modelClass.Id,
                CarDetailsId = modelClass.CarDetails.FirstOrDefault().Id
            };
        }
    }
}
