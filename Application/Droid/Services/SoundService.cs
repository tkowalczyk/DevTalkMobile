using System;

using DevTalkMobile.Services;
using DevTalkMobile.Droid;

using Android.Media;
using Android.Content;
using Android.OS;

[assembly: Xamarin.Forms.Dependency (typeof (SoundService))]
namespace DevTalkMobile.Droid
{
	public class SoundService : ISoundService
	{
		private MediaPlayer player;
		private bool paused;

		private const string Mp3 = @"http://www.montemagno.com/sample.mp3";

		public SoundService ()
		{
			
		}

		#region IAudioOperations implementation

		private void IntializePlayer()
		{
			player = new MediaPlayer();

			//Tell our player to sream music
			player.SetAudioStreamType(Stream.Music);

			//When we have prepared the song start playback
			player.Prepared += (sender, args) => player.Start();

			//When we have reached the end of the song stop ourselves, however you could signal next track here.
			player.Completion += (sender, args) => Stop();

			player.Error += (sender, args) =>
			{
				//playback error
				Console.WriteLine("Error in playback resetting: " + args.What);
				Stop();//this will clean up and reset properly.
			};
		}

		public void SetSource (string pathToFile)
		{
			
		}

		public void Play ()
		{
			if (paused && player != null) {
				paused = false;
				//We are simply paused so just start again
				player.Start();
				return;
			}

			if (player == null) {
				IntializePlayer();
			}

			if (player.IsPlaying)
				return;

			try {
				player.SetDataSourceAsync(global::Android.App.Application.Context, Android.Net.Uri.Parse(Mp3));

				player.PrepareAsync();
			}
			catch (Exception ex) {
				//unable to start playback log error
				Console.WriteLine("Unable to start playback: " + ex);
			}
		}

		public void Stop ()
		{
			if (player == null)
				return;

			if(player.IsPlaying)
				player.Stop();

			player.Reset();
			paused = false;
		}

		public void Pause ()
		{
			if (player == null)
				return;

			if(player.IsPlaying)
				player.Pause();

			paused = true;
		}

		#endregion
	}
}