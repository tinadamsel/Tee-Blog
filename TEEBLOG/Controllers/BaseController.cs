using Microsoft.AspNetCore.Mvc;

namespace TEEBLOG.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
		public IActionResult About()
		{
			return View();
		}
	}
}
