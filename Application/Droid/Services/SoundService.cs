using System;

using DevTalkMobile.Services;
using DevTalkMobile.Droid;

using Android.Media;
using Android.Content;
using Android.OS;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency (typeof (SoundService))]
namespace DevTalkMobile.Droid
{
	public class SoundService : ISoundService
	{
		private MediaPlayer player;
		private bool paused;

		public SoundService ()
		{
			
		}

		#region IAudioOperations implementation

		private void IntializePlayer()
		{
			player = new MediaPlayer();

			//Tell our player to stream music
			player.SetAudioStreamType(Stream.Music);

			//When we have prepared the song start playback
			player.Prepared += (sender, args) => player.Start();

			//When we have reached the end of the song stop ourselves, however you could signal next track here.
			player.Completion += (sender, args) => Stop();

			player.Error += (sender, args) =>
			{
				Stop();//this will clean up and reset properly.
			};
		}

		public async Task<bool> Play (string pathToFile)
		{
			bool ret = false;

			if (paused && player != null) {
				paused = false;
				//We are simply paused so just start again
				player.Start();
				ret = false;
			}

			if (player == null) {
				IntializePlayer();

				ret = false;
			}

			if (player.IsPlaying)
				ret = true;

			try {
				await player.SetDataSourceAsync(global::Android.App.Application.Context, Android.Net.Uri.Parse(pathToFile));

				player.PrepareAsync();

				ret = true;
			}
			catch (Exception ex) {
				Console.WriteLine("Unable to start playback: " + ex);
			}

			return ret;
		}

		public bool Stop ()
		{
			bool ret = false;

			if (player == null)
				ret = false;

			if (player.IsPlaying) 
			{
				player.Stop ();
				ret = true;
			}

			player.Reset();
			paused = false;

			return ret;
		}

		public bool Pause ()
		{
			bool ret = false;

			if (player == null)
				ret  = false;

			if (player.IsPlaying) 
			{
				player.Pause ();
				ret = true;
			}

			paused = true;

			return ret;
		}

		#endregion
	}
}