using Core.Models;
using Core.ViewModels;
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

		public AccountController(IUserHelper userHelper, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
		{
			_userHelper = userHelper;
			_signInManager = signInManager;
			_userManager = userManager;
		}
        public IActionResult Register()
        {
            return View();
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
						var isLoggIn = await _signInManager.PasswordSignInAsync(user, data.Password, true,true);
						if (isLoggIn.Succeeded)
						{
							var role = _userManager.GetRolesAsync(user).Result;
							if (role.Count > 0) {
								//if (role.FirstOrDefault(x => x)
								//{

								//}
								//return
							}
							//return
						}
					}
				}
				
			}
			return Json(new { isError = true, msg = "Please fill the form correctly"});
		}
	}
}
