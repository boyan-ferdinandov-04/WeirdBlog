using Microsoft.EntityFrameworkCore;
using WeirdBlog.DataAccess.Data;
using WeirdBlog.Service;
using Microsoft.AspNetCore.Identity;
using WeirdBlog.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using WeirdBlog.Models;
using WeirdBlog.Service.IService;


namespace WeirdBlog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<IEmailSender, EmailSender>();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            });
            builder.Services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = "111753696304-s3kgnm6b9eq948n45oig4b2b3grp34if.apps.googleusercontent.com";
                options.ClientSecret = "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";
            });

            builder.Services.AddAuthentication().AddDiscord(options =>
            {
                options.ClientId = "1282621682812588088";
                options.ClientSecret = "p-OGCS3kWd5ngGbKw0gMtlHlo8FAyU9N";
                options.CallbackPath = "/signin-discord";
            });

            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<WeirdBlogDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
            }).AddEntityFrameworkStores<WeirdBlogDbContext>().AddDefaultTokenProviders();
            //builder.Services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<WeirdBlogDbContext>().AddDefaultTokenProviders();
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

                options.Lockout.MaxFailedAccessAttempts = 8;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

    }
}
