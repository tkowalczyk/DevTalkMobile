using DevTalkMobile.Services;
using DevTalkMobile.ViewModels;

using Xamarin.Forms;

namespace DevTalkMobile.Views
{
	public class BaseView<T> : ContentPage where T:IViewModel, new()
	{
		#region Properties
		readonly T _viewModel; 
		public T ViewModel
		{
			get {
				return _viewModel;
			}
		}

		private ConnectionType _ct;
		public ConnectionType TypeOfConnection
		{
			get
			{
				return _ct;
			}
		}
		#endregion

		#region Ctor
		public BaseView ()
		{
			_viewModel = new T ();

			BindingContext = _viewModel;

			SetBinding(Page.TitleProperty, new Binding(BaseViewModel.TitlePropertyName));
			SetBinding(Page.IconProperty, new Binding(BaseViewModel.IconPropertyName));
		}
		#endregion

		#region Overrides
		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			_ct = DependencyService.Get<IConnectivity>().InternetConnectionStatus();
		}
		#endregion
	}
}