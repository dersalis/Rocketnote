using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rocketnote.Resources;
using System.Windows;
using System.Collections.ObjectModel;

namespace Rocketnote.Notes
{
	public class NotesAdder : IDisposable
	{
		//maksymalna liczba notatek w notatniku
		private const byte MAX_NOTES_COUNT = 128;

		//
		// Konstruktor
		//
		public NotesAdder() { }

		//
		// Dodaje nową notatkę do notatnika
		//
		public void AddNote(string title, string content, ObservableCollection<Note> notesList)
		{
			/*
			 * CEL:
			 * Dodaje nową notatkę do notatnika
			 * 
			 * PARAMETRY WEJŚCIOWE:
			 * note:Note - notatka
			 * notesList:List<Notes> - lista notatke
			 */

			// Liczba notatek w notatniku
			int notebookCount = GetNotebookCount(notesList.ToList());

			// Sprawdz czy notatnik nie jest przepełniony
			if (notebookCount <= MAX_NOTES_COUNT)
			{
				// Jeśli notatnik nie jest przepełniony to dodaj notatkę
				notesList.Add(CreateNewNote(title, content));
			}

		}

		//
		// Tworzy nową notatkę
		//
		private Note CreateNewNote(string title, string note, NoteCategory category = null)
		{
			/*
			 * CEL:
			 * Tworzy i zwraca nową notatkę
			 * 
			 * PARAMETRY WEJŚCIOWE:
			 * title:string - tytuł notatki
			 * note:string - treść notatki
			 * category:NoteCategory - kategoria notatki
			 * 
			 * PARAMETRY WYJŚCIOWE:
			 * Note - utworzona notatka
			 */

			// Nowa notatka
			Note newNote = new Note();
			// Dodaj tytuł
			newNote.Title = title;
			// Dodaj treść notatki
			newNote.Content = note;
			// Dodaj kategorię
			//newNote.Category = category;
			// Notatka nie znajduje się w koszu tylko notatniku
			newNote.InTrash = false;
			// Data utworzenia
			newNote.CreationData = DateTime.Now;
			// Data modyfikacji
			newNote.ChangeData = DateTime.Now;
			// Data usunięcia
			newNote.DeleteData = DateTime.Now;
			// Indeks
			newNote.Id = CreateNoteIndex();

			//zwróć nową notatkę
			return newNote;
		}

		//
		// Sprawdza czy można dodać nową notatkę
		//
		public bool CheckAdding(int notesCount)
		{
			/*
			 * CEL:
			 * Sprawdza czy można dodać nową notatkę
			 * 
			 * WARTOŚĆ WEJŚCIOWA:
			 * notesCount:int - liczba notatek w liście
			 * 
			 * WARTOŚĆ WYJŚCIOWA:
			 * bool - gdy można dodać true w przeciwnym przypadku false
			 */

			// Jeśli można dodać notatkę to zwróć true
			bool results = true;

			// Sprawdz czy można dodać
			if (notesCount >= MAX_NOTES_COUNT)
			{
				// Jeśli przekroczono liczbę notatek w notatniku
				// Wyświetl komunikat
				MessageNotebookIsFull();
				// Zwróć wartość false
				results = false;
			}

			// Zwróć rezultat
			return results;
		}

		//
		// Zwraca nowy indeks notatki
		//
		private int CreateNoteIndex()
		{
			/*
			 * CEL:
			 * Generuje indeks z zakresu zmiennej int
			 * 
			 * WARTOŚĆ WYJŚĆIOWA:
			 * Wylosowany indeks
			 */

			// Nowy indeks
			int newIndex = 0;
			// Losuj indeks
			Random newRandom = new Random();
			// Losuj z całego zakresu int
			newIndex = newRandom.Next(int.MaxValue);

			// Zwróć indeks
			return newIndex;
		}

		//
		// Ustawia liczbę notatek w notatniku
		//
		private int GetNotebookCount(List<Note> NotesList)
		{
			/*
			 * CEL: 
			 * Zwraca liczbę notatek w notatniku
			 */

			// Początkowa liczba notatek
			int count = 0;
			// Policz notatki
			count = (from note in NotesList where note.InTrash == false select note).Count();
			// Zwróć liczbę notatek
			return count;
		}


		//
		// Komunikat - notatnik jest przepełniony
		//
		private void MessageNotebookIsFull()
		{
			/*
			 * CEL:
			 * Wyśwetla komunikat informujący, że nie można dodać nowej notatki
			 * ponieważ notatnik jest przepełniony
			 */

			// Tytuł
			string title = AppResources.MsgNotebookIsFull;
			// Wiadomość
			string message = AppResources.MsgIfAddNewNote;
			// Wyświetl komunikat
			MessageBox.Show(message, title, MessageBoxButton.OK);
		}

		public void Dispose()
		{
			//throw new NotImplementedException();
		}
	}
}
