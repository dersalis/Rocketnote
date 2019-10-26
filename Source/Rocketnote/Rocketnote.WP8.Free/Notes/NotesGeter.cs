using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.IsolatedStorage;
namespace Rocketnote.Notes
{
	public class NotesGeter : IDisposable
	{
		//ustawienia aplikacji - zapis/odczyt
		////v1.0.1.2
		IsolatedStorageSettings _appSettings;
		//nazwa ustawienia w którym będą zapisane ustawienia
		private const string SORT_INDEX = "SortIndexSetting";

		//index sortowani - zwraca zapisaną liczbę w ustawieniach oraz odczytuje z ustawień
		//v1.0.1.2
		public int SortIndex
		{
			get { return LoadSortIndex(); }
			set { SaveSortIndex(value); }
		}

		//
		//Konstruktor
		////v1.0.1.2
		public NotesGeter()
		{
			//ustawienia
			_appSettings = IsolatedStorageSettings.ApplicationSettings;	
		}

		//
		//zwraca notatki do notatnika
		////v1.0.1.2
		public List<Note> GetNotesToNotebook(List<Note> notesList, int sortIndex)
		{
			/*
			 * CEL:
			 * Zwraca notatki do notatnika
			 * 
			 * WARTOŚĆ WEJŚCIOWA:
			 * notestList:List<Note> - lista notatek
			 * sortIndex:int - indeks sotrowania
			 * ustawienie sortowania listy notatek
			 *  0 - sortowanie alfabetyczne
			 *  1 - sortowanie wg daty utowrzenia
			 *  2 - sortowanie wg daty modyfikacji
			 * 
			 * WARTOŚĆ WYJŚCIOWA:
			 * List<Note> - lista posortowanych notatek
			 */

			//lista zwróconych notatek
			List<Note> notes = new List<Note>();

			//wybierz sposób sortowania
			switch (sortIndex)
			{
				case 0:
					//notatki posortowane alfabetyczne
					notes = NotesSortedAlphabetically(notesList);
					break;
				case 1:
					//notatki posortowane wg daty utowrzenia
					notes = NotesSortedByCreationDate(notesList);
					break;
				case 2:
					//notatki posortowane wg daty modyfikacji
					notes = NotesSortedByChangedDate(notesList);
					break;
			}
			//zwróć listę notatek
			return notes;
		}

		//
		//zwraca listę notatek posortowaną alfabetycznie
		////v1.0.1.2
		private List<Note> NotesSortedAlphabetically(List<Note> notesList)
		{
			/*
			 * CEL:
			 * Zwraca listę notatek posortowaną alfabetycznie
			 * 
			 * WARTOŚĆ WEJŚCIOWA:
			 * notesList:List<Note> - lista notatek
			 * 
			 * WARTOŚĆ WYJŚCIOWA:
			 * List<Note> - lista posortowanych notatek
			 */

			//zwraca listę notatek posortowaną alfabetycznie
			return (from note in notesList where note.InTrash == false orderby note.Title select note).ToList();
		}

		//
		//zwraca listę notatek posortowaną wg daty utworzenia
		////v1.0.1.2
		private List<Note> NotesSortedByCreationDate(List<Note> notesList)
		{
			/*
			 * CEL:
			 * notesList:List<Note> - lista notatek
			 * 
			 * WARTOŚĆ WEJŚCIOWA:
			 * Zwraca listę notatek posortowaną wg daty utworzenia
			 * 
			 * WARTOŚĆ WYJŚCIOWA:
			 * List<Note> - lista posortowanych notatek
			 */

			//sortowanie wg daty utworzenia malejąco oraz alfabetyczne
			return (from note in notesList where note.InTrash == false orderby note.CreationData descending, note.Title ascending select note).ToList();
		}

		//
		//zwraca listę notatek posortowaną wg daty modyfikacji
		////v1.0.1.2
		private List<Note> NotesSortedByChangedDate(List<Note> notesList)
		{
			/*
			 * CEL:
			 * Zwraca listę notatek posortowaną wg daty modyfikacji
			 * 
			 * WARTOŚĆ WEJŚCIOWA:
			 * notesList:List<Note> - lista notatek
			 * 
			 * WARTOŚĆ WYJŚCIOWA:
			 * List<Note> - lista posortowanych notatek
			 */

			//sortowanie wg daty modyfikacji malejąco oraz alfabetyczne
			return (from note in notesList where note.InTrash == false orderby note.ChangeData descending, note.Title ascending select note).ToList();
		}

		//
		//zwraca listę notatek znajdujących się w śmietniku posortowaną wg daty usunięcia i alfabetycznie
		////v1.0.1.2
		public List<Note> GetNotesToTrash(List<Note> notesList)
		{
			/*
			 * CEL:
			 * notesList:List<Note> - lista notatek
			 * 
			 * WARTOŚĆ WEJŚCIOWA:
			 * Zwraca listę notatek znajdujących się w śmietniku posortowaną wg daty usunięcia i alfabetycznie
			 * 
			 * WARTOŚĆ WYJŚCIOWA:
			 * List<Note> - lista posortowanych notatek
			 */

			//posortowaną wg daty usunięcia i alfabetycznie
			return (from note in notesList where note.InTrash == true orderby note.DeleteData descending, note.Title ascending select note).ToList();
		}

		//
		//Odczytuje index sortowania
		////v1.0.1.2
		private int LoadSortIndex()
		{
			/*
			 * CEL:
			 * Odczytuje index sortowania z ustawień
			 * 
			 * WARTOŚĆ ZWRACANA:
			 * int - odczytana wartość
			 */

			//odczytaj wartość
			int sortIndex = 0;
			using (SettingsManagement sm = new SettingsManagement())
			{
				sortIndex = sm.LoadSortIndex(SORT_INDEX);
			}
			//zwróć
			return sortIndex;
		}

		//
		//Zapisuje indeks sortowania do ustawień
		////v1.0.1.2
		private void SaveSortIndex(int index)
		{
			/*
			 * CEL:
			 * Zapisuje indeks sortowania do ustawień
			 * 
			 * WARTOŚĆ WEJŚCIOWA:
			 * index:int - zapisywany indeks sortowania
			 */

			//zapisz ustawienia sortowania
			using (SettingsManagement sm = new SettingsManagement())
			{
				sm.SaveSortIndex(SORT_INDEX, index);
			}
		}

		public void Dispose()
		{
			//throw new NotImplementedException();
		}
	}
}
