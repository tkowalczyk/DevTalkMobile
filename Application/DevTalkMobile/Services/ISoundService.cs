using System;
using System.Threading.Tasks;

namespace DevTalkMobile.Services
{
	public interface ISoundService
	{
		void SetSource(string pathToFile);
		Task<bool> Play();
		Task<bool> Stop();
		Task<bool> Pause();
		Task<string> GetTrackDuration(string pathToFile);
	}
}