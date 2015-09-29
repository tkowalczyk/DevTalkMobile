﻿using System;
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
		#endregion

		#region Commands
		private Command playCommand;
		public Command PlayCommand
		{
			get { return playCommand ?? (playCommand = new Command(async () => 
				await ExecutePlayCommand())); 
			}
		}

		private Command pauseCommand;
		public Command PauseCommand
		{
			get { return pauseCommand ?? (pauseCommand = new Command(async () => 
				await ExecutePauseCommand())); 
			}
		}

		private Command stopCommand;
		public Command StopCommand
		{
			get { return stopCommand ?? (stopCommand = new Command(async () => 
				await ExecuteStopCommand())); 
			}
		}

		private Command<FeedItem> getSelectedItemInfoCommand;
		public Command<FeedItem> GetSelectedItemInfoCommand
		{
			get { return getSelectedItemInfoCommand ?? (getSelectedItemInfoCommand = new Command<FeedItem>(async item => 
				await ExecuteGetSelectedItemInfoCommand(item))); 
			}
		}
		#endregion

		#region Private Methods
		private async Task ExecutePlayCommand()
		{
			_soundService.Play ();
		}

		private async Task ExecutePauseCommand()
		{
			_soundService.Pause ();
		}

		private async Task ExecuteStopCommand()
		{
			_soundService.Stop ();
		}

		private async Task ExecuteGetSelectedItemInfoCommand(FeedItem item)
		{
			PodcastTitle = item.Title;
			PodcastWebPage = item.BlogPost;
			PodcastDate = item.PublishDate;
		}
		#endregion
	}
}