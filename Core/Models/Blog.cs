using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
	public class Blog : Base
	{	
		public string? Picture { get; set; }
		public string? ShortDescription { get; set; }
		public string? Description { get; set; }
	}
}
