using DevTalkMobile.ViewModels;

using Xamarin.Forms;

namespace DevTalkMobile.Views
{
	public class ChatView : BaseView<ChatViewModel>
	{
		#region Ctor
		public ChatView ()
		{
			this.Content = new Label
			{
				Text = "ChatView",
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
			};

			// Accomodate iPhone status bar.
			this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
		}
		#endregion
	}
}