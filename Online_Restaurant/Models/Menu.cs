using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Restaurant.Models
{
    public class Menu
    {
        [Key]
        public int Menu_Id { get; set; }
        [Required]
        [MaxLength(100), MinLength(2)]
        public string Menu_Title { get; set; }
        [Required]
        [MaxLength(200), MinLength(2)]
        public string Description { get; set; }
        public float? Price { get; set; }
        //[Required]
        public string Img { get; set; }

        [DefaultValue(false)]
        public bool Ischecked{ get; set; }

        [DefaultValue(false)]
        public bool Isdeleted { get; set; }

        [ForeignKey("Restaurant")]
        public int? Restaurant_Id { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual List<MenuOrder> OrderItems { get; set; }
    }
}
