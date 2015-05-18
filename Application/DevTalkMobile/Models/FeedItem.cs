using System;
using System.Text.RegularExpressions;

namespace DevTalkMobile.Models
{
	public class FeedItem : BaseChangedModel
	{
		private const int MAX_TITLE_LENGTH = 35;
		private const int MAX_DESC_LENGTH = 65;

		public FeedItem ()
		{
		}

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
					publishDate = time.ToLocalTime().ToString ("D");
				else
					publishDate = value;
			}
		}

		private decimal progress = 0.0M;
		public decimal Progress
		{
			get { return progress; }
			set { progress = value; OnPropertyChanged("Progress"); }
		}

		public string Category { get; set; }
		public string Mp3Url { get; set; }
		public int Id { get; set; }
		public string FileImage { get; set; }
	}
}