using CarsApi.Models;
using CarsApi.Services.Interface;
using CarsApi.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.Services.Implementation
{
    public class Rent : IRent
    {
        private CarsContext _db;

        public Rent (CarsContext db)
        {
            _db = db;
        }
        public CarDetailsViewModel EditRentDetails(int UserId)
        {
            var newdetails = _db.CarDetails.Find(UserId);
            var details = _db.CarDetails
                .Include(i => i.ModelClass)
                .Include(i => i.ModelClass.Model)
                .Include(i => i.ModelClass.Model.Brand)
                .Include(i => i.ModelClass.Model.Type)
                .FirstOrDefault(f => f.IsFromSystem == true && f.ModelClassId==newdetails.ModelClassId);

            var dimensions = _db.Dimension.FirstOrDefault(f => f.CarDetailsId == details.Id);
            var exterior = _db.Exterior.FirstOrDefault(f => f.CarDetailsId == details.Id);
            var interior = _db.Interior.FirstOrDefault(f => f.CarDetailsId == details.Id);
            var safety = _db.Safety.FirstOrDefault(f => f.CarDetailsId == details.Id);
            var multimedia = _db.Multimedia.FirstOrDefault(f => f.CarDetailsId == details.Id);
            var performance = _db.Performance.FirstOrDefault(f => f.CarDetailsId == details.Id);

            if (details == null)
                return new CarDetailsViewModel
                {
                    IsSuccess = false
                };

            CarDetailsViewModel model = new CarDetailsViewModel
            {
                CarDetailsId = details.Id,
                IsSuccess = true,
                Price = details.Price,
                CarName = details.ModelClass.Model.Brand.Name + " " + details.ModelClass.Model.Name + " " + details.ModelClass.Model.Year,
                ClassName = details.ModelClass.ClassName,
                CarType = details.ModelClass.Model.Type.Name,
                LuggageBoxCapacity = dimensions.LuggageBoxCapacity,
                Clearance = dimensions.Clearance,
                Width = dimensions.Width,
                Height = dimensions.Height,
                Wheelbase = dimensions.Wheelbase,
                Length = dimensions.Length,
                TrimSize = exterior.TrimSize,
                RainSensor = exterior.RainSensor,
                AutoFoldingMirror = exterior.AutoFoldingMirror,
                PanoramaRoof = exterior.PanoramaRoof,
                LedDaytimeRunningLamps = exterior.LedDaytimeRunningLamps,
                Headlamps = exterior.Headlamps,
                FogLambs = exterior.FogLambs,
                AutoLighting = exterior.AutoLighting,
                RearLambs = exterior.RearLambs,
                SunRoof = exterior.SunRoof,
                WheelsWithTireSize = exterior.WheelsWithTireSize,
                LightSensors = exterior.LightSensors,
                PowerMirrors = exterior.PowerMirrors,
                PrivacyGlass = exterior.PrivacyGlass,
                CenterLock = interior.CenterLock,
                RearParkingSensors = interior.RearParkingSensors,
                AmbientLighting = interior.AmbientLighting,
                ElectronicWindow = interior.ElectronicWindow,
                DriveModeSelect = interior.DriveModeSelect,
                LeatherTransmission = interior.LeatherTransmission,
                VariableHeatedDriverPassengerSeat = interior.VariableHeatedDriverPassengerSeat,
                MovableSteeringWheel = interior.MovableSteeringWheel,
                RearViewCamera = interior.RearViewCamera,
                KeylessStartStop = interior.KeylessStartStop,
                BackHolder = interior.BackHolder,
                AutoDimmingMirror = interior.AutoDimmingMirror,
                AutoFoldingBackSeats = interior.AutoFoldingBackSeats,
                Ac = interior.Ac,
                Dashboard = interior.Dashboard,
                FrontCamera = interior.FrontCamera,
                PassengerSeat = interior.PassengerSeat,
                LeatherSteeringWheel = interior.LeatherSteeringWheel,
                PaddleShifters = interior.PaddleShifters,
                NumberOfSeats = interior.NumberOfSeats,
                PowerTailgate = interior.PowerTailgate,
                AcBackSeats = interior.AcBackSeats,
                DriverSeat = interior.DriverSeat,
                FrontParkingSensors = interior.FrontParkingSensors,
                KeylessEntry = interior.KeylessEntry,
                LeatherSeats = interior.LeatherSeats,
                MultiFunction = interior.MultiFunction,
                WirlessCharger = multimedia.WirlessCharger,
                Bluetooth = multimedia.Bluetooth,
                SmartphoneLinkSystem = multimedia.SmartphoneLinkSystem,
                Touchscreen = multimedia.Touchscreen,
                Usb = multimedia.Usb,
                MultifunctionSteeringWheel = multimedia.MultifunctionSteeringWheel,
                CdPlayer = multimedia.CdPlayer,
                Speakers = multimedia.Speakers,
                NavigationSystem = multimedia.NavigationSystem,
                Aux = multimedia.Aux,
                HeadUpDisplay = multimedia.HeadUpDisplay,
                Cylinders = performance.Cylinders,
                FuelTankCapacity = performance.FuelTankCapacity,
                FuelType = performance.FuelType,
                MaxToruqe = performance.MaxToruqe,
                FuelConsumption = performance.FuelConsumption,
                GearShifts = performance.GearShifts,
                Acceleration = performance.Acceleration,
                MaxPower = performance.MaxPower,
                MaxSpeed = performance.MaxSpeed,
                TractionControl = safety.TractionControl,
                ElectroncStabilityControl = safety.ElectroncStabilityControl,
                AntiLockBrakingSystem = safety.AntiLockBrakingSystem,
                TirePressureMonitoring = safety.TirePressureMonitoring,
                SpeedLimiter = safety.SpeedLimiter,
                AutoStartStopFunctions = safety.AutoStartStopFunctions,
                PowerAssistedSteering = safety.PowerAssistedSteering,
                Immobilizer = safety.Immobilizer,
                ElectronicBrakeForceDistribution = safety.ElectronicBrakeForceDistribution,
                ChildSeats = safety.ChildSeats,
                HillAssist = safety.HillAssist,
                Airbags = safety.Airbags,
                ActiveParkAssist = safety.ActiveParkAssist,
                ElectricHandbrake = safety.ElectricHandbrake,
                CruiseControl = safety.CruiseControl
            };

            return model;
        }

        public MessageResponseViewModel SaveRentDetails(CarDetailsViewModel model,int cardetailsid)
        {
            var cardetails = _db.CarDetails.Find(cardetailsid);
            cardetails.Price = model.Price;
            Dimension Dim = new Dimension
            {
                LuggageBoxCapacity = model.LuggageBoxCapacity,
                Clearance = model.Clearance,
                Width = model.Width,
                Height = model.Height,
                Wheelbase = model.Wheelbase,
                Length = model.Length,
                CarDetailsId=cardetailsid
            };
            Exterior exterior = new Exterior
            {
                TrimSize = model.TrimSize,
                RainSensor = model.RainSensor,
                AutoFoldingMirror = model.AutoFoldingMirror,
                PanoramaRoof = model.PanoramaRoof,
                LedDaytimeRunningLamps = model.LedDaytimeRunningLamps,
                Headlamps = model.Headlamps,
                FogLambs = model.FogLambs,
                AutoLighting = model.AutoLighting,
                RearLambs = model.RearLambs,
                SunRoof = model.SunRoof,
                WheelsWithTireSize = model.WheelsWithTireSize,
                LightSensors = model.LightSensors,
                PowerMirrors = model.PowerMirrors,
                PrivacyGlass = model.PrivacyGlass,
                CarDetailsId = cardetailsid
            };
            Interior interior = new Interior
            {
                CenterLock = model.CenterLock,
                RearParkingSensors = model.RearParkingSensors,
                AmbientLighting = model.AmbientLighting,
                ElectronicWindow = model.ElectronicWindow,
                DriveModeSelect = model.DriveModeSelect,
                LeatherTransmission = model.LeatherTransmission,
                VariableHeatedDriverPassengerSeat = model.VariableHeatedDriverPassengerSeat,
                MovableSteeringWheel = model.MovableSteeringWheel,
                RearViewCamera = model.RearViewCamera,
                KeylessStartStop = model.KeylessStartStop,
                BackHolder = model.BackHolder,
                AutoDimmingMirror = model.AutoDimmingMirror,
                AutoFoldingBackSeats = model.AutoFoldingBackSeats,
                Ac = model.Ac,
                Dashboard = model.Dashboard,
                FrontCamera = model.FrontCamera,
                PassengerSeat = model.PassengerSeat,
                LeatherSteeringWheel = model.LeatherSteeringWheel,
                PaddleShifters = model.PaddleShifters,
                NumberOfSeats = model.NumberOfSeats,
                PowerTailgate = model.PowerTailgate,
                AcBackSeats = model.AcBackSeats,
                DriverSeat = model.DriverSeat,
                FrontParkingSensors = model.FrontParkingSensors,
                KeylessEntry = model.KeylessEntry,
                LeatherSeats = model.LeatherSeats,
                MultiFunction = model.MultiFunction,
                CarDetailsId = cardetailsid
            };
            Multimedia multimedia = new Multimedia
            {
                WirlessCharger = model.WirlessCharger,
                Bluetooth = model.Bluetooth,
                SmartphoneLinkSystem = model.SmartphoneLinkSystem,
                Touchscreen = model.Touchscreen,
                Usb = model.Usb,
                MultifunctionSteeringWheel = model.MultifunctionSteeringWheel,
                CdPlayer = model.CdPlayer,
                Speakers = model.Speakers,
                NavigationSystem = model.NavigationSystem,
                Aux = model.Aux,
                HeadUpDisplay = model.HeadUpDisplay,
                CarDetailsId = cardetailsid
            };
            Performance performance = new Performance
            {
                Cylinders = model.Cylinders,
                FuelTankCapacity = model.FuelTankCapacity,
                FuelType = model.FuelType,
                MaxToruqe = model.MaxToruqe,
                FuelConsumption = model.FuelConsumption,
                GearShifts = model.GearShifts,
                Acceleration = model.Acceleration,
                MaxPower = model.MaxPower,
                MaxSpeed = model.MaxSpeed,
                CarDetailsId = cardetailsid
            };
            Safety safety = new Safety
            {
                TractionControl = model.TractionControl,
                ElectroncStabilityControl = model.ElectroncStabilityControl,
                AntiLockBrakingSystem = model.AntiLockBrakingSystem,
                TirePressureMonitoring = model.TirePressureMonitoring,
                SpeedLimiter = model.SpeedLimiter,
                AutoStartStopFunctions = model.AutoStartStopFunctions,
                PowerAssistedSteering = model.PowerAssistedSteering,
                Immobilizer = model.Immobilizer,
                ElectronicBrakeForceDistribution = model.ElectronicBrakeForceDistribution,
                ChildSeats = model.ChildSeats,
                HillAssist = model.HillAssist,
                Airbags = model.Airbags,
                ActiveParkAssist = model.ActiveParkAssist,
                ElectricHandbrake = model.ElectricHandbrake,
                CruiseControl = model.CruiseControl,
                CarDetailsId = cardetailsid
            };
           
            try
            {
                _db.Dimension.Add(Dim);
                _db.Exterior.Add(exterior);
                _db.Interior.Add(interior);
                _db.Multimedia.Add(multimedia);
                _db.Safety.Add(safety);
                _db.Performance.Add(performance);
                _db.SaveChanges();
                return new MessageResponseViewModel { IsSuccess=true,Message="Saved Data"} ;
            }
            catch (Exception)
            {
                return new MessageResponseViewModel { IsSuccess = false, Message = "Not Saved Data" };
            }
        }
    }
}
