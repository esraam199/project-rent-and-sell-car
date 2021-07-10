using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.Models
{
    public class CarDetails
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName ="decimal(18,2)")]
        public decimal? Price { get; set; }
        public bool IsFromSystem { get; set; }

        [ForeignKey("ModelClass")]
        public int? ModelClassId { get; set; }

        public virtual ModelClass ModelClass { get; set; }
        public virtual ICollection<CarPhoto> CarPhotos { get; set; }
        public virtual ICollection<Dimension> Dimensions { get; set; }
        public virtual ICollection<Exterior> Exteriors { get; set; }
        public virtual ICollection<Interior> Interiors { get; set; }
        public virtual ICollection<Multimedia> Multimedia { get; set; }
        public virtual ICollection<Performance> Performances { get; set; }
        public virtual ICollection<Safety> Safeties { get; set; }
        public virtual ICollection<UserCar> UserCars { get; set; }

    }
}
