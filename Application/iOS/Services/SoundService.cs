using System;
using System.Threading.Tasks;

using DevTalkMobile.Services;
using DevTalkMobile.iOS;
using AVFoundation;

[assembly: Xamarin.Forms.Dependency (typeof (SoundService))]
namespace DevTalkMobile.iOS
{
	public class SoundService : ISoundService
	{
		private AVPlayer player;
		private bool paused;

		public SoundService ()
		{
		}

		#region ISoundService implementation

		private void IntializePlayer()
		{
			player = new AVPlayer();
		}

		public Task<bool> Play (string pathToFile)
		{
			throw new NotImplementedException ();
		}

		public bool Stop ()
		{
			throw new NotImplementedException ();
		}

		public bool Pause ()
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}