﻿using System;
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
		private AboutView about;
		private ChatView chat;
		private PodcastView podcast;
		#endregion

		#region Ctor
		public MasterView(MenuViewModel viewModel)
		{
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
					case MenuType.About:
						if (about == null)
							about = new AboutView();

						PageSelection = about;
						break;
					case MenuType.Chat:
						{
						if (chat == null)
							chat = new ChatView();

						PageSelection = chat;
						}
						break;
					case MenuType.Podcast:
						if (podcast == null)
							podcast = new PodcastView();

						PageSelection = podcast;
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
