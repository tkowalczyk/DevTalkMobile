using Android.App;
using Android.Content;
using Android.OS;
using Android.Content.PM;

namespace DevTalkMobile.Droid
{
	[Activity(Label = "DevTalk", MainLauncher = true, NoHistory = true, Theme = "@style/Theme.Splash", 
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]			
	public class SplashScreen : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			var intent = new Intent(this, typeof(MainActivity));
			StartActivity(intent);
		}
	}
}