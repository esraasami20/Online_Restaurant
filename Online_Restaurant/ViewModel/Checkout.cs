using Online_Restaurant.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Restaurant.ViewModel
{
    public class Checkout
    {
        public List<Menu> menus { get; set; }
        public string customer_Name { get; set; }
        public int customer_Phone { get; set; }
        [Required]
        [MaxLength(100), MinLength(2)]
        public string customer_Address { get; set; }
        [Required]
        public string customer_Email { get; set; }
    }
}
