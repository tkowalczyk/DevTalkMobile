using System;

using DevTalkMobile.Services;
using DevTalkMobile.Droid;

using Android.Media;
using Android.Content;

[assembly: Xamarin.Forms.Dependency (typeof (SoundService))]
namespace DevTalkMobile.Droid
{
	public class SoundService : ISoundService
	{
		public SoundService ()
		{
			
		}

		#region IAudioOperations implementation

		public void SetSource (string pathToFile)
		{
			
		}

		public void Play ()
		{
			
		}

		public void Stop ()
		{
			
		}

		public void Pause ()
		{
			
		}

		#endregion
	}
}