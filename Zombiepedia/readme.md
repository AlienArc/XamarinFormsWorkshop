#Cross-Platform Mobile with C# & Xamarin

What if there was a zombie apocalypse and the only way to save the brains of all humankind was for you to develop a cross-platform mobile app? Come learn how you can use Xamarin to leverage your existing C# skills to speed up mobile development and save the world from zombies.

Xamarin is already a great tool for decreasing the friction of app development in a multi-platform world, but with the advent of Xamarin Forms we are one step closer to the holy grail of write once, run anywhere code. In this course we will learn how to use the latest features of Xamarin to increase code sharing, reduce time to market, improve customer satisfaction, and boost potential revenue.

We will be using C# and Xamarin Forms XAML to create apps that look, feel, and run native on multiple platforms. This hands-on workshop will teach you how to make a cross-platform app that shares both the user interface design and business logic. We will also take a look at how to support platform specific features like live tiles on Windows Phone.

The class will start with an interactive presentation and discussion that will lead us into a hands-on workshop for the remainder of the session, so make sure you come ready to write some code. Development will be based around the use of the Android Emulator, but we'll work with you to use your device as long as your system meets the requirements and you have the other needed development licenses.

To speed up the session please come with the following:

* Laptop with Windows 8.1 with at least 4GB of RAM (8GB if using emulator)
* Xamarin Android Player
* Visual Studio 2013 (Community Edition or better) with Update 4
* A mobile device (not required, you can use the emulator)
* If you do not have Xamarin Business edition for your target platform you will be signing up for the trial at the start of the session.

Note: A Mac is required for iOS development and debugging on phone or emulator.

####No Zombies please!

##About this workshop and document

This workshop will take you through the basics of working inside the Xamarin.Forms library. When completed you should feel comfortable enough to begin your own projects and expand your skillset. Along the way we will provide commentary on what the code we are modifying is doing and why.

This document is intended to help you follow along in the workshop as we create our Zombiepedia app.

## Setup the project

* Create new ***Blank App (Xamarin.Forms Portable)*** project
* Name it ***ZombiepediaApp*** (this is so namespaces from the included snippets work properly)
* Update all Nuget Packages
		Update-Package

## Initial view setup

* Add a folder called ***Views*** to the shared project
* Add new ***Forms XAML Page*** called ***HomeView*** and paste the following code into it

	```
		<?xml version="1.0" encoding="utf-8" ?>
		<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
					 xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
					 x:Class="ZombiepediaApp.Views.HomeView">
			<Label Text="Goodbye World!" VerticalOptions="Center" HorizontalOptions="Center" />
		</ContentPage>
	```

* Modify the app.cs and set the ***MainPage*** property the new view:

    MainPage = new HomeView();

## Formatting Text

* Replace the Label element with the following

```
	<Label Text="Goodbye World!" VerticalOptions="Center" HorizontalOptions="Center" FontSize="36" />

```

## Grid Layout

* Replace the Label element with the following

```
	<Grid>
	  <Grid.RowDefinitions>
	    <RowDefinition Height="Auto" />
	    <RowDefinition Height="*" />
	  </Grid.RowDefinitions>

	  <Label Grid.Row="0" Text="Zombiepedia" VerticalOptions="Center" HorizontalOptions="Center" FontSize="24" />

	  <Label Grid.Row="1" Text="List of zombies..." VerticalOptions="Center" HorizontalOptions="Center" FontSize="36" />
	</Grid>
```

## Data Binding

* Replace the first Label element with the following:

```
	<Label Grid.Row="0" Text="{Binding Header}" VerticalOptions="Center" HorizontalOptions="Center" FontSize="24" />
```

* Edit the code behind file ***HomeView.xaml.cs*** and replace the contents with the following:

		using Xamarin.Forms;

		namespace ZombiepediaApp.Views
		{
			public partial class HomeView : ContentPage
			{
				public string Header { get; set; }

				public HomeView()
				{
					InitializeComponent();

					Header = "Zombiepedia";
					this.BindingContext = this;
				}
			}
		}


## Add a ListView of Zombies

* Replace the second label with the following

```
	<ListView Grid.Row="1 " ItemsSource="{Binding Zombies}" />
```

