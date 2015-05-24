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
					Id = 4,
					Title = "About",
					MenuType = MenuType.About,
					Icon = "about.png"
				});

			MenuItems.Add (new MenuItem 
				{
					Id = 5,
					Title = "Social",
					MenuType = MenuType.Social,
					Icon = "social.png"
				});
		}
		#endregion
	}
}