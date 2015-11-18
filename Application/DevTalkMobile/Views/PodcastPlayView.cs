using System;
using Xamarin.Forms;

using DevTalkMobile.ViewModels;
using DevTalkMobile.Models;
using DevTalkMobile.Services;
using DevTalkMobile.Helpers;

namespace DevTalkMobile.Views
{
	public class PodcastPlayView : BaseView<PodcastPlayViewModel>
	{
		#region Fields
		private FeedItem _selectedFeedItem;
		#endregion

		public PodcastPlayView (FeedItem selectedFeedItem)
		{
			SetBinding(Page.TitleProperty, new Binding(PodcastPlayViewModel.PodcastTitlePropertyName));

			this._selectedFeedItem = selectedFeedItem;

			#region Toolbar Share
			var share = new ToolbarItem
			{
				Icon = "ic_share.png",
				Text = "Share",
				Command = new Command(() =>
					{
						DependencyService.Get<IShare>()
							.ShareText("Listening to @devtalkpl's " + _selectedFeedItem.Title + " " + _selectedFeedItem.Link);
					})
			};

			ToolbarItems.Add(share);
			#endregion

			Label title = new Label () 
			{
				
			};
			title.SetBinding(Label.TextProperty, "PodcastTitle");

			Label date = new Label () 
			{
				
			};
			date.SetBinding(Label.TextProperty, "PodcastDate");

			ProgressBar progress = new ProgressBar () 
			{
				
			};
			progress.SetBinding (ProgressBar.ProgressProperty, "PodcastCurrentLength");

			Label duration = new Label () 
			{

			};
			duration.SetBinding(Label.TextProperty, "PodcastDuration");
			#region Buttons
			Button playButton = new Button ()
			{
				Text = "Play",
				Image = "ic_play.png",
			};

			playButton.Clicked += (sender, e) => 
			{
				ViewModel.PlayCommand.Execute(_selectedFeedItem.Mp3Url);
			};

			Button stopButton = new Button () 
			{
				Text = "Stop",
				Image = "ic_stop.png",
			};

			stopButton.Clicked += (sender, e) => 
			{
				ViewModel.StopCommand.Execute(null);
			};

			Button pauseButton = new Button () 
			{
				Text = "Pause",
				Image = "ic_pause.png",
			};

			pauseButton.Clicked += (sender, e) => 
			{
				ViewModel.PauseCommand.Execute(null);
			};
			#endregion

			WebView webPage = new WebView () 
			{
				Source = new UrlWebViewSource{},
				VerticalOptions = LayoutOptions.FillAndExpand,
			};
			webPage.SetBinding (WebView.SourceProperty, "PodcastWebPage");

			StackLayout buttonsLayout = new StackLayout () 
			{
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Spacing = 10,
				Children = 
				{
					playButton,
					pauseButton,
					stopButton
				}
			};

			StackLayout layout = new StackLayout () 
			{
				Padding = new Thickness(10),
				Spacing = 5,
				Children = 
				{
					title,
					date,
					progress,
					duration,
					buttonsLayout,
					webPage
				}
			};

			#region Platform Specific Code
			if(Device.OS == TargetPlatform.Android || Device.OS == TargetPlatform.WinPhone)
			{
				playButton.Text = "Play";
				pauseButton.Text = "Pause";
				stopButton.Text = "Stop";
			}

			if(Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
			{
				this.BackgroundColor = Color.White;
				title.TextColor = Color.Black;
				date.TextColor = Color.Black;
				playButton.TextColor = Color.Black;
				pauseButton.TextColor = Color.Black;
				stopButton.TextColor = Color.Black;
			}

			Device.OnPlatform (
				iOS: () => {
					title.FontFamily = "HelveticaNeue-Thin";
					date.FontFamily = "HelveticaNeue-Thin";
					playButton.FontFamily = "HelveticaNeue-Thin";
					pauseButton.FontFamily = "HelveticaNeue-Thin";
					stopButton.FontFamily = "HelveticaNeue-Thin";
				},
				Android: () => {
					title.FontFamily = "sans-serif-condensed";
					date.FontFamily = "sans-serif-condensed";
					playButton.FontFamily = "sans-serif-condensed";
					pauseButton.FontFamily = "sans-serif-condensed";
					stopButton.FontFamily = "sans-serif-condensed";
				}
			);
			#endregion

			// Accomodate iPhone status bar.
			this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

			this.Content = layout;
		}

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
				if(_selectedFeedItem != null)
					ViewModel.GetSelectedItemInfoCommand.Execute(_selectedFeedItem);
			}
		}
		#endregion
	}
}