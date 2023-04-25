using FixIt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FixIt.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> useManager;
        public AdminController(UserManager<IdentityUser> _useManager)
        {
            useManager = _useManager;
        }

        public async Task<IActionResult> Index()
        {
            var admins = (await useManager
                .GetUsersInRoleAsync("Administrator"))
                .ToArray();
            var everyone = await useManager.Users.ToArrayAsync();

            var model = new AdminViewModel
            {
                Administrators = admins,
                Everyone = everyone
            };
            return View();
        }
    }
}
