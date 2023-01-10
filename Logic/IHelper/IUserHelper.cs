using Core.Models;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.IHelper
{
	public interface IUserHelper
	{
        ApplicationUser FindUserByEmail(string email);
        Task<bool> CreateUser(RegisterViewModel data);
        Task<UserVerification?> CreateUserToken(string userEmail);
        bool VerifyUser(Guid token);

    }
}
