using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
	public class SupportViewModel
	{
		public int Id { get; set; }
		[Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide First Name")] 
		public string Name { get; set; }
		[Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please Provide Valid Email")]
        public string Email { get; set; }
		[Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide Subject")]
		public string? Subject { get; set; }
		[Required(AllowEmptyStrings = false, ErrorMessage = "Please Write Message")]
		public string? Message { get; set; }
		public DateTime Date { get; set; }
		public int CustomerId { get; set; }
		[ForeignKey("CustomerId")]
		public virtual Customer? Customers { get; set; }

    }
}
