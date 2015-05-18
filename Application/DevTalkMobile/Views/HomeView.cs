using System;
using DevTalkMobile.Helpers;
using DevTalkMobile.Services;
using DevTalkMobile.ViewModels;
using Xamarin.Forms;

namespace DevTalkMobile.Views
{
	public class HomeView : BaseView<HomeViewModel>
	{
		#region Ctor
		public HomeView ()
		{
			this.Title = "DevTalk";

			var layout = new RelativeLayout ();

			var picture = new Image () {
				Aspect = Aspect.AspectFill,
				Source = new FileImageSource () { File = "" }
			};

			var title = new Label () {
				FontSize = 25, 
				TextColor = Color.FromHex ("#FF6600"),
				FontFamily = Device.OnPlatform ("HelveticaNeue-Medium", "", "")
			};
			title.SetBinding(Label.TextProperty, "Title");

			var category = new Label ();
			category.SetBinding(Label.TextProperty, "Category");

			var scovilleLabel = new Label () {
				Text = "Date:", 
				FontSize = 15, 
				TextColor = Color.FromHex ("#B7A19B"),
				FontFamily = Device.OnPlatform ("HelveticaNeue-CondensedBlack", "sans-serif-condensed", "")
			};

			var date = new Label () {
				FontSize = 20, 
				FontFamily = Device.OnPlatform ("HelveticaNeue", "sans-serif", "")
			};
			date.SetBinding(Label.TextProperty, "PodcastDate");

			var details = new StackLayout () {
				Spacing = 10,
				Padding = new Thickness (20, 10),
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = {
					title,
					category,
					scovilleLabel,
					date,
				}
			};

			layout.Children.Add (
				picture,
				Constraint.Constant (0),
				Constraint.Constant (0),
				Constraint.RelativeToParent ((parent) => {
					return parent.Width;
				}),
				Constraint.RelativeToParent ((parent) => {
					return parent.Height * .5;
				})
			);

			layout.Children.Add (
				details,
				Constraint.Constant (0),
				Constraint.RelativeToParent ((parent) => {
					return parent.Height * .5;
				}),
				Constraint.RelativeToParent ((parent) => {
					return parent.Width;
				}),
				Constraint.RelativeToParent ((parent) => {
					return parent.Height;
				})
			);

			this.Content = layout;

			// Accomodate iPhone status bar.
			this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
		}
		#endregion

		#region Overrides
		protected override void OnAppearing()
		{
			base.OnAppearing ();

			if (ViewModel == null || !ViewModel.CanLoadMore || ViewModel.IsBusy || ViewModel.FeedItems.Count > 0)
				return;

			if (this.TypeOfConnection == ConnectionType.NotReachable) 
			{
				DisplayAlert(
					StaticData.ConnectionHeader,
					StaticData.ConnectionMessage,
					StaticData.ConnectionButton
				);
			}
			else 
			{
				ViewModel.LoadItemsCommand.Execute(null);
			}
		}
		#endregion
	}
}