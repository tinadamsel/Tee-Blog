using Microsoft.AspNetCore.Mvc;

namespace TEEBLOG.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Category()
        {
            return View();
        }
    }
}
