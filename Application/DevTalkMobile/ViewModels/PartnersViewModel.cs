using System;
using System.Threading.Tasks;

using DevTalkMobile.Helpers;
using DevTalkMobile.Services;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace DevTalkMobile.ViewModels
{
	public class PartnersViewModel : BaseViewModel
	{
		#region Fields
		private IContentRepository _contentRepository;
		#endregion

		#region Ctor
		public PartnersViewModel ()
		{
			_contentRepository = new ContentRepository ();
		}
		#endregion

		#region Properties
		private ObservableCollection<Partner> partnersList = new ObservableCollection<Partner>();
		public ObservableCollection<Partner> PartnersList
		{
			get { return partnersList; }
			set
			{
				partnersList = value;
				OnPropertyChanged("PartnersList");
			}
		}
		#endregion

		#region Commands
		private Command loadContentCommand;
		public Command LoadContentCommand
		{
			get { return loadContentCommand ?? (loadContentCommand = new Command(async () => await ExecuteLoadItemsCommand())); }
		}
		#endregion

		#region Private Methods
		private async Task ExecuteLoadItemsCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				PartnersList.Clear();

				var items = await _contentRepository.GetContent(StaticData.DevTalkWeb);

				var partners = ExtensionMethods.GetPartners(items);

				foreach (var item in partners)
				{
					PartnersList.Add(item);
				}
			}
			catch (Exception ex)
			{
				var page = new ContentPage();
				var result = page.DisplayAlert("Error", "Unable to load partners content. " + ex.Message, "OK");
			}

			IsBusy = false;
		}
		#endregion
	}
}