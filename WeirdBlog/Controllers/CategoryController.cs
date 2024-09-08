using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeirdBlog.Models;
using WeirdBlog.Service;
using WeirdBlog.Utility;

namespace WeirdBlog.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            var categories = _categoryService.GetCategories();
            return View(categories);
        }

        public IActionResult Create()
        {
            if (!User.IsInRole(StaticConstants.Role_Admin))
            {
                return Redirect("/Identity/Account/AccessDenied");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.AddCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.Edit(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _categoryService.Delete(id);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
