using System;
using System.Threading.Tasks;

using DevTalkMobile.Services;
using DevTalkMobile.iOS;

[assembly: Xamarin.Forms.Dependency (typeof (SoundService))]
namespace DevTalkMobile.iOS
{
	public class SoundService : ISoundService
	{
		private bool paused;

		public SoundService ()
		{
		}

		#region ISoundService implementation

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