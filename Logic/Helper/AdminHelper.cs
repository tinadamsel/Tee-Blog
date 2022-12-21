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
	public class AdminHelper : IAdminHelper
	{
		private readonly UserManager<ApplicationUser> _userManager;
		public AdminHelper(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}
		
	}
}
