using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeirdBlog.DataAccess.Data;
using WeirdBlog.Models;
using WeirdBlog.Service;
using Xunit;

namespace PostsTest
{
    public class PostServiceTests:IDisposable
    {
        private readonly WeirdBlogDbContext _context;
        private readonly PostService _postService;

        public PostServiceTests()
        {
            var options = new DbContextOptionsBuilder<WeirdBlogDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new WeirdBlogDbContext(options);
            _postService = new PostService(_context);
        }
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public void CreatePost_ShouldAddPost()
        {
            var post = new Post
            {
                PostId = Guid.NewGuid(),
                Title = "Test Title",
                Content = "Test Content",
                ImageUrl = "http://example.com/image.jpg",
                CreatedAt = DateTime.Now,
                CategoryId = 1,
                UserId = Guid.NewGuid()
            };

            _postService.CreatePost(post);

            var result = _context.Posts.FirstOrDefault(p => p.PostId == post.PostId);
            Assert.NotNull(result);
            Assert.Equal("Test Title", result.Title);
        }


        [Fact]
        public async Task LikePost_ShouldReturnTrue_WhenNewLike()
        {
            var postId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var post = new Post
            {
                PostId = postId,
                Title = "Post to like",
                Content = "Content",
                ImageUrl = "http://example.com/image.jpg",
                CreatedAt = DateTime.Now,
                CategoryId = 1,
                UserId = Guid.NewGuid()
            };
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            var result = await _postService.LikePost(postId, userId);

            Assert.True(result);
        }

        [Fact]
        public async Task LikePost_ShouldReturnFalse_WhenAlreadyLiked()
        {
            var postId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var post = new Post
            {
                PostId = postId,
                Title = "Post to like twice",
                Content = "Content",
                ImageUrl = "http://example.com/image.jpg",
                CreatedAt = DateTime.Now,
                CategoryId = 1,
                UserId = Guid.NewGuid()
            };
            _context.Posts.Add(post);
            _context.Likes.Add(new Like
            {
                PostId = postId,
                UserId = userId
            });
            await _context.SaveChangesAsync();

            var result = await _postService.LikePost(postId, userId);

            Assert.False(result);
        }

        [Fact]
        public async Task Delete_ShouldRemovePostAndItsDependencies()
        {
            var postId = Guid.NewGuid();
            var post = new Post
            {
                PostId = postId,
                Title = "Post to delete",
                Content = "Content",
                ImageUrl = "http://example.com/image.jpg",
                CreatedAt = DateTime.Now,
                CategoryId = 1,
                UserId = Guid.NewGuid(),
                Comments = new List<Comment>
                {
                    new Comment
                    {
                        CommentId = Guid.NewGuid(),
                        Content = "Comment",
                        CreatedAt = DateTime.Now,
                        UserId = Guid.NewGuid()
                    }
                },
                Likes = new List<Like>
                {
                    new Like
                    {
                        LikeId = Guid.NewGuid(),
                        UserId = Guid.NewGuid(),
                        PostId = postId
                    }
                }
            };
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            var result = await _postService.Delete(postId);

            Assert.True(result);
            var deletedPost = _context.Posts.FirstOrDefault(p => p.PostId == postId);
            Assert.Null(deletedPost);
        }

        [Fact]
        public async Task Edit_ShouldUpdatePostDetails()
        {
            var postId = Guid.NewGuid();
            var post = new Post
            {
                PostId = postId,
                Title = "Original Title",
                Content = "Original Content",
                ImageUrl = "http://example.com/image.jpg",
                CreatedAt = DateTime.Now,
                CategoryId = 1,
                UserId = Guid.NewGuid()
            };
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            post.Title = "Updated Title";
            post.Content = "Updated Content";
            var updatedPost = await _postService.Edit(post);

            Assert.Equal("Updated Title", updatedPost.Title);
            Assert.Equal("Updated Content", updatedPost.Content);
        }

        [Fact]
        public void GetAllPosts_ShouldReturnAllPosts()
        {
            var post1 = new Post
            {
                PostId = Guid.NewGuid(),
                Title = "Post1",
                Content = "Content1",
                ImageUrl = "http://example.com/image1.jpg",
                CreatedAt = DateTime.Now,
                CategoryId = 1,
                UserId = Guid.NewGuid()
            };
            var post2 = new Post
            {
                PostId = Guid.NewGuid(),
                Title = "Post2",
                Content = "Content2",
                ImageUrl = "http://example.com/image2.jpg",
                CreatedAt = DateTime.Now,
                CategoryId = 1,
                UserId = Guid.NewGuid()
            };
            _context.Posts.AddRange(post1, post2);
            _context.SaveChanges();

            var posts = _postService.GetAllPosts();
            Assert.Equal(2, posts.Count);
        }

        
    }
}