using System;
using DevTalkMobile.iOS;
using DevTalkMobile.Services;
using Foundation;
using UIKit;

[assembly: Xamarin.Forms.Dependency (typeof(Share))]
namespace DevTalkMobile.iOS
{
	public class Share : IShare
	{
		public void ShareText (string text)
		{
			var items = new NSObject[] { new NSString (text) };
			var activityController = new UIActivityViewController (items, null);
			UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewControllerAsync(activityController, true);
		}
	}
}