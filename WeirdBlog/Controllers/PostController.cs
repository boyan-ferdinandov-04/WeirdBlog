using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WeirdBlog.Models;
using WeirdBlog.Models.ViewModels;
using WeirdBlog.Service;
using WeirdBlog.Utility;

namespace WeirdBlog.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;

        public PostController(IPostService postService, ICategoryService categoryService)
        {
            _postService = postService;
            _categoryService = categoryService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(PostVM postVM, int pageIndex = 1, int pageSize = 5)
        {
            postVM.Categories = _categoryService.GetCategories().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.CategoryId.ToString()
            });

            var posts = await _postService.GetPaginatedPostsAsync(pageIndex, pageSize, postVM.SearchTitle, postVM.SelectedCategoryId);

            ViewBag.Posts = posts;
            return View(postVM);
        }

        [AllowAnonymous]
        public IActionResult Details(Guid id)
        {
            var post = _postService.GetPost(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }
        [Authorize]
        public IActionResult Create()
        {
            PostVM postVM = new PostVM()
            {
                Post = new Post(),
                Categories = _categoryService.GetCategories().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.CategoryId.ToString()
                })
            };
            
            return View(postVM);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(PostVM postVM)
        {
            if (ModelState.IsValid)
            {
                _postService.CreatePost(postVM.Post);
                return RedirectToAction("Index");
            }
            postVM.Categories = _categoryService.GetCategories().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.CategoryId.ToString()
            });
            return View(postVM);
        }

        [Authorize(Roles = StaticConstants.Role_Admin)]
        public IActionResult Edit(Guid id)
        {
            var post = _postService.GetPost(id);
            if (post == null)
            {
                return NotFound();
            }
            PostVM postVM = new PostVM()
            {
                Post = post,
                Categories = _categoryService.GetCategories().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.CategoryId.ToString()
                })
            };
            return View(postVM);
        }

        [HttpPost]
        [Authorize(Roles = StaticConstants.Role_Admin)]
        public IActionResult Edit(PostVM obj)
        {
            _postService.Edit(obj.Post);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = StaticConstants.Role_Admin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var post = _postService.GetPost(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = StaticConstants.Role_Admin)]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _postService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
