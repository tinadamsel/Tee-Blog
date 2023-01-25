using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public  class BlogDto
    {
        public string Approved { get; set; }
        public string Rejected { get; set; }
        public virtual List<Blog> Blogs { get; set; }
    }
}
