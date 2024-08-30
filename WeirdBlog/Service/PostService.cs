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

        public void CreatePostEighteen(PostEighteen postEighteen)
        {
            _context.PostsEighteen.Add(postEighteen);
            _context.SaveChanges();
        }

        public async Task<bool> Delete(Guid id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return false;
            }
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEighteen(Guid id)
        {
            var postEighteen = await _context.PostsEighteen.FindAsync(id);
            if (postEighteen == null)
            {
                return false;
            }
            _context.PostsEighteen.Remove(postEighteen);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Post> Edit(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<PostEighteen> EditPost(PostEighteen post)
        {
            _context.PostsEighteen.Update(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public List<Post> GetAllPosts()
        {
            return _context.Posts.ToList();
        }

        public async Task<PaginatedList<PostEighteen>> GetPaginatedPosts(int pageIndex, int pageSize, string searchTitle = null, int? selectedCategoryId = null)
        {
            var query = _context.PostsEighteen.Include(p => p.CategoryEighteen).OrderByDescending(p => p.CreatedAt).AsQueryable();
            if (!string.IsNullOrEmpty(searchTitle))
            {
                query = query.Where(p => p.Title.ToLower().Contains(searchTitle.ToLower()));
            }

            if (selectedCategoryId.HasValue && selectedCategoryId.Value > 0)
            {
                query = query.Where(p => p.CategoryEighteenId == selectedCategoryId.Value);
            }
            return await PaginatedList<PostEighteen>.CreateAsync(query.AsNoTracking(), pageIndex, pageSize);
        }

        public async Task<PaginatedList<Post>> GetPaginatedPostsAsync(int pageIndex, int pageSize, string searchTitle = null, int? selectedCategoryId = null)
        {
            var query = _context.Posts.Include(p => p.Category).OrderByDescending(p => p.CreatedAt).AsQueryable();

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
            return _context.Posts.Include(p => p.Category).FirstOrDefault(p => p.PostId == id);
        }

        public PostEighteen? GetPostEighteen(Guid id)
        {
            return _context.PostsEighteen.Include(p => p.CategoryEighteen).FirstOrDefault(p => p.PostEighteenId == id);
        }

        public List<PostEighteen> GetPosts()
        {
            return _context.PostsEighteen.ToList();
        }
    }
}