* Replace the contents of ***HomeView.xaml.cs*** with the following

		using System.Collections.ObjectModel;
		using Xamarin.Forms;

		namespace ZombiepediaApp.Views
		{
		    public partial class HomeView : ContentPage
		    {
		        public string Header { get; set; }
		        public ObservableCollection<string> Zombies { get; set; }

		        public HomeView()
		        {
		            InitializeComponent();

		            Header = "Zombiepedia";
		            Zombies = new ObservableCollection<string>() { "Ankle Biters", "Shufflers"};
		            this.BindingContext = this;
		        }
		    }
		}

## Switch to ViewModel

* Add a folder called ***ViewModels*** to the shared project
* Add a new class to the ***ViewModels*** folder and call it ***HomeViewModel***
* Replace the contents of the new class with the following

		using System.Collections.ObjectModel;

		namespace ZombiepediaApp.ViewModels
		{
		    public class HomeViewModel
		    {
		        public string Header { get; set; }
		        public ObservableCollection<string> Zombies { get; set; }

		        public HomeViewModel()
		        {
		            Header = "Zombiepedia";
		            Zombies = new ObservableCollection<string>() { "Ankle Biters", "Shufflers" };
		        }
		    }
		}

* Replace the contents of the ***HomeView.xaml.cs*** with the following

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

# Get Zombies from Web Service

* Add the Newtonsoft JSON nuget package to the portable project
		Install-Package Newtonsoft.JSON ZombiepediaApp
* Add the Microsoft.Net.Http nuget package to the portable project
		Get-Project -All | Install-Package Microsoft.Net.Http
* Add a ***Models*** folder to the portable project
* Add a class to the ***Models*** folder called ***Zombie.cs*** and replace the contents with the following

		namespace ZombiepediaApp.Models
		{
		    public class Zombie
		    {
		        public int Id { get; set; }
		        public string Name { get; set; }
		        public string Description { get; set; }
		    }
		}

* Add a ***Services*** folder to the portable project
* Add a class to the ***Services*** folder called ***ZombieDataService.cs*** and replace the content with the following

		using System;
		using System.Collections.Generic;
		using System.Net.Http;
		using System.Threading.Tasks;
		using Newtonsoft.Json;
		using ZombiepediaApp.Models;

		namespace ZombiepediaApp.Services
		{
		    public static class ZombieDataService
		    {
		        public static async Task<List<Zombie>> GetZombies()
		        {
		            var service = new HttpClient
		            {
		                BaseAddress = new Uri("http://zombiepedia.azurewebsites.net")
		            };
		            var response = await service.GetAsync("api/zombies");
		            var data = await response.Content.ReadAsStringAsync();
		            var zombies = JsonConvert.DeserializeObject<List<Zombie>>(data);
		            return zombies;
		        }
		    }
		}

* Replace the contents of the ***HomeViewModel.cs*** file with the following

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

* You can see how the observable collection notifies the UI of changes by adding a delay in populating the list (add after Zombies.Add(zombie);)
		await Task.Delay(1000);

## Better display with DataTemplates

* Replace the ListView element with the following

```
	<ListView Grid.Row="1 " ItemsSource="{Binding Zombies}">
	  <ListView.ItemTemplate>
	    <DataTemplate>
	      <ViewCell>
	        <Label Text="{Binding Name}"/>
	      </ViewCell>
	    </DataTemplate>
	  </ListView.ItemTemplate>
	</ListView>
```

## Advanced DataTemplate Example

* Replace the ListView element with the following

```
	<ListView Grid.Row="1 " ItemsSource="{Binding Zombies}">
	  <ListView.ItemTemplate>
	    <DataTemplate>
	      <ViewCell>
	        <StackLayout Orientation="Horizontal">
	          <Image Source="{Binding ImagePath}" />
	          <Label Text="{Binding Name}" FontSize="24"/>
	        </StackLayout>
	      </ViewCell>
	    </DataTemplate>
	  </ListView.ItemTemplate>
	</ListView>
```

## Add Details Page

* Add a new ***Forms Xaml Page*** called ***DetailView*** to the Views folder and replace its contents with the following

```
	<?xml version="1.0" encoding="utf-8" ?>
	<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
	             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
	             x:Class="ZombiepediaApp.Views.DetailView">
	  <Grid>
	    <Grid.RowDefinitions>
	      <RowDefinition Height="Auto"/>
	      <RowDefinition Height="Auto"/>
	      <RowDefinition Height="Auto"/>
	      <RowDefinition Height="Auto"/>
	    </Grid.RowDefinitions>

	    <Image Grid.Row="0" Source="{Binding ImagePath}"/>

	    <Label Grid.Row="1" Text="{Binding Name}" FontSize="36" HorizontalOptions="Center" />

	    <Label Grid.Row="2" Text="{Binding Description}" FontSize="18" HorizontalOptions="Center" />

	    <ListView Grid.Row="3" ItemsSource="{Binding Comments}" />

	  </Grid>
	</ContentPage>
```

