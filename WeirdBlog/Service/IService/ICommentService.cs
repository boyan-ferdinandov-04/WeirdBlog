using WeirdBlog.Models;

namespace WeirdBlog.Service.IService
{
    public interface ICommentService
    {
        Task AddComment(Comment comment);

        Task<bool> DeleteComment(Guid id);

        Task<bool> DeleteCommentWithReplies(Guid commentId);

        Task<bool> EditComment(Guid commentId, string content);

        List<Comment> GetAllComments(Guid postId);
    }
}
