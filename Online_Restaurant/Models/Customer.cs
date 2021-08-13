using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Restaurant.Models
{
    public class Customer
    {
        [Key]
        public int customer_Id { get; set; }
        [Required]
        [MaxLength(100), MinLength(2)]
        public string customer_Name { get; set; }
        public int customer_Phone { get; set; }
        [Required]
        [MaxLength(100), MinLength(2)]
        public string customer_Address { get; set; }
        [Required]
        public string customer_Email { get; set; }
        [DefaultValue(false)]
        public bool Isdeleted { get; set; }


        public virtual List<Order> Orders { get; set; }
    }
}
