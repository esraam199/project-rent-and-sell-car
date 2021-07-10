namespace CarsApi.Services.Implementation
{
    using CarsApi.Models;
    using CarsApi.Services.Interface;
    using CarsApi.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;

    public class ClassDb : IClass
    {
        private CarsContext _db;

        public ClassDb(CarsContext db)
        {
            _db = db;
        }

        public ClassViewModelList GetClassesInModel(int modelId)
        {
            ClassViewModelList ReturnClasses = new ClassViewModelList();

            var classes = _db.ModelClass
                .Include(i => i.Class)
                .Where(w => w.Model.Id == modelId)
                .ToList();

            if (classes == null)
                return new ClassViewModelList
                {
                    IsSuccess = false
                };

            foreach (var @class in classes)
            {
                ClassViewModel classElement = new ClassViewModel
                {
                    ClassId = @class.Class.Id,
                    ClassName = @class.Class.Name + " Trim (" + @class.ClassName + " )"
                };
                ReturnClasses.ClassesList.Add(classElement);
            }
            ReturnClasses.IsSuccess = true;

            return ReturnClasses;
        }
    }
}
