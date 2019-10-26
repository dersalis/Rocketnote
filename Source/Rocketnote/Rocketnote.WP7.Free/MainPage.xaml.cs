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
using System.Reflection;
using System.Resources;
using Rocketnote.Resources;
using Rocketnote.Notes;

namespace Rocketnote
{
	public partial class MainPage : PhoneApplicationPage
	{
		App app = App.Current as App;

		//
		// Konstructor
		//
		public MainPage()
		{
			InitializeComponent();

            // Odświeża appBar aby przy jasnym tle nie pojawiał się ciemny appBar
            Loaded += (obj, args) =>
            {
                ApplicationBar.MatchOverriddenTheme();
            };

			// Set the data context of the listbox control to the sample data
			DataContext = RnModelView.Instance;
			
			//ustaw appBar
			ApplicationBar = CreateNotebookAppBar();

			// Ułatwia usuwanie notatek gdy usuchamiane są z kafelka
			app._isStartByTitle = false;
		}

		
		#region ZDARZENIA

		//
		//zdarzenie naciśnięcia notatki na liście
		//
		private void lstActivNotes_Tap(object sender, System.Windows.Input.GestureEventArgs e)
		{
			/*
			 * CEL:
			 * Wyświetla wyróżnioną notatkę
			 */

			//przechodzi do okna widoku notatki jeśli na liście są notatki oraz jeśli jedna wyróżniona
			//if (lstActivNotes.SelectedIndex >= 0)
			//{
			//	//przekaż notatkę
			//	RnModelView.Instance.SelectedNote = RnModelView.Instance.GetNotesToNotebook[lstActivNotes.SelectedIndex];
			//	//przejdź do strony widoku
			//	NavigationService.Navigate(new Uri("/Pages/ViewNotePage.xaml", UriKind.Relative));
			//}
		}

		//
		//zdarzenie wybrania elementu appBar usuń wszystkie notatki
		//
		private void btnClearList_Click(object sender, EventArgs e)
		{
			/*
			 * CEL:
			 * Usuwa wszystkie notatki w koszu
			 */

			//usuń wszystkie notatki z kosza
			RnModelView.Instance.DeleteAllNotesInTrash();
		}

		//
		//zdarzenie wybrania elementu contextMenu przenieś do notatnika
		//
		private void btnMoveToTrash_Tap(object sender, System.Windows.Input.GestureEventArgs e)
		{
			/*
			 * CEL:
			 * Wyróżnia notatkę z listy i przenosi do kosza
			 */

			//wybierz notatkę
			if (lstActivNotes.ItemContainerGenerator == null) return;
			var selectedListBoxItem = lstActivNotes.ItemContainerGenerator.ContainerFromItem(((MenuItem)sender).DataContext) as ListBoxItem;
			if (selectedListBoxItem == null) return;
			var selectedIndex = lstActivNotes.ItemContainerGenerator.IndexFromContainer(selectedListBoxItem);
			//przekaż wyróżnioną notatkę
			RnModelView.Instance.SelectedNote = RnModelView.Instance.GetNotesToNotebook[selectedIndex];

			//przenieś do kosza
			RnModelView.Instance.MoveToTrash();

			//dodaje lub ukrywa przycisk sortowania
			EnableSortButon();
		}

		//
		//zdarzenie wybrania elementu contextMenu przenieś do notatnika
		//
		private void btnMoveToActive_Tap(object sender, System.Windows.Input.GestureEventArgs e)
		{
			/*
			 * CEL:
			 * Wyróżnia notatkę z listy i przenosi do notatnika
			 */

			//wybierz notatkę
			if (lstNotesInTrash.ItemContainerGenerator == null) return;
			var selectedListBoxItem = lstNotesInTrash.ItemContainerGenerator.ContainerFromItem(((MenuItem)sender).DataContext) as ListBoxItem;
			if (selectedListBoxItem == null) return;
			var selectedIndex = lstNotesInTrash.ItemContainerGenerator.IndexFromContainer(selectedListBoxItem);
			//przekaż wyróżnioną notatkę
			RnModelView.Instance.SelectedNote = RnModelView.Instance.GetNotesToTrash[selectedIndex];

			//przenieś do notatnika
			RnModelView.Instance.MoveToActiveNotes();

			//dodaje lub ukrywa przycisk usuwania wszystkich notatek
			EnableClearAllNotesButon();
		}

