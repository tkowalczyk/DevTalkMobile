using System;

using DevTalkMobile.Services;
using DevTalkMobile.ViewModels;
using Xamarin.Forms;

namespace DevTalkMobile.Views
{
	public class SocialView : BaseView<SocialViewModel>
	{
		public SocialView ()
		{
			this.Title = "Share & Subscribe";

			#region RSS
			var rssButton = new Button ()
			{
				Text = "RSS",
				BackgroundColor = Color.FromHex ("#FF6600"),
				Image = "rss.png",
			};
			rssButton.Clicked += (sender, e) => 
			{
				DependencyService.Get<IShare>()
					.LaunchBrowser("http://devtalk.pl/feed");
			};
			#endregion

			#region Newsletter
			var emailButton = new Button ()
			{
				Text = "Newsletter",
				BackgroundColor = Color.FromHex ("#848484"),
				Image = "newsletter.png",
			};
			emailButton.Clicked += (sender, e) => 
			{
				DependencyService.Get<IShare>()
					.LaunchBrowser("http://eepurl.com/bem-oP");
			};
			#endregion

			#region Facebook
			var facebookButton = new Button ()
			{
				Text = "Facebook",
				BackgroundColor = Color.FromHex ("#0099cc"),
				Image = "fb.png",
			};
			facebookButton.Clicked += (sender, e) => 
			{
				DependencyService.Get<IShare>()
					.LaunchBrowser("https://www.facebook.com/devtalkpl");
			};
			#endregion

			#region Twitter
			var twitterButton = new Button ()
			{
				Text = "Twitter",
				BackgroundColor = Color.FromHex ("#33b5e5"),
				Image = "twitter.png",
			};
			twitterButton.Clicked += (sender, e) => 
			{
				DependencyService.Get<IShare>()
					.LaunchBrowser("https://twitter.com/devtalkpl");
			};
			#endregion

			#region iTunes
			var itunesButton = new Button ()
			{
				Text = "iTunes",
				BackgroundColor = Color.FromHex ("#aa66cc"),
				Image = "itunes.png",
			};
			itunesButton.Clicked += (sender, e) => 
			{
				DependencyService.Get<IShare>()
					.LaunchBrowser("https://itunes.apple.com/pl/podcast/devtalk/id933567686");
			};
			#endregion

			this.Content = new StackLayout { 
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Spacing = 5,
				Children = {
					rssButton,
					emailButton,
					facebookButton,
					twitterButton,
					itunesButton,
				}
			};
		}
	}
}