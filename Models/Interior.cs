using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.Models
{
    public class Interior
    {
        [Key]
        public int Id { get; set; }
        public bool? CenterLock { get; set; }
        public bool? RearParkingSensors { get; set; }
        public string AmbientLighting { get; set; }
        public bool? ElectronicWindow { get; set; }
        public int? DriveModeSelect { get; set; }
        public bool? LeatherTransmission { get; set; }
        public bool? VariableHeatedDriverPassengerSeat { get; set; }
        public bool? MovableSteeringWheel { get; set; }
        public bool? RearViewCamera { get; set; }
        public bool? KeylessStartStop { get; set; }
        public bool? BackHolder { get; set; }
        public bool? AutoDimmingMirror { get; set; }
        public bool? AutoFoldingBackSeats { get; set; }
        public string Ac { get; set; }
        public string Dashboard { get; set; }
        public bool? FrontCamera { get; set; }
        public string PassengerSeat { get; set; }
        public bool? LeatherSteeringWheel { get; set; }
        public bool? PaddleShifters { get; set; }
        public int? NumberOfSeats { get; set; }
        public bool? PowerTailgate { get; set; }
        public bool? AcBackSeats { get; set; }
        public string DriverSeat { get; set; }
        public bool? FrontParkingSensors { get; set; }
        public bool? KeylessEntry { get; set; }
        public bool? LeatherSeats { get; set; }
        public bool? MultiFunction { get; set; }

        [ForeignKey("CarDetails")]
        public int? CarDetailsId { get; set; }

        public virtual CarDetails CarDetails { get; set; }
    }
}
