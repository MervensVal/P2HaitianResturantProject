using HaitianRestaurantApp2.Data;
using HaitianRestaurantApp2.Models;
using HaitianRestaurantApp2.Models.ViewModels;
using HaitianRestaurantApp2.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaitianRestaurantApp2.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = Constants.RoleCustomer)]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        [BindProperty]
        public ShoppingCartViewModel ShoppingCartVM { get; set; }

        public CartController(ApplicationDbContext context, UserManager<IdentityUser> userManager) 
        {
            _context = context;
            _userManager = userManager;
        }

#nullable enable
        /*C# 8.0 Nullable enable. C# 8.0 has come with new feature i.e. 
         * 'nullable enable', enabling this feature will allow compiler 
         * to verify all properties and parameters in your code and track 
         * where null reference is possible and inform you to fix it*/
        public async Task<IActionResult> Index(int? cartId, string checkoutMessage, ShoppingCartViewModel ShoppingCartViewModel)
        {
            if (cartId != 0) 
            {
                ViewBag.CartItem = cartId;
            }
            if (checkoutMessage != null)
            {
                ViewBag.CheckoutError = true;
                TempData["CheckoutError"] = checkoutMessage;
            }
            else
            {
                ViewBag.CheckoutError = false;

                ApplicationUser currentUser = (ApplicationUser)await _userManager.GetUserAsync(User);

                ShoppingCartVM = new ShoppingCartViewModel
                {
                    Order = new Models.OrderHeader(),
                    CartList = _context.ShoppingCarts.Include(c => c.Menu).Where(c => c.ApplicationUserId == currentUser.Id)
                };

                ShoppingCartVM.Order.OrderTotal = 0;
                ShoppingCartVM.Order.ApplicationUser = _context.ApplicationUsers.FirstOrDefault(u => u.Id == currentUser.Id);

                foreach (var list in ShoppingCartVM.CartList) 
                {
                    if (list.Menu != null) 
                    {
                        ShoppingCartVM.Order.OrderTotal += list.Menu.Price * list.Count;
                        if (list.Menu?.Description?.Length > 100) 
                        {
                            list.Menu.Description = list.Menu.Description.Substring(0, 99) + "...";
                        }
                    }               
                }
                _context.SaveChanges();
                var count = _context.ShoppingCarts.Where(c => c.ApplicationUserId == currentUser.Id).AsEnumerable().Count();
                // Set session with the number of items in the  cart 
                HttpContext.Session.SetInt32(Constants.ssShoppingCart, count);
                return View(ShoppingCartVM);

            }
            return View();
        }
    }
}
