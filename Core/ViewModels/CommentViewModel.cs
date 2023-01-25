using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class CommentViewModel
    {
        public BlogViewModel? Blog { get; set; }
        public List<ReviewViewModel>? Reviews{ get; set; }
    }
}
