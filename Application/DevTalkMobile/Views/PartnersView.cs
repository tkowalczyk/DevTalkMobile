using System;

using DevTalkMobile.Helpers;
using DevTalkMobile.Services;
using DevTalkMobile.ViewModels;
using Xamarin.Forms;

namespace DevTalkMobile.Views
{
	public class PartnersView : BaseView<PartnersViewModel>
	{
		#region Ctor
		public PartnersView ()
		{
			this.Title = "Partners";

			#region ActivityIndicator
			var activity = new ActivityIndicator
			{
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				IsEnabled = true
			};

			activity.SetBinding(ActivityIndicator.IsVisibleProperty, "IsBusy");
			activity.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
			#endregion

			var lab = new Label () 
			{
				Text = "Partners",
			};

			var layout = new StackLayout ()
			{
				Children = 
				{
					lab
				}
			};

			var overlay = new AbsoluteLayout();

			AbsoluteLayout.SetLayoutFlags(layout, AbsoluteLayoutFlags.PositionProportional);
			AbsoluteLayout.SetLayoutBounds(layout, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
			AbsoluteLayout.SetLayoutFlags(activity, AbsoluteLayoutFlags.PositionProportional);
			AbsoluteLayout.SetLayoutBounds(activity, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
			overlay.Children.Add(layout);
			overlay.Children.Add(activity);

			this.Content = overlay;

			// Accomodate iPhone status bar.
			this.Padding = new Thickness(30, Device.OnPlatform(20, 0, 0), 30, 5);
		}
		#endregion

		#region Overrides
		protected override void OnAppearing()
		{
			base.OnAppearing ();

			if (ViewModel == null || !ViewModel.CanLoadMore || ViewModel.IsBusy)
				return;

			if (this.TypeOfConnection == ConnectionType.NotReachable) 
			{
				DisplayAlert(
					StaticData.ConnectionHeader,
					StaticData.ConnectionMessage,
					StaticData.ConnectionButton
				);
			}
			else 
			{
				ViewModel.LoadContentCommand.Execute(null);
			}
		}
		#endregion
	}
}