using System;
using System.ComponentModel;

namespace DevTalkMobile.Models
{
	public class BaseChangedModel : INotifyPropertyChanged
	{
		public BaseChangedModel ()
		{
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(string name)
		{
			if (PropertyChanged == null)
				return;
			PropertyChanged(this, new PropertyChangedEventArgs(name));
		}
	}
}