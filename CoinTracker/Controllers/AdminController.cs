using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CoinTracker.Models.ViewModels;

namespace CoinTracker.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: /Admin
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();

            var model = users.Select(u => new UserRoleViewModel
            {
                UserId = u.Id,
                Email = u.Email!,
                Roles = _userManager.GetRolesAsync(u).Result
            }).ToList();

            return View(model);
        }

        // POST: /Admin/UpdateRole
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRole(string userId, string role, bool isInRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return RedirectToAction(nameof(Index));

            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }

            if (isInRole)
                await _userManager.AddToRoleAsync(user, role);
            else
                await _userManager.RemoveFromRoleAsync(user, role);

            return RedirectToAction(nameof(Index));
        }
    }
}
