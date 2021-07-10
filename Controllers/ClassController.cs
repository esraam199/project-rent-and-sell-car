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
    public class ClassController : ControllerBase
    {
        private IClass _db;

        public ClassController(IClass db)
        {
            _db = db;
        }

        [HttpGet("ClassesInModel/{modelId}")]
        public ActionResult GetClassesInModel(int modelId)
        {
            var result = _db.GetClassesInModel(modelId);
            if (!result.IsSuccess)
                return BadRequest();

            return Ok(result.ClassesList);
        }
    }
}
