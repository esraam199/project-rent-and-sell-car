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
    public class ModelClassController : ControllerBase
    {
        private IModelClass _db;

        public ModelClassController(IModelClass db)
        {
            _db = db;
        }

        [HttpGet("GetInModel")]
        public ActionResult GetModelClassByModel([FromQuery]int modelId, int classId)
        {
            var ClassModel = _db.GetModelClassByModelAndClass(modelId, classId);

            if (ClassModel == null)
                return NotFound();

            return Ok(ClassModel);
        }
    }
}
