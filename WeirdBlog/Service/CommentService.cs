using Microsoft.EntityFrameworkCore;
using WeirdBlog.DataAccess.Data;
using WeirdBlog.Models;

namespace WeirdBlog.Service
{
    public class CommentService : ICommentService
    {
        private readonly WeirdBlogDbContext _dbContext;
        public CommentService(WeirdBlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddComment(Comment comment)
        {
            _dbContext.Comments.Add(comment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteComment(Guid id)
        {
            var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.CommentId == id);

            if (comment == null)
            {
                return false;
            }
            _dbContext.Comments.Remove(comment);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public List<Comment> GetAllComments(Guid postId)
        {
            var comments = _dbContext.Comments
                .Where(c => c.PostId == postId)
                .Include(u => u.User)
                .ToList();

            return comments;
        }
    }
}
