using System;

using DevTalkMobile.ViewModels;
using Xamarin.Forms;

namespace DevTalkMobile.Views
{
	public class PartnersView : BaseView<PartnersViewModel>
	{
		public PartnersView ()
		{
			this.Title = "Partners";

			this.Content = new Label
			{
				Text = "Partners",
			};

			// Accomodate iPhone status bar.
			this.Padding = new Thickness(30, Device.OnPlatform(20, 0, 0), 30, 5);
		}
	}
}