using System;
using System.Collections.Generic;
using DevTalkMobile.Helpers;
using DevTalkMobile.Models;
using DevTalkMobile.ViewModels;
using Xamarin.Forms;

namespace DevTalkMobile.Views
{
	public class CustomMasterDetailPage : MasterDetailPage
	{
		#region Fields
		private MenuViewModel ViewModel
		{
			get { return BindingContext as MenuViewModel; }
		}

		private MasterView master;

		private Dictionary<MenuType, NavigationPage> pages;
		#endregion

		#region Ctor
		public CustomMasterDetailPage()
		{
			pages = new Dictionary<MenuType, NavigationPage>();

			this.BindingContext = new MenuViewModel();

			this.Master = master = new MasterView(ViewModel);

			this.IsGestureEnabled = false;

			var homeNav = new NavigationPage(master.PageSelection)
			{

			};

			this.Detail = homeNav;

			pages.Add(MenuType.Home, homeNav);

			master.PageSelectionChanged = (menuType) =>
			{
				NavigationPage newPage;

				if (pages.ContainsKey(menuType))
				{
					newPage = pages[menuType];
				}
				else
				{
					newPage = new NavigationPage(master.PageSelection)
					{

					};
					pages.Add(menuType, newPage);
				}
				Detail = newPage;
				Detail.Title = master.PageSelection.Title;
				IsPresented = false;
			};

			this.Icon = StaticData.MainMenuIcon;

			// Accomodate iPhone status bar.
			this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
		}
		#endregion
	}
}