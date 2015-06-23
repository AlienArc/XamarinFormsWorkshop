using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using ZombiepediaApp.Models;
using ZombiepediaApp.Services;

namespace ZombiepediaApp.ViewModels
{
    public class DetailViewModel
    {
        private Zombie Zombie { get; set; }
        public String Header {get{ return "Zombiepedia - " + Zombie.Name; }}
		public String Nam { get { return Zombie.Name; } }
		public String Description { get { return Zombie.Description; } }
		public String ImagePath { get { return Zombie.ImagePath; } }
        public ObservableCollection<string> Comments { get; set; }
	    public string Comment { get; set; }
	    public ICommand AddCommentCommand { get; set; }

        public DetailViewModel(Zombie zombie)
        {
            Zombie = zombie;
			AddCommentCommand = new Command(AddComment);
            GetComments();
        }

	    private async void AddComment()
	    {
		    await ZombieDataService.AddComment(Zombie.Id, Comment);
			Comments.Add(Comment);
		    Comment = "";
	    }

	    private async void GetComments()
        {
            Comments = new ObservableCollection<string>();
            var comments = await ZombieDataService.GetComments(Zombie.Id);

            foreach (var comment in comments)
            {
                Comments.Add(comment);
            }
        }
    }
}
