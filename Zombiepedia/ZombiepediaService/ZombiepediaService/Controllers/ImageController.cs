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
using ZombiepediaService.Services;

namespace ZombiepediaService.Controllers
{
    public class ImageController : ApiController
    {
        public HttpResponseMessage GetImage(int id)
        {
            var zombieData = ZombieDataService.GetZombies();
            var zombie = zombieData.FirstOrDefault(z => z.Id == id);
            if (zombie == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            var filePath = HostingEnvironment.MapPath($"~/Images/{zombie.Id}.jpg");
            if (!File.Exists(filePath))
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            var memoryStream = new MemoryStream();
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var image = Image.FromStream(fileStream);
                image.Save(memoryStream, ImageFormat.Jpeg);
            }

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(memoryStream.ToArray());
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

            return result;
        }
    }
}
