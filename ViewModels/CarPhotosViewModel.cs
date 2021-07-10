using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.ViewModels
{

    public class ImagesViewModel
    {
        public bool IsSuccess { get; set; }
        public List<CarPhotosViewModel> Images = new List<CarPhotosViewModel>();
    }


    public class CarPhotosViewModel
    {
        public string PreviewImageSrc { get; set; }
        public string ThumbnailImageSrc { get; set; }
        public string Alt { get; set; }
    }
}
