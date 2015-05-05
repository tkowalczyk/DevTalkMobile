using System;
using DevTalkMobile.Helpers;
using DevTalkMobile.Services;
using DevTalkMobile.ViewModels;
using Xamarin.Forms;

namespace DevTalkMobile.Views
{
	public class HomeView : BaseView<HomeViewModel>
	{
		#region Ctor
		public HomeView ()
		{
			this.Title = "DevTalk";

			this.Content = new Label
			{
				Text = "HomeView",
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
			};

			// Accomodate iPhone status bar.
			this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
		}
		#endregion

		#region Overrides
		protected override void OnAppearing()
		{
			base.OnAppearing ();

			if (this.TypeOfConnection == ConnectionType.NotReachable) 
			{
				DisplayAlert(
					StaticData.ConnectionHeader,
					StaticData.ConnectionMessage,
					StaticData.ConnectionButton
				);
			}
		}
		#endregion
	}
}