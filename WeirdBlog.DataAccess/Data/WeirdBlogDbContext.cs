using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WeirdBlog.Models;

namespace WeirdBlog.DataAccess.Data
{
    public class WeirdBlogDbContext : IdentityDbContext<IdentityUser>
    {
        public WeirdBlogDbContext(DbContextOptions<WeirdBlogDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostEighteen> PostsEighteen { get; set; }
        public DbSet<CategoryEighteen> CategoriesEighteen { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CategoryEighteen>().HasData(
                new CategoryEighteen { CategoryEighteenId = 1, Name = "Animated" },
                new CategoryEighteen { CategoryEighteenId = 2, Name = "Live-Action" }
                );
        }
    }
}
