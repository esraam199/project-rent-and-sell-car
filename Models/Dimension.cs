using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.Models
{
    public class Dimension
    {
        [Key]
        public int Id { get; set; }
        public double? LuggageBoxCapacity { get; set; }
        public double? Clearance { get; set; }
        public double? Width { get; set; }
        public double? Wheelbase { get; set; }
        public double? Length { get; set; }
        public double? Height { get; set; }

        [ForeignKey("CarDetails")]
        public int? CarDetailsId { get; set; }

        public virtual CarDetails CarDetails { get; set; }
    }
}
