using Microsoft.EntityFrameworkCore;
using Online_Restaurant.Helper;
using Online_Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Restaurant.Services
{
    public class OrderService
    {
        private RestaurantContext _db;

        public OrderService(RestaurantContext db)
        {
            _db = db;
        }

        // get all Orders
        public List<Order> GetAllOrders()
        {
            var order = _db.Orders.Where(c => c.Isdeleted == false).Include(i => i.OrderItems.Where(r => r.Isdeleted == false)).ThenInclude(r => r.Order).ThenInclude(a=>a.Customer).ToList();
            return order;
        }

        // get all Orders for Customer
        public List<Order> GetAllOrdersForCustomer(Customer customer)
        {
            var custId = _db.Customers.Where(c => c.customer_Id == customer.customer_Id && c.Isdeleted == false).FirstOrDefault().customer_Id;
            var order = _db.Orders.Where(c => c.customer_Id == custId && c.Isdeleted == false).ToList();
            return order;
        }
        //get order by id
        public Order GetOrderById(int id)
        {
            var res = _db.Orders.Where(c => c.Order_Id == id && c.Isdeleted == false).Include(i => i.OrderItems.Where(r => r.Isdeleted == false)).ThenInclude(r => r.Order).ThenInclude(a => a.Customer).FirstOrDefault();
            return res;
        }
        //get Order by id for Customer
        public Order GetOrderByIdForCustomer(int id, Customer customer)
        {

            var custId = _db.Customers.Where(c => c.customer_Id == customer.customer_Id && c.Isdeleted == false).FirstOrDefault().customer_Id;
            Order order = _db.Orders.Include(i => i.OrderItems.Where(r => r.Isdeleted == false)).ThenInclude(r => r.Order).ThenInclude(a=>a.Customer).SingleOrDefault(c => c.Order_Id == id && c.customer_Id == custId && c.Isdeleted == false);
            return order;
        }

        //add new Order 
        public async Task<Response> addOrdertAsync(Order order, int menu_id)
        {
            var result = _db.Menus.FirstOrDefault(i => i.Menu_Id == menu_id && i.Isdeleted == false);
            if (result != null)
            {
                try
                {
                    _db.Orders.Add(order);
                    _db.SaveChanges();
                }
                catch (Exception e)
                {

                }

                // add to menuorder

                _db.OrderItems.Add(new MenuOrder { Menu_Id = menu_id, Order_Id = order.Order_Id, Total = order.Total_Price,Quantity=order.Quantity });
                _db.SaveChanges();
                return new Response { Status = "Success", Message = "Ordered added successfully", data = order };

            }

            return new Response { Status = "Error", Message = "Inventory not found" };

        }

        //add Order to Customer
        public Customer AddOrderToCustomer(int custid, int orderid)
        {
            var order = _db.Orders.FirstOrDefault(p => p.Order_Id == orderid && p.Isdeleted == false);

            if (order != null)
            {
                Customer customer = _db.Customers.FirstOrDefault(p => p.customer_Id == custid);
                customer.customer_Id = custid;
                _db.SaveChanges();
                return customer;
            }
            return null;
        }

        //edit Order
        //public async Task<bool> EditOrderAsync(Order order)
        //{
        //    Order order1 = _db.Orders.FirstOrDefault(p => p.Order_Id == order.Order_Id && p.Isdeleted == false);

        //    if (order1 != null)
        //    {
        //        order1.Order_Date = order.Order_Date;
        //        order1.Total_Price = order.Total_Price;

        //        return true;
        //    }

        //    return false;
        //}
        public Response EditOrderAsync(Order order)
        {
            List<Menu> menus = _db.Menus.Include(ip => ip.OrderItems).Where(i =>  i.Isdeleted == false).ToList();
            if (menus != null)
            {
                Order OrderFound = null;
                foreach (var menu in menus)
                {
                    //var orderItem = order.OrderItems.Where(p => p.Order_Id == order.Order_Id && p.Isdeleted == false).FirstOrDefault();
                    OrderFound = _db.OrderItems.Include(p => p.Order).FirstOrDefault(p => p.Order_Id == order.Order_Id).Order;
                    if (OrderFound != null)
                    {
                        OrderFound.Total_Price = order.Total_Price;
                        OrderFound.Quantity = order.Quantity;
                       
                        _db.SaveChanges();
                        return new Response { Status = "Success", Message = "Order update successfully", data = OrderFound };

                    }
                    return new Response { Status = "Error", Message = "Order Not Found", data = OrderFound };

                }
            }

            return new Response { Status = "Error", Message = "Order Not Found" };

        }

        //delete order
        public bool DeleteOrder(int id)
        {


            Order order = _db.Orders.FirstOrDefault(p => p.Order_Id == id && p.Isdeleted == false);

            if (order != null)
            {
                order.Isdeleted = true;
                _db.SaveChanges();
                return true;
            }

            return false;

        }
    }
}
