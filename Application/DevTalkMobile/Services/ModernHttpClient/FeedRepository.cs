using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

using DevTalkMobile.Models;
using DevTalkMobile.Services;
using DevTalkMobile.Helpers;
using ModernHttpClient;

namespace DevTalkMobile.Services.ModernHttpClient
{
	public class FeedRepository : IFeedRepository
	{
		public FeedRepository ()
		{
		}

		#region IFeedRepository implementation

		public async Task<List<FeedItem>> GetAll(string rss)
		{
			XNamespace content = "http://purl.org/rss/1.0/modules/content/";

			var httpClient = new HttpClient (new NativeMessageHandler ());

			var responseString = await httpClient.GetStringAsync(rss);

			return await Task.Run(() =>
				{
					var xdoc = XDocument.Parse(responseString);
					var id = 0;
					return (from item in xdoc.Descendants("item")
						select new FeedItem
						{
							Title = (string)item.Element("title"),
							Description = (string)item.Element("description"),
							DescriptionLongHtml = (string)item.Element("description"),
							FileImage = (string)item.Element(content.GetName("encoded")).Value.GetImageFile(),
							Link = (string)item.Element("link"),
							PublishDate = (string)item.Element("pubDate"),
							Mp3Url = (string)item.Element(content.GetName("encoded")).Value.GetLinkToMp3File(),
							BlogPost = (string)item.Element("link"),
							Id = id++
						}).ToList();
				});
		}

		public async Task<List<FeedItem>> GetFilteredFeed(string rss, string filter)
		{
			XNamespace content = "http://purl.org/rss/1.0/modules/content/";

			var httpClient = new HttpClient(new NativeMessageHandler());

			var responseString = await httpClient.GetStringAsync(rss);

			return await Task.Run(() =>
				{
					var xdoc = XDocument.Parse(responseString);
					var id = 0;
					return (from item in xdoc.Descendants("item")
						select new FeedItem
						{
							Title = (string)item.Element("title"),
							Description = (string)item.Element("description"),
							DescriptionLongHtml = (string)item.Element("description"),
							FileImage = (string)item.Element(content.GetName("encoded")).Value.GetImageFile(),
							Link = (string)item.Element("link"),
							PublishDate = (string)item.Element("pubDate"),
							Mp3Url = (string)item.Element(content.GetName("encoded")).Value.GetLinkToMp3File(),
							BlogPost = (string)item.Element("link"),
							Id = id++
						}).ToList().Where(item => item.Title.ToLower().Contains(filter.ToLower())).ToList();
				});
		}
		#endregion
	}
}