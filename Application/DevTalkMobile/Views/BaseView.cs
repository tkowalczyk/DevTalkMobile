using System;
using DevTalkMobile.ViewModels;
using Xamarin.Forms;

namespace DevTalkMobile.Views
{
	public class BaseView<T> : ContentPage where T:IViewModel, new()
	{
		readonly T _viewModel; 
		public T ViewModel
		{
			get {
				return _viewModel;
			}
		}

		public BaseView ()
		{
			_viewModel = new T ();

			BindingContext = _viewModel;

			SetBinding(Page.TitleProperty, new Binding(BaseViewModel.TitlePropertyName));
			SetBinding(Page.IconProperty, new Binding(BaseViewModel.IconPropertyName));
		}
	}
}