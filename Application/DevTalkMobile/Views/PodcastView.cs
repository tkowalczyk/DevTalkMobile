using DevTalkMobile.CustomControls;
using DevTalkMobile.Helpers;
using DevTalkMobile.Models;
using DevTalkMobile.Services;
using DevTalkMobile.ViewModels;

using Xamarin.Forms;

namespace DevTalkMobile.Views
{
	public class PodcastView : BaseView<PodcastViewModel>
	{
		#region Ctor
		public PodcastView ()
		{
			this.Title = "Episodes";

			#region MainGrid
			Grid mainGrid = new Grid
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				RowDefinitions = 
				{
					new RowDefinition { Height = GridLength.Auto},
					new RowDefinition { Height = GridLength.Auto},
					new RowDefinition { Height = new GridLength(10, GridUnitType.Star)},
				},
				ColumnDefinitions = 
				{
					new ColumnDefinition { Width = new GridLength(10, GridUnitType.Star)}
				}
			};
			#endregion

			#region SearchBar
			SearchBar searchBar = new SearchBar
			{
				Placeholder = "Search by episode name",
			};

			searchBar.TextChanged += (sender, e) =>
			{
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
					ViewModel.LoadFilteredItemsCommand.Execute(searchBar.Text);
				}
			};

			mainGrid.Children.Add(searchBar, 0, 0);
			#endregion

			#region ActivityIndicator
			var activity = new ActivityIndicator
			{
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				IsEnabled = true
			};

			activity.SetBinding(ActivityIndicator.IsVisibleProperty, "IsBusy");
			activity.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");

			mainGrid.Children.Add(activity, 0, 1);
			#endregion

			#region QuestList
			ListView listView = new ListView();

			listView.ItemsSource = ViewModel.FeedItems;

			var cell = new DataTemplate(typeof(FeedItemCell));

			listView.ItemTapped += (sender, args) =>
			{
				if (listView.SelectedItem == null)
					return;

				ViewModel.SelectedFeedItem = listView.SelectedItem as FeedItem;

				var selectedFeed = listView.SelectedItem as FeedItem;

				if (selectedFeed != null)
				{
					this.Navigation.PushAsync(new PodcastPlayView(selectedFeed));
				}

				listView.SelectedItem = null;
			};

			listView.ItemTemplate = cell;

			mainGrid.Children.Add(listView, 0, 2);
			#endregion

			this.Content = mainGrid;

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