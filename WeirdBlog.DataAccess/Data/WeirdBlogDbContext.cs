using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WeirdBlog.Models;

namespace WeirdBlog.DataAccess.Data
{
    public class WeirdBlogDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
        public WeirdBlogDbContext(DbContextOptions<WeirdBlogDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Action"},
                new Category { CategoryId = 2, Name = "SciFi"},
                new Category { CategoryId = 3, Name = "History"}
            );

        }
    }
}
