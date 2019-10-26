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

namespace Rocketnote.Pages
{
	public partial class CategoryPage : PhoneApplicationPage
	{
		private CategoryManagement _category;

		public CategoryPage()
		{
			InitializeComponent();

			//utwórz AppBar
			ApplicationBar = CreateNewNoteAppBar();

			_category = CategoryManagement.Instance;
			CreateCategoryList();
			//brdCat0.Background = new SolidColorBrush(Colors.LightGray);
		}

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
			NewNoteAppBar.ForegroundColor = Colors.White;
			NewNoteAppBar.BackgroundColor = (Color)Application.Current.Resources["PhoneAccentColor"];

			//utwórz przyciksk zapisz
			ApplicationBarIconButton btnSaveNote = new ApplicationBarIconButton();
			btnSaveNote.IconUri = new Uri("/Assets/AppBar/save.png", UriKind.Relative);
			btnSaveNote.Text = AppResources.AppBarSave;
			btnSaveNote.Click += new EventHandler(btnSaveNote_Click);
			NewNoteAppBar.Buttons.Add(btnSaveNote);

			//utwórz przycisk anuluj
			ApplicationBarIconButton btnCancelNote = new ApplicationBarIconButton();
			btnCancelNote.IconUri = new Uri("/Toolkit.Content/ApplicationBar.Cancel.png", UriKind.Relative);
			btnCancelNote.Text = AppResources.AppBarCancel;
			btnCancelNote.Click += new EventHandler(btnCancelNote_Click);
			NewNoteAppBar.Buttons.Add(btnCancelNote);

			//zwróć pasek appBar
			return NewNoteAppBar;
		}

		private void btnCancelNote_Click(object sender, EventArgs e)
		{
			//throw new NotImplementedException();
		}

		private void btnSaveNote_Click(object sender, EventArgs e)
		{
			//throw new NotImplementedException();
		}
		#endregion

		private void CreateCategoryList()
		{
			brdCat0.Background = _category.GetCategoryColor(0);
			txtCat0.Text = _category.GetCategoryName(0);
			brdCat1.Background = _category.GetCategoryColor(1);
			txtCat1.Text = _category.GetCategoryName(1);
			brdCat2.Background = _category.GetCategoryColor(2);
			txtCat2.Text = _category.GetCategoryName(2);
			brdCat3.Background = _category.GetCategoryColor(3);
			txtCat3.Text = _category.GetCategoryName(3);
			brdCat4.Background = _category.GetCategoryColor(4);
			txtCat4.Text = _category.GetCategoryName(4);
			brdCat5.Background = _category.GetCategoryColor(5);
			txtCat5.Text = _category.GetCategoryName(5);
			brdCat6.Background = _category.GetCategoryColor(6);
			txtCat6.Text = _category.GetCategoryName(6);
		}
	}
}