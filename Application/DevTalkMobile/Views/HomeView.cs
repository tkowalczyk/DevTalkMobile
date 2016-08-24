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

			var buttonPlay = new Button () {
				Text = "Show",
				BackgroundColor = Color.FromHex ("#FF6600"),
				WidthRequest = 150
			};
			buttonPlay.SetBinding(Button.IsVisibleProperty, "IsNotBusy");

			buttonPlay.Clicked += async (sender, args) =>
			{
				await this.Navigation.PushAsync(new PodcastPlayView(ViewModel.LastFeedItem));
			};

			var picture = new Image () {
				Aspect = Aspect.AspectFit,
				HeightRequest = 150,
				WidthRequest = 150,
			};
			picture.SetBinding (Image.SourceProperty, "FileImage");

			var tapGestureRecognizer = new TapGestureRecognizer();
			tapGestureRecognizer.Tapped += async (s, e) => 
			{
				await this.Navigation.PushAsync(new PodcastPlayView(ViewModel.LastFeedItem));
			};
			picture.GestureRecognizers.Add(tapGestureRecognizer);

			var title = new Label () {
				FontSize = 25, 
				TextColor = Color.FromHex ("#FF6600"),
				FontFamily = Device.OnPlatform ("HelveticaNeue-Medium", "", "")
			};
			title.SetBinding(Label.TextProperty, "PodcastTitle");

			var dateLabel = new Label () {
				Text = "Date:", 
				FontSize = 15, 
				TextColor = Color.FromHex ("#B7A19B"),
				FontFamily = Device.OnPlatform ("HelveticaNeue-CondensedBlack", "sans-serif-condensed", "")
			};
			dateLabel.SetBinding(Label.IsVisibleProperty, "IsNotBusy");

			var date = new Label () {
				FontSize = 20, 
				FontFamily = Device.OnPlatform ("HelveticaNeue", "sans-serif", "")
			};
			date.SetBinding(Label.TextProperty, "PodcastDate");

			var details = new StackLayout () {
				Spacing = 10,
				Padding = new Thickness (20, 10),
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = {
					title,
					dateLabel,
					date,
				}
			};

			#region MainGrid
			Grid mainGrid = new Grid
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				RowDefinitions = 
				{
					new RowDefinition { Height = GridLength.Auto},
					new RowDefinition { Height = GridLength.Auto},
					new RowDefinition { Height = new GridLength(10, GridUnitType.Star)}
				},
				ColumnDefinitions = 
				{
					new ColumnDefinition { Width = GridLength.Auto}
				}
			};

			mainGrid.Children.Add(buttonPlay, 0, 0);
			mainGrid.Children.Add(picture, 0, 1);
			mainGrid.Children.Add(details, 0, 2);
			#endregion

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

			#region Platform Specific Code
			Device.OnPlatform (
				iOS: () => {
					buttonPlay.TextColor = Color.FromHex("#ffffff");
					buttonPlay.FontFamily = "HelveticaNeue-Thin";
					title.FontFamily = "HelveticaNeue-Thin";
					date.FontFamily = "HelveticaNeue-Thin";
					dateLabel.FontFamily = "HelveticaNeue-Thin";
				},
				Android: () => {
					buttonPlay.FontFamily = "sans-serif-condensed";
					title.FontFamily = "sans-serif-condensed";
					date.FontFamily = "sans-serif-condensed";
					dateLabel.FontFamily = "sans-serif-condensed";
				}
			);
			#endregion

			var overlay = new AbsoluteLayout();

			AbsoluteLayout.SetLayoutFlags(mainGrid, AbsoluteLayoutFlags.PositionProportional);
			AbsoluteLayout.SetLayoutBounds(mainGrid, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
			AbsoluteLayout.SetLayoutFlags(activity, AbsoluteLayoutFlags.PositionProportional);
			AbsoluteLayout.SetLayoutBounds(activity, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
			overlay.Children.Add(mainGrid);
			overlay.Children.Add(activity);

			this.Content = overlay;

			// Accomodate iPhone status bar.
			this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
		}
		#endregion

		#region Overrides
		protected override void OnAppearing()
		{
			base.OnAppearing ();

			if (ViewModel == null || !ViewModel.CanLoadMore || ViewModel.IsBusy || ViewModel.FeedItems.Count > 0)
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
				ViewModel.LoadItemsCommand.Execute(null);
			}
		}
		#endregion
	}
}