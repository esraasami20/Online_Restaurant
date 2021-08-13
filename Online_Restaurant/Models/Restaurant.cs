using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Restaurant.Models
{
    public class Restaurant
    {
        [Key]
        public int Restaurant_Id { get; set; }
        [Required]
        [MaxLength(100), MinLength(2)]
        public string Restaurant_Name { get; set; }
        [Required]
        [MaxLength(100), MinLength(2)]
        public string Description { get; set; }

        //[Required]
        public string RestaurantImg { get; set; }

        [DefaultValue(false)]
        public bool Isdeleted { get; set; }


        [ForeignKey("Cities")]
        public int? City_Id { get; set; }


        public virtual City Cities { get; set; }
        public virtual List<Menu> Menus { get; set; }

    }
}
