using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Restaurant.Models;
using Online_Restaurant.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private CityService _cityService;

        public CityController(CityService cityService)
        {
            _cityService = cityService;
        }

        // get all cities
        [HttpGet]
        public ActionResult<List<City>> GetAll()
        {
            return _cityService.GetAllCities();
        }
        //get City by id 
        [HttpGet("{id}")]
        public ActionResult<City> GetCityById(int id)
        {
            var result = _cityService.GetCityById(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        //add new City
        [HttpPost]
        public async Task<ActionResult<City>> AddCityAsync([FromForm] City city)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                City result = await _cityService.addCitytAsync(city);

                return Ok(result);
            }

        }
        //edit city

        [HttpPut("{id}")]
        public async Task<ActionResult> EditCityAsync(int id, [FromForm] City city)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                city.City_Id = id;
                var result = await _cityService.EditCityAsync(city);
                if (result)
                    return NoContent();
                return NotFound();
            }

        }
        //delete city
        [HttpDelete("{id}")]
        public ActionResult deleteCity(int id)
        {

            var result = _cityService.DeleteCity(id);
            if (result)
                return NoContent();
            return NotFound();
        }
    }
}
