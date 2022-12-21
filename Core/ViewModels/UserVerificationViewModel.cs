using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
	public class UserVerificationViewModel
	{
		[Key]
		public Guid? Token { get; set; }
		public string? UserId { get; set; }
		public bool Used { get; set; }
		public DateTime? DateUsed { get; set; }

		[ForeignKey("UserId")]
		public virtual ApplicationUser? User { get; set; }
	}
}
