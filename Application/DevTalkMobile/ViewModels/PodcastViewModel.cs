using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DevTalkMobile.Helpers;
using DevTalkMobile.Models;
using DevTalkMobile.Services;
using DevTalkMobile.Services.ModernHttpClient;
using Xamarin.Forms;

namespace DevTalkMobile.ViewModels
{
	public class PodcastViewModel : BaseViewModel
	{
		#region Fields
		private IFeedRepository _feedRepository;
		#endregion

		#region Ctor
		public PodcastViewModel ()
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

		private FeedItem selectedFeedItem;
		public FeedItem SelectedFeedItem
		{
			get { return selectedFeedItem; }
			set { selectedFeedItem = value; OnPropertyChanged("SelectedFeedItem"); }
		}
		#endregion

		#region Commands
		private Command loadItemsCommand;
		public Command LoadItemsCommand
		{
			get { return loadItemsCommand ?? (loadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand())); }
		}

		private Command<string> loadFilteredItemsCommand;
		public Command<string> LoadFilteredItemsCommand
		{
			get { return loadFilteredItemsCommand ?? (loadFilteredItemsCommand = new Command<string>(async filter => await ExecuteLoadFilteredItemsCommand(filter))); }
		}
		#endregion

		#region Private Methods
		private async Task ExecuteLoadItemsCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				FeedItems.Clear();

				var items = await _feedRepository.GetAll(StaticData.RssFeed);

				foreach (var item in items)
				{
					FeedItems.Add(item);
				}
			}
			catch (Exception ex)
			{
				var page = new ContentPage();
				var result = page.DisplayAlert("Error", "Unable to load podcast feed. " + ex.Message, "OK");
			}

			IsBusy = false;
		}

		private async Task ExecuteLoadFilteredItemsCommand(string filter)
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				FeedItems.Clear();

				var items = await _feedRepository.GetFilteredFeed(StaticData.RssFeed, filter);

				foreach (var item in items)
				{
					FeedItems.Add(item);
				}
			}
			catch (Exception ex)
			{
				var page = new ContentPage();
				var result = page.DisplayAlert("Error", "Unable to load filtered podcast feed. " + ex.Message, "OK");
			}

			IsBusy = false;
		}
		#endregion
	}
}