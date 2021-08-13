using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Restaurant.Models;
using Online_Restaurant.Services;
using Online_Restaurant.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private OrderService _orderService;


        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }
        // get all  Order
        [HttpGet]
        public ActionResult<List<Order>> GetAllRestaurant()
        {
            return _orderService.GetAllOrders();
        }
        //get Order by id
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrderById(int id)
        {
            var result = _orderService.GetOrderById(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }


        //get all Orders for Customer
        [HttpGet]
        [Route("customer-order")]
        public ActionResult<Order> GetAllOrdersForCustomer(Customer res)
        {
            var result = _orderService.GetAllOrdersForCustomer(res);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        // add Order
        [HttpPost]
        public async Task<ActionResult> AddProductAsync([FromBody] Checkout order)
        {
            if (ModelState.IsValid)
            {
                var result = await _orderService.addOrdertAsync(order);
                if (result.Status == "Success")

                    return Ok(result);
                return BadRequest(result.Message);
            }
            else
            {
                return BadRequest();
            }

        }

        // add Order to Customer 
        [HttpPost("Order-customer/{Order_Id}")]
        public ActionResult AddRestaurantToCity(int Order_Id, [FromBody] int Customer_Id)
        {
            var result = _orderService.AddOrderToCustomer(Order_Id, Customer_Id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }



        //edit Order
        [HttpPut("{id}")]
        public ActionResult EditOrder(int id, [FromBody] Order order)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                order.Order_Id = id;
                var result = _orderService.EditOrderAsync(order);
                if (result.Status == "Error" || result.Status == "Error" && result.data == null)
                    return NotFound();
                return NoContent();

            }

        }



        // delete Order
        [HttpDelete("{id}")]
        public ActionResult deleteOrder(int id)
        {

            var result = _orderService.DeleteOrder(id);
            if (result)
                return NoContent();
            return NotFound();
        }

    }
}
