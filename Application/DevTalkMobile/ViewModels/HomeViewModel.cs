using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using DevTalkMobile.Helpers;
using DevTalkMobile.Models;
using DevTalkMobile.Services;
using DevTalkMobile.Services.ModernHttpClient;
using Xamarin.Forms;
using Xamarin;

namespace DevTalkMobile.ViewModels
{
	public class HomeViewModel : BaseViewModel
	{
		#region Fields
		private IFeedRepository _feedRepository;
		#endregion

		#region Ctor
		public HomeViewModel ()
		{
			_feedRepository = new FeedRepository();
		}
		#endregion

		#region Properties
		private ObservableCollection<FeedItem> feedItems = new ObservableCollection<FeedItem>();
		public ObservableCollection<FeedItem> FeedItems
		{
			get { return feedItems; }
			set { feedItems = value; OnPropertyChanged("FeedItems"); }
		}

		private string podcastTitle;
		public string PodcastTitle
		{
			get { return podcastTitle; }
			set { podcastTitle = value; OnPropertyChanged("PodcastTitle"); }
		}

		private string podcastDate;
		public string PodcastDate
		{
			get { return podcastDate; }
			set { podcastDate = value; OnPropertyChanged("PodcastDate"); }
		}

		private string fileImage;
		public string FileImage
		{
			get { return fileImage; }
			set { fileImage = value; OnPropertyChanged("FileImage"); }
		}

		private FeedItem lastFeedItem;
		public FeedItem LastFeedItem
		{
			get { return lastFeedItem; }
			set { lastFeedItem = value; OnPropertyChanged("LastFeedItem"); }
		}
		#endregion

		#region Commands
		private Command loadItemsCommand;
		public Command LoadItemsCommand
		{
			get { return loadItemsCommand ?? (loadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand())); }
		}
		#endregion

		#region Private Methods
		private async Task ExecuteLoadItemsCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;
			IsNotBusy = false;

			try
			{
				FeedItems.Clear();

				var items = await _feedRepository.GetAll(StaticData.RssFeed);

				foreach (var item in items)
				{
					FeedItems.Add(item);
				}

				LastFeedItem = FeedItems[0];

				PodcastTitle = LastFeedItem.Title;
				PodcastDate = LastFeedItem.PublishDate;

				if(LastFeedItem.FileImage != null)
				{
					FileImage = LastFeedItem.FileImage;
				}
				else
				{
					FileImage = "empty_image.png";
				}
			}
			catch (Exception ex)
			{
				var page = new ContentPage();
				var result = page.DisplayAlert("Error", "Unable to load podcast feed. " + ex.Message, "OK");

				Insights.Report(ex, new Dictionary<string, string> { 
					{"Error", "Unable to load podcast feed."},
					{"Message", ex.Message},
					{"Result", result.ToString()}
				});
			}

			IsBusy = false;
			IsNotBusy = true;
		}
		#endregion
	}
}