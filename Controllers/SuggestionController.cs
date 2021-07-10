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
    public class SuggestionController : ControllerBase
    {
        private ISuggest _db;

        public SuggestionController(ISuggest db)
        {
            _db = db;
        }

        [HttpPost("Suggest")]
        public ActionResult SuggestCars( [FromBody] SuggestionViewModel form)
        {
            var result = _db.SuggestCar(form);
            if (result.IsSuccess)
                return Ok(result.SearchResults);

            return NotFound();
        }
    }
}