		//
		//zdarzenie wybrania elementu contextMenu edytuj notatkę
		//
		private void btnEditNote_Tap(object sender, System.Windows.Input.GestureEventArgs e)
		{
			/*
			 * CEL:
			 * Wyróżnia notatkę z listy i przechodzi do okna edycji notatki
			 */

			//wybierz notatkę
			if (lstActivNotes.ItemContainerGenerator == null) return;
			var selectedListBoxItem = lstActivNotes.ItemContainerGenerator.ContainerFromItem(((MenuItem)sender).DataContext) as ListBoxItem;
			if (selectedListBoxItem == null) return;
			var selectedIndex = lstActivNotes.ItemContainerGenerator.IndexFromContainer(selectedListBoxItem);
			//przekaż wyróżnioną notatkę
			RnModelView.Instance.SelectedNote = RnModelView.Instance.GetNotesToNotebook[selectedIndex];

			//otwórz stronę edycji notatki
			NavigationService.Navigate(new Uri("/Pages/EditNotePage.xaml", UriKind.Relative));
		}

		//
		//zdarzenie naciśnięcia elementu menu test
		//
		private void btnTester_Click(object sender, EventArgs e)
		{
			/*
			 * CEL:
			 * Uruchamia stronę z testami
			 */

			//otwórz stronę z testami
			NavigationService.Navigate(new Uri("/Pages/TesterPage.xaml", UriKind.Relative));
		}

		//
		//zdarzenie zachodzące przy przesuwaniu stron w pivot
		//
		private void pivRocketnote_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			/*
			 * CEL:
			 * W zależności od aktywnej strony w pivot pokazywane są odpowiednie pozycje appBar
			 */

			//jeśli aktywna pierwsza strona - notatnik
			if (pivRocketnote.SelectedIndex == 0)
			{
				//pokasz appBar notatnika
				ApplicationBar = CreateNotebookAppBar();
			}
			//jeśli aktywna druga strona - kosz
			if (pivRocketnote.SelectedIndex == 1)
			{
				//pokaż appBar kosza
				ApplicationBar = CreateTrashAppBar();
			}

			//dodaje lub ukrywa przycisk sortowania
			EnableSortButon();
			//dodaje lub ukrywa przycisk usuwania wszystkich notatek
			EnableClearAllNotesButon();
		}

		//
		//zdarzenie zachodzące po naciśnięciu pozycji menu ustawienia
		//
		private void mnuRnSettings_click(object sender, EventArgs e)
		{
			/*
			 * CEL:
			 * Otwiera stronę ustawień
			 */

			//otwórz stronę ustawień
			NavigationService.Navigate(new Uri("/Pages/SettingsPage.xaml", UriKind.Relative));
		}

		//
		//zdarzenie zachodzące przy naciśnięciu przycisku udostępnij
		//
		private void btnShareNote_Tap(object sender, System.Windows.Input.GestureEventArgs e)
		{
			/*
			 * CEL:
			 * Otwiera stronę udostępniania notatek
			 */

			if (lstActivNotes.ItemContainerGenerator == null) return;
			var selectedListBoxItem = lstActivNotes.ItemContainerGenerator.ContainerFromItem(((MenuItem)sender).DataContext) as ListBoxItem;
			if (selectedListBoxItem == null) return;
			var selectedIndex = lstActivNotes.ItemContainerGenerator.IndexFromContainer(selectedListBoxItem);
			RnModelView.Instance.SelectedNote = RnModelView.Instance.GetNotesToNotebook[selectedIndex];

			//otwórz stronę udostępniania
			NavigationService.Navigate(new Uri("/Pages/SharePage.xaml", UriKind.Relative));
		}

		//
		//zdarzenie zachodzące przy naciśnięciu przycisku usuń wszystkie notatki
		//
		private void btnClearAllNotes_click(object sender, EventArgs e)
		{
			/*
			 * CEL:
			 * Zdarzenie zachodzące przy naciśnięciu przycisku usuń wszystkie notatki.
			 * Usuwa wszystkie notatki z kosza.
			 */

			//Usuń wszystkie notatki
			RnModelView.Instance.DeleteAllNotesInTrash();

			//dodaje lub ukrywa przycisk sortowania
			EnableClearAllNotesButon();
		}

