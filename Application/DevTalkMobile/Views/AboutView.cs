using System;
using DevTalkMobile.ViewModels;
using Xamarin.Forms;

namespace DevTalkMobile.Views
{
	public class AboutView : BaseView<AboutViewModel>
	{
		public AboutView ()
		{
			this.Title = "About";

			this.Content = new ScrollView 
			{
				Content = new Label
				{
					FormattedText = new FormattedString
					{
						Spans =
						{
							new Span
							{
								Text = "O DevTalk\n\n",
								FontAttributes = FontAttributes.Bold,
								FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
							},
							new Span
							{
								Text = "DevTalk",
								FontAttributes = FontAttributes.Bold,
								FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
							},
							new Span
							{
								Text = " to podcast programistyczny prowadzony przez ",
							},
							new Span
							{
								Text = " Macieja Aniserowicza",
								FontAttributes = FontAttributes.Bold
							},
							new Span
							{
								Text = " . W każdym odcinku wyśmienity Gość i interesujący" + 
									" temat, a czasami nawet konkurs z wartościowymi nagrodami!\n\n"
							},
							new Span
							{
								Text = "Zawsze coś nowego w ",
								FontAttributes = FontAttributes.None
							},
							new Span
							{
								Text = "pierwszy i trzeci poniedziałek",
								FontAttributes = FontAttributes.Bold
							},
							new Span
							{
								Text = " miesiąca, nie przegap! Chcesz być na bieżąco? " + 
									"Zasubskrybuj kanał RSS lub obserwuj w iTunes. " + 
									"Dołącz też do społeczności DevTalk na Twitterze oraz Facebooku. Zapraszam!\n\n"
							}
							,
							new Span
							{
								Text = "Chcesz wystąpić? Nie zwlekaj, zgłoś się!\n" + 
									"Chcesz posłuchać kogoś konkretnego? Wyślij propozycję!" +
									" Zobaczę co da się zrobić.\nJesteś zainteresowany współpracą komercyjną?" + 
									" Zapraszam na stronę Współpraca!\n\n"
							},
							new Span
							{
								Text = "Kontakt",
								FontAttributes = FontAttributes.Bold
							},
							new Span
							{
								Text = " pod adresem: ",
								FontAttributes = FontAttributes.None
							},
							new Span
							{
								Text = "kontakt@devtalk.pl.\n\n",
								FontAttributes = FontAttributes.Bold
							},
							new Span
							{
								Text = "Ale przede wszystkim...",
								FontAttributes = FontAttributes.None
							},
							new Span
							{
								Text = " wpuść programistyczny głos do swojego domu",
								FontAttributes = FontAttributes.Bold
							},
							new Span
							{
								Text = " . Albo samochodu, jak wygodniej ;).\n\nPoczynając od trzeciego odcinka, " + 
									", realizacją DevTalka od strony technicznej " + 
									"(montaż, synchronizacja, edycja, “odszumianie” itd) zajmuje się ",
								FontAttributes = FontAttributes.None
							},
							new Span
							{
								Text = "Krzysztof Śmigiel.\n\n",
								FontAttributes = FontAttributes.Bold
							},
							new Span
							{
								Text = "Logo oraz avatar DevTalka zaprojektował ",
								FontAttributes = FontAttributes.None
							},
							new Span
							{
								Text = "Rafał Sańda.\n\n",
								FontAttributes = FontAttributes.Bold
							},
							new Span
							{
								Text = "Aplikację mobilną wykonał ",
								FontAttributes = FontAttributes.None
							},
							new Span
							{
								Text = "Tomasz Kowalczyk.\n\n",
								FontAttributes = FontAttributes.Bold
							},
						}
						},

					HorizontalOptions = LayoutOptions.Center,
					VerticalOptions = LayoutOptions.Start
				},
			};

			// Accomodate iPhone status bar.
			this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
		}
	}
}