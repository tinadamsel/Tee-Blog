using Core.Models;
using Core.ViewModels;
using Logic.Helper;
using Logic.IHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TEEBLOG.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailHelper emailHelper;
        private readonly IEmailConfiguration emailConfiguration;

        public AccountController(IUserHelper userHelper, SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager, IEmailHelper emailHelper, IEmailConfiguration emailConfiguration)
        {
            _userHelper = userHelper;
            _signInManager = signInManager;
            _userManager = userManager;
            this.emailHelper = emailHelper;
            this.emailConfiguration = emailConfiguration;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string allDetail)
        {
            if (allDetail != null)//conditional statement for checks
            {
                var person = JsonConvert.DeserializeObject<RegisterViewModel>(allDetail);

                if (person != null)
                {
                    var result = await _userHelper.CreateUser(person);

                    if (result)
                    {
                        var userToken = await _userHelper.CreateUserToken(person.Email);
                        if (userToken != null)
                        {
                            string linkToClick = HttpContext.Request.Scheme.ToString() + "://" + HttpContext.Request.Host.ToString()
                            + "/Account/UserVerification/" + userToken.Token;
                            var user = _userHelper.FindUserByEmail(person.Email);
                            await _userManager.AddToRoleAsync(user, "Admin");
                            var sendEmail = emailHelper.SendVerificationEmail(user, linkToClick);

                            return Json(new { isError = false, message = "Registration successful. Login to your email to verify account.", url = "/Account/login" });
                        }
                        else
                            return Json(new { isError = true, message = "Your Registration was not successful" });
                    }
                    return Json(new { isError = true, message = "Your Registration was not successful" });
                }
            }
            return Json(new { isError = true, message = "Please fill the form correctly" });
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string detail)
        {
            if (detail != null)
            {
                var data = JsonConvert.DeserializeObject<LoginViewModel>(detail);

                if (data != null)
                {
                    var user = _userHelper.FindUserByEmail(data.Email);
                    if (user != null)
                    {
                        if (user.EmailConfirmed)
                        {
                            var isLoggIn = await _signInManager.PasswordSignInAsync(user, data.Password, true, true);
                            if (isLoggIn.Succeeded)
                            {
                                var role = _userManager.GetRolesAsync(user).Result;
                                if (role.Count > 0)
                                {
                                    if (role.FirstOrDefault().Contains("Admin"))
                                    {
                                        return Json(new { isError = false, url = "/Admin/Dashboard" });
                                    }
                                    return Json(new { isError = false, url = "/Base/Index" });
                                }
                            }
                        }
                        return Json(new { isError = true, message = "Please proceed to your email to verify your account." });
                    }
                    return Json(new { isError = true, message = "Email or password is incorrect" });
                }

            }

            return Json(new { isError = true, message = "Please kindly register to continue" });

            //return Json(new { isError = true, msg = "Please fill the form correctly"});
        }

        [HttpGet]
        public IActionResult UserVerification(string id)
        {
            if (id != null)
            {
                var guidId = Guid.Parse(id);
                var data = _userHelper.VerifyUser(guidId);
                return View();
            }
            return Json(new { msg = "Verification Failed" });

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}

