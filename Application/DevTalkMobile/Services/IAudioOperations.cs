using System;

namespace DevTalkMobile.Services
{
	public interface IAudioOperations
	{
		void SetSource(string pathToFile);
		void Play();
		void Stop();
		void Pause();
	}
}