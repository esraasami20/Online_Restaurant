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
    public class RestaurantService
    {
        private RestaurantContext _db;

        public RestaurantService(RestaurantContext db)
        {
            _db = db;
        }

        //search reatarant
        public List<Restaurant> SearchRestaurant(string name)
        {
            return _db.Restaurants.Include(i => i.Cities).Where(p => p.Restaurant_Name.Contains(name) && p.Isdeleted == false).ToList();
        }


        // get all Restaurant
        public List<Restaurant> GetAllRestaurants()
        {
            var restaurant = _db.Restaurants.Where(c=> c.Isdeleted == false).Include(a=>a.Menus).ThenInclude(a=>a.Restaurant).ThenInclude(a=>a.Cities).ToList();
            return restaurant;
        }

        //get restaurant by id
        public Restaurant GetRestaurantById(int id)
        {

            var res = _db.Restaurants.Where(c => c.Restaurant_Id == id && c.Isdeleted == false).Include(a=>a.Menus).FirstOrDefault();
            return res;
        }
        // get all Restaurant for City
        public List<Restaurant> GetAllRestaurantsForCity(City city)
        {
            var cityId = _db.Cities.Where(c => c.City_Id == city.City_Id && c.Isdeleted == false).FirstOrDefault().City_Id;
            var restaurant = _db.Restaurants.Where(c=> c.City_Id == cityId && c.Isdeleted == false).ToList();
            return restaurant;
        }
        //get Restaurants by id for City
        public Restaurant GetRestaurantByIdForCity(int id, City city)
        {

            var cityId = _db.Cities.Where(c=>c.City_Id==city.City_Id && c.Isdeleted == false).FirstOrDefault().City_Id;
            Restaurant restaurant = _db.Restaurants.Include("City").SingleOrDefault(c => c.Restaurant_Id == id && c.City_Id == cityId && c.Isdeleted == false);
            return restaurant;
        }

        //add new restaurant
        public async Task<Restaurant> addRestaurantAsync(Restaurant restaurant, IFormFile file)
        {
            _db.Restaurants.Add(restaurant);
            _db.SaveChanges();

            if (file != null)
            {
                string path = await FileHelper.SaveImageAsync(restaurant.Restaurant_Id, file, "Restaurant");
                restaurant.RestaurantImg = path;
            }
            _db.SaveChanges();

            return restaurant;
        }

        //add restaurant to city
        public City AddRestaurantToCity(int restaurantId, int CityId)
        {
            var restaurant = _db.Restaurants.FirstOrDefault(p => p.Restaurant_Id == restaurantId && p.City_Id == CityId && p.Isdeleted == false);

            if (restaurant != null)
            {
                City city = _db.Cities.FirstOrDefault(p => p.City_Id == CityId);
                restaurant.Restaurant_Id = restaurantId;
                _db.SaveChanges();
                return city;
            }
            return null;
        }



        //edit restaurant
        public async Task<bool> EditRestaurantAsync(Restaurant restaurant, IFormFile file)
        {
            Restaurant restaurant1 = _db.Restaurants.FirstOrDefault(p => p.Restaurant_Id == restaurant.Restaurant_Id && p.Isdeleted == false);

            if (restaurant1 != null)
            {
                restaurant1.Description = restaurant.Description;
                restaurant1.Restaurant_Name = restaurant.Restaurant_Name;


                if (file != null)
                {
                    // delete old image
                    File.Delete(restaurant1.RestaurantImg);
                    // add new image
                    restaurant1.RestaurantImg = await FileHelper.SaveImageAsync(restaurant.Restaurant_Id, file, "Restaurant");

                }
                _db.SaveChanges();
                return true;
            }

            return false;
        }

        //delete restaurants
        public bool DeleteRestaurant(int id)
        {


            Restaurant restaurant = _db.Restaurants.FirstOrDefault(p => p.Restaurant_Id == id && p.Isdeleted == false);

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
