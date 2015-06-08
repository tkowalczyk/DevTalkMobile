using System;
using System.Collections.Generic;

using DevTalkMobile.Models;
using DevTalkMobile.Services;
using Xamarin.Forms;

namespace DevTalkMobile.Views.XAML
{
	public partial class PodcastPlayView : ContentPage
	{
		public PodcastPlayView (FeedItem item)
		{
			InitializeComponent ();

			BindingContext = item;

			webView.Source = new HtmlWebViewSource
			{
				Html = item.DescriptionLongHtml
			};

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

			play.Clicked += (sender, args) => player.PlaybackState = 0;
			pause.Clicked += (sender, args) => player.PlaybackState = 1;
			stop.Clicked += (sender, args) => player.PlaybackState = 2;

			#region Platform Specific Code
			if(Device.OS == TargetPlatform.Android || Device.OS == TargetPlatform.WinPhone)
			{
				play.Text = "Play";
				pause.Text = "Pause";
				stop.Text = "Stop";
			}

			if(Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
			{
				this.BackgroundColor = Color.White;
				this.title.TextColor = Color.Black;
				this.date.TextColor = Color.Black;
				this.play.TextColor = Color.Black;
				this.pause.TextColor = Color.Black;
				this.stop.TextColor = Color.Black;
			}

			Device.OnPlatform (
				iOS: () => {
					title.FontFamily = "HelveticaNeue-Thin";
					date.FontFamily = "HelveticaNeue-Thin";
					play.FontFamily = "HelveticaNeue-Thin";
					pause.FontFamily = "HelveticaNeue-Thin";
					stop.FontFamily = "HelveticaNeue-Thin";
				},
				Android: () => {
					title.FontFamily = "sans-serif-condensed";
					date.FontFamily = "sans-serif-condensed";
					play.FontFamily = "sans-serif-condensed";
					pause.FontFamily = "sans-serif-condensed";
					stop.FontFamily = "sans-serif-condensed";
				}
			);
			#endregion
		}
	}
}