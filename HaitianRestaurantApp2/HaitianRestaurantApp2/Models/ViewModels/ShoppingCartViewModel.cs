using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaitianRestaurantApp2.Models.ViewModels
{
    public class ShoppingCartViewModel
    {
        public IEnumerable<ShoppingCart> CartList { get; set; }
        public OrderHeader Order { get; set; }
    }
}
