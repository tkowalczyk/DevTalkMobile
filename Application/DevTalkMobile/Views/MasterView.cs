using System;
using DevTalkMobile.Helpers;
using DevTalkMobile.Models;
using DevTalkMobile.ViewModels;
using DevTalkMobile.Views;
using Xamarin.Forms;

namespace DevTalkMobile.Views
{
	public class MasterView : ContentPage
	{
		#region Fields
		private Page pageSelection;
		private MenuType menuType = MenuType.About;

		public Action<MenuType> PageSelectionChanged;
		public Page PageSelection
		{
			get { return pageSelection; }
			set
			{
				pageSelection = value;
				if (PageSelectionChanged != null)
					PageSelectionChanged(menuType);
			}
		}
		#endregion

		#region Menu
		private HomeView home;
		private PodcastView podcast;
		private AboutView about;
		private SocialView social;
		#endregion

		#region Ctor
		public MasterView(MenuViewModel viewModel)
		{
			/// Hamburger menu icon
			this.Icon = StaticData.MainMenuIcon;

			this.BindingContext = viewModel;

			this.Title = "DevTalk";

			var layout = new StackLayout { Spacing = 0 };

			var listView = new ListView();

			var cell = new DataTemplate(typeof(CustomControls.ListImageCell));

			cell.SetBinding(TextCell.TextProperty, MenuViewModel.TitlePropertyName);
			cell.SetBinding(ImageCell.ImageSourceProperty, "Icon");

			listView.ItemTemplate = cell;

			listView.ItemsSource = viewModel.MenuItems;
			if (about == null)
				about = new AboutView();

			PageSelection = about;

			listView.ItemSelected += (sender, args) =>
			{
				var menuItem = listView.SelectedItem as DevTalkMobile.Models.MenuItem;
				menuType = menuItem.MenuType;

				switch (menuItem.MenuType)
				{
					case MenuType.Home:
						if (home == null)
							home = new HomeView();

						PageSelection = home;
						break;
					case MenuType.Podcast:
						if (podcast == null)
							podcast = new PodcastView();

						PageSelection = podcast;
						break;
					case MenuType.About:
						if (about == null)
							about = new AboutView();

						PageSelection = about;
						break;
					case MenuType.Social:
						if (social == null)
							social = new SocialView();

						PageSelection = social;
						break;
				}
			};

			listView.SelectedItem = viewModel.MenuItems[0];
			layout.Children.Add(listView);

			// Accomodate iPhone status bar.
			this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

			this.Content = layout;
		}
		#endregion
	}
}

