using WeirdBlog.Models;

namespace WeirdBlog.Service
{
    public interface ICommentService
    {
        Task AddComment(Comment comment);

        Task<bool> DeleteComment(Guid id);

        List<Comment> GetAllComments(Guid postId);
    }
}
