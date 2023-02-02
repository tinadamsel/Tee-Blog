using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<Blog> blogs { get; set; }
        public int Totalblogs { get { return blogs.Count(); } }
        public IEnumerable<Category> categories { get; set; }
        public int TotalCategories { get { return categories.Count(); } }
        public IEnumerable<SupportViewModel> supports { get; set; }
        public int TotalSupports { get { return supports.Count(); } }



    }
}
