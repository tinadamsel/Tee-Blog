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
	public class BlogViewModel
	{
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide Name")]
        public string? Name { get; set; }
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category? Categories { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Add a Picture")]
        public string? Picture { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide Short Description")]
        public string? ShortDescription { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide Description")]
        public string? Description { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DateCreated { get; set; }
        public List<Review>? Reviews { get; set; }
    }
}
