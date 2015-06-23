using Xamarin.Forms;
using ZombiepediaApp.Models;
using ZombiepediaApp.ViewModels;

namespace ZombiepediaApp.Views
{
    public partial class DetailView : ContentPage
    {
        public DetailView(Zombie zombie)
        {
            InitializeComponent();
            var viewModel = new DetailViewModel(zombie);
            this.BindingContext = viewModel;
        }
    }
}
