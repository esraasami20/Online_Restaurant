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
    public class RestaurantController : ControllerBase
    {
        private RestaurantService _restaurantService;


        public RestaurantController(RestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }
        // get all  Restaurant
        [HttpGet]
        public ActionResult<List<Restaurant>> GetAllRestaurant()
        {
            return _restaurantService.GetAllRestaurants();
        }

        // search [FromQuery]

        [HttpGet("search/{name}")]
        public ActionResult Search( string name)
        {

            var result = _restaurantService.SearchRestaurant(name);
            return Ok(result);
        }
        //get Restaurant by id
        [HttpGet("{id}")]
        public ActionResult<Restaurant> GetRestaurantById(int id)
        {
            var result = _restaurantService.GetRestaurantById(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }


        // get all Restaurant for City
        [HttpGet]
        [Route("restaurant-city")]
        public ActionResult<Restaurant> GetAllMenusForRestaurants(City res)
        {
            var result = _restaurantService.GetAllRestaurantsForCity(res);
            if (result == null)
                return NotFound();
            return Ok(result);
        }




        // add Restaurant
        [HttpPost]
        public async Task<ActionResult<Restaurant>> AddRestaurantAsync([FromForm] Restaurant res, IFormFile file)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                Restaurant result = await _restaurantService.addRestaurantAsync(res, file);

                return Ok(result);
            }

        }


        // add Restaurant to City 
        [HttpPost("{Restaurant_Id}")]
        public ActionResult AddRestaurantToCity(int Restaurant_Id, [FromBody] int City_Id)
        {
            var result = _restaurantService.AddRestaurantToCity(Restaurant_Id, City_Id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }



        //edit Restaurant
        [HttpPut("{id}")]
        public async Task<ActionResult> EditRestaurantAsync(int id, [FromForm] Restaurant restaurant, IFormFile file)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                restaurant.Restaurant_Id = id;
                var result = await _restaurantService.EditRestaurantAsync(restaurant, file);
                if (result)
                    return NoContent();
                return NotFound();
            }

        }



        // delete Restaurant
        [HttpDelete("{id}")]
        public ActionResult deleteRestaurant(int id)
        {

            var result = _restaurantService.DeleteRestaurant(id);
            if (result)
                return NoContent();
            return NotFound();
        }

    }
}
