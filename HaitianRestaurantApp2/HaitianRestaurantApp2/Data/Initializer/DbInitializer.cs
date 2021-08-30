using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HaitianRestaurantApp2.Utility;
using HaitianRestaurantApp2.Models;

namespace HaitianRestaurantApp2.Data.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) 
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;

        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex) 
            {
                
            }

            if (_db.Roles.Any(r => r.Name == Constants.RoleAdmin)) 
            {
                return; //stops the function from continuing
            }

            _roleManager.CreateAsync(new IdentityRole(Constants.RoleAdmin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Constants.RoleEmployee)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Constants.RoleCustomer)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "admin123@email.com",
                Email = "admin123@email.com",
                Name = "Merv"
            }, "Admin@123").GetAwaiter().GetResult();

            ApplicationUser user = _db.ApplicationUsers.Where(u => u.Email == "admin123@email.com").FirstOrDefault();
            _userManager.AddToRoleAsync(user, Constants.RoleAdmin).GetAwaiter().GetResult();
        }
    }
}
