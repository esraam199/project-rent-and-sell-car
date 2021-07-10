using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.Models
{
    public class ModelClass
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("Model")]
        public int? ModelId { get; set; }
        [ForeignKey("Class")]
        public int? ClassId { get; set; }
        public string ClassName { get; set; }

        public virtual Class Class { get; set; }
        public virtual Model Model { get; set; }
        public virtual ICollection<CarDetails> CarDetails { get; set; }
    }
}
