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
    public class LocationsForCustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocationsForCustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Locations.ToListAsync());
        }
    }
}


