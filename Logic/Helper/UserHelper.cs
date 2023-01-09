using Core.DbContext;
using Core.Models;
using Core.ViewModels;
using Logic.IHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Helper
{
    public class UserHelper : IUserHelper
    {
        private readonly AppDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserHelper(UserManager<ApplicationUser> userManager, AppDBContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public ApplicationUser FindUserByEmail(string email)
        {
            //query
            return _userManager.Users.Where(x => x.Email == email && !x.IsDeactivated)?.FirstOrDefault();
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
    }
}
