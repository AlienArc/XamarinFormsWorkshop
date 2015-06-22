using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombiepediaService.Models;

namespace ZombiepediaService.Services
{
    public static class ZombieDataService
    {
        private static readonly ZombieInfo[] ZombieData;

        static ZombieDataService()
        {
            var serverAddress = "http://zombiepedia.azurewebsites.net/api/image/";
            ZombieData = new ZombieInfo[]
            {
                new ZombieInfo()
                {
                    Id = 1,
                    Name = "Crawler",
                    Description =
                        "Unable to walk, either due to missing legs or damaged nerves, and must therefore crawl everywhere. Beware of these and never assume a motionless zombie on the floor is dead.",
                    ImagePath = $"{serverAddress}1"
                },
                new ZombieInfo()
                {
                    Id = 2,
                    Name = "Walker",
                    Description = "Your standard slow moving zombie. Moves in a jerky shuffle.",
                    ImagePath = $"{serverAddress}2"
                },
                new ZombieInfo()
                {
                    Id = 3,
                    Name = "Runner",
                    Description =
                        "Fast moving and tireless. If you encounter a runner, do not try to outrun it as it will not fatigue and you will. Fortunately they are not very durable and are easy to kill.",
                    ImagePath = $"{serverAddress}3"
                },
                new ZombieInfo()
                {
                    Id = 4,
                    Name = "Teacher",
                    Description =
                        "These zombies used to be school teachers and are the traditional brain hungry zombie. The high brain diet and former life as an educator has left them smarter than your average zombie, beware.",
                    ImagePath = $"{serverAddress}4"
                },
                new ZombieInfo()
                {
                    Id = 5,
                    Name = "Teen",
                    Description =
                        "Zombified in the midst of adolecense these zombies are easily distracted by TV, smart phones, and any kind of game. Beware, though, they may immediately lose interest in the distraction at any time as their hunger takes over.",
                    ImagePath = $"{serverAddress}5"
                },
            };
        }

        public static ZombieInfo[] GetZombies()
        {
            return ZombieData;
        }
    }
}
