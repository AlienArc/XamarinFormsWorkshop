using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;

using Xamarin.Forms;
using ZombiepediaApp.Models;
using ZombiepediaApp.Views;

namespace ZombiepediaApp
{
    public class App : Application
    {
        private NavigationPage rootNavigationPage;
        public App()
        {
            // The root page of your application
            MessagingCenter.Subscribe<Zombie>(this, "NavigateToZombie", NavigateToZombie);
            rootNavigationPage = new NavigationPage(new HomeView());
            MainPage = rootNavigationPage;
        }

        private void NavigateToZombie(Zombie zombie)
        {
            rootNavigationPage.PushAsync(new DetailView(zombie));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
