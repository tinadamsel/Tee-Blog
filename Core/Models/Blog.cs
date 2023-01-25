using Core.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static Core.DbContext.TeeEnums;

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
        public blogEnum BlogStatus { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        [NotMapped]
        public string Approved { get; set; }

        [NotMapped]
        public string Rejected { get; set; }
    } 
}

