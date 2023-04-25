using FixIt.Models;
using FixIt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FixIt.Controllers
{
    [Authorize]
    public class FixItController : Controller
    {
        private readonly IFixItService fixitService;
        private readonly UserManager<IdentityUser> userManager;
        public FixItController(IFixItService _fixitService, UserManager<IdentityUser> _userManager)
        {
            fixitService = _fixitService;
            userManager = _userManager;
        }
        public async Task<IActionResult> Index()
        {
            // hämta logged in User
            var currentUser = await userManager.GetUserAsync(User);
            // hämta todo items
            var items = await fixitService.GetIncompleteAsync(currentUser);
            //Skicka items till en modell
            var model = new ToDoViewModel()
            {
                Items = items
        };
            //Skicka resultatet till en view
            return View(model);
        }
        public async Task<IActionResult> AddItem(ToDoItem newitem)
        {
            var currentUser = await userManager.GetUserAsync(User);
            var successful = await fixitService.AddItemAsync(newitem, currentUser);
            if (!successful)
            {
                return BadRequest("Could not add this item");
            }
            return RedirectToAction("Index");
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkDone(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }
            var currentUser = await userManager.GetUserAsync(User);
            var successful = await fixitService.MarkDoneAsync(id, currentUser);
            if (!successful)
            {
                return BadRequest("Could not mark item as done.");
            }

            return RedirectToAction("Index");
        }


    }
}
