using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
	public class Category : Base
	{
		[NotMapped]
		public string Error { get; set; }

        [NotMapped]
        public string Success { get; set; }
    }
}
