using Microsoft.EntityFrameworkCore;
using Online_Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Restaurant.Services
{
    public class CustomerService
    {
        private RestaurantContext _db;

        public CustomerService(RestaurantContext db)
        {
            _db = db;
        }


        // get all Customers
        public List<Customer> GetAllCustomers()
        {
            var customer = _db.Customers.Where(c => c.Isdeleted == false).Include(a=>a.Orders).ToList();
            return customer;
        }

        //get customer by id 
        public Customer GetCustomerById(int id)
        {

            var customer = _db.Customers.Where(c => c.customer_Id == id && c.Isdeleted == false).Include(a=>a.Orders).FirstOrDefault();
            return customer;
        }

        //add new Customer
        public async Task<Customer> addCustomertAsync(Customer customer)
        {
            await _db.Customers.AddAsync(customer);
            _db.SaveChanges();
            return customer;
        }

        //edit Customer
        public async Task<bool> EditCustomerAsync(Customer customer)
        {
            Customer customer1 = _db.Customers.FirstOrDefault(p => p.customer_Id == customer.customer_Id && p.Isdeleted == false);

            if (customer1 != null)
            {
                customer1.customer_Name = customer.customer_Name;
                customer1.customer_Phone = customer.customer_Phone;
                customer1.customer_Address = customer.customer_Address;
                customer1.customer_Email = customer.customer_Email;
                _db.SaveChanges();
                return true;
            }

            return false;
        }


        //delete customer
        public bool DeleteCustomer(int id)
        {


            Customer customer = _db.Customers.FirstOrDefault(p => p.customer_Id == id && p.Isdeleted == false);

            if (customer != null)
            {
                customer.Isdeleted = true;
                _db.SaveChanges();
                return true;
            }

            return false;

        }

    }
}
