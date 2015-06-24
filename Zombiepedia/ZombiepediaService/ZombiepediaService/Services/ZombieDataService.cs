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
        private static List<ZombieInfo> ZombieData;
		private static Random Random = new Random();

	    private static List<string> CommentQuotes = new List<string>
	    {
		    "Mmmm, brains!",
			"Ahhhhhhh!!!!!!",
			"I won't bite, I promise.",
			"You look good enough to eat.",
			"Hey I know that guy.",
			"If if it was an iZombie, then you would like it.",
			"It's all Obama's fault!!!",
			"He reminds me of someone I work with.",
			"I work from home and make $20k per week. http://scam.gov",
			"Zombies Rule!",
			"I once killed 50 zombies with a soup spoon.",
			"You can kill this kind by covering them with salt.",
			"Looks like you mom!",
			"No comments for you!",
			"I blow my nose in your general direction!",
			"He needs some Brawndo",
			"Like from the toilet?",
			"I fell of the Jetway again.",
			"You got your law degree from Costco?",
			"You have the minimum number of pieces of flair.",
			"What would you do with a million bucks?",
			"And now for something completly different"
	    };

        static ZombieDataService()
        {
            CreateZombies();
            AddDefaultComments();
        }

        private static void AddDefaultComments()
        {
            foreach (var zombie in ZombieData)
            {
	            var numOfComments = Random.Next(1, 10);
                for (int i = 0; i < numOfComments; i++)
                {
                    zombie.Comments.Add(GetRandomComment());
                }
            }
        }

	    private static string GetRandomComment()
	    {
		    var position = Random.Next(0, CommentQuotes.Count());
		    return CommentQuotes[position];
	    }

	    private static void CreateZombies()
        {
            var serverAddress = "http://zombiepedia.azurewebsites.net/api/image/";
            ZombieData = new List<ZombieInfo>
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

        public static List<ZombieInfo> GetZombies()
        {
            return ZombieData;
        }

        public static IEnumerable<string> GetComments(int zombieId)
        {
            return ZombieData.First(z => z.Id == zombieId).Comments;
        }

	    public static void AddComment(int zombieId, string comment)
	    {
		    var zombie = ZombieData.FirstOrDefault(z => z.Id == zombieId);

		    if (zombie != null)
		    {
			    zombie.Comments.Add(comment);
		    }
	    }
    }
}
