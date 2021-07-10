using CarsApi.Models;
using CarsApi.Services.Interface;
using CarsApi.ViewModels;
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
    public class BrandController : ControllerBase
    {
        private IBrand _db;
        public BrandController(IBrand db)
        {
            _db = db;
        }


        [HttpPost("Create")]
        public ActionResult AddBrand(Brand brand)
        {
            if (ModelState.IsValid)
            {
                var result = _db.AddBrand(brand);

                if (result.IsSuccess)
                    return Ok(result);


                return BadRequest(result);
            }

            return BadRequest("Check Your Data");
        }

        [HttpPut("{id}")]
        public ActionResult EditBrand(int id, UpdateBrandViewModel brand)
        {
            if (ModelState.IsValid)
            {
                var result = _db.EditBrand(id, brand);

                if (result.IsSuccess)
                    return Ok(result);


                return BadRequest(result);
            }
            return BadRequest("Data is not correct");
        }

        [HttpGet("All")]
        public ActionResult ViewAllBrand()
        {
            var brands = _db.GetAllBrands();
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public ActionResult ViewOneBrandDetails(int id)
        {
            var brand = _db.GetBrandByID(id);
            if (brand == null)
                return NotFound();


            return Ok(brand);
        }
    }
}
