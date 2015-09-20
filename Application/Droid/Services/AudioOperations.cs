using System;

using DevTalkMobile.Services;
using DevTalkMobile.Droid;
using Android.Media;

[assembly: Xamarin.Forms.Dependency (typeof (AudioOperations))]
namespace DevTalkMobile.Droid
{
	public class AudioOperations : IAudioOperations
	{
		private MediaPlayer _mediaPlayer;

		public AudioOperations ()
		{
		}

		#region IAudioOperations implementation

		public void SetSource (string pathToFile)
		{
			_mediaPlayer = MediaPlayer.Create(
				global::Android.App.Application.Context, 
				global::Android.Net.Uri.Parse(pathToFile)
			);
		}

		public void Play ()
		{
			_mediaPlayer.Start();
		}

		public void Stop ()
		{
			_mediaPlayer.Stop ();
		}

		public void Pause ()
		{
			_mediaPlayer.Pause ();
		}

		#endregion
	}
}