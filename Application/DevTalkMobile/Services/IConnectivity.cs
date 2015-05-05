using System;
using System.Threading.Tasks;

namespace DevTalkMobile.Services
{
	public interface IConnectivity
	{
		Task<bool> IsReachable(string host, TimeSpan timeout);
		Task<bool> IsReachableByWifi(string host, TimeSpan timeout);
		ConnectionType InternetConnectionStatus();
		event Action<ConnectionType> ReachabilityChanged;
	}
}