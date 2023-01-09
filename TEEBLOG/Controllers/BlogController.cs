using Microsoft.AspNetCore.Mvc;

namespace TEEBLOG.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Blog()
        {
            return View();
        }
    }
}
