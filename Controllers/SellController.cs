using CarsApi.Services.Interface;
using CarsApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class SellController : ControllerBase
    {
        private ISell _db;
        public SellController(ISell db)
        {
            _db = db;
        }

        [HttpPost("AddSelling/{carDetailsId}")]
        public ActionResult AddSellingData([FromRoute]int carDetailsId,[FromBody] SellingDataViewModel model)
        {
            var result = _db.EnterSellingData(model, carDetailsId);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("GetSelling/{carDetailsId}")]
        public ActionResult GetSellingData([FromRoute] int carDetailsId)
        {
            var result = _db.GetSellingData(carDetailsId);
            if (result != null)
                return Ok(result);

            return BadRequest();
        }
    }
}
