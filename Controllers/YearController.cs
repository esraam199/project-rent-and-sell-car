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
    public class YearController : ControllerBase
    {
        private IYear _db;
        public YearController(IYear db)
        {
            _db = db;
        }

        [HttpGet("{brandId}")]
        public ActionResult GetYears(int brandId)
        {
            var result = _db.GetYearsOfModelsInBrand(brandId);
            if (result.Count == 0)
                return BadRequest();

            return Ok(result);
        }

        [HttpGet("All")]
        public ActionResult GetAllYears()
        {
            var result = _db.GetAllYears();
            if (result.Count == 0)
                return BadRequest();

            return Ok(result);
        }
    }
}
