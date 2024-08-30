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
        public DbSet<PostEighteen> PostsEighteen { get; set; }
        public DbSet<CategoryEighteen> CategoriesEighteen { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryEighteen>().HasData(
                new CategoryEighteen { CategoryEighteenId = 1 ,Name = "Animated"},
                new CategoryEighteen { CategoryEighteenId = 2 ,Name = "Live-Action"}
                );
        }
    }
}
