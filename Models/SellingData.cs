using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.Models
{
    public class SellingData
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Km { get; set; }
        public bool? AgencyMaintainance { get; set; }
        public bool? Guarntee { get; set; }
        public bool? Fabrique { get; set; }
        public string Notes { get; set; }

        [ForeignKey("UserCar")]
        public int? UserCarId { get; set; }

        public virtual UserCar UserCar { get; set; }
    }
}
