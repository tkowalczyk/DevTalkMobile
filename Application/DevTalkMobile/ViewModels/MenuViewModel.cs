using System;
using System.Collections.ObjectModel;
using DevTalkMobile.Models;

namespace DevTalkMobile.ViewModels
{
	public class MenuViewModel : BaseViewModel
	{
		#region Properties
		public ObservableCollection<MenuItem> MenuItems { get; set; }
		#endregion

		#region Ctor
		public MenuViewModel()
		{
			CanLoadMore = true;
			Title = "DevTalk";
			MenuItems = new ObservableCollection<MenuItem>();

			MenuItems.Add(new MenuItem
				{
					Id = 0,
					Title = "Home",
					MenuType = MenuType.Home,
					Icon = "home.png"
				});

			MenuItems.Add(new MenuItem
				{
					Id = 1,
					Title = "Podcasts",
					MenuType = MenuType.Podcast,
					Icon = "podcast.png"
				});

			MenuItems.Add(new MenuItem
				{
					Id = 2,
					Title = "Chat",
					MenuType = MenuType.Chat,
					Icon = "chat.png"
				});

			MenuItems.Add(new MenuItem
				{
					Id = 3,
					Title = "About",
					MenuType = MenuType.About,
					Icon = "about.png"
				});
		}
		#endregion
	}
}