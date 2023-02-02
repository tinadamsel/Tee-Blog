using Core.DbContext;
using Core.Models;
using Core.ViewModels;
using Logic.IHelper;
using Logic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Core.DbContext.TeeEnums;

namespace Logic.Helper
{
    public class UserHelper : IUserHelper
    {
        private readonly AppDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailHelper emailHelper;
        private readonly IEmailService emailService;
        private readonly IEmailConfiguration emailConfiguration;


        public UserHelper(UserManager<ApplicationUser> userManager, AppDBContext context,
            IEmailHelper emailHelper, IEmailService emailService, IEmailConfiguration emailConfiguration)
        {
            _userManager = userManager;
            _context = context;
            this.emailHelper = emailHelper;
            this.emailService = emailService;
            this.emailConfiguration = emailConfiguration;
        }

        public ApplicationUser FindUserByEmail(string email) 
        {
            //query
         var user = _userManager.Users.Where(x => x.Email == email && !x.IsDeactivated)?.FirstOrDefault();
            return user;
        }

        public async Task<bool> CreateUser(RegisterViewModel data)
        {
            
            var user = new ApplicationUser() //instanciating or initialize
            {
               
                Email = data.Email,
                FirstName = data.FirstName,
                LastName = data.LastName,
                UserName = data.Email,
                DateRegistered = DateTime.Now,
            }; 
            var result = await _userManager.CreateAsync(user, data.Password);
            if (result.Succeeded)
                return true;
            return false;
        }

