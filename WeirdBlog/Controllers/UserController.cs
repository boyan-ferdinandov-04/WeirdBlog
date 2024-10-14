using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeirdBlog.DataAccess.Data;

namespace WeirdBlog.Controllers
{
    public class UserController : Controller
    {
        private readonly WeirdBlogDbContext _context;

        public UserController(WeirdBlogDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Details(Guid userId)
        {
            var user = await _context.Users
                .Include(u => u.Posts)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}
