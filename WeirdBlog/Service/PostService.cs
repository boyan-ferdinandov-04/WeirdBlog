using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeirdBlog.DataAccess.Data;
using WeirdBlog.Models;
using WeirdBlog.Service.IService;

namespace WeirdBlog.Service
{
    public class PostService : IPostService
    {
        private readonly WeirdBlogDbContext _context;
        public PostService(WeirdBlogDbContext dbContext)
        {
            _context = dbContext;   
        }
        public void CreatePost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public async Task<PaginatedList<Post>> GetPendingPostsAsync(int pageIndex, int pageSize)
        {
            var query = _context.Posts
                .Where(p => !p.IsApproved)
                .Include(p => p.Category)
                .OrderByDescending(p => p.CreatedAt)
                .AsQueryable();

            return await PaginatedList<Post>.CreateAsync(query.AsNoTracking(), pageIndex, pageSize);
        }

        public async Task<bool> ApprovePost(Guid postId)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.PostId == postId);
            if (post == null)
            {
                return false;
            }
            post.IsApproved = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> LikePost(Guid postId, Guid userId)
        {
            var existingLike = await _context.Likes
                .FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId);
            if (existingLike != null)
            {
                _context.Likes.Remove(existingLike);
                await _context.SaveChangesAsync();
                return false; // Like removed.
            }

            var existingDislike = await _context.Dislikes
                .FirstOrDefaultAsync(d => d.PostId == postId && d.UserId == userId);
            if (existingDislike != null)
            {
                _context.Dislikes.Remove(existingDislike);
            }

            var like = new Like
            {
                PostId = postId,
                UserId = userId
            };

            await _context.Likes.AddAsync(like);
            await _context.SaveChangesAsync();
            return true; 
        }

        public Post? GetPostBySlug(string slug)
        {
            return _context.Posts
                .Include(c => c.Category)
                .Include(u => u.User)
                .Include(t => t.Comments)
                .ThenInclude(c => c.User)
                .Include(t => t.Comments)
                .ThenInclude(c => c.Replies)
                .ThenInclude(r => r.User)
                .Include(l => l.Likes)
                .Include(d => d.Dislikes)
                .FirstOrDefault(p => p.Slug == slug);
        }

        public async Task<bool> Delete(Guid id)
        {
            var post = await _context.Posts.Include(c => c.Comments).Include(l => l.Likes).FirstOrDefaultAsync(p => p.PostId == id);
            if (post == null)
            {
                return false;
            }
            _context.Comments.RemoveRange(post.Comments);
            _context.Likes.RemoveRange(post.Likes);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Post> Edit(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public List<Post> GetAllPosts()
        {
            return _context.Posts.ToList();
        }


        public async Task<PaginatedList<Post>> GetPaginatedPostsAsync(int pageIndex, int pageSize, string searchTitle = null, int? selectedCategoryId = null)
        {
            var query = _context.Posts
                .Where(p => p.IsApproved)
                .Include(p => p.Category)
                .OrderByDescending(p => p.CreatedAt)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTitle))
            {
                query = query.Where(p => p.Title.ToLower().Contains(searchTitle.ToLower()));
            }

            if (selectedCategoryId.HasValue && selectedCategoryId.Value > 0)
            {
                query = query.Where(p => p.CategoryId == selectedCategoryId.Value);
            }

            return await PaginatedList<Post>.CreateAsync(query.AsNoTracking(), pageIndex, pageSize);
        }


        public Post? GetPost(Guid id)
        {
            //var post = _context.Posts
            //    .FirstOrDefault(p => p.PostId == id);
            //var user = _context.ApplicationUsers.FirstOrDefault(u => u.Id == u.Posts.FirstOrDefault(p => p.PostId == id).User.Id);
            //var category = post.Category;

            return _context.Posts
                .Include(c => c.Category)
                .Include(u => u.User)
                .Include(t => t.Comments)
                .ThenInclude(c => c.User)
                .Include(t => t.Comments)
                .ThenInclude(c => c.Replies)
                .ThenInclude(r => r.User) 
                .Include(l => l.Likes)
                .Include(d => d.Dislikes)
                .FirstOrDefault(p => p.PostId == id);
        }

        public async Task<bool> DislikePost(Guid postId, Guid userId)
        {
            var existingDislike = await _context.Dislikes
                .FirstOrDefaultAsync(d => d.PostId == postId && d.UserId == userId);
            if (existingDislike != null)
            {
                _context.Dislikes.Remove(existingDislike);
                await _context.SaveChangesAsync();
                return false; 
            }

            var existingLike = await _context.Likes
                .FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId);
            if (existingLike != null)
            {
                _context.Likes.Remove(existingLike);
            }

            var dislike = new Dislike
            {
                PostId = postId,
                UserId = userId
            };

            await _context.Dislikes.AddAsync(dislike);
            await _context.SaveChangesAsync();
            return true; 
        }
    }
}
