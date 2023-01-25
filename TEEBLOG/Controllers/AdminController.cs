using Core.DbContext;
using Core.Models;
using Logic.IHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TEEBLOG.Controllers
{
    [Authorize(Roles ="Admin, SuperAdmin")]
	public class AdminController : Controller
    { 
        private readonly AppDBContext _context;
        private readonly IUserHelper _userHelper;
        public AdminController(AppDBContext context, IUserHelper userHelper) 
        {
            _context = context;
            _userHelper = userHelper;  
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
        public IActionResult AddCategory(Category category)
        {
            var isExisting = _userHelper.GetExistingCategory(category?.Name);
            if (isExisting != null)
            {
                isExisting.Error = "Name already Exist";
                return View(isExisting);
            }
            if (category != null)
            {
                category.DateCreated = DateTime.Now;
                category.Active = true;
                _context.Categories.Add(category);
                _context.SaveChanges();
                category.Success = "Name successfully Added";
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
            return View(category);
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

        //public IActionResult ShowCategoryPage(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    var category = _context.Categories.Find(id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(category);

        //}
    }

}
