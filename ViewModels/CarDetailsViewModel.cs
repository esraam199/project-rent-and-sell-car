using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.ViewModels
{
    public class CarDetailsViewModel
    {
        public bool? IsSuccess { get; set; } 
        public string CarName { get; set; }
        public string ClassName { get; set; }
        public decimal? Price { get; set; }
        public string CarType { get; set; }
        public int CarDetailsId { get; set; }

        //dimensions
        public double? LuggageBoxCapacity { get; set; }
        public double? Clearance { get; set; }
        public double? Width { get; set; }
        public double? Wheelbase { get; set; }
        public double? Length { get; set; }
        public double? Height { get; set; }

        //exterior

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

        //interior

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

        //multimedia

        public bool? WirlessCharger { get; set; }
        public bool? Bluetooth { get; set; }
        public string SmartphoneLinkSystem { get; set; }
        public decimal? Touchscreen { get; set; }
        public bool? Usb { get; set; }
        public bool? MultifunctionSteeringWheel { get; set; }
        public bool? CdPlayer { get; set; }
        public bool? Speakers { get; set; }
        public bool? NavigationSystem { get; set; }
        public bool? Aux { get; set; }
        public bool? HeadUpDisplay { get; set; }

        //performance

        public int? Cylinders { get; set; }
        public double? FuelTankCapacity { get; set; }
        public int? FuelType { get; set; }
        public string MaxToruqe { get; set; }
        public double? FuelConsumption { get; set; }
        public int? GearShifts { get; set; }
        public double? Acceleration { get; set; }
        public string MaxPower { get; set; }
        public double? MaxSpeed { get; set; }

        //safety

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
    }
}
