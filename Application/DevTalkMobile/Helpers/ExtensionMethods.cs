using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
	}
}