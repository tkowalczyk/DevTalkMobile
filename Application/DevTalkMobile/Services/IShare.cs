﻿using System;

namespace DevTalkMobile.Services
{
	public interface IShare
	{
		void ShareText(string text);
		void LaunchBrowser(string url);
	}
}