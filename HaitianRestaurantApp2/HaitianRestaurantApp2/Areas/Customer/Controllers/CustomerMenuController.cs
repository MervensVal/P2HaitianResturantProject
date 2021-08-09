using HaitianRestaurantApp2.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaitianRestaurantApp2.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CustomerMenuController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerMenuController(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Menu.Include(m => m.Category);
            return View(await applicationDbContext.ToListAsync());
        }
    }
}



/*
 *         private readonly ApplicationDbContext _context;

        public MenuController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Menu
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Menu.Include(m => m.Category);
            return View(await applicationDbContext.ToListAsync());
        }

 */