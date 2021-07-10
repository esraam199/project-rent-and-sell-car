using CarsApi.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarsApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CarsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentController : ControllerBase
    {
        private IRent _db;
        public RentController(IRent db)
        {
            _db = db;
        }
        [HttpGet("RentInfo/{carDetailsId}")]
        [Authorize]
        public ActionResult RentResult(int carDetailsId)
        {
            var Result = _db.EditRentDetails(carDetailsId);
            if (Result != null)
            {
                return Ok(Result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("SaveRent")]
        [Authorize]
        public ActionResult SaveRentResult([FromBody] CarDetailsViewModel model, [FromQuery] int cardetailsid)
        {
            var Result = _db.SaveRentDetails(model, cardetailsid);
            if (Result.IsSuccess)
            {
                return Ok(Result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
