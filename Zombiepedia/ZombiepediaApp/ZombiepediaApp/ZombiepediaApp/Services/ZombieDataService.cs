using System;
using System.Collections.Generic;
using System.Net.Http;
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
            var response = await service.GetAsync("api/zombies");
            var data = await response.Content.ReadAsStringAsync();
            var zombies = JsonConvert.DeserializeObject<List<Zombie>>(data);
            return zombies;
        }
    }
}
