using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.Models
{
    public class Exterior
    {
        [Key]
        public int Id { get; set; }
        public string TrimSize { get; set; }
        public bool? RainSensor { get; set; }
        public bool? AutoFoldingMirror { get; set; }
        public bool? PanoramaRoof { get; set; }
        public bool? LedDaytimeRunningLamps { get; set; }
        public string Headlamps { get; set; }
        public bool? FogLambs { get; set; }
        public bool? AutoLighting { get; set; }
        public string RearLambs { get; set; }
        public bool? SunRoof { get; set; }
        public string WheelsWithTireSize { get; set; }
        public bool? LightSensors { get; set; }
        public bool? PowerMirrors { get; set; }
        public bool? PrivacyGlass { get; set; }

        [ForeignKey("CarDetails")]
        public int? CarDetailsId { get; set; }

        public virtual CarDetails CarDetails { get; set; }
    }
}
