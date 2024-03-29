﻿using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
	public class ReviewViewModel
	{
        public int Id { get; set; }
        public string Name { get; set; }
		public string Email { get; set; }
		public int BlogId { get; set; }
		[ForeignKey("BlogId")]
		public virtual Blog? Blogs { get; set; }
		public int CustomerId { get; set; }
		[ForeignKey("CustomerId")]
		public virtual Customer? Customers { get; set; }
		public string? Message { get; set; }
		public DateTime Date { get; set; }
        public string Error { get; set; }

        public string Success { get; set; }
    }
}
