using Core.Models;
using Core.ViewModels;
using Microsoft.AspNetCore.Http;
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
        bool CreateBlog(Blog blog);
        bool CreateContact(SupportViewModel support);
        Customer CreateCustomer(Customer data);
        //bool SendAdminEmail(ApplicationUser Admin);
        Review CreateReview(ReviewViewModel review);
        Customer GetExistingCustomer(string email);
        string GetUserRole(string userId);
        List<Review> GetListOfReviews(int blogId);
        public Category GetExistingCategory(string name);
        string UploadedFile(IFormFile image, string webRootPath);
    }
}
