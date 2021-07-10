using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.Models
{
    public class Performance
    {
        [Key]
        public int Id { get; set; }
        public int? Cylinders { get; set; }
        public double? FuelTankCapacity { get; set; }
        public int? FuelType { get; set; }
        public string MaxToruqe { get; set; }
        public double? FuelConsumption { get; set; }
        public int? GearShifts { get; set; }
        public double? Acceleration { get; set; }
        public string MaxPower { get; set; }
        public double? MaxSpeed { get; set; }

        [ForeignKey("CarDetails")]
        public int? CarDetailsId { get; set; }

        public virtual CarDetails CarDetails { get; set; }
    }
}
