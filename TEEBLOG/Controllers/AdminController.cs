using Core.DbContext;
using Core.Models;
using Core.ViewModels;
using Logic.IHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection.Metadata;

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
            var cate = _context.Categories.ToList();
            var blog = _context.Blogs.Include(a => a.Categories).ToList();
            
            var result = new List<SupportViewModel>();
            var mainSupport = _context.Supports.Where(x => x.Message != null).Include(c => c.Customers).ToList();
            if (mainSupport.Any())
            {
                result = mainSupport.Select(x => new SupportViewModel()
                {
                    Message = x.Message,
                    Subject = x.Subject,
                    CustomerId = x.CustomerId,
                    Date = x.Date,
                    Customers = x.Customers,
                    Email = x.Customers.Email,
                    Name = x.Customers.Name,
                }).ToList();
            }   
           
            var dashboard = new DashboardViewModel {
                blogs = blog,
                categories = cate,
                
                supports = result,
            };
            
            return View(dashboard);
            //return View();
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
            var category = _context.Categories.Where(x => x.Id == id).FirstOrDefault();
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
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
                _context.Categories.Update(category);
                _context.SaveChanges();
                category.Success = "Name successfully Edited";
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
			var category = _context.Categories.Where(x => x.Id == id).FirstOrDefault();
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
                return RedirectToAction("Category");

            }
            return View(category);
        }
    }

}
