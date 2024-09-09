using Microsoft.EntityFrameworkCore;
using WeirdBlog.DataAccess.Data;
using WeirdBlog.Service;
using Microsoft.AspNetCore.Identity;
using WeirdBlog.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using AspNet.Security.OAuth.Discord;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Globalization;
using Microsoft.AspNetCore.Authentication.Google;


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
            builder.Services.AddScoped<IEmailSender, EmailSender>();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
                options.LoginPath = "/Identity/Account/AccessDenied";
            });
            builder.Services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = "111753696304-s3kgnm6b9eq948n45oig4b2b3grp34if.apps.googleusercontent.com";
                options.ClientSecret = "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";
            });

            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<WeirdBlogDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            
            builder.Services.AddIdentity<IdentityUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<WeirdBlogDbContext>().AddDefaultTokenProviders();
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
