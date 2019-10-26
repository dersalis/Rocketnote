using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Rocketnote.Resources;
using System.Windows.Media;
using Rocketnote.Notes;

namespace Rocketnote
{
	public partial class MainPage : PhoneApplicationPage
	{
		// Instancja programu
		Notes.Rocketnote rocketnote = Notes.Rocketnote.Instance;

		// 
		// Konstruktor
		//
		public MainPage()
		{
			InitializeComponent();

			// Ustaw DataContext
			this.DataContext = rocketnote;
		}

		//
		// Polecenia startu strony
		//
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			//if (!App.ViewModel.IsDataLoaded)
			//{
			//	App.ViewModel.LoadData();
			//}
		}

		#region APPBAR

		//
		// Zdarzenie wywoływane zmianą / przesunięciem strony
		//
		private void pivAllNotes_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// Sprawdz jaka strona jest widoczna
			if (pivAllNotes.SelectedIndex == 0)
			{
				// Jeśli wybrano pierszą stronę
				ApplicationBar = CreateNotebookAppBar();
			}
			else
			{
				// Jeśli wybrano drugą stronę
				ApplicationBar = CreateTrashAppBar();
			}
		}

		// Appbar dla strony
		ApplicationBar notebookAppBar;
		ApplicationBar trashAppBar;

		//
		// AppBar dla karty Notebook
		//
		private ApplicationBar CreateNotebookAppBar()
		{
			/*
			 * CEL:
			 * Tworzy AppBar dla karty Notebook
			 */

			// Ustawienia appbar
			notebookAppBar = new ApplicationBar();
			notebookAppBar.Mode = ApplicationBarMode.Default;
			notebookAppBar.Opacity = 1.0;
			notebookAppBar.IsVisible = true;
			notebookAppBar.IsMenuEnabled = true;
			notebookAppBar.ForegroundColor = Colors.White;
			notebookAppBar.BackgroundColor = (Color)Application.Current.Resources["PhoneAccentColor"];

			// Button dodający nową notatkę
			ApplicationBarIconButton btnAddNewNote = new ApplicationBarIconButton();
			btnAddNewNote.IconUri = new Uri("/Toolkit.Content/ApplicationBar.Add.png", UriKind.Relative);
			btnAddNewNote.Text = AppResources.AppBarNewNote;
			btnAddNewNote.Click += new EventHandler(btnNewNote_click);
			notebookAppBar.Buttons.Add(btnAddNewNote);

			// Button sortujący notatki
			ApplicationBarIconButton btnSortNotes = new ApplicationBarIconButton();
			btnSortNotes.IconUri = new Uri("/Images/AppBar/sort.png", UriKind.Relative);
			btnSortNotes.Text = AppResources.AppBarSortNotes;
			btnSortNotes.Click += new EventHandler(btnSortNotes_click);
			notebookAppBar.Buttons.Add(btnSortNotes);

			// Pozycja menu synchronizacja
			ApplicationBarMenuItem mnuRnSynchronize = new ApplicationBarMenuItem();
			mnuRnSynchronize.Text = AppResources.AppBarSynchronize;
			mnuRnSynchronize.Click += new EventHandler(mnuRnSynchronize_click);
			notebookAppBar.MenuItems.Add(mnuRnSynchronize);

			// Pozycja menu przewodnik
			ApplicationBarMenuItem mnuRnGuide = new ApplicationBarMenuItem();
			mnuRnGuide.Text = AppResources.AppBarGuide;
			mnuRnSynchronize.Click += new EventHandler(mnuRnGuide_click);
			notebookAppBar.MenuItems.Add(mnuRnGuide);

			// Pozycja menu ustawienia
			ApplicationBarMenuItem mnuRnSettings = new ApplicationBarMenuItem();
			mnuRnSettings.Text = AppResources.AppBarSettings;
			mnuRnSettings.Click += new EventHandler(mnuRnSettings_click);
			notebookAppBar.MenuItems.Add(mnuRnSettings);

			// Zwróć appbar
			return notebookAppBar;
		}

		//
		// AppBar dla karty Trash
		//
		private ApplicationBar CreateTrashAppBar()
		{
			/*
			 * CEL:
			 * Tworzy AppBar dla karty Trash
			 */

			// Ustawienia appbar
			trashAppBar = new ApplicationBar();
			trashAppBar.Mode = ApplicationBarMode.Default;
			trashAppBar.Opacity = 1.0;
			trashAppBar.IsVisible = true;
			trashAppBar.IsMenuEnabled = true;
			trashAppBar.ForegroundColor = Colors.White;
			trashAppBar.BackgroundColor = (Color)Application.Current.Resources["PhoneAccentColor"];

			// Button czyszczący wszytkie notatki w koszu
			ApplicationBarIconButton btnClearAllNotes = new ApplicationBarIconButton();
			btnClearAllNotes.IconUri = new Uri("/Images/AppBar/clean.png", UriKind.Relative);
			btnClearAllNotes.Text = AppResources.AppBarEmptyBar;
			btnClearAllNotes.Click += new EventHandler(btnClearAllNotes_click);
			trashAppBar.Buttons.Add(btnClearAllNotes);

			// Zwróć appbar
			return trashAppBar;
		}

		//
		// Zdarzenie kliknięcia przycisku dodaj notatkę
		//
		private void btnNewNote_click(object sender, EventArgs e)
		{
			/*
			 * CEL:
			 * Przenosi do strony dodawania notaki
			 */

			//sprawdz czy można dodać nową notatkę
			//jeśli liczba notatek w notatniku nie przekracza dozwolonej (128) to dodaj
			if (rocketnote.CheckAdding())
			{
				//jeśli można
				//otwórz stronę do dodawania nowej notatki
				NavigationService.Navigate(new Uri("/Pages/NewNotePage.xaml", UriKind.Relative));
			}

			//dodaje lub ukrywa przycisk sortowania
			//EnableSortButon();
		}

		//
		// Zdarzenia kliknięcia przycisku sortuj notatki
		//
		private void btnSortNotes_click(object sender, EventArgs e)
		{
			/*
			 * CEL:
			 * Przenosi do strony sortowania notatek
			 */

			// Przejdz do strony sortowania
			NavigationService.Navigate(new Uri("/Pages/SortNotesPage.xaml", UriKind.Relative));
		}

		//
		// Zdarzenia kliknięcia elementu menu synchronizacja
		//
		private void mnuRnSynchronize_click(object sender, EventArgs e)
		{
			/*
			 * CEL:
			 * Przenosi do strony synchronizacji
			 */

			//throw new NotImplementedException();
		}

		//
		// Zdarzenia kliknięcia elementu menu przewodnik
		//
		private void mnuRnGuide_click(object sender, EventArgs e)
		{
			/*
			 * CEL:
			 * Przenosi do strony przewodnika
			 */

			//throw new NotImplementedException();
		}

		//
		// Zdarzenia kliknięcia elementu menu ustawienia
		//
		private void mnuRnSettings_click(object sender, EventArgs e)
		{
			/*
			 * CEL:
			 * Przenosi do ustawień
			 */

			// Przejdz do ustawień
			NavigationService.Navigate(new Uri("/Pages/CategoryPage.xaml", UriKind.Relative));
		}


		//
		// Zdarzenia kliknięcia przycisku usuń wszystkie notatki
		//
		private void btnClearAllNotes_click(object sender, EventArgs e)
		{
			/*
			 * CEL:
			 * Usuwa wszystkie notatki z kosza
			 */

			//throw new NotImplementedException();
		}

		#endregion

		//
		// Zdarzenie kliknięcia notatki na liście notatnika
		//
		private void lstNotebook_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			/*
			 * CEL:
			 * Przekierowuje na stronę z widokiem notatki oraz przekazuje wybraną notatkę
			 */

			// Jeśli nic nie wybrano to porzuć
			if (lstNotebook.SelectedItem == null)
				return;

			//todo: Rozwiązanie tymczasowe
			//NavigationService.Navigate(new Uri("/Pages/ViewNotePage.xaml", UriKind.Relative));

			// Przełącz na nową stronę i przekaż parametr - wskazaną notatkę
			NavigationService.Navigate(new Uri("/Pages/ViewNotePage.xaml?selectedItem=" + (lstNotebook.SelectedItem as Note).Id, UriKind.Relative));

			// Ustaw wybrany element na null - brak wyboru
			lstNotebook.SelectedItem = null;
		}
	}
}