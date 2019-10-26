using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Rocketnote.Resources;
using Rocketnote.Notes;

namespace Rocketnote.Pages
{
	public partial class NewNotePage : PhoneApplicationPage
	{
		// Przycisk zapisz notatkę
		ApplicationBarIconButton saveNoteAppBar;

		// Istancja klasy programu
		RnModelView _rn = RnModelView.Instance;

		//
		// Konstruktor
		//
		public NewNotePage()
		{
			
			InitializeComponent();

			// Ustaw DataContext
			this.DataContext = RnModelView.Instance;

			//utwórz AppBar
			ApplicationBar = CreateNewNoteAppBar();

			//dezaktywuj przycisk zapisz notatkę
			saveNoteAppBar = ApplicationBar.Buttons[0] as ApplicationBarIconButton;
			saveNoteAppBar.IsEnabled = false;

			//wyczyść nową notatkę
			_rn.TempNote = new Note();
		}

		#region ZDARZENIA

		//
		//zarzenie wywołne przez wprowadzanie tekstu do pola txtNewContent
		//
		private void txtNewContent_TextChanged(object sender, TextChangedEventArgs e)
		{
			/*
			 * CEL:
			 * Wprowadzanie tekstu do pola powoduje aktualizację zmiennej źródłowej oraz 
			 * adtywację lub dezaktywację przycisku zapisz
			 */

			//aktualizuje właściwość podczas wpisywania
			UpdateSourceData(sender);
			//wprowadzono zmiany w nowej notatce
			EnableSaveButton();
		}

		//
		//zarzenie wywołne przez wprowadzanie tekstu do pola txtNewTitle
		//
		private void txtNewTitle_TextChanged(object sender, TextChangedEventArgs e)
		{
			/*
			 * CEL:
			 * Wprowadzanie tekstu do pola powoduje aktualizację zmiennej źródłowej oraz 
			 * adtywację lub dezaktywację przycisku zapisz
			 */

			//aktualizuje właściwość podczas wpisywania
			UpdateSourceData(sender);
			//wprowadzono zmiany w nowej notatce
			EnableSaveButton();
		}

		//
		//zdarzenie naciśnięcia przycisku SaveNote - zapisywanie notatki
		//
		private void btnSaveNote_Click(object sender, EventArgs e)
		{
			/*
			 * CEL:
			 * Naciśnięcie przycisku powoduje zapisanie przygotowanej notatki oraz powrót do poprzedniej strony
			 */

			//dodaj notatkę
			_rn.AddNewNote();
			//powróć do strony głównej
			NavigationService.GoBack();
		}

		//
		//zdarzenie naciśnięcia przycisku CancelNote - anulowanie tworzenia notatki
		//
		private void btnCancelNote_Click(object sender, EventArgs e)
		{
			/*
			 * CEL:
			 * Naciśnięcie przycisku powoduje anulowanie wprowadzania nowej notatki oraz powrót do poprzedniej strony
			 */

			//wyczyść notatkę tymczasową

			//wróć do poprzedniej strony
			NavigationService.GoBack();
		}
		#endregion

		#region APPBAR

		//
		//tworzy pasek AppBar
		//
		private ApplicationBar CreateNewNoteAppBar()
		{
			/*
			 * CEL:
			 * Tworzy oraz zwraca pasek AppBar na stronie NewNote
			 * Tworzone są przyciski SaveNote, CancelNote
			 */

			//instancja paska appBar
			ApplicationBar NewNoteAppBar = new ApplicationBar();

			//ustawienia paska appBar
			NewNoteAppBar.Mode = ApplicationBarMode.Default;
			NewNoteAppBar.Opacity = 1.0;
			NewNoteAppBar.IsVisible = true;
			NewNoteAppBar.IsMenuEnabled = true;
            //NewNoteAppBar.ForegroundColor = Colors.White;
            //NewNoteAppBar.BackgroundColor = (Color)Application.Current.Resources["PhoneAccentColor"];

			//utwórz przyciksk zapisz
			ApplicationBarIconButton btnSaveNote = new ApplicationBarIconButton();
			btnSaveNote.IconUri = new Uri("/Assets/AppBar/appbar.save.rest.png", UriKind.Relative);
			btnSaveNote.Text = AppResources.AppBarSave;
			btnSaveNote.Click += new EventHandler(btnSaveNote_Click);
			NewNoteAppBar.Buttons.Add(btnSaveNote);

			//utwórz przycisk anuluj
			ApplicationBarIconButton btnCancelNote = new ApplicationBarIconButton();
			btnCancelNote.IconUri = new Uri("/Assets/AppBar/appbar.cancel.rest.png", UriKind.Relative);
			btnCancelNote.Text = AppResources.AppBarCancel;
			btnCancelNote.Click += new EventHandler(btnCancelNote_Click);
			NewNoteAppBar.Buttons.Add(btnCancelNote);

			//zwróć pasek appBar
			return NewNoteAppBar;
		}
		#endregion

		#region METODY POMOCNICZE

		//
		//aktualizuje źródło danych
		//
		private void UpdateSourceData(object mySender)
		{
			/*
			 * CEL:
			 *	Aktualizowanie zródła danych za pomocą danych wprowadzonych do pola TextBox
			 *	
			 * WARTOŚCI WEJŚCIOWE:
			 *	mySender:object - komponent z którego dane będą wysyłane do źródła
			 */

			var textBox = mySender as TextBox;
			textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
		}

		//
		//uaktywnia przycisk zapisz
		//
		private void EnableSaveButton()
		{
			/*
			 * CEL:
			 * Uaktywnia i dezaktywuje przycisk zapisz - saveNoteAppBar
			 * Przycisk aktywowany jest w przypadku gdy w polach txtNewTitle i txtNewContent znajduje się jakiś text
			 */

			// jeśli pola tekstowe są wypełnione
			if (txtNewTitle.Text.Length > 0 && txtNewContent.Text.Length > 0)
			{
				saveNoteAppBar.IsEnabled = true;
				_rn.IsWritingNewNote = true;
			}
			else
			{
				saveNoteAppBar.IsEnabled = false;
				_rn.IsWritingNewNote = true;
			}
		}

		#endregion

        private void tglPriority_Checked(object sender, RoutedEventArgs e)
        {
            tglPriority.Content = AppResources.TextPriorityHigh;
        }

        private void tglPriority_Unchecked(object sender, RoutedEventArgs e)
        {
            tglPriority.Content = AppResources.TextPriorityNormal;
        }
	}
}