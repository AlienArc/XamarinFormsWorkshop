using Xamarin.Forms;
using ZombiepediaApp.ViewModels;

namespace ZombiepediaApp.Views
{
    public partial class HomeView : ContentPage
    {
        public HomeView()
        {
            InitializeComponent();
            this.BindingContext = new HomeViewModel();
        }
    }
}
