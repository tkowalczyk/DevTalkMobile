using System;
using DevTalkMobile.Services;
using Xamarin.Forms;
using DevTalkMobile.iOS;
using System.Threading.Tasks;

[assembly: Dependency(typeof(AppleConnection))]
namespace DevTalkMobile.iOS
{
	public class AppleConnection : IConnectivity
	{
		public AppleConnection () { }

		#region IConnectivity implementation

		private event Action<ConnectionType> reachabilityChanged;

		void HandleReachabilityChanged (object sender, EventArgs e)
		{
			var reachabilityChanged = this.reachabilityChanged;
			if (reachabilityChanged != null)
				reachabilityChanged(this.InternetConnectionStatus());
		}

		public event Action<ConnectionType> ReachabilityChanged
		{
			add
			{
				if (this.reachabilityChanged == null)
				{
					// TODO: check if this actually works
					Reachability.ReachabilityChanged += HandleReachabilityChanged;
				}

				this.reachabilityChanged += value;
			}

			remove
			{
				this.reachabilityChanged -= value;

				if (this.reachabilityChanged == null)
				{
					Reachability.ReachabilityChanged -= HandleReachabilityChanged;
				}
			}
		}

		public ConnectionType InternetConnectionStatus()
		{
			return Reachability.InternetConnectionStatus();
		}

		public Task<bool> IsReachable(string host, TimeSpan timeout)
		{
			return Task<bool>.Run(() => Reachability.IsHostReachable(host));
		}

		public Task<bool> IsReachableByWifi(string host, TimeSpan timeout)
		{
			return Task<bool>.Run(() => Reachability.RemoteHostStatus(host) == ConnectionType.ReachableViaWiFiNetwork);
		}

		#endregion
	}
}