using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using HaitianRestaurantApp2.Utility;

namespace HaitianRestaurantApp2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Constants.RoleAdmin)]

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
