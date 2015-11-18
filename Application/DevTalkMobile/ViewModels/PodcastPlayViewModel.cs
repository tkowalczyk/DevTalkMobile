using System;
using System.Threading.Tasks;
using Xamarin.Forms;

using DevTalkMobile.ViewModels;
using DevTalkMobile.Services;
using DevTalkMobile.Models;

namespace DevTalkMobile.ViewModels
{
	public class PodcastPlayViewModel : BaseViewModel
	{
		#region Fields
		private ISoundService _soundService;
		#endregion

		#region Ctor
		public PodcastPlayViewModel ()
		{
			_soundService = DependencyService.Get<ISoundService> ();
		}
		#endregion

		#region Properties
		private string podcastTitle = string.Empty;
		public const string PodcastTitlePropertyName = "PodcastTitle";
		public string PodcastTitle
		{
			get { return podcastTitle; }
			set { SetProperty(ref podcastTitle, value, PodcastTitlePropertyName); }
		}

		private string podcastWebPage;
		public string PodcastWebPage
		{
			get { return podcastWebPage; }
			set { podcastWebPage = value; OnPropertyChanged("PodcastWebPage"); }
		}

		private string podcastDate;
		public string PodcastDate
		{
			get { return podcastDate; }
			set { podcastDate = value; OnPropertyChanged("PodcastDate"); }
		}

		private FeedItem selectedFeedItem;
		public FeedItem SelectedFeedItem
		{
			get { return selectedFeedItem; }
			set { selectedFeedItem = value; OnPropertyChanged("SelectedFeedItem"); }
		}

		private double podcastCurrentLength;
		public double PodcastCurrentLength
		{
			get { return podcastCurrentLength; }
			set { podcastCurrentLength = value; OnPropertyChanged("PodcastCurrentLength"); }
		}

		private string podcastDuration;
		public string PodcastDuration
		{
			get { return podcastDuration; }
			set { podcastDuration = value; OnPropertyChanged("PodcastDuration"); }
		}
		#endregion

		#region Commands
		private Command<string> playCommand;
		public Command<string> PlayCommand
		{
			get { return playCommand ?? (playCommand = new Command<string>(async pathToFile => 
				await ExecutePlayCommand(pathToFile))); 
			}
		}

		private Command pauseCommand;
		public Command PauseCommand
		{
			get { return pauseCommand ?? (pauseCommand = new Command(() => 
				ExecutePauseCommand())); 
			}
		}

		private Command stopCommand;
		public Command StopCommand
		{
			get { return stopCommand ?? (stopCommand = new Command(() => 
				ExecuteStopCommand())); 
			}
		}

		private Command<FeedItem> getSelectedItemInfoCommand;
		public Command<FeedItem> GetSelectedItemInfoCommand
		{
			get { return getSelectedItemInfoCommand ?? (getSelectedItemInfoCommand = new Command<FeedItem>(item => 
				ExecuteGetSelectedItemInfoCommand(item))); 
			}
		}
		#endregion

		#region Private Methods
		private async Task ExecutePlayCommand(string pathToFile)
		{
			await _soundService.Play (pathToFile);
		}

		private void ExecutePauseCommand()
		{
			_soundService.Pause ();
		}

		private void ExecuteStopCommand()
		{
			_soundService.Stop ();
		}

		private void ExecuteGetSelectedItemInfoCommand(FeedItem item)
		{
			PodcastTitle = item.Title;
			PodcastWebPage = item.BlogPost;
			PodcastDate = item.PublishDate;
		}
		#endregion
	}
}