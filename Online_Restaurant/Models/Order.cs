using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Restaurant.Models
{
    public class Order
    {
        [Key]
        public int Order_Id { get; set; }
        public DateTime Order_Date { get; set; } = DateTime.Now;
        public float? Total_Price { get; set; }
        [Required]
        public int Quantity { get; set; }

        [DefaultValue(false)]
        public bool Isdeleted { get; set; }


        [ForeignKey("Customer")]
        public int? customer_Id { get; set; }
        public Customer Customer { get; set; }



        public virtual List<MenuOrder> OrderItems { get; set; }


    }
}
