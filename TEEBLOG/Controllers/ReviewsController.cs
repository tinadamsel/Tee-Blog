using Core.DbContext;
using Core.Models;
using Core.ViewModels;
using Logic.Helper;
using Logic.IHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using System.Xml.Linq;
using static Core.DbContext.TeeEnums;

namespace TEEBLOG.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IUserHelper _userHelper; 

        public ReviewsController(AppDBContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }
        public IActionResult AdminReviews()
        {
            var result = new List<ReviewViewModel>();
            var mainReview = _context.Reviews.Where(x => x.Message != null).Include(c => c.Customers).Include(x => x.Blogs).ToList();
            if (mainReview.Any())
            {
                result = mainReview.Select(x => new ReviewViewModel()
                {
                    Id = x.Id,
                    Message = x.Message,
                    CustomerId = x.CustomerId,
                    BlogId = x.BlogId,
                    Date = x.Date,
                    Customers = x.Customers,
                    Blogs= x.Blogs,
                    Email = x.Customers.Email,
                    Name = x.Customers.Name,
                }).ToList();

                return View(result);
            }
            else
            {
                return null;
            }
        }

        public IActionResult AddReview(string allReview)
        {
            if (allReview != null)
            {
                var data = JsonConvert.DeserializeObject<ReviewViewModel>(allReview);

                if (data != null)
                {
                    var result = _userHelper.CreateReview(data);
                    if (result != null)
                    {
                        return Json(new { isError = false, message = "Review Successfully Added. Thank you!", url = "/Blog/Show?name=" + data?.BlogId });
                    }

                    return Json(new { isError = true, message = "Review Not Successful" });
                }
                return Json(new { isError = true, message = "Review Not Successful" });
            }
            return Json(new { isError = true, message = "Error Occured While Adding Review" });

        }
        

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var review = _context.Reviews.Where(x => x.BlogId == id).Include(a => a.Blogs).Include(b => b.Customers).FirstOrDefault();
            if (review != null)
            {
                var result = new ReviewViewModel()
                {
                    Message = review.Message,
                    CustomerId = review.CustomerId,
                    BlogId = review.BlogId,
                    Date = review.Date,
                    Customers = review.Customers,
                    Blogs = review.Blogs,
                    Email = review.Customers.Email,
                    Name = review.Customers.Name,
                };
                return View(result);
            }
            return RedirectToAction("Reviews", "AdminReviewsPage");
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int? id)
        {
            var review = _context.Reviews.Where(x => x.BlogId == id).FirstOrDefault();
            if (review != null)
            {
                _context.Reviews.Remove(review);
                _context.SaveChanges();
                return RedirectToAction("AdminReviews");

            }
            return View(review);
        }

        public IActionResult Accept(int? id)
        {
            if (id > 0)
            {
                var review = _context.Reviews.Where(x => x.Id == id && !x.Blogs.Deleted).FirstOrDefault();
                if (review != null)
                {
                    review.isApproved = true;
                    _context.Reviews.Update(review);
                    _context.SaveChanges();

                    TempData["Message"] = "Review Approved";
                }
            }
            return RedirectToAction("AdminReviews","Reviews");
        }

        public IActionResult Decline(int? id)
        {
            if (id > 0)
            {
                var review = _context.Reviews.Where(x => x.BlogId == id && !x.Blogs.Deleted).FirstOrDefault();
                if (review != null)
                {
                    review.isApproved = false;
                    _context.Reviews.Update(review);
                    _context.SaveChanges(); 
                    TempData["Message"] = "Review Declined";
                }
            }
            return RedirectToAction("AdminReviews", "Reviews");
        }
    }
}
