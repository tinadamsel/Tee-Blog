using Microsoft.AspNetCore.Mvc;

namespace TEEBLOG.Controllers
{
    public class FAQsController : Controller
    {
        public IActionResult FAQs()
        {
            return View();
        }
    }
}
