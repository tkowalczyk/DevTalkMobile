﻿using System;
using System.Net;
using CoreFoundation;
using DevTalkMobile.Services;
using SystemConfiguration;

namespace DevTalkMobile.iOS
{
	public static class Reachability
	{
		/// <summary>
		/// The ad hoc wi fi network reachability
		/// </summary>
		private static NetworkReachability adHocWiFiNetworkReachability;

		/// <summary>
		/// The default route reachability
		/// </summary>
		private static NetworkReachability defaultRouteReachability;

		/// <summary>
		/// The remote host reachability
		/// </summary>
		private static NetworkReachability remoteHostReachability;

		/// <summary>
		/// Checks if network is reachable without requiring connection.
		/// </summary>
		/// <param name="flags">The reachability flags.</param>
		/// <returns>True if reachable, false if connection is required.</returns>
		public static bool IsReachableWithoutRequiringConnection(NetworkReachabilityFlags flags)
		{
			return (flags & NetworkReachabilityFlags.Reachable) != 0
				&& (((flags & NetworkReachabilityFlags.IsWWAN) != 0)
					|| (flags & NetworkReachabilityFlags.ConnectionRequired) == 0);
		}

		/// <summary>
		/// Determines if host is reachable.
		/// </summary>
		/// <param name="host">The host address.</param>
		/// <returns>True if host is reachable, otherwise false.</returns>
		public static bool IsHostReachable(string host)
		{
			if (string.IsNullOrEmpty(host))
			{
				return false;
			}

			using (var r = new NetworkReachability(host))
			{
				NetworkReachabilityFlags flags;

				if (r.TryGetFlags(out flags))
				{
					return IsReachableWithoutRequiringConnection(flags);
				}
			}
			return false;
		}

		/// <summary>
		/// The reachability changed event.
		/// </summary>
		/// <remarks>Raised every time there is an interesting reachable event,
		/// we do not even pass the info as to what changed, and
		/// we lump all three status we probe into one.</remarks>
		public static event EventHandler ReachabilityChanged;

		/// <summary>
		/// Determines if AdHoc WiFi network is available.
		/// </summary>
		/// <param name="flags">Optional extra network reachability flags.</param>
		/// <returns>Returns true if it is possible to reach the AdHoc WiFi network, otherwise false.</returns>
		public static bool IsAdHocWiFiNetworkAvailable(out NetworkReachabilityFlags flags)
		{
			if (adHocWiFiNetworkReachability == null)
			{
				adHocWiFiNetworkReachability = new NetworkReachability(new IPAddress(new byte[] { 169, 254, 0, 0 }));
				adHocWiFiNetworkReachability.SetNotification(OnChange);
				adHocWiFiNetworkReachability.Schedule(CFRunLoop.Current, CFRunLoop.ModeDefault);
			}

			if (!adHocWiFiNetworkReachability.TryGetFlags(out flags))
			{
				return false;
			}

			return IsReachableWithoutRequiringConnection(flags);
		}

		/// <summary>
		/// The remote host status.
		/// </summary>
		/// <param name="hostName">The host name.</param>
		/// <returns>The <see cref="NetworkStatus" />.</returns>
		public static ConnectionType RemoteHostStatus(string hostName)
		{
			NetworkReachabilityFlags flags;
			bool reachable;

			if (remoteHostReachability == null)
			{
				remoteHostReachability = new NetworkReachability(hostName);

				// Need to probe before we queue, or we wont get any meaningful values
				// this only happens when you create NetworkReachability from a hostname
				reachable = remoteHostReachability.TryGetFlags(out flags);
				remoteHostReachability.SetNotification(OnChange);
				remoteHostReachability.Schedule(CFRunLoop.Current, CFRunLoop.ModeDefault);
			}
			else
			{
				reachable = remoteHostReachability.TryGetFlags(out flags);
			}

			if (!reachable)
			{
				return ConnectionType.NotReachable;
			}

			if (!IsReachableWithoutRequiringConnection(flags))
			{
				return ConnectionType.NotReachable;
			}

			return (flags & NetworkReachabilityFlags.IsWWAN) != 0
				? ConnectionType.ReachableViaCarrierDataNetwork
					: ConnectionType.ReachableViaWiFiNetwork;
		}

		/// <summary>
		/// The internet connection status.
		/// </summary>
		/// <returns>The <see cref="NetworkStatus" />.</returns>
		public static ConnectionType InternetConnectionStatus()
		{
			NetworkReachabilityFlags flags;

			if ((IsNetworkAvailable(out flags) && ((flags & NetworkReachabilityFlags.IsDirect) != 0)) || flags == 0)
			{
				return ConnectionType.NotReachable;
			}

			return (flags & NetworkReachabilityFlags.IsWWAN) != 0
				? ConnectionType.ReachableViaCarrierDataNetwork
					: ConnectionType.ReachableViaWiFiNetwork;
		}

		/// <summary>
		/// The local WiFi connection status.
		/// </summary>
		/// <returns>The <see cref="NetworkStatus" />.</returns>
		public static ConnectionType LocalWifiConnectionStatus()
		{
			NetworkReachabilityFlags flags;
			return (!IsAdHocWiFiNetworkAvailable(out flags) || (flags & NetworkReachabilityFlags.IsDirect) == 0)
				? ConnectionType.NotReachable
					: ConnectionType.ReachableViaWiFiNetwork;
		}

		/// <summary>
		/// Called when [change].
		/// </summary>
		/// <param name="flags">The flags.</param>
		private static void OnChange(NetworkReachabilityFlags flags)
		{
			var h = ReachabilityChanged;
			if (h != null)
			{
				h(null, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Returns network reachability flags and network availability.
		/// </summary>
		/// <param name="flags">The network reachability flags.</param>
		/// <returns>True if network is available, otherwise false.</returns>
		public static bool IsNetworkAvailable(out NetworkReachabilityFlags flags)
		{
			if (defaultRouteReachability == null)
			{
				defaultRouteReachability = new NetworkReachability(new IPAddress(0));
				defaultRouteReachability.SetNotification(OnChange);
				defaultRouteReachability.Schedule(CFRunLoop.Current, CFRunLoop.ModeDefault);
			}

			return defaultRouteReachability.TryGetFlags(out flags) && IsReachableWithoutRequiringConnection(flags);
		}
	}
}