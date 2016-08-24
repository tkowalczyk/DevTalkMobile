using System;

namespace DevTalkMobile.Models
{
	public class FeedItem : BaseChangedModel
	{
		private const int MAX_TITLE_LENGTH = 35;
		private const int MAX_DESC_LENGTH = 65;

		private string title;
		public string Title 
		{ 
			get { return title;}
			set {
				if (!string.IsNullOrWhiteSpace(value) && value.Length > MAX_TITLE_LENGTH)
					title = value.Substring (0, MAX_TITLE_LENGTH) + "...";
				else
					title = value;
			}
		}

		public string DescriptionLongHtml { get; set; }

		private string description;
		public string Description 
		{ 
			get { return description;}
			set {
				if (!string.IsNullOrWhiteSpace(value) && value.Length > MAX_DESC_LENGTH)
					description = value.Substring (0, MAX_DESC_LENGTH) + "...";
				else
					description = value;
			}
		}

		public string Link { get; set; }

		private string publishDate;
		public string PublishDate 
		{
			get { return publishDate; }
			set{
				DateTime time; 
				if (DateTime.TryParse (value, out time))
					publishDate = time.ToLocalTime().ToString ("dd/MM/yyyy");
				else
					publishDate = value;
			}
		}

		public string Mp3Url { get; set; }
		public int Id { get; set; }
		public string FileImage { get; set; }
		public string BlogPost { get; set; }
	}
}