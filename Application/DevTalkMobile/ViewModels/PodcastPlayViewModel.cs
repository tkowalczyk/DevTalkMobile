﻿using System.Threading.Tasks;

using DevTalkMobile.Services;
using DevTalkMobile.Models;

using Xamarin.Forms;

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

		private bool isPlayEnabled = true;
		public bool IsPlayEnabled
		{
			get { return isPlayEnabled; }
			set { isPlayEnabled = value; OnPropertyChanged("IsPlayEnabled"); }
		}

		private bool isStopEnabled = false;
		public bool IsStopEnabled
		{
			get { return isStopEnabled; }
			set { isStopEnabled = value; OnPropertyChanged("IsStopEnabled"); }
		}

		private bool isPauseEnabled = false;
		public bool IsPauseEnabled
		{
			get { return isPauseEnabled; }
			set { isPauseEnabled = value; OnPropertyChanged("IsPauseEnabled"); }
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
			if (!string.IsNullOrWhiteSpace (pathToFile)) 
			{
				await _soundService.Play (pathToFile);
				IsPlayEnabled = false;
				IsStopEnabled = true;
				IsPauseEnabled = true;
			}
		}

		private void ExecutePauseCommand()
		{
			_soundService.Pause ();
			IsPlayEnabled = true;
			IsStopEnabled = false;
			IsPauseEnabled = false;
		}

		private void ExecuteStopCommand()
		{
			_soundService.Stop ();
			IsPlayEnabled = true;
			IsStopEnabled = false;
			IsPauseEnabled = false;
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