using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.Models
{
    public class Model
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Year { get; set; }


        [ForeignKey("Brand")]
        public int? BrandId { get; set; }
        [ForeignKey("Type")]
        public int? TypeId { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Type Type { get; set; }
        public virtual ICollection<ModelClass> ModelClasses { get; set; }
    }
}
