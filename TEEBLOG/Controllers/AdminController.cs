using Microsoft.AspNetCore.Mvc;

namespace TEEBLOG.Controllers
{
	public class AdminController : Controller
	{
		public IActionResult Dashboard()
		{
			return View();
		}

		public IActionResult BlogPost()
		{
			return View();
		}

        public IActionResult AddBlogPost()
        {
            return View();
        }
    }
}
