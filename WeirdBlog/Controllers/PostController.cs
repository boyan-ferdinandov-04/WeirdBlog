using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WeirdBlog.Models;
using WeirdBlog.Models.ViewModels;
using WeirdBlog.Service;

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

        public IActionResult Index(PostVM postVM)
        {
            var posts = _postService.GetAllPosts();

            if (!string.IsNullOrEmpty(postVM.SearchTitle))
            {
                posts = posts.Where(p => p.Title.Contains(postVM.SearchTitle, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (postVM.SelectedCategoryId.HasValue && postVM.SelectedCategoryId.Value > 0)
            {
                posts = posts.Where(p => p.CategoryId == postVM.SelectedCategoryId.Value).ToList();
            }

            postVM.Categories = _categoryService.GetCategories().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.CategoryId.ToString()
            });

            ViewBag.Posts = posts;
            return View(postVM);
        }



        public IActionResult Details(Guid id)
        {
            var post = _postService.GetPost(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

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
        public IActionResult Edit(PostVM obj)
        {
            _postService.Edit(obj.Post);
            return RedirectToAction("Index");
        }

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
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _postService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
