using System;
using System.Threading.Tasks;

namespace DevTalkMobile.Services
{
	public interface ISoundService
	{
		Task<bool> Play(string pathToFile);
		Task<bool> Stop();
		Task<bool> Pause();
		Task<string> GetTrackDuration(string pathToFile);
	}
}