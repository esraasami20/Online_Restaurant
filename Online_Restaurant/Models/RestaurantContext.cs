using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Restaurant.Models
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<MenuOrder> OrderItems { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().ToTable("City");
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Menu>().ToTable("Menu");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<MenuOrder>().ToTable("OrderItem");
            modelBuilder.Entity<Restaurant>().ToTable("Restaurant");

            modelBuilder.Entity<MenuOrder>()
              .HasKey(cs => new { cs.Order_Id, cs.Menu_Id });


        }
    }
}
