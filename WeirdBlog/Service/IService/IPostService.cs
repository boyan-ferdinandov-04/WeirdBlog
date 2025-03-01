using WeirdBlog.Models;

namespace WeirdBlog.Service.IService
{
    public interface IPostService
    {
        void CreatePost(Post post);
        List<Post> GetAllPosts();
        Task<Post> Edit(Post post);
        Task<bool> Delete(Guid id);
        Post? GetPost(Guid id);
        Task<PaginatedList<Post>> GetPaginatedPostsAsync(int pageIndex, int pageSize, string searchTitle = null,
            int? selectedCategoryId = null);
        Task<PaginatedList<Post>> GetPendingPostsAsync(int pageIndex, int pageSize);
        Task<bool> ApprovePost(Guid postId);
        Task<bool> LikePost(Guid postId, Guid userId);
        Task<bool> DislikePost(Guid postId, Guid userId);

        Post? GetPostBySlug(string slug);
    }
}
