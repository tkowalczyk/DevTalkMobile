using System;
using DevTalkMobile.Services;
using DevTalkMobile.Services.ModernHttpClient;
using System.Collections.ObjectModel;
using DevTalkMobile.Models;
using Xamarin.Forms;
using DevTalkMobile.Helpers;
using System.Threading.Tasks;

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

		private string title;
		public string Title
		{
			get { return title; }
			set { title = value; OnPropertyChanged("Title"); }
		}

		private string category;
		public string Category
		{
			get { return category; }
			set { category = value; OnPropertyChanged("Category"); }
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

				Title = LastFeedItem.Title;
				PodcastDate = LastFeedItem.PublishDate;
				Category = LastFeedItem.Category;
				FileImage = LastFeedItem.FileImage;
			}
			catch (Exception ex)
			{
				var page = new ContentPage();
				var result = page.DisplayAlert("Error", "Unable to load podcast feed. " + ex.Message, "OK");
			}

			IsBusy = false;
			IsNotBusy = true;
		}
		#endregion
	}
}