using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.Models
{
    public class Multimedia
    {
        [Key]
        public int Id { get; set; }
        public bool? WirlessCharger { get; set; }
        public bool? Bluetooth { get; set; }
        public string SmartphoneLinkSystem { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Touchscreen { get; set; }
        public bool? Usb { get; set; }
        public bool? MultifunctionSteeringWheel { get; set; }
        public bool? CdPlayer { get; set; }
        public bool? Speakers { get; set; }
        public bool? NavigationSystem { get; set; }
        public bool? Aux { get; set; }
        public bool? HeadUpDisplay { get; set; }

        [ForeignKey("CarDetails")]
        public int? CarDetailsId { get; set; }

        public virtual CarDetails CarDetails { get; set; }
    }
}
