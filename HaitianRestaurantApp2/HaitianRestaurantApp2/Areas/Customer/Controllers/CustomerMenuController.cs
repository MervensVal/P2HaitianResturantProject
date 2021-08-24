using HaitianRestaurantApp2.Data;
using HaitianRestaurantApp2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using HaitianRestaurantApp2.Utility;
using Microsoft.AspNetCore.Identity;

namespace HaitianRestaurantApp2.Areas.Customer.Controllers
{
    [Authorize(Roles =Constants.RoleCustomer)]
    [Area("Customer")]
    public class CustomerMenuController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CustomerMenuController(ApplicationDbContext context, UserManager<IdentityUser> uerManager) 
        {
            _context = context;
            _userManager = uerManager;

        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Menu.Include(m => m.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var MenuItemFromDb = await _context.Menu.Include(p => p.Category).FirstOrDefaultAsync(u => u.MenuId == id);

            if (MenuItemFromDb == null)
            {
                return NotFound();
            }

            ShoppingCart cartObj = new ShoppingCart()
            {
                Menu = MenuItemFromDb,
                MenuItemId = MenuItemFromDb.MenuId
            };
            return View(cartObj);
        }

        //-------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(ShoppingCart CartObject) 
        {
            //if modelstate is valid then we will add to cart
            if (ModelState.IsValid)
            { 
                //get current user
                ApplicationUser currentUser = (ApplicationUser)await _userManager.GetUserAsync(User);
                
                //sets the cartObj ApplicationUserId equal to the current user
                CartObject.ApplicationUserId = currentUser.Id;

                //var claimsIdentity = (ClaimsIdentity)User.Identity;
                //var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                //CartObject.ApplicationUserId = claim.Value;

                //grabs a cart from the db where the ApplicationUserId == CartObject.ApplicationUserId and MenuItemId == CartObject.MenuItemId
                ShoppingCart cartFromDb = await _context.ShoppingCarts.Include(c => c.Menu).
                    FirstOrDefaultAsync(u => u.ApplicationUserId == CartObject.ApplicationUserId
                && u.MenuItemId == CartObject.MenuItemId
                );

                if (cartFromDb == null) //if there is no cart in db
                {
                    //no records exists in database for that product for that user so add the shopping cart
                    _context.ShoppingCarts.Add(CartObject);
                }
                else
                {
                    //if record exists for this item increment the count
                    cartFromDb.Count += CartObject.Count;

                    //updating shopping cart with the new cart object
                    _context.ShoppingCarts.Update(cartFromDb);
                }
                _context.SaveChanges();

                //amount of items in cart of the user
                var count = _context.ShoppingCarts.Where(c => c.ApplicationUserId == CartObject.ApplicationUserId).AsEnumerable().Count();

                // Set session with the number of items in the  cart 
                HttpContext.Session.SetInt32(Constants.ssShoppingCart, count);
                return RedirectToAction(nameof(Index));
            }
            else 
            {
                var menuItemFromDb = await _context.Menu.Include(p => p.Category).FirstOrDefaultAsync(u => u.MenuId == CartObject.
                MenuItemId);

                ShoppingCart cartObj = new ShoppingCart()
                {
                    Menu = menuItemFromDb,
                    MenuItemId = menuItemFromDb.MenuId
                };
                return View(cartObj);
            }
        }
    }
}


/*var menu = await _context.Menu
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.MenuId == id);*/