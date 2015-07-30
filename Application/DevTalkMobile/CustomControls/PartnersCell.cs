using System;
using Xamarin.Forms;

namespace DevTalkMobile
{
	public class PartnersCell : ViewCell
	{
		public PartnersCell ()
		{
			BoxView spacer = new BoxView () 
			{
				HeightRequest = 1,
				BackgroundColor = Color.FromHex("#FF6600"),
			};

			Label titleLabel = new Label()
			{
				FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label)),
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.End,
				TextColor = Color.FromHex("#000000"),
			};
			titleLabel.SetBinding(Label.TextProperty, "Title");

			Image logo = new Image () 
			{
				Aspect = Aspect.AspectFit,
			};
			logo.SetBinding (Image.SourceProperty, "Logo");

			StackLayout mainLayout = new StackLayout()
			{
				Orientation = StackOrientation.Vertical,
				VerticalOptions = LayoutOptions.StartAndExpand,
				BackgroundColor = Color.FromHex("#ffffff"),
				Children = 
				{
					titleLabel,
					logo,
					spacer
				}
			};

			#region Platform Specific Code
			Device.OnPlatform (
				iOS: () => {
					titleLabel.FontFamily = "HelveticaNeue-Thin";
				},
				Android: () => {
					titleLabel.FontFamily = "sans-serif-condensed";
				}
			);
			#endregion

			View = mainLayout;
		}
	}
}