		//
		//otwiera stronę umożliwiającą dodawanie nowych notatek
		//
		private void btnNewNote_click(object sender, EventArgs e)
		{
			/*
			 * CEL:
			 * 
			 */

			// Znacznik określający czy można dodać 

			//sprawdz czy można dodać nową notatkę
			//jeśli liczba notatek w notatniku nie przekracza dozwolonej (128) to dodaj
			if (AddingCheck())
			{
				//jeśli można
				//otwórz stronę do dodawania nowej notatki
				NavigationService.Navigate(new Uri("/Pages/NewNotePage.xaml", UriKind.Relative));
			}

			//dodaje lub ukrywa przycisk sortowania
			EnableSortButon();
		}

		//
		//otwiera stronę umożliwiającą sortowanie notatek
		//
		private void btnSortNotes_click(object sender, EventArgs e)
		{
			/*
			 * CEL:
			 * Otwiera stronę umożliwiającą sortowanie notatek
			 */

			//otwórz stronę sortowania notatek
			NavigationService.Navigate(new Uri("/Pages/SortPage.xaml", UriKind.Relative));
		}

		//
		//zdarzenie zachodzące przy wejściu na stronę
		//
		protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
		{
			/*
			 * CEL:
			 * Zdarzenia zachodzące przy wejściu na stronę
			 */

			base.OnNavigatedTo(e);

			//dodaje lub ukrywa przycisk sortowania
			EnableSortButon();
		}

		//
		// Zdarzenie kliknięcia notatki na liście notatnika
		//
		private void lstActivNotes_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			/*
			 * CEL:
			 * Przekierowuje na stronę z widokiem notatki oraz przekazuje wybraną notatkę
			 */

			// Jeśli nic nie wybrano to porzuć
			if (lstActivNotes.SelectedItem == null)
				return;

			//todo: Rozwiązanie tymczasowe
			//NavigationService.Navigate(new Uri("/Pages/ViewNotePage.xaml", UriKind.Relative));

			// Przełącz na nową stronę i przekaż parametr - wskazaną notatkę
			NavigationService.Navigate(new Uri("/Pages/ViewNotePage.xaml?selectedItem=" + (lstActivNotes.SelectedItem as Note).Id, UriKind.Relative));

