using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
	public class Review 
	{
		public int Id { get; set; }
		public int BlogId { get; set; }
		[ForeignKey("BlogId")]
		public virtual Blog? Blogs { get; set; }
		public int CustomerId { get; set; }
		[ForeignKey("CustomerId")]
		public virtual Customer? Customers { get; set; }
		public string? Message { get; set; }
		public DateTime Date { get; set; }
		public bool isApproved { get; set; }

        [NotMapped]
        public string Error { get; set; }

        [NotMapped]
        public string Success { get; set; }

    }
}
