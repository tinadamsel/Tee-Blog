using Core.Models;
using Logic.IHelper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Helper
{
	public class UserHelper : IUserHelper
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public UserHelper(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}

		public ApplicationUser FindUserByEmail(string email)
		{
			return _userManager.Users.Where(x => x.Email == email && !x.IsDeactivated)?.FirstOrDefault();
		}
	}
}
