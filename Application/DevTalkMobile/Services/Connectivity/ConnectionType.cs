using System;

namespace DevTalkMobile.Services
{
	public enum ConnectionType
	{
		/// <summary>
		/// Network not reachable.
		/// </summary>
		NotReachable,

		/// <summary>
		/// Network reachable via carrier data network.
		/// </summary>
		ReachableViaCarrierDataNetwork,

		/// <summary>
		/// Network reachable via WiFi network.
		/// </summary>
		ReachableViaWiFiNetwork
	}
}