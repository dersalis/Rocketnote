using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Rocketnote.Notes
{
	public class Rocketnote : INotifyPropertyChanged
	{

		//
		//Konstruktor
		//
		public Rocketnote() 
		{ 
			//zainicjuj listę notatek
			NotesList = new ObservableCollection<Note>();
			//odczytaj indeks sortowania
			SortList = GetSortIndex();
			//notatka tymczasowa pusta
			TempNote = null;

			//ustaw znaczniki
			IsDataLoaded = false;
			IsDataChanged = false;


			//wczytanie tymczasowych danych
			LoadTestData();
		}

		#region SINGLETON

		private static Rocketnote _instance = null;
		public static Rocketnote Instance
		{
			get
			{
				if (_instance == null) _instance = new Rocketnote();
				return _instance;
			}
		}

		#endregion

		#region LISTA NOTATEK

		//lista notatek
		public ObservableCollection<Note> NotesList { get; private set; }

		#endregion

		#region TYMCZASOWA NOTATKA

		//tymczasowa notatka
		private Note _tempNote;
		public Note TempNote
		{
			get { return _tempNote; }
			set
			{
				if (_tempNote != value) _tempNote = value;
				NotifyPropertyChanged("TempNote");
			}
		}

		#endregion

		#region ZWRACANIE NOTATEK

		//indeks sortowania
		private int _sortList;
		public int SortList
		{
			get { return _sortList; }
			set
			{
				if (_sortList != value)
				{
					_sortList = value;
					new NotesGeter().SortIndex = _sortList;
					NotifyPropertyChanged("SortList");
					NotifyPropertyChanged("GetNotesToNotebook");
				}
			}
		}

		//zwraca aktywne notatki
		////v1.1.0.2
		public List<Note> GetNotesToNotebook
		{
			get { return GetNotebook(); }
		}

		//
		//zwraca listę notatek do notatnika
		////v1.1.0.2
		private List<Note> GetNotebook()
		{
			/*
			 * CEL:
			 * Zwraca liczbę notatek do notatnika
			 */

			//lista notatek
			List<Note> notebook = null;

			using (NotesGeter ng = new NotesGeter())
			{
				notebook = ng.GetNotesToNotebook(NotesList.ToList(), SortList);
			}
			//zwróć listę
			return notebook;
		}
		//
		//zwraca listę notatek znajdujących się w śmietniku posortowaną wg daty utowrzenia
		////v1.1.0.2
		public List<Note> GetNotesToTrash
		{
			get
			{
				//sortowanie wg daty utworzenia malejąco oraz alfabetyczne
				return GetTrash();
			}
		}

		//
		//zwraca listę notatek do kosza
		////v1.1.0.2
		private List<Note> GetTrash()
		{
			/*
			 * CEL:
			 * Zwraca listę notatek do kosza
			 */

			//lista notatek
			List<Note> trash = null;

			using (NotesGeter ng = new NotesGeter())
			{
				trash = ng.GetNotesToTrash(NotesList.ToList());
			}
			//zwróć listę
			return trash;
		}

		//
		//zwraca indeks sortowania
		////v1.1.0.2
		private int GetSortIndex()
		{
			/*
			 * CEL:
			 * Zwraca indeks sortowania
			 */

			int index = 0;
			using (NotesGeter ng = new NotesGeter())
			{
				index = ng.SortIndex;
			}
			return index;
		}

		//
		//zapisuje indeks sortowania
		////v1.1.0.2
		private void SetSortIndex(int index)
		{
			/*
			 * CEL:
			 * Zapisuje indeks sortowania
			 */

			using (NotesGeter ng = new NotesGeter())
			{
				ng.SortIndex = index;
			}
		}

		#endregion

		// 2.0.0.2
		#region DODAWANIE NOTATEK

		// Znacznik określający czy wprowadzono nową notatkę
		public bool IsWritingNewNote { get; set; }

		//
		// Dodaje nową notatkę
		// v1.1.0.2
		public void AddNewNote()
		{
			/*
			 * CEL:
			 *	Zapisuje wprowadzoną notatkę
			 *	Zawartość notatki znajduje się w TempNote
			 */

			// Jeśli wprowadzono zmiany w nowej notatce to zapisz ją
			if (IsWritingNewNote == true)
			{
				// Dodaj notatkę
				using (NotesAdder _addNote = new NotesAdder())
				{
					_addNote.AddNote(TempNote.Title, TempNote.Content, NotesList);
				}

				// Wyczyść notatke tymczasową
				CleanTempNote();

				// Odświerz listę notatek
				NotifyPropertyChanged("GetNotesToNotebook");

				// Wrowadzono zmiany w liście notatek
				IsDataChanged = true;
			}
		}

		//
		// Sprawdza czy można dodać nową notatkę
		//
		public bool CheckAdding()
		{
			/*
			 * CEL:
			 * Sprawdza czy można dodać nową notatkę.
			 * Zwraca true gdy można dodać lub false gdy nie można dodać
			 * 
			 * WARTOŚĆ ZWRACANA:
			 * bool - wynik sprawdzania
			 */

			bool result = false;
			// Sprawdz czy można dodać nową notatkę
			using (NotesAdder notesAdder = new NotesAdder())
			{
				result = notesAdder.CheckAdding(GetNotesToNotebook.Count);
			}
			// Zwróć wnik
			return result;
		}


		//
		// Wyczyść nową notatkę
		//
		public void CleanTempNote()
		{
			/*
			 * CEL:
			 *	Usuwa treść wprowadzonej notatki
			 */

			//stan zmian w nowej notatce ustaw na false
			IsWritingNewNote = false;
			//wyczyść notatkę
			TempNote = null;
		}

		//
		//Dodaje pierwszą notatkę - notatkę powitalną
		//
		private void AddFirstNote()
		{
			/*
			 * CEL:
			 * Dodaje pierwszą notatkę do notatnika przy pierwszym uruchomieniu programu
			 */

			//dodaj pierwszą notatkę
			//new NotesManagement().AddFirstNote(StartsCounter, NotesList);

			//odświerz listę notatek
			NotifyPropertyChanged("GetNotesToNotebook");

			//prowadzono zmiany w liście notatek
			IsDataChanged = true;
		}

		#endregion

		#region ODCZYTYWANIE / ZAPISYWANIE NOTATEK DO PLIKU

		//znacznik odczytania dany z pliku
		public bool IsDataLoaded { get; private set; }
		//znacznik dokonania zmian w liście notatek
		public bool IsDataChanged { get; private set; }

		#endregion


		#region KAFELKI

		public bool TileExist()
		{
			bool tileExist = false;
			using (TileManager tm = new TileManager(TempNote))
			{
				tileExist = tm.TileExist();
			}

			return tileExist;
		}


		public void CreateTile()
		{
			using (TileManager tm = new TileManager(TempNote))
			{
				tm.CreateTile();
			}
		}


		public void UpdateTile()
		{
			using (TileManager tm = new TileManager(TempNote))
			{
				tm.UpdateTile();
			}
		}


		public void DeleteTile()
		{
			using (TileManager tm = new TileManager(TempNote))
			{
				tm.DeleteTile();
			}
		}

		#endregion

		#region TYMCZASOWE NOTATKI - TEST

		public void LoadTestData()
		{
			//NotesList.Add(new Note("Ulubione piwka", "Lista ulubionych piwek", 3));
			NotesList.Add(new Note() {Title = "Zebranie", Content = "Zawiadomić wszystkich o zebraniu w klubie wędkarskim, w piątek pierwszy tydzień sytcznia", ChangeData = DateTime.Now, InTrash = false, Category = 1});
			NotesList.Add(new Note() {Title = "Zrobić sprawozdanie", Content = "Sprawozdanie wykonać na podstawie danych z laboratorium", ChangeData = DateTime.Now,InTrash = false, Category = 2 });
			NotesList.Add(new Note() {Title = "Ocenić pracowników", Content = "Ocena pracowników na poniedziałek", ChangeData = DateTime.Now, InTrash = false, Category = 1 });
			NotesList.Add(new Note() {Title = "Lista zakupów", Content = "Wykonać nową listę zakupów", ChangeData = new DateTime(2013, 5, 21), InTrash = false, Category = 3 });
			NotesList.Add(new Note() {Title = "Kod doskonały", Content = "Książa dostępna w księgarni Helion", ChangeData = new DateTime(2013, 6, 30), InTrash = false, Category = 4 });
			NotesList.Add(new Note() {Title = "Wycieczka do Karpacza!", Content = "Zabrać aparat, polar, buty...", ChangeData = new DateTime(2013, 3, 23), InTrash = false, Category = 5 });
			NotesList.Add(new Note() {Title = "Wizyta u dentysty", Content = "Huta, za tydzień", ChangeData = DateTime.Now, InTrash = true, Category = 6 });
			NotesList.Add(new Note() {Title = "Nalewka z pigwy", Content = "Pigwa, wódka, cukier", ChangeData = DateTime.Now, InTrash = true, Category = 0 });
			NotesList.Add(new Note() {Title = "Kupić wino", Content = "Nie wiecej niż 30 zl.", ChangeData = DateTime.Now, InTrash = false, Category = 1 });

			IsDataLoaded = true;
		}

		#endregion

		#region WIDOK NOTATKI

		//
		// ustawia na tymczasową notatkę, notatkę o podanym indeksie
		//
		public void SetTemporaryNote(int index)
		{ 
			//wyszukaj notatkę o podanym indeksie
			Note tempNote = null;
			foreach (Note note in NotesList)
			{
				if (note.Id == index)
				{
					tempNote = note;
					break;
				}
			}
			//ustaw notatkę tymczasową
			TempNote = tempNote;
		}

		#endregion

		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged(String propertyName)
		{
			//PropertyChangedEventHandler handler = PropertyChanged;
			//if (null != handler)
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
				//handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
