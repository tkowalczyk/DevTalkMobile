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
					Icon = "ic_home_white_24dp.png"
				});

			MenuItems.Add(new MenuItem
				{
					Id = 1,
					Title = "Episodes",
					MenuType = MenuType.Episode,
					Icon = "ic_settings_voice_white_24dp.png"
				});

			MenuItems.Add(new MenuItem
				{
					Id = 2,
					Title = "About",
					MenuType = MenuType.About,
					Icon = "ic_explore_white_24dp.png"
				});

			MenuItems.Add (new MenuItem 
				{
					Id = 3,
					Title = "Social",
					MenuType = MenuType.Social,
					Icon = "ic_face_white_24dp.png"
				});

			MenuItems.Add (new MenuItem 
				{
					Id = 4,
					Title = "Partners",
					MenuType = MenuType.Partners,
					Icon = "ic_supervisor_account_white_24dp.png"
				});
		}
		#endregion
	}
}