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
    public class GeneralInfoController : ControllerBase
    {
        private IGeneral _db;

        public GeneralInfoController(IGeneral db)
        {
            _db = db;
        }

        [HttpGet("GeneralInfo")]
        public ActionResult GetGeneralInfo()
        {
            var info = _db.GetGeneralInfo();
            if (info == null)
                return BadRequest();

            return Ok(info);
        }
    }
}
