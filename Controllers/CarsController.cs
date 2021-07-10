using CarsApi.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private ICarDetails _db;

        public CarsController(ICarDetails db)
        {
            _db = db;
        }

        [HttpGet("{carDetailsId}")]
        public ActionResult GetCarDetails(int carDetailsId)
        {
            var result = _db.ViewCarDetails(carDetailsId);

            if (result.IsSuccess == false)
                return NotFound();

            return Ok(result);
        }


        [HttpGet("RentCarDetailsId")]
        public ActionResult GetRentCarDetails(int carDetailsId)
        {
            var result = _db.ViewCarRentDetails(carDetailsId);

            if (result.IsSuccess == false)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("photos/{carDetailsId}")]
        public ActionResult GetCarImages(int carDetailsId)
        {
            var result = _db.GetImages(carDetailsId);

            if (!result.IsSuccess)
                return BadRequest();

            return Ok(result.Images);
        }

        [HttpGet("GetCars")]
        public ActionResult GetAllCarsClassified ([FromQuery]int rent)
        {
            var result = _db.GetAllCarsClassified(rent);

            if (result.IsSuccess)
                return Ok(result.SearchResults);

            return NotFound();
        }


    }
}
