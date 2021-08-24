using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HaitianRestaurantApp2.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Count = 1;
        }

        [Key]
        public int ShoppingCartId { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

        public int MenuItemId { get; set; } //equivalent of a productId
        [ForeignKey("MenuItemId")]
        public Menu Menu { get; set; }

        [Range(1, 20, ErrorMessage = "Please enter a value between 1 and 20. To many items in cart.")]
        public int Count { get; set; }

        //public int LocationId { get; set; }
        //[ForeignKey("LocationId")]
        //public Location Location { get; set; }
    }
}
