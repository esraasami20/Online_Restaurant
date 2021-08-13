using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Restaurant.Models
{
    public class City
    {
        [Key]
        public int City_Id { get; set; }

        [Required]
        [MaxLength(100), MinLength(2)]
        public string City_Name { get; set; }

        [DefaultValue(false)]
        public bool Isdeleted { get; set; }

        public virtual List<Restaurant> Restaurants { get; set; }
    }
}
