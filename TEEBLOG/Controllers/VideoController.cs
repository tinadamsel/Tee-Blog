using Microsoft.AspNetCore.Mvc;

namespace TEEBLOG.Controllers
{
    public class VideoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Video()
        {
            return View();
        }
    }
}
