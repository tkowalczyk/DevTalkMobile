using System;

namespace DevTalkMobile.Services
{
	public interface ISoundService
	{
		void SetSource(string pathToFile);
		void Play();
		void Stop();
		void Pause();
	}
}