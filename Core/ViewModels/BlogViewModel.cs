using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
	public class BlogViewModel
	{
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category? Categories { get; set; }
        public string? Picture { get; set; }
		public string? ShortDescription { get; set; }
		public string? Description { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DateCreated { get; set; }
        public List<Review>? Reviews { get; set; }
    }
}
