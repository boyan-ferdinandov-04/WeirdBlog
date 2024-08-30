using WeirdBlog.Models;

namespace WeirdBlog.Service
{
    public interface IPostService
    {
        void CreatePost(Post post);
        void CreatePostEighteen(PostEighteen postEighteen);
        List<Post> GetAllPosts();
        List<PostEighteen> GetPosts(); 
        Task<Post> Edit(Post post);
        Task<PostEighteen> EditPost(PostEighteen post);
        Task<bool> Delete(Guid id);
        Post? GetPost(Guid id);
        PostEighteen? GetPostEighteen(Guid id);
        Task<PaginatedList<Post>> GetPaginatedPostsAsync(int pageIndex, int pageSize, string searchTitle = null, int? selectedCategoryId = null);
        Task<PaginatedList<PostEighteen>> GetPaginatedPosts(int pageIndex, int pageSize, string searchTitle = null, int? selectedCategoryId = null);
    }
}
