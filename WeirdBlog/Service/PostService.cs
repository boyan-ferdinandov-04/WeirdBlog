using Microsoft.EntityFrameworkCore;
using WeirdBlog.DataAccess.Data;
using WeirdBlog.Models;

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

        public async Task<bool> Delete(Guid id)
        {
            var product = await _context.Posts.FindAsync(id);
            if (product == null)
            {
                return false;
            }
            _context.Posts.Remove(product);
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

        public Post? GetPost(Guid id)
        {
            return _context.Posts.Include(p => p.Category).FirstOrDefault(p => p.Guid == id);
        }

    }
}
