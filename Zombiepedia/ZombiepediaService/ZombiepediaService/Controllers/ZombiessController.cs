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

namespace ZombiepediaService.Controllers
{
	public class ZombiesController : ApiController
	{

		private ZombieInfo[] zombieData = new ZombieInfo[]
		{
			new ZombieInfo()
			{
				Id = 1,
				Name = "Crawler",
				Decsription =
					"Unable to walk, either due to missing legs or damaged nerves, and must therefore crawl everywhere. Beware of these and never assume a motionless zombie on the floor is dead."
			},
			new ZombieInfo() {Name = "Walker", Decsription = "Your standard slow moving zombie. Moves in a jerky shuffle."},
			new ZombieInfo()
			{
				Id = 2,
				Name = "Runner",
				Decsription =
					"Fast moving and tireless. If you encounter a runner, do not try to outrun it as it will not fatigue and you will. Fortunately they are not very durable and are easy to kill."
			},
			new ZombieInfo()
			{
				Id = 3,
				Name = "Teacher",
				Decsription =
					"These zombies used to be school teachers and are the traditional brain hungry zombie. The high brain diet and former life as an educator has left them smarter than your average zombie, beware."
			},
			new ZombieInfo()
			{
				Id = 4,
				Name = "Teen",
				Decsription =
					"Zombified in the midst of adolecense these zombies are easily distracted by TV, smart phones, and any kind of game. Beware, though, they may immediately lose interest in the distraction at any time as their hunger takes over."
			},
		};

		// GET api/Zombies
		public IEnumerable<ZombieInfo> Get()
		{
			return new List<ZombieInfo>(zombieData);
		}

		// GET api/Zombies/5
		public ZombieInfo Get(int id)
		{
			return zombieData.FirstOrDefault(z => z.Id == id);
		}

		public HttpResponseMessage GetImage(int id)
		{

			var zombie = zombieData.FirstOrDefault(z => z.Id == id);
			if (zombie == null)
			{
				return new HttpResponseMessage(HttpStatusCode.NotFound);
			}

			var result = new HttpResponseMessage(HttpStatusCode.OK);
			var filePath = HostingEnvironment.MapPath(string.Format("~/Images/{0}.jpg", zombie.Name));
			var fileStream = new FileStream(filePath, FileMode.Open);
			var image = Image.FromStream(fileStream);
			var memoryStream = new MemoryStream();
			image.Save(memoryStream, ImageFormat.Jpeg);
			result.Content = new ByteArrayContent(memoryStream.ToArray());
			result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

			return result;


		}

	}
}
