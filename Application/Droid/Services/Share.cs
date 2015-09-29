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
	}
}