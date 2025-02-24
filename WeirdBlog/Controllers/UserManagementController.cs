using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeirdBlog.Models;
using WeirdBlog.Models.ViewModels;
using WeirdBlog.Utility;

namespace WeirdBlog.Controllers
{
    [Authorize(Roles = StaticConstants.Role_Admin)]
    public class UserManagementController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManagementController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }
        public async Task<IActionResult> Manage(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var model = new ManageUserViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                Roles = userRoles.ToList(),
                AllRoles = new List<string> { StaticConstants.Role_Admin, StaticConstants.Role_User }
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(ManageUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId.ToString());
            if (user == null)
            {
                return NotFound();
            }

            // Remove any existing roles.
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            // Determine the new role to assign.
            string newRole = model.SelectedRole;

            // Ensure the role exists; if not, create it.
            if (!await _roleManager.RoleExistsAsync(newRole))
            {
                await _roleManager.CreateAsync(new IdentityRole(newRole));
            }

            // Add the new role.
            await _userManager.AddToRoleAsync(user, newRole);
            TempData["success"] = "User role updated successfully.";

            return RedirectToAction("Index");
        }
    }
}
