using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using Rocketnote.Resources;

namespace Rocketnote.Pages
{
	public partial class ViewNotePage : PhoneApplicationPage
	{
		private Notes.Rocketnote _rocketnote = Notes.Rocketnote.Instance;

		public ViewNotePage()
		{
			InitializeComponent();

			// Ustaw datacontext
			this.DataContext = _rocketnote;

			ApplicationBar = CreateNoteAppBar();
		}

		//
		// Polecenia wykonywanie przy starcie strony
		//
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
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

			//if (DataContext == null)
			//{
			//	string selectedIndex = "";
			//	if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
			//	{
			//		int index = int.Parse(selectedIndex);
			//		DataContext = App.ViewModel.Items[index];
			//	}
			//}
		}

		private ApplicationBar CreateNoteAppBar()
		{
			ApplicationBar NoteAppBar = new ApplicationBar();

			NoteAppBar.Mode = ApplicationBarMode.Default;
			NoteAppBar.Opacity = 1.0;
			NoteAppBar.IsVisible = true;
			NoteAppBar.IsMenuEnabled = true;
			NoteAppBar.ForegroundColor = Colors.White;
			NoteAppBar.BackgroundColor = (Color)Application.Current.Resources["PhoneAccentColor"];


			//ApplicationBarIconButton btnEditNote = new ApplicationBarIconButton();
			//btnEditNote.IconUri = new Uri("/Assets/AppBar/appbar.edit.rest.png", UriKind.Relative);
			//btnEditNote.Text = AppResources.AppBarEdit;
			//btnEditNote.Click += new EventHandler(btnEditNote_Click);
			//NoteAppBar.Buttons.Add(btnEditNote);

			ApplicationBarIconButton btnPinNote = new ApplicationBarIconButton();
			btnPinNote.IconUri = new Uri("/Images/pin.png", UriKind.Relative);
			btnPinNote.Text = "Pin";
			btnPinNote.Click += new EventHandler(btnPinNote_Click);
			NoteAppBar.Buttons.Add(btnPinNote);

			//ApplicationBarIconButton btnShareNote = new ApplicationBarIconButton();
			//btnShareNote.IconUri = new Uri("/Images/share.png", UriKind.Relative);
			//btnShareNote.Text = AppResources.AppBarShareNote;
			//btnShareNote.Click += new EventHandler(btnShareNote_Click);
			//NoteAppBar.Buttons.Add(btnShareNote);

			//ApplicationBarIconButton btnDeleteNote = new ApplicationBarIconButton();
			//btnDeleteNote.IconUri = new Uri("/Assets/AppBar/appbar.delete.rest.png", UriKind.Relative);
			//btnDeleteNote.Text = AppResources.AppBarMoveToTrash;
			//btnDeleteNote.Click += new EventHandler(btnDeleteNote_Click);
			//NoteAppBar.Buttons.Add(btnDeleteNote);

			return NoteAppBar;
		}

		private void btnPinNote_Click(object sender, EventArgs e)
		{
			//throw new NotImplementedException();
			_rocketnote.CreateTile();
		}
	}
}