using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using HtmlAgilityPack;

namespace DevTalkMobile.Helpers
{
	public static class ExtensionMethods
	{
		public static string GetImageFile(this string input)
		{
			return (from img in GetImagesInHTMLString(input)
				where img.Contains("-150x150")
				select img).FirstOrDefault();
		}

		public static List<string> GetImagesInHTMLString(string htmlString) {
			List<string> images = new List<string>();
			string pattern = @"(?<=src="").*?(?="")";

			Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
			MatchCollection matches = rgx.Matches(htmlString);

			for(int i=0, l=matches.Count; i<l; i++) {
				images.Add(matches[i].Value);
			}

			return images;
		}

		public static string GetLinkToMp3File(this string input)
		{
			return (from link in GetLinksFromHTMLString(input)
				where link.Contains(".mp3")
				select link).FirstOrDefault();
		}

		public static List<string> GetLinksFromHTMLString(string htmlString) {
			List<string> links = new List<string>();
			string pattern = @"(?<=href="").*?(?="")";

			Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
			MatchCollection matches = rgx.Matches(htmlString);

			for(int i=0, l=matches.Count; i<l; i++) {
				links.Add(matches[i].Value);
			}

			return links;
		}

		public static List<Partner> GetPartners(string htmlContent)
		{
			List<Partner> partnersList = new List<Partner> ();

			var devTalkDocument = new HtmlDocument();
			devTalkDocument.LoadHtml(htmlContent);

			IEnumerable<HtmlNode> partnersWidget = devTalkDocument.DocumentNode.Descendants("div")
				.Where(d => 
					d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("textwidget")
				)
				.FirstOrDefault() //Find first div with textwidget class
				.ChildNodes //Find its p elements
				.Skip(1) //Skip the first p 
				.Where (child =>
					child.NodeType == HtmlNodeType.Element);

			foreach (HtmlNode child in partnersWidget)
			{
				RemoveComments (child);

				if (child.InnerHtml != string.Empty) 
				{
					HtmlNode imgs = child.Descendants ("img").Where (d => 
					d.Attributes.Contains ("src")).Single ();

					Partner partner = new Partner () 
					{
						Logo = StaticData.DevTalkWeb + imgs.Attributes["src"].Value,
						Title = imgs.Attributes["title"].Value
					};

					partnersList.Add (partner);
				}
			}

			return partnersList;
		}

		public static void RemoveComments(HtmlNode node)
		{
			foreach (var n in node.ChildNodes.ToArray())
				RemoveComments(n);
			if (node.NodeType == HtmlNodeType.Comment)
				node.Remove();
		}
	}
}