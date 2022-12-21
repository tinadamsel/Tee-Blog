using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DbContext
{
	public class AppDBContext : IdentityDbContext 
	{
		public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
		{

		}

		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		public DbSet<Blog> Blogs { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<Support> Supports { get; set; }
		public DbSet<Video> Videos { get; set; }
		public DbSet<Audio> Audios { get; set; }
		public DbSet<UserVerification> verifications { get; set; }



	}
	
}
