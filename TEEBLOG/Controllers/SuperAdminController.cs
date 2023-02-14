using Core.DbContext;
using Core.Models;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using static Core.DbContext.TeeEnums;

namespace TEEBLOG.Controllers
{ 
    public class SuperAdminController : Controller
    {
        private readonly AppDBContext _context;

        public SuperAdminController(AppDBContext context) 
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult SuperAdminBlogPage()
        {

            var blogs = _context.Blogs.Include(c => c.Categories).ToList();
            var data = new BlogDto()
            {
                Blogs = blogs,
            };
            return View(data);
        }

        [HttpGet]
        public IActionResult ShowBlogPage(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var blog = _context.Blogs.Find(id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);

        }
        public IActionResult Accept(int? id) 
        {
            if (id > 0)
            {
                var blog = _context.Blogs.Where(x => x.Id == id && !x.Deleted).FirstOrDefault();
                if (blog != null)
                {
                    blog.Active = true;
                    blog.BlogStatus = blogEnum.Approve;
                    _context.Blogs.Update(blog);
                    _context.SaveChanges();
                    //blog.Approved = "Blog Post Approved";
                    TempData["Message"] = "Blog Post Approved";
                }
                
            }
             return RedirectToAction("SuperAdminBlogPage", "SuperAdmin");
        }
        public IActionResult Decline(int? id) 
        {
            if (id > 0)
            {
                var blog = _context.Blogs.Where(x => x.Id == id && !x.Deleted).FirstOrDefault();
                if (blog != null)
                {
                    blog.Active = false;
                    blog.BlogStatus = blogEnum.Decline;
                    _context.Blogs.Remove(blog);
                    _context.SaveChanges();
                    //blog.Rejected = "Blog Post Rejected";
                }
                TempData["Message"] = "Blog Post Declined";
            }
            return RedirectToAction("SuperAdminBlogPage", "SuperAdmin");
        }

        public IActionResult ApprovedPosts()
        {
            IEnumerable<Blog> blogs = _context.Blogs.Where(x => x.BlogStatus == blogEnum.Approve || x.BlogStatus == blogEnum.Decline && !x.Deleted).Include(c => c.Categories).ToList();
            return View(blogs);
        }

    }
}

