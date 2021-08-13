using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Online_Restaurant.Helper;
using Online_Restaurant.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Restaurant.Services
{
    public class MenueService
    {
        private RestaurantContext _db;

        public MenueService(RestaurantContext db)
        {
            _db = db;
        }
        //search reatarant
        public List<Menu> SearchMenu(string name)
        {
            return _db.Menus.Include(i => i.Restaurant).Where(p => p.Menu_Title.Contains(name) && p.Isdeleted == false).ToList();
        }
        // get all Menu
        public List<Menu> GetAllMenus()
        {
            var menu = _db.Menus.Where(c => c.Isdeleted == false).Include(i => i.OrderItems.Where(r => r.Isdeleted == false)).ThenInclude(r => r.Menu).ToList();
            return menu;
        }

        // get all Menu for Restaurant
        public List<Menu> GetAllMenusForRestaurants(Restaurant restaurant)
        {
            var restId = _db.Restaurants.Where(c => c.Restaurant_Id == restaurant.Restaurant_Id && c.Isdeleted == false).FirstOrDefault().Restaurant_Id;
            var menu = _db.Menus.Where(c => c.Restaurant_Id == restId && c.Isdeleted == false).ToList();
            return menu;
        }

        //get Menu by id 
        public Menu GetMenuById(int id)
        {

            var menu = _db.Menus.Include("Restaurant").Where(c => c.Menu_Id == id && c.Isdeleted == false).FirstOrDefault();
            return menu;
        }
        //get Menu by id for resturant
        public Menu GetMenutByIdForRestaurant(int id, Restaurant restaurant)
        {

            var restId = _db.Restaurants.Where(c => c.Restaurant_Id == restaurant.Restaurant_Id && c.Isdeleted == false).FirstOrDefault().Restaurant_Id;
            Menu menu = _db.Menus.Include(i => i.OrderItems.Where(r => r.Isdeleted == false)).ThenInclude(r => r.Menu).SingleOrDefault(c => c.Menu_Id == id && c.Restaurant_Id == restId && c.Isdeleted == false);
            return menu;
        }

        //add new Menu 
        public async Task<Menu> addMenuAsync(Menu menu, IFormFile file)
        {
            _db.Menus.Add(menu);
            _db.SaveChanges();

            if (file != null)
            {
                string path = await FileHelper.SaveImageAsync(menu.Menu_Id, file, "Menu");
                menu.Img = path;
            }
            _db.SaveChanges();

            return menu;
        }

        //add Menu to restaurant
        public Restaurant AddMenuToRestaurant( int restid, int menuid)
        {
            var menu = _db.Menus.FirstOrDefault(p => p.Menu_Id == menuid &&  p.Isdeleted == false);

            if (menu != null)
            {
                Restaurant rest = _db.Restaurants.FirstOrDefault(p => p.Restaurant_Id == restid);
                rest.Restaurant_Id = restid;
                _db.SaveChanges();
                return rest;
            }
            return null;
        }
        
        //edit Menu
        public async Task<bool> EditMenutAsync(Menu menu, IFormFile file)
        {
            Menu menu1 = _db.Menus.FirstOrDefault(p => p.Menu_Id == menu.Menu_Id && p.Isdeleted == false);

            if (menu1 != null)
            {
                menu1.Description = menu.Description;
                menu1.Menu_Title = menu.Menu_Title;
                menu1.Price = menu.Price;


                if (file != null)
                {
                    // delete old image
                    File.Delete(menu1.Img);
                    // add new image
                    menu1.Img = await FileHelper.SaveImageAsync(menu.Menu_Id, file, "Menu");

                }
                _db.SaveChanges();
                return true;
            }

            return false;
        }

        //delete restaurants
        public bool DeleteMenu(int id)
        {


            Menu restaurant = _db.Menus.FirstOrDefault(p => p.Menu_Id == id && p.Isdeleted == false);

            if (restaurant != null)
            {
                restaurant.Isdeleted = true;
                _db.SaveChanges();
                return true;
            }

            return false;

        }
    }
}
