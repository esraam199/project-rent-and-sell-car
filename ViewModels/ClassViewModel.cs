using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.ViewModels
{

    public class ClassViewModelList
    {
        public bool IsSuccess { get; set; }
        public List<ClassViewModel> ClassesList = new List<ClassViewModel>();
    }
    public class ClassViewModel
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
    }
}
