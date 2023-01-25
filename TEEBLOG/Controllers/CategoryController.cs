using Core.DbContext;
using Core.Models;
using Logic.IHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TEEBLOG.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IUserHelper _userHelper;


        public CategoryController(AppDBContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Category()
        {
            IEnumerable<Blog> blogs = _context.Blogs.Include(c => c.Categories).ToList();
            return View(blogs);
        }
    }
}
