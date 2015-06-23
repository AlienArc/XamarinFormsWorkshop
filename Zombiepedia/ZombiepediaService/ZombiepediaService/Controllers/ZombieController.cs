using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Hosting;
using System.Web.Http;
using ZombiepediaService.Models;
using ZombiepediaService.Services;

namespace ZombiepediaService.Controllers
{
	public class ZombieController : ApiController
	{
		// GET api/Zombies
		public IEnumerable<ZombieInfo> Get()
		{
			return new List<ZombieInfo>(ZombieDataService.GetZombies());
		}

		// GET api/Zombies/5
		public ZombieInfo Get(int id)
		{
		    var zombieData = ZombieDataService.GetZombies();
			return zombieData.FirstOrDefault(z => z.Id == id);
		}
    }
}
