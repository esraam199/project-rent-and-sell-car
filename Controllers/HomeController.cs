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
    public class HomeController : ControllerBase
    {
        private IHome _db;

        public HomeController(IHome db)
        {
            _db = db;
        }

        [HttpGet("HomeCars")]
        public ActionResult GetHomeCars()
        {
            var result = _db.GetCarsDate();

            if (result.IsSuccess)
                return Ok(result.homeCars);

            return BadRequest(result);

        }
    }
}
