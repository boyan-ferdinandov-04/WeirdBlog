﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WeirdBlog.Models;
using WeirdBlog.Models.ViewModels;
using WeirdBlog.Service.IService;
using WeirdBlog.Utility;

namespace WeirdBlog.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        private readonly ICommentService _commentService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostController(IPostService postService, ICategoryService categoryService,
            ICommentService commentService, UserManager<ApplicationUser> userManager)
        {
            _postService = postService;
            _categoryService = categoryService;
            _commentService = commentService;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(PostVM postVM, int pageIndex = 1, int pageSize = 5)
        {
            postVM.Categories = _categoryService.GetCategories().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.CategoryId.ToString()
            });

            var posts = await _postService.GetPaginatedPostsAsync(pageIndex, pageSize, postVM.SearchTitle,
                postVM.SelectedCategoryId);

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
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }

            return View(postVM);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(PostVM postVM)
        {
            if (ModelState.IsValid)
            {
                postVM.Post.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                _postService.CreatePost(postVM.Post);
                TempData["success"] = "Post Created Successfully";
                return RedirectToAction("Index");
            }

            postVM.Categories = _categoryService.GetCategories().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.CategoryId.ToString()
            });

            return View(postVM);
        }

        [Authorize]
        public IActionResult Edit(Guid id)
        {
            var post = _postService.GetPost(id);
            if (post == null)
            {
                return NotFound();
            }

            var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (post.UserId != currentUserId && !User.IsInRole(StaticConstants.Role_Admin))
            {
                return Forbid();
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
        [Authorize]
        public IActionResult Edit(PostVM obj)
        {
            if (ModelState.IsValid)
            {
                var currentPost = _postService.GetPost(obj.Post.PostId);
                if (currentPost == null)
                {
                    return NotFound();
                }

                var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (currentPost.UserId != currentUserId && !User.IsInRole(StaticConstants.Role_Admin))
                {
                    return Forbid();
                }

                currentPost.Title = obj.Post.Title;
                currentPost.Content = obj.Post.Content;
                currentPost.ImageUrl = obj.Post.ImageUrl;
                currentPost.CategoryId = obj.Post.CategoryId;

                _postService.Edit(currentPost);
                TempData["info"] = "Post edited successfully";
                return RedirectToAction("Index");
            }

            obj.Categories = _categoryService.GetCategories().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.CategoryId.ToString()
            });

            return View(obj);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Like(Guid postId)
        {
            var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var post = _postService.GetPost(postId);

            if (post == null)
            {
                return NotFound();
            }

            var likeAdded = await _postService.LikePost(postId, currentUserId);

            if (likeAdded)
            {
                TempData["info"] = "You liked the post!";
            }
            else
            {
                TempData["info"] = "You already liked this post.";
            }

            return RedirectToAction("Details", new { id = postId });
        }


        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var post = _postService.GetPost(id);
            if (post == null)
            {
                return NotFound();
            }

            var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (post.UserId != currentUserId && !User.IsInRole(StaticConstants.Role_Admin))
            {
                return Forbid();
            }

            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _postService.Delete(id);
            TempData["error"] = "Post Deleted Successfully";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(Guid postId, string content)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Redirect("/Identity/Account/Login");
            }

            var comment = new Comment
            {
                Content = content,
                PostId = postId,
                UserId = user.Id,
                CreatedAt = DateTime.Now
            };

            await _commentService.AddComment(comment);

            return RedirectToAction("Details", new { id = postId });
        }

        [HttpPost]
        public async Task<IActionResult> AddReply(Guid commentId, Guid postId, string content)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Redirect("/Identity/Account/Login");
            }

            var reply = new Comment
            {
                Content = content,
                PostId = postId,
                UserId = user.Id,
                CreatedAt = DateTime.Now,
                ParentCommentId = commentId
            };

            await _commentService.AddComment(reply);

            return RedirectToAction("Details", new { id = postId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCommentWithReplies(Guid commentId, Guid postId)
        {
            bool result = await _commentService.DeleteCommentWithReplies(commentId);

            if (!result)
            {
                TempData["Error"] = "Unable to delete the comment.";
            }
            else
            {
                TempData["Success"] = "Comment and its replies have been deleted successfully.";
            }

            return RedirectToAction("Details", new { id = postId });
        }
    }
}
