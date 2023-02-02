using Core.DbContext;
using Core.Models;
using Logic.Helper;
using Logic.IHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TEEBLOG.Controllers
{
    public class BaseController : Controller 
    {
        private readonly AppDBContext _context;
        private readonly IUserHelper _userHelper; 

        public BaseController(AppDBContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }
        public IActionResult Index() 
        
        {
            var blogs = new List<Blog>();
            blogs = _context.Blogs.Where(x => x.Active && !x.Deleted).Include(c => c.Categories).ToList();
            if (blogs.Any())
            {
                return View(blogs);
            }
            else
            {
                return View(blogs);
            }
        }
		public IActionResult About()
		{
			return View();
		}
	}
}
