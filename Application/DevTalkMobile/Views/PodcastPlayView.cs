using System;

using DevTalkMobile.ViewModels;
using Xamarin.Forms;
using DevTalkMobile.Models;
using DevTalkMobile.Services;

namespace DevTalkMobile.Views
{
	public class PodcastPlayView : BaseView<PodcastPlayViewModel>
	{
		public PodcastPlayView (FeedItem item)
		{
			//SetBinding(Page.TitleProperty, new Binding(item.Title));

			#region Toolbar Share
			var share = new ToolbarItem
			{
				Icon = "ic_share.png",
				Text = "Share",
				Command = new Command(() =>
					{
						DependencyService.Get<IShare>()
							.ShareText("Listening to @devtalkpl's " + item.Title + " " + item.Link);
					})
			};

			ToolbarItems.Add(share);
			#endregion

			Label title = new Label () 
			{
				
			};

			Label date = new Label () 
			{
				
			};

			ProgressBar progress = new ProgressBar () 
			{
				
			};

			DependencyService.Get<IAudioOperations> ().SetSource(
				item.Mp3Url
			);

			#region Buttons
			Button playButton = new Button ()
			{
				Text = "Play",
				Image = "ic_play.png",
			};

			playButton.Clicked += (sender, e) => 
			{
				DependencyService.Get<IAudioOperations> ().Play();
			};

			Button stopButton = new Button () 
			{
				Text = "Stop",
				Image = "ic_stop.png",
			};

			stopButton.Clicked += (sender, e) => 
			{
				DependencyService.Get<IAudioOperations> ().Stop();
			};

			Button pauseButton = new Button () 
			{
				Text = "Pause",
				Image = "ic_pause.png",
			};

			pauseButton.Clicked += (sender, e) => 
			{
				DependencyService.Get<IAudioOperations> ().Pause();
			};
			#endregion

			WebView webPage = new WebView () 
			{
				Source = new UrlWebViewSource
				{
					Url = item.BlogPost,
				},
				VerticalOptions = LayoutOptions.FillAndExpand,
			};

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
	}
}