			// Ustaw wybrany element na null - brak wyboru
			lstActivNotes.SelectedItem = null;
		}

        //
        //zdarzenie zachodzące po naciśnięciu pozycji menu o programie
        //
        private void mnuRnAbout_click(object sender, EventArgs e)
        {
            /*
			 * CEL:
			 * Otwiera stronę o programie
			 */

            //otwórz stronę ustawień
            NavigationService.Navigate(new Uri("/Pages/AboutPage.xaml", UriKind.Relative));
        }

		#endregion


		#region APPBAR

		//
		//Pasek appBar Notatnika
		//
		private ApplicationBar CreateNotebookAppBar()
		{
			//nowy pasek
			ApplicationBar notebookAppBar = new ApplicationBar();
			//ustawienia paska
			notebookAppBar.Mode = ApplicationBarMode.Default;
			notebookAppBar.Opacity = 1.0;
			notebookAppBar.IsVisible = true;
			notebookAppBar.IsMenuEnabled = true;
            //notebookAppBar.ForegroundColor = Colors.White;
            //notebookAppBar.BackgroundColor = (Color)Application.Current.Resources["PhoneAccentColor"];

			//przycisk dodający nową notatkę
			ApplicationBarIconButton btnAddNewNote = new ApplicationBarIconButton();
			btnAddNewNote.IconUri = new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative);
			btnAddNewNote.Text = AppResources.AppBarNewNote;
			btnAddNewNote.Click += new EventHandler(btnNewNote_click);
			notebookAppBar.Buttons.Add(btnAddNewNote);

			//przycisk sortujący notatki
			ApplicationBarIconButton btnSortNotes = new ApplicationBarIconButton();
			btnSortNotes.IconUri = new Uri("/Images/sort.png", UriKind.Relative);
			btnSortNotes.Text = AppResources.AppBarSortNotes;
			btnSortNotes.Click += new EventHandler(btnSortNotes_click);
			notebookAppBar.Buttons.Add(btnSortNotes);

			//pozycja menu ustawienia
			ApplicationBarMenuItem mnuRnSettings = new ApplicationBarMenuItem();
			mnuRnSettings.Text = AppResources.AppBarSettings;
			mnuRnSettings.Click += new EventHandler(mnuRnSettings_click);
			notebookAppBar.MenuItems.Add(mnuRnSettings);

            //pozycja menu o programie
            ApplicationBarMenuItem mnuRnAbout = new ApplicationBarMenuItem();
            mnuRnAbout.Text = AppResources.AppBarAbout;
            mnuRnAbout.Click += new EventHandler(mnuRnAbout_click);
            notebookAppBar.MenuItems.Add(mnuRnAbout);

			//zwróć pasek
			return notebookAppBar;
		}

		//
		//Pasek appBar Kosza
		//
		private ApplicationBar CreateTrashAppBar()
		{
			//nowy pasek
			ApplicationBar trashAppBar = new ApplicationBar();
			//ustawienia paska
			trashAppBar.Mode = ApplicationBarMode.Default;
			trashAppBar.Opacity = 1.0;
			trashAppBar.IsVisible = true;
			trashAppBar.IsMenuEnabled = true;
            //trashAppBar.ForegroundColor = Colors.White;
            //trashAppBar.BackgroundColor = (Color)Application.Current.Resources["PhoneAccentColor"];

			//przycisk usuwający wszystkie notatki w koszu
			ApplicationBarIconButton btnClearAllNotes = new ApplicationBarIconButton();
			btnClearAllNotes.IconUri = new Uri("/Images/Clean.png", UriKind.Relative);
			btnClearAllNotes.Text = AppResources.AppBarEmptyTrash;
			btnClearAllNotes.Click += new EventHandler(btnClearAllNotes_click);
			trashAppBar.Buttons.Add(btnClearAllNotes);

			//zwróć pasek
			return trashAppBar;
		}

		#endregion

		#region METODY POMOCNICZE

		//
		//Uaktywnia / ukrywa przycisk sortowania
		//
		private void EnableSortButon()
		{
			/*
			 * CEL:
			 * Uaktywnia / ukrywa przycisk sortowania.
			 * Jeśli jest więcej niż jedna notatka w koszu to przycisk jest widoczny.
			 */

			//sprawdz czy aktywna jest strona Notetnika
			if (pivRocketnote.SelectedIndex == 0)
			{
				//przycisk sortowania notatek - drugi przycisk
				ApplicationBarIconButton sortNotesAppBar = ApplicationBar.Buttons[1] as ApplicationBarIconButton;
				//jeśli notatek w notatniku jest więcej niż jedna to uaktywnij przycisk
				//w przeciwnym przypadku ukryj
				if (RnModelView.Instance.GetNotesToNotebook.Count > 1)
				{
					sortNotesAppBar.IsEnabled = true;
				}
				else
				{
					sortNotesAppBar.IsEnabled = false;
				}
			}
		}

		//
		//Uaktywnia / ukrywa przycisk usuwania wszystkich notatek
		//
		private void EnableClearAllNotesButon()
		{
			/*
			 * CEL:
			 * Uaktywnia / ukrywa przycisk usuwania wszystkich notatek.
			 * Jeśli w koszu znajdują się notatki to przycisk jest widoczny.
			 */

			//sprawdz czy aktywna jest strona kosza
			if (pivRocketnote.SelectedIndex == 1)
			{
				//przycisk usuń wszystkie notatki z kosza - pierwszy przycisk
				ApplicationBarIconButton clearAllNotesAppBar = ApplicationBar.Buttons[0] as ApplicationBarIconButton;
				//jeśli w koszu są notatki to uaktywnij przycisk
				//w przeciwnym przypadku ukryj
				if (RnModelView.Instance.GetNotesToTrash.Count > 0)
				{
					clearAllNotesAppBar.IsEnabled = true;
				}
				else
				{
					clearAllNotesAppBar.IsEnabled = false;
				}
			}
		}

		//
		// Sprawdza czy można dodać notatkę
		//
		private bool AddingCheck()
		{
			/*
			 * CEL:
			 * Sprawdza czy można dodać notakę
			 * 
			 * WARTOŚĆ ZWRACANA:
			 * bool - parametr określający czy można dodać notatkę
			 */

			bool result = false;

			using (NotesAdder notesAdder = new NotesAdder())
			{
				result = notesAdder.AddingCheck(RnModelView.Instance.NotesList.ToList());
			}
			return result;
		}

		#endregion

	}
}