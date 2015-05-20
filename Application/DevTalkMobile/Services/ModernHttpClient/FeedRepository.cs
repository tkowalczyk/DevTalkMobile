﻿using System;
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
						let enclosure = item.Element("enclosure")
						where enclosure != null
						select new FeedItem
						{
							Title = (string)item.Element("title"),
							Description = (string)item.Element("description"),
							DescriptionLongHtml = (string)item.Element("description"),
							FileImage = (string)item.Element(content.GetName("encoded")).Value.GetImageFile(),
							Link = (string)item.Element("link"),
							PublishDate = (string)item.Element("pubDate"),
							Category = (string)item.Element("category"),
							Mp3Url = (string)enclosure.Attribute("url"),
							Id = id++
						}).ToList();
				});
		}

		public async Task<List<FeedItem>> GetFilteredFeed(string rss, string filter)
		{
			var httpClient = new HttpClient(new NativeMessageHandler());

			var responseString = await httpClient.GetStringAsync(rss);

			return await Task.Run(() =>
				{
					var xdoc = XDocument.Parse(responseString);
					var id = 0;
					return (from item in xdoc.Descendants("item")
						let enclosure = item.Element("enclosure")
						where enclosure != null
						select new FeedItem
						{
							Title = (string)item.Element("title"),
							Description = (string)item.Element("description"),
							Link = (string)item.Element("link"),
							PublishDate = (string)item.Element("pubDate"),
							Category = (string)item.Element("category"),
							Mp3Url = (string)enclosure.Attribute("url"),
							Id = id++
						}).ToList().Where(item => item.Title.ToLower().Contains(filter.ToLower())).ToList();
				});
		}
		#endregion
	}
}