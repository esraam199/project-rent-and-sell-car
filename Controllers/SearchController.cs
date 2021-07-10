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
    public class SearchController : ControllerBase
    {
        private ISearch _db;
        public SearchController(ISearch db)
        {
            _db = db;
        }
        [HttpGet("SearchByBrand")]
        public ActionResult SearchResult(int Brand)
        {
            var Result = _db.SearchViewModel(Brand);
            if (Result != null)
            {
                return Ok(Result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("SearchByAll")]
        public ActionResult SearchResult(decimal minprice,decimal maxprice, int brand, int model, int body, int year)
        {
            var Result = _db.GetAll(minprice, maxprice,  brand,  model,  body,  year);
            if (Result != null)
            {
                return Ok(Result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("UsedCars")]
        public ActionResult SearchRent(decimal minprice, decimal maxprice, int brand, int model, int body, int year)
        {
            var Result = _db.GetRent(minprice, maxprice, brand, model, body, year);
            if (Result != null)
            {
                return Ok(Result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("AllRent")]
        public ActionResult SearchResult()
        {
            var Result = _db.SearchViewModel();
            if (Result != null)
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

