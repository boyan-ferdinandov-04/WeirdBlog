using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeirdBlog.Models;
using WeirdBlog.Models.ViewModels;
using WeirdBlog.Utility;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WeirdBlog.Controllers
{
    [Authorize(Roles = StaticConstants.Role_Admin)]
    public class UserManagementController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public UserManagementController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<Guid>> roleManager)
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
                UserId = user.Id.ToString(),
                Email = user.Email,
                Roles = userRoles.ToList(),
                AllRoles = new List<string> { StaticConstants.Role_Admin, StaticConstants.Role_User }
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(ManageUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            
            if (model.SelectedRoles != null && model.SelectedRoles.Any())
            {
                foreach (var roleName in model.SelectedRoles)
                {
                    if (!await _roleManager.RoleExistsAsync(roleName))
                    {
                        var role = new IdentityRole<Guid> { Name = roleName };
                        await _roleManager.CreateAsync(role);
                    }
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }
            
            TempData["success"] = "User roles updated successfully.";
            return RedirectToAction("Index");
        }

    }
}
