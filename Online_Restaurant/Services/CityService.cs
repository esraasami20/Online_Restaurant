using Microsoft.EntityFrameworkCore;
using Online_Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Restaurant.Services
{
    public class CityService
    {
        private RestaurantContext _db;

        public CityService(RestaurantContext db)
        {
            _db = db;
        }


        // get all cities
        public List<City> GetAllCities()
        {
            var city = _db.Cities.Where(c => c.Isdeleted == false).Include(a => a.Restaurants).ToList();
            return city;
        }

        //get City by id for Restaurants 
        public City GetCityById(int id)
        {

            var city = _db.Cities.Where(c => c.City_Id == id&& c.Isdeleted == false).Include(a=>a.Restaurants).FirstOrDefault();
            return city;
        }

        //add new City
        public async Task<City> addCitytAsync(City city)
        {
             await _db.Cities.AddAsync(city);
            _db.SaveChanges();
            return city;
        }

        //edit city
        public async Task<bool> EditCityAsync(City city)
        {
            City city1 = _db.Cities.FirstOrDefault(p => p.City_Id == city.City_Id && p.Isdeleted == false);

            if (city1 != null)
            {
                city1.City_Name = city.City_Name;
                _db.SaveChanges();
                return true;
            }

            return false;
        }


        //delete city
        public bool DeleteCity(int id)
        {


            City city = _db.Cities.FirstOrDefault(p => p.City_Id == id && p.Isdeleted == false);

            if (city != null)
            {
                city.Isdeleted = true;
                _db.SaveChanges();
                return true;
            }

            return false;

        }

    }
}
