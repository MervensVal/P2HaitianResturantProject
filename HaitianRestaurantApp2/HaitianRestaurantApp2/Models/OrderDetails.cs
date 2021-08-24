using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HaitianRestaurantApp2.Models
{
    //The Order Details is a list of items ordered
    public class OrderDetails
    {
        [Key]
        public int OrderDetailsId { get; set; }


        [Required]
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public OrderHeader OrderHeader { get; set; }


        [Required]
        public int MenuItemId { get; set; }
        [ForeignKey("MenuItemId")]
        public Menu Menu { get; set; }


        public int Count { get; set; }

        public double Price { get; set; }


    }
}
