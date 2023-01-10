using Core.DbContext;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace TEEBLOG.Controllers
{
	public class AdminController : Controller
    { 
        private readonly AppDBContext _context;
        public AdminController(AppDBContext context)
        {
            _context = context;
        }

		public IActionResult Dashboard()
		{
			return View();
		}

        [HttpGet]
        public IActionResult Category()
        {
            IEnumerable<Category>categories = _context.Categories.ToList();
            return View(categories);
        }

        //for Category
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNewCategory(Category category)
        {
            if (category != null)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction("Category");

            }
            return View(category);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (category != null)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
                return RedirectToAction("Category");

            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult DeletePost(int? id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
                return RedirectToAction("Category");

            }
            return View(category);
        }

        //for Blog Post
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
