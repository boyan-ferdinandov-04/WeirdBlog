using WeirdBlog.Models;

namespace WeirdBlog.Service
{
    public interface ICommentService
    {
        Task AddComment(Comment comment);

        List<Comment> GetAllComments(Guid postId);
    }
}
