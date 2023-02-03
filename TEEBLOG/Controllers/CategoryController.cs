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

        [HttpGet]
        public IActionResult BlogByCategory(int categoryId)
        {
            var listOfCategory = new List<Blog>();
            if (categoryId != 0)
            {
                var blogs = _context.Blogs.Where(c => c.Id != 0 && c.CategoryId == categoryId && c.Active).Include(x => x.Categories).ToList();
                if (blogs != null && blogs.Count > 0)
                {
                    return View(blogs);
                }
            }
            return View(listOfCategory);
        }
    }
}
