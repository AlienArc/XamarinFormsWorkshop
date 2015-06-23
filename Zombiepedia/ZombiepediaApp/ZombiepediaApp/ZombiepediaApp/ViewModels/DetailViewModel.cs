using System;
using System.Collections.ObjectModel;
using ZombiepediaApp.Models;

namespace ZombiepediaApp.ViewModels
{
    public class DetailViewModel
    {
        private Zombie Zombie { get; set; }
        public String Header => $"Zombiepedia - {Zombie.Name}";
        public String Name => Zombie.Name;
        public String Description => Zombie.Description;
        public String ImagePath => Zombie.ImagePath;
        public ObservableCollection<string> Comments { get; set; }

        public DetailViewModel(Zombie zombie)
        {
            Zombie = zombie;
        }

    }
}