        public async Task<UserVerification?> CreateUserToken(string userEmail)
        {
            var user = _context.ApplicationUsers.Where(x => x.Email == userEmail && !x.IsDeactivated).FirstOrDefault();
            if (user != null)
            {
                var userVerification = new UserVerification()
                {
                    UserId = user.Id,
                };
                await _context.AddAsync(userVerification);
                await _context.SaveChangesAsync();
                return userVerification;
            }
            return null;
        }
        //public async Task<bool> MarkTokenAsUsed(UserVerification userVerification)
        //{
        //    var VerifiedUser = _context.verifications.Where(s => s.UserId == userVerification.User.Id && s.Used != true).FirstOrDefault();
        //    if (VerifiedUser != null)
        //    {
        //        userVerification.Used = true;
        //        userVerification.DateUsed = DateTime.Now;
        //        _context.Update(userVerification);
        //        await _context.SaveChangesAsync();
        //        return true;
        //    }
        //    return false;
        //}
        public bool VerifyUser(Guid token)
        {
            var verify = _context.verifications.Where(x => x.Token == token && !x.Used).Include(c => c.User).FirstOrDefault();
            if (verify != null)
            {
                verify.Used = true;
                verify.DateUsed = DateTime.Now;
                verify.User.EmailConfirmed = true;
                _context.Update(verify);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
         
        public bool CreateBlog(Blog blog)
        { 
            if (blog != null)
            {
                var newBlog = new Blog() //instanciating or initialize
                {
                    Name = blog.Name,
                    DateCreated = DateTime.Now,
                    ShortDescription = blog.ShortDescription,
                    Description = blog.Description,
                    Picture = blog.Picture,
                    CategoryId = blog.CategoryId,
                    Categories = blog.Categories,
                };
                var result = _context.Add(newBlog);
                    _context.SaveChanges();
                if (result != null)
                {
                    return true;
                }

            }
          return false;
        }

        public bool CreateContact(SupportViewModel support)
        {
            if (support != null)
            {
                var isExisting = GetExistingCustomer(support.Email);
                if (isExisting != null)
                {
                    var newSupport = new Support()
                    {
                        Date = DateTime.Now,
                        Subject = support.Subject,
                        Message = support.Message,
                        CustomerId = isExisting.Id,
                    };
                    _context.Add(newSupport);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    var customer = new Customer()
                    {
                        Name = support.Name,
                        Email = support.Email,
                    };

                    var customerDetail = CreateCustomer(customer);
                    if (customerDetail != null)
                    {
                        var newSupport = new Support()
                        {
                            Date = DateTime.Now,
                            Subject = support.Subject,
                            Message = support.Message,
                            CustomerId = customerDetail.Id,
                        };
                        _context.Add(newSupport);
                        _context.SaveChanges();
                        return true;
                    }
                }
            }
            return false;
        }

        //public bool SendAdminEmail(SupportViewModel )
        //{
        //    if (Admin != null)
        //    {
        //        string toEmail = Admin.Email;
        //        string subject = "From Tee Blog";

        //        var message = "Dear " + Admin.FirstName + "," + "</br> " +
        //         "This is to notify you, that you have a message from Tee Blog." +
        //          "Tee Group.";

        //        var isSent = emailService.SendEmail(toEmail, subject, message);
        //        if (isSent)
        //        {
        //            return true;
        //        }


        //    }
        //    return false;
        //}

        public Customer CreateCustomer(Customer data) 
        {
            if ( !String.IsNullOrEmpty(data.Name) && !String.IsNullOrEmpty(data.Email))
            {
                data.DateCreated = DateTime.Now;
                data.Active = true;
                data.Deleted = false;
                _context.Add(data); 
                _context.SaveChanges();
                return data;
            }
            return null;
        }

        public Review CreateReview(ReviewViewModel review)
        {
            if (review != null)
            {
                var person = GetExistingCustomer(review.Email);
                if (person != null)
                    {
                    var newReview = new Review()
                    {
                        Date = DateTime.Now,
                        Message = review.Message,
                        CustomerId = person.Id,
                        BlogId = review.BlogId, 
                    };
                    _context.Add(newReview);
                    _context.SaveChanges();
                    return newReview;
                }
                else
                {
                    var customer = new Customer()
                    {
                        Name = review.Name,
                        Email = review.Email, 
                    };

                    var customerDetail = CreateCustomer(customer);
                    if (customerDetail != null)
                    {
                        var newReview = new Review()
                        {
                            Date = DateTime.Now,
                            Message = review.Message,
                            CustomerId = customerDetail.Id,
                            BlogId = review.BlogId,
                        };
                        _context.Add(newReview);
                        _context.SaveChanges();
                        return newReview;
                    }
                } return null;

            }
            return null;
        }
        public Customer GetExistingCustomer(string email)
        {
            if (email != null)
            {
                var cust = _context.Customers.Where(x => x.Email == email && !x.Deleted).FirstOrDefault();
                if (cust != null)
                {
                    return cust;
                }
            }
            return null;
        }

        public Category GetExistingCategory(string name)
        {
            if (name != null)
            {
                var categoryExist = _context.Categories.Where(x => x.Name == name && x.Active && !x.Deleted).FirstOrDefault();
                if (categoryExist != null)
                {
                    return categoryExist;
                }
            }
            return null;
        }

        public string GetUserRole(string userId)
        {
            if (userId != null)
            {
                var userRole = _context.UserRoles.Where(x => x.UserId == userId).FirstOrDefault();
                if (userRole != null)
                {
                    var roles = _context.Roles.Where(x => x.Id == userRole.RoleId).FirstOrDefault();
                    if (roles != null)
                    {
                        return roles.Name;
                    }
                }
            }
            return null;
        }

        //public async Task<string> GetUserLayout(string userId)
        //{
        //    if (userId != null)
        //    {
        //        var returnUrl = "";
        //        var User = await FindByIdAsync(userId).ConfigureAwait(false);
        //        var roles = await _userManager.GetRolesAsync(User).ConfigureAwait(false);
        //        foreach (var userRole in roles)
        //        {
        //            switch (userRole)
        //            {
        //                case "SuperAdmin":
        //                    returnUrl = "/SuperAdmin/SuperAdminBlogPage";
        //                    break;
        //                case "Admin":
        //                    returnUrl = "/Admin/Dashboard";
        //                    break;
        //                default:
        //                    break;
        //            }
        //        }
        //        return returnUrl;
        //    }
        //    return null;

        //}



        public List<Review> GetListOfReviews(int blogId)
        {
            var reviews = new List<Review>();
            if (blogId > 0)
            {
                var result = _context.Reviews.Where(x => x.BlogId == blogId && x.isApproved).Include(s => s.Blogs).Include(a => a.Customers).ToList();
                if (result.Any())
                {
                    reviews = result;
                    return reviews;
                }
            }
            return reviews;
        }

        public string UploadedFile(IFormFile image, string webRootPath)
        {
            string uniqueFileName = string.Empty;
            string base64ImageRepresentation = string.Empty;
            if (image != null)
            {
                string uploadsFolder = Path.Combine(webRootPath, "uploads");
                string pathString = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(pathString))
                {
                    Directory.CreateDirectory(pathString);
                }
                uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
                byte[] imageArray = File.ReadAllBytes(filePath);
                base64ImageRepresentation = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(imageArray));
            }
            return base64ImageRepresentation;
        }

    }
}
