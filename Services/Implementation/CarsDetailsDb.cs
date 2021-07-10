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
    public class CarsDetailsDb : ICarDetails
    {
        private CarsContext _db;

        public CarsDetailsDb(CarsContext db)
        {
            _db = db;
        }

        public SearchOutputViewModel GetAllCarsClassified(int rent)
        {
            SearchOutputViewModel result = new SearchOutputViewModel();

            var cars = GetAllCars();


            if (rent == 1)
                cars = cars.Where(w => w.SellingData.Count == 0 && w.CarDetails.Dimensions.Count > 0).ToList();

            else if (rent == 0)
                cars = cars.Where(w => w.SellingData.Count > 0 && w.CarDetails.Dimensions.Count > 0).ToList();
            else
                return new SearchOutputViewModel
                {
                    IsSuccess = false
                };

            if (cars.Count == 0)
                return new SearchOutputViewModel
                {
                    IsSuccess = false
                };


            foreach (var car in cars)
            {
                SearchViewModel output = new SearchViewModel
                {
                    Brand = car.CarDetails.ModelClass.Model.Brand.Name,
                    Model = car.CarDetails.ModelClass.Model.Name,
                    CarDetailsId = car.CarDetails.Id,
                    ClassName = car.CarDetails.ModelClass.ClassName,
                    Img = car.CarDetails.CarPhotos.Select(s => s.PhotoName).FirstOrDefault(),
                    modelclassId = car.CarDetails.ModelClass.Id,
                    modelId = car.CarDetails.ModelClass.Model.Id,
                    price = car.CarDetails.Price.Value,
                    UserEmail = car.User.Email
                };
                result.SearchResults.Add(output);
            }
            result.IsSuccess = true;
            return result;
        }

        public ImagesViewModel GetImages(int carDetailsId)
        {
            ImagesViewModel images = new ImagesViewModel();

            var photos = _db.CarPhoto
                .Include(i => i.CarDetails.ModelClass.Model.Brand)
                .Include(i => i.CarDetails.ModelClass.Model)
                .Where(w => w.CarDetailsId == carDetailsId).ToList();

            if (photos == null)
                return new ImagesViewModel
                {
                    IsSuccess = false
                };

            foreach (var photo in photos)
            {
                CarPhotosViewModel img = new CarPhotosViewModel
                {
                    PreviewImageSrc = photo.PhotoName,
                    ThumbnailImageSrc = photo.PhotoName,
                    Alt = photo.CarDetails.ModelClass.Model.Brand.Name + " " + photo.CarDetails.ModelClass.Model.Name
                };

                images.Images.Add(img);
            }
            images.IsSuccess = true;
            return images;
        }

        public CarDetailsViewModel ViewCarDetails(int carDetailsId)
        {
            CarDetailsViewModel model = new CarDetailsViewModel();

            var details = _db.CarDetails
                .Include(i => i.ModelClass)
                .Include(i => i.ModelClass.Model)
                .Include(i => i.ModelClass.Model.Brand)
                .Include(i => i.ModelClass.Model.Type)
                .FirstOrDefault(f => f.Id == carDetailsId);



            var dimensions = _db.Dimension.FirstOrDefault(f => f.CarDetailsId == carDetailsId);
            var exterior = _db.Exterior.FirstOrDefault(f => f.CarDetailsId == carDetailsId);
            var interior = _db.Interior.FirstOrDefault(f => f.CarDetailsId == carDetailsId);
            var safety = _db.Safety.FirstOrDefault(f => f.CarDetailsId == carDetailsId);
            var multimedia = _db.Multimedia.FirstOrDefault(f => f.CarDetailsId == carDetailsId);
            var performance = _db.Performance.FirstOrDefault(f => f.CarDetailsId == carDetailsId);

            if (details == null)
                return new CarDetailsViewModel
                {
                    IsSuccess = false
                };

            model.Price = details.Price;
            model.CarName = details.ModelClass.Model.Brand.Name + " " + details.ModelClass.Model.Name + " " + details.ModelClass.Model.Year;
            model.ClassName = details.ModelClass.ClassName;
            model.CarType = details.ModelClass.Model.Type.Name;
            model.IsSuccess = true;

            if (dimensions != null)
            {
                model.LuggageBoxCapacity = dimensions.LuggageBoxCapacity;
                model.Clearance = dimensions.Clearance;
                model.Width = dimensions.Width;
                model.Height = dimensions.Height;
                model.Wheelbase = dimensions.Wheelbase;
                model.Length = dimensions.Length;
            }

            if (exterior != null)
            {
                model.TrimSize = exterior.TrimSize;
                model.RainSensor = exterior.RainSensor;
                model.AutoFoldingMirror = exterior.AutoFoldingMirror;
                model.PanoramaRoof = exterior.PanoramaRoof;
                model.LedDaytimeRunningLamps = exterior.LedDaytimeRunningLamps;
                model.Headlamps = exterior.Headlamps;
                model.FogLambs = exterior.FogLambs;
                model.AutoLighting = exterior.AutoLighting;
                model.RearLambs = exterior.RearLambs;
                model.SunRoof = exterior.SunRoof;
                model.WheelsWithTireSize = exterior.WheelsWithTireSize;
                model.LightSensors = exterior.LightSensors;
                model.PowerMirrors = exterior.PowerMirrors;
                model.PrivacyGlass = exterior.PrivacyGlass;
            }

            if (interior != null)
            {
                model.CenterLock = interior.CenterLock;
                model.RearParkingSensors = interior.RearParkingSensors;
                model.AmbientLighting = interior.AmbientLighting;
                model.ElectronicWindow = interior.ElectronicWindow;
                model.DriveModeSelect = interior.DriveModeSelect;
                model.LeatherTransmission = interior.LeatherTransmission;
                model.VariableHeatedDriverPassengerSeat = interior.VariableHeatedDriverPassengerSeat;
                model.MovableSteeringWheel = interior.MovableSteeringWheel;
                model.RearViewCamera = interior.RearViewCamera;
                model.KeylessStartStop = interior.KeylessStartStop;
                model.BackHolder = interior.BackHolder;
                model.AutoDimmingMirror = interior.AutoDimmingMirror;
                model.AutoFoldingBackSeats = interior.AutoFoldingBackSeats;
                model.Ac = interior.Ac;
                model.Dashboard = interior.Dashboard;
                model.FrontCamera = interior.FrontCamera;
                model.PassengerSeat = interior.PassengerSeat;
                model.LeatherSteeringWheel = interior.LeatherSteeringWheel;
                model.PaddleShifters = interior.PaddleShifters;
                model.NumberOfSeats = interior.NumberOfSeats;
                model.PowerTailgate = interior.PowerTailgate;
                model.AcBackSeats = interior.AcBackSeats;
                model.DriverSeat = interior.DriverSeat;
                model.FrontParkingSensors = interior.FrontParkingSensors;
                model.KeylessEntry = interior.KeylessEntry;
                model.LeatherSeats = interior.LeatherSeats;
                model.MultiFunction = interior.MultiFunction;
            }

            if (multimedia != null)
            {
                model.WirlessCharger = multimedia.WirlessCharger;
                model.Bluetooth = multimedia.Bluetooth;
                model.SmartphoneLinkSystem = multimedia.SmartphoneLinkSystem;
                model.Touchscreen = multimedia.Touchscreen;
                model.Usb = multimedia.Usb;
                model.MultifunctionSteeringWheel = multimedia.MultifunctionSteeringWheel;
                model.CdPlayer = multimedia.CdPlayer;
                model.Speakers = multimedia.Speakers;
                model.NavigationSystem = multimedia.NavigationSystem;
                model.Aux = multimedia.Aux;
                model.HeadUpDisplay = multimedia.HeadUpDisplay;
            }

            if (performance != null)
            {
                model.Cylinders = performance.Cylinders;
                model.FuelTankCapacity = performance.FuelTankCapacity;
                model.FuelType = performance.FuelType;
                model.MaxToruqe = performance.MaxToruqe;
                model.FuelConsumption = performance.FuelConsumption;
                model.GearShifts = performance.GearShifts;
                model.Acceleration = performance.Acceleration;
                model.MaxPower = performance.MaxPower;
                model.MaxSpeed = performance.MaxSpeed;
            }

            if (safety != null)
            {
                model.TractionControl = safety.TractionControl;
                model.ElectroncStabilityControl = safety.ElectroncStabilityControl;
                model.AntiLockBrakingSystem = safety.AntiLockBrakingSystem;
                model.TirePressureMonitoring = safety.TirePressureMonitoring;
                model.SpeedLimiter = safety.SpeedLimiter;
                model.AutoStartStopFunctions = safety.AutoStartStopFunctions;
                model.PowerAssistedSteering = safety.PowerAssistedSteering;
                model.Immobilizer = safety.Immobilizer;
                model.ElectronicBrakeForceDistribution = safety.ElectronicBrakeForceDistribution;
                model.ChildSeats = safety.ChildSeats;
                model.HillAssist = safety.HillAssist;
                model.Airbags = safety.Airbags;
                model.ActiveParkAssist = safety.ActiveParkAssist;
                model.ElectricHandbrake = safety.ElectricHandbrake;
                model.CruiseControl = safety.CruiseControl;
            }

            return model;
        }

        public CarDetailsViewModel ViewCarRentDetails(int carDetailsId)
        {
            var details = _db.CarDetails
                .Include(i => i.ModelClass)
                .Include(i => i.ModelClass.Model)
                .Include(i => i.ModelClass.Model.Brand)
                .Include(i => i.ModelClass.Model.Type)
                .Where(i => i.IsFromSystem == false)
                .FirstOrDefault(f => f.Id == carDetailsId);



            var dimensions = _db.Dimension.FirstOrDefault(f => f.CarDetailsId == carDetailsId);
            var exterior = _db.Exterior.FirstOrDefault(f => f.CarDetailsId == carDetailsId);
            var interior = _db.Interior.FirstOrDefault(f => f.CarDetailsId == carDetailsId);
            var safety = _db.Safety.FirstOrDefault(f => f.CarDetailsId == carDetailsId);
            var multimedia = _db.Multimedia.FirstOrDefault(f => f.CarDetailsId == carDetailsId);
            var performance = _db.Performance.FirstOrDefault(f => f.CarDetailsId == carDetailsId);

            if (details == null)
                return new CarDetailsViewModel
                {
                    IsSuccess = false
                };

            CarDetailsViewModel model = new CarDetailsViewModel
            {
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


        private List<UserCar> GetAllCars()
        {
            return _db.UserCar
                .Include(i => i.User)
                .Include(i => i.CarDetails)
                .Include(i => i.CarDetails.Dimensions)
                .Include(i => i.CarDetails.CarPhotos)
                .Include(i => i.SellingData)
                .Include(i => i.CarDetails.ModelClass)
                .Include(i => i.CarDetails.ModelClass.Model)
                .Include(i => i.CarDetails.ModelClass.Model.Brand)
                .Where(w => w.IsDeleted == false)
                .ToList();
        }

    }
}
