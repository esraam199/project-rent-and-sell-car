using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Nationality { get; set; }
        public string Brief { get; set; }
        public string Icon { get; set; }


        public virtual ICollection<Model> Models { get; set; }
    }
}
