using HaitianRestaurantApp2.Data;
using HaitianRestaurantApp2.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaitianRestaurantApp2.Areas.Customer.Controllers
{
    [Authorize(Roles =Constants.RoleCustomer)]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        public IActionResult Index(int? id)
        {
            return View();
        }
    }
}
