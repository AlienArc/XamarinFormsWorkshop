using System;
using Xamarin.Forms;

namespace ZombiepediaApp.Models
{
    public class Zombie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
    }
}
