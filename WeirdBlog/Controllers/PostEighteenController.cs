using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WeirdBlog.Models;
using WeirdBlog.Models.ViewModels;
using WeirdBlog.Service;

namespace WeirdBlog.Controllers
{
    public class PostEighteenController : Controller
    {
        public ICategoryService _categoryService;
        public IPostService _postService;
        public PostEighteenController(ICategoryService categoryService,IPostService postService)
        { 
        
            _categoryService = categoryService; 
            _postService = postService;
        }
        public async Task<IActionResult> Index(PostEighteenVM postVM, int pageIndex = 1, int pageSize = 3)
        {
            postVM.Categories = _categoryService.GetAllCategoriesEighteen().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.CategoryEighteenId.ToString()
            });

            var posts = await _postService.GetPaginatedPosts(pageIndex, pageSize, postVM.SearchTitle, postVM.SelectedCategoryId);

            ViewBag.Posts = posts;
            return View(postVM);
        }

        public IActionResult Details(Guid id)
        {
            var post = _postService.GetPostEighteen(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        public IActionResult Create()
        {
            PostEighteenVM postVM = new PostEighteenVM()
            {
                Post = new PostEighteen(),
                Categories = _categoryService.GetAllCategoriesEighteen().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.CategoryEighteenId.ToString()
                })
            };
            return View(postVM);
        }

        [HttpPost]
        public IActionResult Create(PostEighteenVM postVM)
        {
            if (ModelState.IsValid)
            {
                _postService.CreatePostEighteen(postVM.Post);
                return RedirectToAction("Index");
            }
            postVM.Categories = _categoryService.GetAllCategoriesEighteen().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.CategoryEighteenId.ToString()
            });
            return View(postVM);
        }

        public IActionResult Edit(Guid id)
        {
            var post = _postService.GetPostEighteen(id);
            if (post == null)
            {
                return NotFound();
            }
            PostEighteenVM postVM = new PostEighteenVM()
            {
                Post = post,
                Categories = _categoryService.GetAllCategoriesEighteen().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.CategoryEighteenId.ToString()
                })
            };
            return View(postVM);
        }

        [HttpPost]
        public IActionResult Edit(PostEighteenVM obj)
        {
            _postService.EditPost(obj.Post);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var post = _postService.GetPostEighteen(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _postService.DeleteEighteen(id);
            return RedirectToAction("Index");
        }
    }
}
