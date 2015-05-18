using System;
using Android.Content;
using DevTalkMobile.Droid;
using DevTalkMobile.Services;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency (typeof(Share))]
namespace DevTalkMobile.Droid
{
	public class Share : IShare
	{
		public void ShareText (string text)
		{
			var intent = new Intent (Intent.ActionSend);
			intent.SetType ("text/plain");
			intent.PutExtra (Intent.ExtraText, text);
			Forms.Context.StartActivity (Intent.CreateChooser (intent, "Share"));
		}

		public void LaunchBrowser (string url)
		{
			if (string.IsNullOrWhiteSpace (url))
				return;
			try {
				var intent = new Intent (Intent.ActionView);
				intent.SetData (Android.Net.Uri.Parse (url));
				Forms.Context.StartActivity (intent);

			}
			catch (Exception ex) {
			}
		}
	}
}