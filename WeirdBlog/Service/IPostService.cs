using WeirdBlog.Models;

namespace WeirdBlog.Service
{
    public interface IPostService
    {
        void CreatePost(Post post);
        List<Post> GetAllPosts();
    }
}
