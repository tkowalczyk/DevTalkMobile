using System;

namespace DevTalkMobile.Models
{
	public class MenuItem : BaseModel
	{
		public MenuItem()
		{
			MenuType = MenuType.Home;
		}

		public string Icon { get; set; }
		public MenuType MenuType { get; set; }
	}
}