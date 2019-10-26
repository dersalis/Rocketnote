using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rocketnote.Resources;
using System.Windows;
using System.Collections.ObjectModel;

namespace Rocketnote.Notes
{
	public class NotesManagement
	{
		//maksymalna liczba notatek w notatniku
		private const byte MAX_NOTES_COUNT = 128;
		//zmienna sygnalizuje czy wprowadzono miany w liście notatke
		public static bool IsChangedNotesList { get; private set; }

		#region KONSTRUKTORY
		//
		//konstruktor statyczny
		//
		static NotesManagement()
		{ 
			//jeszcze nie wprowadzono zmian
			IsChangedNotesList = false;
		}

		#endregion

		#region NOTATKA POWITALNA

		//
		//dodaje notatkę powitalną do listy
		//
		public void AddFirstNote(int startCount, ObservableCollection<Note> notesList)
		{
			/*
			 * CEL:
			 * Dodaje pierwszą notatkę do list jeśli program został pierwszy raz uruchomiony
			 */

			//jeśli aplikację uruchomiono pierwszy raz to utwórz notatkę powitalną 
			if (startCount <= 1)
			{
				//dodaj notatkę do listy
				notesList.Add(CreateFirstNote());
			}
		}

		//
		//towrzy treść pierwszej notatki
		//
		private Note CreateFirstNote()
		{
			/*
			 * CEL:
			 * Tworzy pierszą notatkę na podstawie treści z tłumaczeń w zasobach
			 */

			//tytuł notatki - tłumaczenia w zasobach
			string title = AppResources.FirstNoteTitle;
			//treść notatki notatki - tłumaczenia w zasobach
			string note = AppResources.FirstNoteContent;

			//utwórz pierwszą notatkę
			//Note firstNote = CreateNewNote(title, note);
			Note firstNote = null;
			using (NotesAdder notesAdder = new NotesAdder())
			{
				firstNote = notesAdder.CreateNewNote(title, note, true);
			}

			//zwróć notatkę
			return firstNote;
		}

		#endregion

		#region DODAJ NOTATKĘ

		////
		////dodaje nową notatkę do notatnika
		////
		//public void AddNote(Note note, ref List<Note> notesList)
		//{
		//	/*
		//	 * CEL:
		//	 * Dodaje nową notatkę do notatnika
		//	 * 
		//	 * PARAMETRY WEJŚCIOWE:
		//	 * note:Note - notatka
		//	 * notesList:List<Notes> - lista notatke
		//	 */

		//	//sprawdz czy notatnik nie jest przepełniony
		//	if (notesList.Count >= MAX_NOTES_COUNT)
		//	{ 
		//		//jeśli notatnik nie jest przepełniony to dodaj notatkę
		//		notesList.Add(CreateNewNote(note.Title, note.Content));

		//		//dodano notatkę
		//		IsChangedNotesList = true;
		//	}

		//}

		////
		////tworzy nową notatkę
		////
		//private Note CreateNewNote(string title, string note, NoteCategory category = null)
		//{
		//	/*
		//	 * CEL:
		//	 * Tworzy i zwraca nową notatkę
		//	 * 
		//	 * PARAMETRY WEJŚCIOWE:
		//	 * title:string - tytuł notatki
		//	 * note:string - treść notatki
		//	 * category:NoteCategory - kategoria notatki
		//	 * 
		//	 * PARAMETRY WYJŚCIOWE:
		//	 * Note - utworzona notatka
		//	 */

		//	//nowa notatka
		//	Note newNote = new Note();
		//	//dodaj tytuł
		//	newNote.Title = title;
		//	//dodaj treść notatki
		//	newNote.Content = note;
		//	//dodaj kategorię
		//	//newNote.Category = category;
		//	//znajduje się w notatniku
		//	newNote.InTrash = false;
		//	//data utworzenia
		//	//newNote.CreationData = DateTime.Now;
		//	//data modyfikacji
		//	newNote.ChangeData = DateTime.Now;
		//	//data usunięcia
		//	newNote.DeleteData = DateTime.Now;
		//	//indeks
		//	//newNote.Id = CreateNoteIndex();

		//	//zwróć nową notatkę
		//	return newNote;
		//}

		////
		////sprawdza czy można dodać nową notatkę
		////
		//public bool AddingCheck(int notesCount)
		//{
		//	/*
		//	 * CEL:
		//	 * Sprawdza czy można dodać nową notatkę
		//	 * 
		//	 * WARTOŚĆ WEJŚCIOWA:
		//	 * notesCount:int - liczba notatek w liście
		//	 * 
		//	 * WARTOŚĆ WYJŚCIOWA:
		//	 * bool - gdy można dodać true w przeciwnym przypadku false
		//	 */

		//	//jeśli można dodać notatkę to zwróć true
		//	bool results = true;

		//	//sprawdz czy można dodać
		//	if (notesCount >= MAX_NOTES_COUNT)
		//	{ 
		//		//jeśli przekroczono liczbę notatek w notatniku
		//		//wyświetl komunikat
		//		MessageNotebookIsFull();
		//		//zwróć wartość false
		//		results = false;
		//	}

		//	//zwróć rezultat
		//	return results;
		//}

		////
		////zwraca nowy indeks notatki
		////
		//private int CreateNoteIndex()
		//{
		//	/*
		//	 * CEL:
		//	 * Generuje indeks z zakresu zmiennej int
		//	 * 
		//	 * WARTOŚĆ WYJŚĆIOWA:
		//	 * Wylosowany indeks
		//	 */

		//	//nowy indeks
		//	int newIndex = 0;
		//	//losuj indeks
		//	Random newRandom = new Random();
		//	//losuj z całego zakresu int
		//	newIndex = newRandom.Next(int.MaxValue);

		//	//zwróć indeks
		//	return newIndex;
		//}

		#endregion

		#region KOMUNIKATY
		//
		//komunikat - notatnik jest przepełniony
		//
		private void MessageNotebookIsFull()
		{
			/*
			 * CEL:
			 * Wyśwetla komunikat informujący, że nie można dodać nowej notatki
			 * ponieważ notatnik jest przepełniony
			 */

			//tytuł
			string title = AppResources.MsgNotebookIsFull;
			//wiadomość
			string message = AppResources.MsgIfAddNewNote;
			//wyświetl komunikat
			MessageBox.Show(message, title, MessageBoxButton.OK);
		}
		#endregion
	}
}