* Replace the contents of the new code behind page with the following

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

* Add a new class called ***DetailViewModel.cs*** to the ViewModels folder and replace its contents with the following

		using System;
		using System.Collections.ObjectModel;
		using ZombiepediaApp.Models;

		namespace ZombiepediaApp.ViewModels
		{
		    public class DetailViewModel
		    {
		        private Zombie Zombie { get; set; }
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

* Add a folder to the portable project called ***XamlHelpers***
* Add a new class to the new ***XamlHelpers*** folder called ***ListViewCommands.cs*** and replace its contents with the following

		using Xamarin.Forms;

		namespace ZombiepediaApp.XamlHelpers
		{
			public class ListViewCommands
			{
				public static readonly BindableProperty ItemTappedProperty =
					BindableProperty.CreateAttached<ListViewCommands, Command>(
						bindable => GetItemTapped(bindable),
						null,
						BindingMode.OneWay,
						null,
						OnItemTappedChanged,
						null,
						null);

				public static Command GetItemTapped(BindableObject bo)
				{
					return (Command)bo.GetValue(ItemTappedProperty);
				}

				public static void SetItemTapped(BindableObject bo, Command value)
				{
					bo.SetValue(ItemTappedProperty, value);
				}

				public static void OnItemTappedChanged(BindableObject bo, Command oldValue, Command newValue)
				{
					var lv = bo as ListView;
					if (lv != null)
					{
						lv.ItemTapped += (sender, args) =>
						{
							newValue.Execute(args.Item);
						};
					}
				}
			}
		}

* Update the ListView element on the HomeView.Xaml view to the following

```
	<ListView Grid.Row="1" ItemsSource="{Binding Zombies}"
	          SelectedItem="{Binding SelectedZombie}"
	          xh:ListViewCommands.ItemTapped="{Binding ZombieSelectedCommand}" >
	  <ListView.ItemTemplate>
	    <DataTemplate>
	      <ViewCell>
	        <StackLayout Orientation="Horizontal">
	          <Image Source="{Binding ImagePath}" />
	          <Label Text="{Binding Name}" FontSize="24"/>
	        </StackLayout>
	      </ViewCell>
	    </DataTemplate>
	  </ListView.ItemTemplate>
	</ListView>
```

* Replace the contents of the ***HomeViewModel.cs*** file with the following

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

* Update the ***App.cs*** file with the following

		private NavigationPage rootNavigationPage;
		public App()
		{
			// The root page of your application
			MessagingCenter.Subscribe<Zombie>(this, "NavigateToZombie", NavigateToZombie);
			rootNavigationPage = new NavigationPage(new HomeView());
			MainPage = rootNavigationPage;
		}

## Update Title

* Update the ***ContentPage*** element of ***HomeView.Xaml*** with the following

```
	<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
	             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
	             xmlns:xh="clr-namespace:ZombiepediaApp.XamlHelpers;assembly=ZombiepediaApp"
	             x:Class="ZombiepediaApp.Views.HomeView"
	             Title="{Binding Header}">
```

* Update the ***ContentPage*** element of ***DetailView.Xaml*** with the following

```
	<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
	             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
	             x:Class="ZombiepediaApp.Views.DetailView"
	             Title="{Binding Header}">
```

* Add the following property to the ***DetailViewModel.cs***

    public String Header => $"Zombiepedia - {Zombie.Name}";

##Add comments to detail page

* Add the following to the ***ZombieDataService***

		public static async Task<List<string>> GetComments(int id)
		{
		    var service = new HttpClient
		    {
		        BaseAddress = new Uri("http://zombiepedia.azurewebsites.net")
		    };
		    var response = await service.GetAsync($"api/comment/{id}");
		    var data = await response.Content.ReadAsStringAsync();
		    var comments = JsonConvert.DeserializeObject<List<string>>(data);
		    return comments;
		}

* Update the following in the ***DetailViewModel***

		public DetailViewModel(Zombie zombie)
		{
		    Zombie = zombie;
		    GetComments();
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
