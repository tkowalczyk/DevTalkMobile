using System;
using Xamarin.Forms;

namespace DevTalkMobile.CustomControls
{
	public class FeedItemCell : ViewCell
	{
		public FeedItemCell ()
		{
			Label titleLabel = new Label()
			{
				FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label)),
				VerticalOptions = LayoutOptions.Center,
			};
			titleLabel.SetBinding(Label.TextProperty, "Title");

			Label subTitleLabel = new Label()
			{
				FontSize = Device.GetNamedSize (NamedSize.Micro, typeof(Label)),
				VerticalOptions = LayoutOptions.Center,
			};
			subTitleLabel.SetBinding(Label.TextProperty, "PublishDate");

			StackLayout mainLayout = new StackLayout()
			{
				Orientation = StackOrientation.Vertical,
				VerticalOptions = LayoutOptions.StartAndExpand,
				Children = 
				{
					titleLabel,
					subTitleLabel,
				}
			};

			View = mainLayout;
		}
	}
}