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
    public class CustomerController : ControllerBase
    {
        private CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        // get all Customers
        [HttpGet]
        public ActionResult<List<Customer>> GetAll()
        {
            return _customerService.GetAllCustomers();
        }
        //get Customer by id 
        [HttpGet("{id}")]
        public ActionResult<City> GetCustomerById(int id)
        {
            var result = _customerService.GetCustomerById(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        //add new Customer
        [HttpPost]
        public async Task<ActionResult<Customer>> AddCustomerAsync([FromForm] Customer customer)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                Customer result = await _customerService.addCustomertAsync(customer);

                return Ok(result);
            }

        }
        //edit Customer

        [HttpPut("{id}")]
        public async Task<ActionResult> EditCustomerAsync(int id, [FromForm] Customer customer)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                customer.customer_Id = id;
                var result = await _customerService.EditCustomerAsync(customer);
                if (result)
                    return NoContent();
                return NotFound();
            }

        }
        //delete Customer
        [HttpDelete("{id}")]
        public ActionResult deleteCustomer(int id)
        {

            var result = _customerService.DeleteCustomer(id);
            if (result)
                return NoContent();
            return NotFound();
        }
    }
}
