using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ZombiepediaApp.Models;

namespace ZombiepediaApp.Services
{
    public static class ZombieDataService
    {
        public static async Task<List<Zombie>> GetZombies()
        {
            var service = new HttpClient
            {
                BaseAddress = new Uri("http://zombiepedia.azurewebsites.net")
            };
            var response = await service.GetAsync("api/zombie");
            var data = await response.Content.ReadAsStringAsync();
            var zombies = JsonConvert.DeserializeObject<List<Zombie>>(data);
            return zombies;
        }

        public static async Task<List<string>> GetComments(int id)
        {
            var service = new HttpClient
            {
                BaseAddress = new Uri("http://zombiepedia.azurewebsites.net")
            };
            var response = await service.GetAsync($"api/comment/{id}");
            var data = await response.Content.ReadAsStringAsync();
            var comments = JsonConvert.DeserializeObject<List<string>>(data);
            return comments;
        }
        public static async Task<HttpResponseMessage> AddComment(int zombieId, string comment)
        {
            var service = new HttpClient
            {
                BaseAddress = new Uri("http://zombiepedia.azurewebsites.net")
            };
	        //var values = String.Format("{0}|{1}", zombieId.ToString(), comment);

			//var content = new StringContent(values);
			//var json = String.Format("{ \"ZombieId\":{0}; \"Comment\":\"{1}\"", zombieId, comment);
			//var content = new StringContent();
			//content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
	        var encodedComment = WebUtility.UrlEncode(comment);

			try
			{
				var response = await service.GetAsync(String.Format("api/addcomment?ZombieId={0}&Comment={1}", zombieId, encodedComment));
				return response;

			}
			catch (Exception ex)
			{
				return null;
			}

		}
    }
}
