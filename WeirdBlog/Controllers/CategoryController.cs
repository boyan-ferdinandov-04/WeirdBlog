using Microsoft.AspNetCore.Mvc;

namespace WeirdBlog.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
