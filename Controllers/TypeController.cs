using CarsApi.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Type = CarsApi.Models.Type;

namespace CarsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private IType _db;

        public TypeController(IType db)
        {
            _db = db;
        }

        [HttpPost("Create")]
        public ActionResult AddType(Type type)
        {
            if (ModelState.IsValid)
            {
                var result = _db.AddType(type);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest("Check Your Data!");
        }

        [HttpPut("{id}")]
        public ActionResult EditType(int id, Type type)
        {
            if (ModelState.IsValid)
            {
                var result = _db.EditType(id, type);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest("Data is not correct");
        }

        [HttpGet("All")]
        public ActionResult ViewAllTypes()
        {
            var types = _db.GetAllTypes();
            return Ok(types);
        }

        [HttpGet("{id}")]
        public ActionResult ViewOneType(int id)
        {
            var type = _db.GetTypeById(id);
            if (type == null)
                return NotFound();

            return Ok(type);
        }

        [HttpGet("ByBrand")]
        public ActionResult GetTypesInBrand([FromQuery] int brandId, int year, int modelId)
        {
            List<Type> typesss = _db.GetTypesInBrand(brandId, year, modelId);

            if (typesss.Count == 0)
                return NotFound();

            return Ok(typesss);
        }

        [HttpGet("ForPreSearch")]
        public ActionResult GetTypesForPreSearch()
        {
            var types = _db.GetTypesForPreSearch();
            if (types.Count == 0)
                return NotFound();

            return Ok(types);
        }
    }
}
