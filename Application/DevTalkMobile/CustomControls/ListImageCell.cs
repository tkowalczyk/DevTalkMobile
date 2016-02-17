using System;
using Xamarin.Forms;

namespace DevTalkMobile.CustomControls
{
	public class ListImageCell : ViewCell 
	{
		public ListImageCell()
		{
			Label titleLabel = new Label()
			{
				FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label)),
				VerticalOptions = LayoutOptions.Center,
			};
			titleLabel.SetBinding(Label.TextProperty, "Title");

			Image image = new Image()
			{
				VerticalOptions = LayoutOptions.Center,
			};
			image.SetBinding(Image.SourceProperty, "Icon");

			StackLayout mainLayout = new StackLayout()
			{
				Orientation = StackOrientation.Horizontal,
				VerticalOptions = LayoutOptions.StartAndExpand,
				BackgroundColor = Color.Transparent,
				Children = 
				{
					image,
					titleLabel
				}
			};

			View = mainLayout;
		}
	}
}