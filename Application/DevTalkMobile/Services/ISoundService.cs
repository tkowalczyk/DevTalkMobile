using System;
using System.Threading.Tasks;

namespace DevTalkMobile.Services
{
	public interface ISoundService
	{
		Task<bool> Play(string pathToFile);
		bool Stop();
		bool Pause();
	}
}