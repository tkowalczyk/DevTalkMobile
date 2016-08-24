using System.Collections.Generic;
using System.Threading.Tasks;

using DevTalkMobile.Models;

namespace DevTalkMobile.Services
{
	public interface IFeedRepository
	{
		Task<List<FeedItem>> GetAll(string rss);
		Task<List<FeedItem>> GetFilteredFeed(string rss, string filter);
	}
}