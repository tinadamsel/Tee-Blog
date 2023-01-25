using Microsoft.AspNetCore.Mvc;

namespace TEEBLOG.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profile()
        
        {
            return View();
        }
        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public IActionResult EditProfile()
        {
            return View();
        }
        public IActionResult skill()
        {
            return View();
        }
    }
}
