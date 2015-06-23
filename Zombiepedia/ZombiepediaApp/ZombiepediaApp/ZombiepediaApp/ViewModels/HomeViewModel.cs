using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using ZombiepediaApp.Models;
using ZombiepediaApp.Services;

namespace ZombiepediaApp.ViewModels
{
    public class HomeViewModel
    {
        public string Header { get; set; }
        public ObservableCollection<Zombie> Zombies { get; set; }
        public Zombie SelectedZombie { get; set; }
        public ICommand ZombieSelectedCommand { get; private set; }

        public HomeViewModel()
        {
            Header = "Zombiepedia";
            ZombieSelectedCommand = new Command(ZombieSelectedExecute);
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

        private void ZombieSelectedExecute()
        {
            MessagingCenter.Send(SelectedZombie, "NavigateToZombie");
        }

    }
}
