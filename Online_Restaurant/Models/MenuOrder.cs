using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Restaurant.Models
{
    public class MenuOrder
    {
        [Key]
        [ForeignKey("Order")]
        [Column(Order = 0)]
        public int Order_Id { get; set; }
        [Key]
        [ForeignKey("Menu")]
        [Column(Order = 1)]
        public int Menu_Id { get; set; }

        public int Quantity { get; set; }
        public float? Total { get; set; }

        [DefaultValue(false)]
        public bool Isdeleted { get; set; }

        public Order Order { get; set; }
        public Menu Menu { get; set; }

    }
}
