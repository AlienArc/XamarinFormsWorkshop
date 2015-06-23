using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ZombiepediaService.Models
{
	public class ZombieInfo
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	    public string ImagePath { get; set; }
	    public List<string> Comments { get; set; }

	    public ZombieInfo()
	    {
	        Comments = new List<string>();
	    }
	}
}
