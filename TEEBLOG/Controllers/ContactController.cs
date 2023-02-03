using Core.DbContext;
using Core.ViewModels;
using Logic.IHelper;
using Logic.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;

namespace TEEBLOG.Controllers
{
    public class ContactController : Controller
    { 
        private readonly AppDBContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IEmailHelper emailHelper;
        private readonly IEmailService emailService;
        private readonly IEmailConfiguration emailConfiguration;


        public ContactController(AppDBContext context, IUserHelper userHelper, IEmailHelper emailHelper, IEmailConfiguration emailConfiguration, IEmailService emailService)
        {
            _context = context;
            _userHelper = userHelper;
            this.emailHelper = emailHelper;
            this.emailConfiguration = emailConfiguration;  
            this.emailService = emailService;
        }
        // adding a ViewModel 
        public IActionResult AdminContactPage()
        {
            var result = new List<SupportViewModel>();
            var mainSupport = _context.Supports.Where(x => x.Message != null).Include( c => c.Customers).ToList();
            if (mainSupport.Any())
            {
                result = mainSupport.Select(x => new SupportViewModel() 
                {
                    Id = x.Id,
                    Message = x.Message,
                    Subject = x.Subject,
                    CustomerId = x.CustomerId,
                    Date = x.Date, 
                    Customers= x.Customers,
                    Email = x.Customers?.Email,
                    Name = x.Customers?.Name,
                }).ToList();

                return View(result);
            }
            return View(result);
        }
        [HttpGet]

        public IActionResult Contact()
        {

            return View();
        }
        [HttpPost]
        public JsonResult AddSupport(string allSupport)
        {
            if (allSupport != null)
            {
                var data = JsonConvert.DeserializeObject<SupportViewModel>(allSupport);
                if (data != null)
                {
                    var result = _userHelper.CreateContact(data);
                    if (result)
                    {
                        var sendEmail = emailHelper.SendAdminEmail(data);

                        return Json(new { isError = false, message = "Your Message was successfully sent. Thank you for reaching out.", url = "/Contact/Contact"});
                    }
                    return Json(new { isError = true, message = "Your Message was not sent.", url = "/Contact/Contact" });
                }
                return Json(new { isError = true, message = "Your Message was not sent. Please try again", url = "/Contact/Contact" });
            }
            return Json(new { isError = true, message = "Please fill the form correctly", url = "/Contact/Contact" });
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var support = _context.Supports.Where(x => x.CustomerId == id).Include(a => a.Customers).FirstOrDefault();
            if (support != null)
            {
                var result = new SupportViewModel()
                {
                    Message = support.Message,
                    Subject = support.Subject,
                    CustomerId = support.CustomerId,
                    Date = support.Date,
                    Customers = support.Customers,
                    Email = support.Customers.Email,
                    Name = support.Customers.Name,
                };
                return View(result);
            }
            return RedirectToAction("Contact", "Contact");
        }

        [HttpPost]
        public IActionResult DeletePost(int? id)
        {
            var support = _context.Supports.Where(x => x.CustomerId == id).FirstOrDefault();
            if (support != null)
            {
               _context.Supports.Remove(support);
                _context.SaveChanges();
                return RedirectToAction("AdminContactPage");
            }
            return View(support);
        }

        public JsonResult GetMessage(int? id)
        {
            try
            {
                if (id > 0)
                {
                    var msg = _context.Supports.Where(x => x.CustomerId == id).FirstOrDefault();
                    if (msg != null)
                    {
                        return Json(new { isError = false, data = msg });
                    }
                }
                return Json(new { isError = true, message = "No Message found!" });
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
