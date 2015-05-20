using System;
using System.Collections.Generic;

using Xamarin.Forms;
using DevTalkMobile.Models;
using DevTalkMobile.Services;

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
		}
	}
}