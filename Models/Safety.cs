using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.Models
{
    public class Safety
    {
        [Key]
        public int Id { get; set; }
        public bool? TractionControl { get; set; }
        public bool? ElectroncStabilityControl { get; set; }
        public bool? AntiLockBrakingSystem { get; set; }
        public bool? TirePressureMonitoring { get; set; }
        public bool? SpeedLimiter { get; set; }
        public bool? AutoStartStopFunctions { get; set; }
        public string PowerAssistedSteering { get; set; }
        public bool? Immobilizer { get; set; }
        public bool? ElectronicBrakeForceDistribution { get; set; }
        public bool? ChildSeats { get; set; }
        public bool? HillAssist { get; set; }
        public int? Airbags { get; set; }
        public bool? ActiveParkAssist { get; set; }
        public bool? ElectricHandbrake { get; set; }
        public bool? CruiseControl { get; set; }

        [ForeignKey("CarDetails")]
        public int? CarDetailsId { get; set; }

        public virtual CarDetails CarDetails { get; set; }
    }
}
