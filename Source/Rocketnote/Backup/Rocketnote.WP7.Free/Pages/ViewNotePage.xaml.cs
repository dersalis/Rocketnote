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

namespace Rocketnote.Pages
{
	public partial class ViewNotePage : PhoneApplicationPage
	{
		RnModelView _rocketnote = RnModelView.Instance;

		//przycisk przypnij notatkę
		private ApplicationBarIconButton pinNoteAppBar;
		//przycisk usuń notatkę
		private ApplicationBarIconButton deleteNoteAppBar;

		public ViewNotePage()
		{
			InitializeComponent();
			//ustaw datacontext
			DataContext = RnModelView.Instance;

			//utwórz appBar
			ApplicationBar = CreateNoteAppBar();

			//przycisk przypnij notatkę
			pinNoteAppBar = ApplicationBar.Buttons[1] as ApplicationBarIconButton;
			//przycisk usuń notatkę
			deleteNoteAppBar = ApplicationBar.Buttons[3] as ApplicationBarIconButton;
		}

		//edytuj notatkę
		private void btnEditNote_Click(object sender, EventArgs e)
		{
			//edytuj notatkę
			NavigationService.Navigate(new Uri("/Pages/EditNotePage.xaml", UriKind.Relative));			
		}

		//przenieś notatkę do kosza
		private void btnDeleteNote_Click(object sender, EventArgs e)
		{
			//przenieś do kosza
			RnModelView.Instance.MoveToTrash();
			//wróć do strony głównej
			//NavigationService.GoBack();
			App app = App.Current as App;
			// Sprawdza czy aplikacja została uruchomina czy użyto kafelka
			// Jeśli uruchomiono to usuwa notatkę i wraca do poprzedniej strony
			// Jeśli użyto kafelka to usuwa notatkę i dezaktywuje kafelek usuwania, strona pozostaje
			if (app._isStartByTitle != true)
			{
				NavigationService.GoBack();
			}
			else
			{
				deleteNoteAppBar.IsEnabled = false;
				//todo Zrobić komunikat informujący o przeniesieniu notatki do kosza
			}
		}

		private ApplicationBar CreateNoteAppBar()
		{
			ApplicationBar NoteAppBar = new ApplicationBar();

			NoteAppBar.Mode = ApplicationBarMode.Default;
			NoteAppBar.Opacity = 1.0;
			NoteAppBar.IsVisible = true;
			NoteAppBar.IsMenuEnabled = true;
            //NoteAppBar.ForegroundColor = Colors.White;
            //NoteAppBar.BackgroundColor = (Color)Application.Current.Resources["PhoneAccentColor"];


			ApplicationBarIconButton btnEditNote = new ApplicationBarIconButton();
			btnEditNote.IconUri = new Uri("/Assets/AppBar/appbar.edit.rest.png", UriKind.Relative);
			btnEditNote.Text = AppResources.AppBarEdit;
			btnEditNote.Click += new EventHandler(btnEditNote_Click);
			NoteAppBar.Buttons.Add(btnEditNote);

			ApplicationBarIconButton btnPinNote = new ApplicationBarIconButton();
			btnPinNote.IconUri = new Uri("/Images/pin.png", UriKind.Relative);
			btnPinNote.Text = AppResources.AppBarPin;
			btnPinNote.Click += new EventHandler(btnPinNote_Click);
			NoteAppBar.Buttons.Add(btnPinNote);

			ApplicationBarIconButton btnShareNote = new ApplicationBarIconButton();
			btnShareNote.IconUri = new Uri("/Images/share.png", UriKind.Relative);
			btnShareNote.Text = AppResources.AppBarShareNote;
			btnShareNote.Click += new EventHandler(btnShareNote_Click);
			NoteAppBar.Buttons.Add(btnShareNote);

			ApplicationBarIconButton btnDeleteNote = new ApplicationBarIconButton();
			btnDeleteNote.IconUri = new Uri("/Assets/AppBar/appbar.delete.rest.png", UriKind.Relative);
			btnDeleteNote.Text = AppResources.AppBarMoveToTrash;
			btnDeleteNote.Click += new EventHandler(btnDeleteNote_Click);
			NoteAppBar.Buttons.Add(btnDeleteNote);

			return NoteAppBar;
		}

		private void btnPinNote_Click(object sender, EventArgs e)
		{
			// Utwórz kafelek
			_rocketnote.CreateTile();
			// Ukryj przycisk
			pinNoteAppBar.IsEnabled = false;
		}

		private void btnShareNote_Click(object sender, EventArgs e)
		{
			NavigationService.Navigate(new Uri("/Pages/SharePage.xaml", UriKind.Relative));	
		}

		//
		// Polecenia wykonywanie przy starcie strony
		//
		protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
		{
			//base.OnNavigatedTo(e);
			/*
			 * CEL:
			 * 
			 */

			string selectedIndex = "";
			if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
			{
				int index = int.Parse(selectedIndex);
				_rocketnote.SetTemporaryNote(index);
				//DataContext = App.ViewModel.Items[index];
			}
			pinNoteAppBar.IsEnabled = !(_rocketnote.TileExist());
		}


        //
        // Edytuj notatkę
        //
        private void txtTitle_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            // Edytuj notatkę
            NavigationService.Navigate(new Uri("/Pages/EditNotePage.xaml", UriKind.Relative));
        }
	}
}