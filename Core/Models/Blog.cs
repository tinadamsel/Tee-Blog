using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
	public class Blog : Base
	{
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category? Categories { get; set; }
        public string? Picture { get; set; }
		public string? ShortDescription { get; set; }
		public string? Description { get; set; }
	}
}
