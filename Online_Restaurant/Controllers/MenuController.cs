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
    public class MenuController : ControllerBase
    {
        private MenueService _menueService;

        public MenuController(MenueService menueService)
        {
            _menueService = menueService;
        }

        // search 

        [HttpGet("search")]
        public ActionResult Search([FromQuery] string name)
        {

            var result = _menueService.SearchMenu(name);
            return Ok(result);
        }
        // get all  Menus
        [HttpGet]
        public ActionResult<List<Menu>> GetAllMenu()
        {
            return _menueService.GetAllMenus();
        }
        //get menu by id
        [HttpGet("{id}")]
        public ActionResult<Menu> GetMenueById(int id)
        {
            var result = _menueService.GetMenuById(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }


        //get Menus for rest
        [HttpGet]
        [Route("restaurant-menu")]
        public ActionResult<Menu> GetAllMenusForRestaurants(Restaurant res)
        {
            var result =_menueService.GetAllMenusForRestaurants(res);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        
        // add Menu
        [HttpPost]
        public async Task<ActionResult<Menu>> AddMenuAsync([FromForm] Menu menu, IFormFile file)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                Menu result = await _menueService.addMenuAsync(menu, file);

                return Ok(result);
            }

        }


        // add Menu to Restaurant 
        [HttpPost("{Menu_Id}")]
        public ActionResult AddMenuToRestaurant(int Menu_Id, [FromBody] int Restaurant_Id)
        {
            var result = _menueService.AddMenuToRestaurant(Menu_Id, Restaurant_Id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }



        //edit Menu
        [HttpPut("{id}")]
        public async Task<ActionResult> EditMenuAsync(int id, [FromForm] Menu menu, IFormFile file)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                menu.Menu_Id = id;
                var result = await _menueService.EditMenutAsync(menu, file);
                if (result)
                    return NoContent();
                return NotFound();
            }

        }



        // delete Menu
        [HttpDelete("{id}")]
        public ActionResult deleteMenu(int id)
        {

            var result = _menueService.DeleteMenu(id);
            if (result)
                return NoContent();
            return NotFound();
        }

    }
}
