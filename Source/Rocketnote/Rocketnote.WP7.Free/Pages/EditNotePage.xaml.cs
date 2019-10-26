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
	public partial class EditNotePage : PhoneApplicationPage
	{
		//przycisk zapisz notatkę
		private ApplicationBarIconButton saveNoteAppBar;
		//tymczasowa notatka
		private Note tempNote;
		//notatka została edytowana
		private bool isEditedNote;
		//liczby znaków w polach
		int titleLength, contentLength;
        // Zmieniono priorytet
        bool priorityIsChanged;

		public EditNotePage()
		{
			InitializeComponent();
			//data context
			this.DataContext = RnModelView.Instance;

			//dodaj appBar
			ApplicationBar = CreateEditNoteAppBar();

			//dezaktywuj przycisk zapisz notatkę
			saveNoteAppBar = ApplicationBar.Buttons[0] as ApplicationBarIconButton;
			saveNoteAppBar.IsEnabled = false;
			//notatka nie była edytowana
			isEditedNote = false;

			tempNote = RnModelView.Instance.SelectedNote;
			txtNewTitle.Text = tempNote.Title;
            txtNewContent.Text = tempNote.Content;
            tglPriority.IsChecked = tempNote.IsHighPriority;
			titleLength = txtNewTitle.Text.Length;
            contentLength = txtNewContent.Text.Length;
            priorityIsChanged = (bool)tglPriority.IsChecked;
		}

		

		//uaktywnia elementy ui
		private void EnableSaveButton()
		{
			// jeśli pola tekstowe są wypełnione
            if (txtNewTitle.Text.Length != titleLength || txtNewContent.Text.Length != contentLength || tglPriority.IsChecked != priorityIsChanged)
			{
				saveNoteAppBar.IsEnabled = true;
				//RnModelView.Instance.IsWritingNewNote = true;
				isEditedNote = true;
			}
			//else
			//{
			//	saveNoteAppBar.IsEnabled = false;
			//	//RnModelView.Instance.IsWritingNewNote = false;
			//	isEditedNote = false;
			//}
		}

		private void txtNewTitle_TextChanged(object sender, TextChangedEventArgs e)
		{
			//aktualizuje właściwość podczas wpisywania
			//UpdateSourceData(sender);
			//wprowadzono zmiany w nowej notatce
			EnableSaveButton();
		}

		private void txtNewContent_TextChanged(object sender, TextChangedEventArgs e)
		{
			//aktualizuje właściwość podczas wpisywania
			//UpdateSourceData(sender);
			//wprowadzono zmiany w nowej notatce
			EnableSaveButton();
		}

		private void btnSaveNote_Click(object sender, EventArgs e)
		{
			if (isEditedNote)
			{
				tempNote.Title = txtNewTitle.Text;
				tempNote.Content = txtNewContent.Text;
                tempNote.IsHighPriority = (bool)tglPriority.IsChecked;
				RnModelView.Instance.SelectedNote = tempNote;

				RnModelView.Instance.SaveEditedNote();
			}

			//wróć do poprzedniej strony
			NavigationService.GoBack();
		}

		private void btnCancelNote_Click(object sender, EventArgs e)
		{
			//wróć do poprzedniej strony
			NavigationService.GoBack();
		}

		private ApplicationBar CreateEditNoteAppBar()
		{
			ApplicationBar editNoteAppBar = new ApplicationBar();

			editNoteAppBar.Mode = ApplicationBarMode.Default;
			editNoteAppBar.Opacity = 1.0;
			editNoteAppBar.IsVisible = true;
			editNoteAppBar.IsMenuEnabled = true;
            //editNoteAppBar.ForegroundColor = Colors.White;
            //editNoteAppBar.BackgroundColor = (Color)Application.Current.Resources["PhoneAccentColor"];


			ApplicationBarIconButton btnSaveNote = new ApplicationBarIconButton();
			btnSaveNote.IconUri = new Uri("/Assets/AppBar/appbar.save.rest.png", UriKind.Relative);
			btnSaveNote.Text = AppResources.AppBarSave;
			btnSaveNote.Click += new EventHandler(btnSaveNote_Click);
			editNoteAppBar.Buttons.Add(btnSaveNote);

			ApplicationBarIconButton btnCancelNote = new ApplicationBarIconButton();
			btnCancelNote.IconUri = new Uri("/Assets/AppBar/appbar.cancel.rest.png", UriKind.Relative);
			btnCancelNote.Text = AppResources.AppBarCancel;
			btnCancelNote.Click += new EventHandler(btnCancelNote_Click);
			editNoteAppBar.Buttons.Add(btnCancelNote);

			return editNoteAppBar;
		}

        private void tglPriority_Unchecked(object sender, RoutedEventArgs e)
        {
            tglPriority.Content = AppResources.TextPriorityNormal;
            EnableSaveButton();
        }

        private void tglPriority_Checked(object sender, RoutedEventArgs e)
        {
            tglPriority.Content = AppResources.TextPriorityHigh;
            EnableSaveButton();
        }
	}
}