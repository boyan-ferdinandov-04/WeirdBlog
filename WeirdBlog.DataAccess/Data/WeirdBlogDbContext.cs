using Microsoft.EntityFrameworkCore;
using WeirdBlog.Models;

namespace WeirdBlog.DataAccess.Data
{
    public class WeirdBlogDbContext : DbContext
    {
        public WeirdBlogDbContext(DbContextOptions<WeirdBlogDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
