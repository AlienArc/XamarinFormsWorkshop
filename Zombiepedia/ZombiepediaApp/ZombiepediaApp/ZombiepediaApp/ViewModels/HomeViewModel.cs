using System.Collections.ObjectModel;
using ZombiepediaApp.Models;
using ZombiepediaApp.Services;

namespace ZombiepediaApp.ViewModels
{
    public class HomeViewModel
    {
        public string Header { get; set; }
        public ObservableCollection<Zombie> Zombies { get; set; }

        public HomeViewModel()
        {
            Header = "Zombiepedia";
            Zombies = new ObservableCollection<Zombie>();
            LoadZombies();
        }

        public async void LoadZombies()
        {
            var serviceZombies = await ZombieDataService.GetZombies();
            foreach (var zombie in serviceZombies)
            {
                Zombies.Add(zombie);
            }
        }
    }
}
