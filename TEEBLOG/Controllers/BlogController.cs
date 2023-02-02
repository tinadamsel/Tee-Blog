using Core.DbContext;
using Core.Models;
using Core.ViewModels;
using Logic.Helper;
using Logic.IHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace TEEBLOG.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IDropdownHelper _dropdownHelper; 
        private readonly IUserHelper _userHelper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BlogController(AppDBContext context, IDropdownHelper dropdownHelper, IUserHelper userHelper, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _dropdownHelper = dropdownHelper;
            _userHelper = userHelper;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Blog(int? name)
        {
            IEnumerable<Blog> blogs = _context.Blogs.Include(c => c.Categories).ToList();
            return View(blogs);
            
        }
        public IActionResult AdminBlogPage()
        {

            IEnumerable<Blog>blogs = _context.Blogs.Include(c => c.Categories).ToList();
            return View(blogs);
        }

        [HttpGet]
        public IActionResult AddBlogPost()
        {
            ViewBag.Categories = _dropdownHelper.DropdownOfCategories(); 
            return View();
        }

        [HttpPost]
        public IActionResult addblog(string allPosts) 
        {
            ViewBag.Categories = _dropdownHelper.DropdownOfCategories();

            if (allPosts != null)
            {
                var data = JsonConvert.DeserializeObject<Blog>(allPosts);
                
                if (data != null)
                {
                    var result = _userHelper.CreateBlog(data);
                    if (result)
                    {
                        return Json(new { isError = false, message = "Post successfully added.", url = "/Blog/AdminBlogPage" });
                    }

                    return Json(new { isError = true, message = "Post Not Successful", url = "/Blog/AddBlogPost" });
                }
                return Json(new { isError = true, message = "Post Not Successful", url = "/Blog/AddBlogPost" });
            }
            return Json(new { isError = true, message = "Post Not Successful", url = "/Admin/Dashboard"});
        }


        //public IActionResult NewBlogPost(Blog blog)
        //{
        //    
        //    if (blog != null)
        //    {
        //        _context.Blogs.Add(blog);
        //        _context.SaveChanges(); 
        //        return RedirectToAction("AdminBlogPage");
        //    }
        //    return View(blog);
        //}

        [HttpGet]
        
        public IActionResult Edit(int? id)
        {
            ViewBag.Categories = _dropdownHelper.DropdownOfCategories();
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var blog = _context.Blogs.Find(id);
            if (blog != null)
            {
                return View(blog);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Blog blog)
        {
            ViewBag.Categories = _dropdownHelper.DropdownOfCategories();
            if (blog != null)
            {
                var webRoot = _webHostEnvironment.WebRootPath;
                var images = _userHelper.UploadedFile(blog.Image,webRoot);
                blog.DateCreated = DateTime.Now;
                blog.Name = blog.Name;
                blog.ShortDescription = blog.ShortDescription;
                blog.Description = blog.Description;
                blog.Picture = images;
                blog.CategoryId = blog.CategoryId;
               
                _context.Blogs.Update(blog);
                _context.SaveChanges();
                return RedirectToAction("AdminBlogPage");

            }
           
            return View(blog);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            ViewBag.Categories = _dropdownHelper.DropdownOfCategories();
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

        [HttpPost]
        public IActionResult DeletePost(int? id)
        {
            ViewBag.Categories = _dropdownHelper.DropdownOfCategories();
            var blog = _context.Blogs.Find(id);
            if (blog != null)
            {
                _context.Blogs.Remove(blog);
                _context.SaveChanges();
                return RedirectToAction("AdminBlogPage");

            }
            return View(blog);
        }

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
        //[HttpGet]
        //public IActionResult Description(int? id)
        //{
        //    if (id > 0)
        //    {
        //        var description = _context.Blogs.Where(x => x.Id == id && !x.Deleted && x.Active).FirstOrDefault();
        //        if (description != null)
        //        {
        //            return View(description);
        //        }
        //    }
        //    return NotFound();
        //}

        [HttpGet]
        public JsonResult GetDescription(int? id)
        {
            try
            {
                if (id > 0)
                {
                    var description = _context.Blogs.Where(x => x.Id == id && !x.Deleted && x.Active).FirstOrDefault();
                    if (description != null)
                    {
                        return Json(new { isError = false, data = description });
                    }
                }
                return Json(new { isError = true, message = "No description found!" });
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IActionResult Show(int? name)
        {
            if (name == null || name == 0)
            {
                return RedirectToAction("Category", "Category");
            }
            var blog = _context.Blogs.Find(name);
            if (blog != null)
            {
                var model = new BlogViewModel()
                {
                    Id = blog.Id,
                    Name = blog.Name,
                    CategoryId = blog.CategoryId,
                    Picture = blog.Picture,
                    ShortDescription = blog.ShortDescription,
                    Description = blog.Description,
                    DateCreated = blog.DateCreated,
                    Reviews = _userHelper.GetListOfReviews(blog.Id)
                };
                return View(model);
            }
            return RedirectToAction("Category", "Category");
        }

    }


    



}

