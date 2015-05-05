using System;
using System.Threading.Tasks;
using Android.Net;
using DevTalkMobile.Droid;
using DevTalkMobile.Services;
using Java.Net;
using Xamarin.Forms;
using Android.Content;

[assembly: Dependency(typeof(AndroidConnection))]
namespace DevTalkMobile.Droid
{
	public class AndroidConnection : IConnectivity
	{
		public AndroidConnection () { }

		#region IConnectivity implementation

		public event Action<ConnectionType> ReachabilityChanged;

		public ConnectionType InternetConnectionStatus ()
		{
			ConnectionType status = ConnectionType.NotReachable;

			ConnectivityManager cm = (ConnectivityManager) global::Android.App.Application.Context.GetSystemService(Context.ConnectivityService);
			NetworkInfo ni = cm.ActiveNetworkInfo;

			if (ni != null && ni.IsConnectedOrConnecting)
			{
				var name = ni.TypeName.ToUpper();
				if (name.Contains("WIFI"))
				{
					status = ConnectionType.ReachableViaWiFiNetwork;
				}
				else if (name.Contains("MOBILE"))
				{
					status = ConnectionType.ReachableViaCarrierDataNetwork;
				}
			}

			return status;
		}

		public Task<bool> IsReachable(string host, TimeSpan timeout)
		{
			return Task.Run(() =>
				{
					try
					{
						var address = InetAddress.GetByName(host);

						return address != null;// && (address.IsReachable((int)timeout.TotalMilliseconds) || );
					}
					catch (Java.Net.UnknownHostException)
					{
						return false;
					}
				});
		}

		public async Task<bool> IsReachableByWifi(string host, TimeSpan timeout)
		{
			return this.InternetConnectionStatus() == ConnectionType.ReachableViaWiFiNetwork && await this.IsReachable(host, timeout);
		}

		#endregion
	}
}