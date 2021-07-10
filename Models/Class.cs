using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.Models
{
    public class Class
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ModelClass> ModelClasses { get; set; }
    }
}
