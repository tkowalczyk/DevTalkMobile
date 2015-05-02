using System;
using System.Text.RegularExpressions;

namespace DevTalkMobile.Models
{
	public class FeedItem : BaseChangedModel
	{
		public FeedItem ()
		{
		}

		public string Title { get; set; }
		public string Description { get; set; }
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
	}
}