using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Rocketnote.ViewModels;
using System.Windows.Input;
using System.Windows;
using System.IO.IsolatedStorage;
using System.IO;
using Rocketnote.Resources;
using Rocketnote.Notes;


namespace Rocketnote
{
	public class RnModelView : INotifyPropertyChanged
	{
		//plik z danymi
		private const string DATA_FILE = "RocketnoteData.xml";

		//konstruktor
		private RnModelView()
		{
			NotesList = new ObservableCollection<Note>();
			_appSettings = IsolatedStorageSettings.ApplicationSettings;	//ustawienia aplikacji

			//ustaw zmienne - wyzeruj
			TempNote = null;
			//odczytaj i ustaw indeks sortowania
			SortList = GetSortIndex();

			IsWritingNewNote = false;
			IsDataChanged = false;
			IsDataLoaded = false;
			_isSettingsChanged = false;

			//ustaw rozmiar notatnika
			NotebookSize = new int[] {16, 32, 64};
			//ustawienie rozmiaru kosza
			TrashSize = new int[] { 4, 8, 16};

			//odczytaj ustawiena aplikacji
			ReadSettings();

			//jesli to pierwsze uruchomienie to dodaj notatkę powitalną
			AddFirstNote();

			//wczytaj dane z pliku
			if (IsDataLoaded != true) LoadData();
			//if (IsDataLoaded != true) LoadTestData();
		}

		#region SINGLETON
			private static RnModelView _instance;
			public static RnModelView Instance
			{
				get
				{
					if (_instance == null) _instance = new RnModelView();
					return _instance;
				}
			}
		#endregion


		////v1.1.0.2
		#region LISTA NOTATEK I ZAWRACANIE NOTATEK

		//private ObservableCollection<Note> _notesList;
		public ObservableCollection<Note> NotesList { get; private set; }

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


		////v1.1.0.2
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


		#region DODAJ NOWĄ NOTATKĘ

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
					_addNote.AddNote(TempNote.Title, TempNote.Content, TempNote.IsHighPriority,NotesList);
				}

				// Wyczyść notatke tymczasową
				CleanTempNote();

				// Odświerz listę notatek
				NotifyPropertyChanged("GetNotesToNotebook");

				// Wrowadzono zmiany w liście notatek
				IsDataChanged = true;
			}
		}

		//wyczyść nową notatkę
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
			new NotesManagement().AddFirstNote(StartsCounter, NotesList);

			//odświerz listę notatek
			NotifyPropertyChanged("GetNotesToNotebook");

			//prowadzono zmiany w liście notatek
			IsDataChanged = true;
		}

		#endregion

		#region ZAPISZ I ODCZYTAJ DANE

		//sprawdza czy wprowadzono zmiany w danych
		public bool IsDataChanged { get; private set; }

		//sprawdza czy wczytano dane
		public bool IsDataLoaded { get; private set; }
		

		//wczytuje dane
		public void LoadData()
		{ 
			//jeśli jeszcze nie wczytano danych to wczytaj
			if (IsDataLoaded == false)
			{
				FileManagement fileManagement = new FileManagement();
				List<Note> tempList = fileManagement.OpenFromXml(DATA_FILE);

				foreach (Note note in tempList)
					NotesList.Add(note);
				//wczytano dane
				IsDataLoaded = true;
			}
		}

		public void SaveData()
		{ 
			//sprawdz czy wprowadzono zmiany w liście
			if (IsDataChanged == true)
			{ 
				//zapisz listę do pliku
				new FileManagement().SaveToXml(DATA_FILE, NotesList.ToList());
				//zapisano dane
				IsDataChanged = false;
			}
		}

		#endregion

		#region USTAWIENIA

		//ustawienia applikacji
		private IsolatedStorageSettings _appSettings;

		//sprawdza czy wprowadzono zmiany w ustawieniach
		private bool _isSettingsChanged;

        // Ustawienia motywu
        private bool _isDarkTheme = false;
        public bool IsDarkTheme
        {
            get { return _isDarkTheme; }
            set
            {
                if (_isDarkTheme != value)
                {
                    _isDarkTheme = value;
                    // Wprowadzono zmiany
                    _isSettingsChanged = true;
                    NotifyPropertyChanged("IsDarkTheme");
                }
            }
        }

		//ustawienia wyświetlania listy notatek
		// 0 - lista złożona
		// 1 - lista prosta
		private int _settingsShowNotesList = 1;
		public int SettingsShowNotesList
		{
			get { return _settingsShowNotesList; }
			set
			{
				if (_settingsShowNotesList != value)
				{
					_settingsShowNotesList = value;
					//wprowadzono zmiany
					_isSettingsChanged = true;
					NotifyPropertyChanged("SettingsShowNotesList");
					NotifyPropertyChanged("GetNotesToNotebook");
				}
			}
		}

		//ustawienie sortowania listy notatek
		// 0 - sortowanie alfabetyczne
		// 1 - sortowanie wg daty utowrzenia
		// 2 - sortowanie wg daty modyfikacji
		private int _settingsSortNotesList = 1;
		public int SettingsSortNotesList
		{
			get { return _settingsSortNotesList; }
			set
			{
				if (_settingsSortNotesList != value)
				{
					_settingsSortNotesList = value;
					//wprowadzono zmiany
					_isSettingsChanged = true;
					NotifyPropertyChanged("SettingsSortNotesList");
					NotifyPropertyChanged("GetNotesToNotebook");
				}
			}
		}

		//ustawienie rozmiaru notesu
		public int[] NotebookSize { get; private set; }
		private int _settingsNotebookSizeIndex = 0;
		public int SettingsNotebookSizeIndex
		{
			get { return _settingsNotebookSizeIndex; }
			set 
			{
				if (_settingsNotebookSizeIndex != value)
				{ 
					//sprawdz czy mozna dokonać zmianę
					if (NotebookSize[value] <= GetNotesToNotebook.Count)
					{
						MessageBox.Show(AppResources.MsgDeleteOrMoveNote, AppResources.MsgNotebookIsFull, MessageBoxButton.OK);
					}
					else 
					{
						_settingsNotebookSizeIndex = value;
						NotifyPropertyChanged("SettingsNotebookSize");
					}
				}
			}
		}

		//licznik uruchomień programu
		private int _startsCounter = 0;
		public int StartsCounter
		{
			get { return _startsCounter; }
			set { if (value != _startsCounter) _startsCounter = value; }
		}

		//ustawienie rozmiaru kosza
		public int[] TrashSize { get; private set; }
		private int _settingsTrashSizeIndex = 0;
		public int SettingsTrashSizeIndex
		{
			get { return _settingsTrashSizeIndex; }
			set
			{
				if (_settingsTrashSizeIndex != value)
				{
					//sprawdz czy kosz jest pełny
					if (GetNotesToTrash.Count > TrashSize[value])
					{
						//wyświetl komunikat
						MessageBoxResult msgres = MessageBox.Show(AppResources.MsgDeleteOldestNotes, AppResources.MsgTrashIsFull, MessageBoxButton.OKCancel);
						if (msgres == MessageBoxResult.OK)
						{
							//ustaw dane
							_settingsTrashSizeIndex = value;
							//NotifyPropertyChanged("SettingsTrashSize");
							//usuń najstarsze notatki
							DeleteOldestNotesInTrash();

							NotifyPropertyChanged("GetNotesToTrash");
						}
					}
					else
					{
						//jeśli kosz nie jest przepełniony ustaw dane
						_settingsTrashSizeIndex = value;
						
					}
					NotifyPropertyChanged("SettingsTrashSize");
				}
			}
		}

		//zapisuje ustawienia aplikacji
		public void SaveSettings()
		{ 
			/*
			 * CEL:
			 *	Zapisuje ustawienia aplikacji w pamięci
			 */

			//jeśli ustawiania zostały zmienione to zapisz je
			if (_isSettingsChanged)
			{
				////zapisz ustawienia sortowania
				//if (_appSettings.Contains("sortNotesList"))
				//	_appSettings["sortNotesList"] = SettingsSortNotesList;
				//else _appSettings.Add("sortNotesList", SettingsSortNotesList);

				//zapisz ustawienia rozmiaru notatnika i kosza
				if (_appSettings.Contains("notebookSizeIndex"))
					_appSettings["notebookSizeIndex"] = SettingsNotebookSizeIndex;
				else _appSettings.Add("notebookSizeIndex", SettingsNotebookSizeIndex);

				if (_appSettings.Contains("trashSizeIndex"))
					_appSettings["trashSizeIndex"] = SettingsTrashSizeIndex;
				else _appSettings.Add("trashSizeIndex", SettingsTrashSizeIndex);

				//zapisz licznik uruchomień
				if (_appSettings.Contains("startsCounter"))
					_appSettings["startsCounter"] = StartsCounter;
				else _appSettings.Add("startsCounter", StartsCounter);

                // Zapisz ustawienia motywu
                if (_appSettings.Contains("themes"))
                    _appSettings["themes"] = IsDarkTheme;
                else _appSettings.Add("themes", IsDarkTheme);

				//zapisz
				_appSettings.Save();
				//brak nowych ustawień
				_isSettingsChanged = false;
			}
		}

		//odczytuje ustawienia aplikacji
		public void ReadSettings()
		{
			/*
			 * CEL:
			 *	Odczytuje ustawienia z pamięci
			 */

			////odczytaj ustawienia sortowania
			//if (_appSettings.Contains("sortNotesList")) 
			//	SettingsSortNotesList = (int)_appSettings["sortNotesList"];

			//odczytaj ustawienia rozmiaru notatnika i kosza
			if (_appSettings.Contains("notebookSizeIndex"))
				SettingsNotebookSizeIndex = (int)_appSettings["notebookSizeIndex"];

			if (_appSettings.Contains("trashSizeIndex"))
				SettingsTrashSizeIndex = (int)_appSettings["trashSizeIndex"];

			//odczytaj licznik uruchomień
			if (_appSettings.Contains("startsCounter"))
				StartsCounter = (int)_appSettings["startsCounter"];

            // Odczytaj ustawienia motywu
            if (_appSettings.Contains("themes"))
                IsDarkTheme = (bool)_appSettings["themes"];
            else IsDarkTheme = false;

			//zwieksz licznik uruchomień
			StartsCounter++;
			_isSettingsChanged = true;
		}

		public void ResetNotesList()
		{
			MessageBoxResult _messageBoxResults = MessageBox.Show(AppResources.MsgAllNotesDeleted, AppResources.MsgDeleteAllNotes, MessageBoxButton.OKCancel);
			if (_messageBoxResults == MessageBoxResult.OK)
			{
				//MessageBox.Show("Usuwam");
				NotesList.Clear();

				IsDataChanged = true;
				NotifyPropertyChanged("GetNotesToNotebook");
				NotifyPropertyChanged("GetNotesToTrash");
			}
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
			SelectedNote = tempNote;
		}

		#endregion

		#region WYBRANA NOTATKA

		//wybrana notatka
		private Note _selectedNote;
		public Note SelectedNote
		{
			get
			{
				if (_selectedNote == null) _selectedNote = new Note();
				return _selectedNote;
			}
			set
			{
				if (_selectedNote != value) _selectedNote = value;
				NotifyPropertyChanged("SelectedNote");
			}
		}

		#endregion

		#region USUWANIE NOTATEK

		public void MoveToTrash()
		{ 
			/*
			 * CEL:
			 *	Przesuwa notatkę do kosza
			 */

			//sprawdz czy kosz jest przepełniony
			if (GetNotesToTrash.Count >= TrashSize[SettingsTrashSizeIndex])
			{
				//wyświetl komunikat
				MessageBoxResult msgres = MessageBox.Show(AppResources.MsgDeleteOldestNotes, AppResources.MsgTrashIsFull, MessageBoxButton.OKCancel);
				if (msgres == MessageBoxResult.OK)
				{
					//usuń najstarsze notatki
					DeleteOldestNotesInTrash();
					//przesuń notatkę
					//wyszukaj indeks wskazanej notatki
					int index = GetSelectedNoteIndex(SelectedNote, NotesList.ToList());
					//przenieś do kosza
					SelectedNote.InTrash = true;
					SelectedNote.DeleteData = DateTime.Now;
					//uaktualnij notatkę w liście
					//NotesList[index] = SelectedNote;
					NotesList.Remove(SelectedNote);
					NotesList.Add(SelectedNote);

					//wprowadzono zmiany w liście
					IsDataChanged = true;
					//odświerz listy notatek
					NotifyPropertyChanged("GetNotesToNotebook");
					NotifyPropertyChanged("GetNotesToTrash");
				}

			}
			else
			{
				//wyszukaj indeks wskazanej notatki
				int index = GetSelectedNoteIndex(SelectedNote, NotesList.ToList());
				//przenieś do kosza
				SelectedNote.InTrash = true;
				SelectedNote.DeleteData = DateTime.Now;
				//uaktualnij notatkę w liście
				//NotesList[index] = SelectedNote;
				NotesList.Remove(SelectedNote);
				NotesList.Add(SelectedNote);

				// Usuń kafelek
				DeleteTile();

				//wprowadzono zmiany w liście
				IsDataChanged = true;
				//odświerz listy notatek
				NotifyPropertyChanged("GetNotesToNotebook");
				NotifyPropertyChanged("GetNotesToTrash");
			}
		}

		public void MoveToActiveNotes()
		{
			/*
			 * CEL:
			 *	Przesuwa notatkę do aktywnch notatek
			 */

			//wyszukaj indeks wskazanej notatki
			int index = GetSelectedNoteIndex(SelectedNote, NotesList.ToList());
			//przenieś do kosza
			SelectedNote.InTrash = false;
			//uaktualnij notatkę w liście
			//NotesList[index] = SelectedNote;
			NotesList.Remove(SelectedNote);
			NotesList.Add(SelectedNote);
			//NotesList.Insert(index, SelectedNote);

			//wprowadzono zmiany w liście
			IsDataChanged = true;
			//odświerz listy notatek
			NotifyPropertyChanged("GetNotesToNotebook");
			NotifyPropertyChanged("GetNotesToTrash");
		}

		private int GetSelectedNoteIndex(Note selectedNote, List<Note> list)
		{
			/*
			 * CEL:
			 *	Zwraca indeks wybranej notatki
			 *	
			 * PARAMETRY:
			 *	selectedNote:Note - wybrana notatka
			 *	list:List<Note> - lista notatek
			 */

			int index;
			//przeszukaj listę
			for (index = 0; index < list.Count; index++)
			{
				if (selectedNote.Id == list[index].Id) break;
				//if (list[index].Equals(selectedNote)) break;
			}
			//
			return index;
		}

		//usuwa wszystkie notatki w koszu
		public void DeleteAllNotesInTrash()
		{
			foreach (Note note in GetNotesToTrash)
			{
				if (note.InTrash == true)
					NotesList.Remove(note);
			}
				
				//wprowadzono zmiany w liście
				IsDataChanged = true;
			//odświerz listy notatek
				NotifyPropertyChanged("GetNotesToNotebook");
			NotifyPropertyChanged("GetNotesToTrash");
		}

		//zwraca indeks najstarszej notatki
		private int GetIndexOldestNoteInTrash()
		{
			/*
			 * CEL:
			 *	Zwraca indeks najstarszej notatki w koszu
			 */

			//najstarsza notatka - początkowo ustawiona na pierwszą
			Note oldNote = GetNotesToTrash[0];
			//przesukaj wszystkie notatki
			foreach (var note in GetNotesToTrash)
			{ 
				//jeśli data usunęcia notatki oldNote jest mniejsza od note to oldNote = note
				if (oldNote.DeleteData > note.DeleteData) oldNote = note;
			}
			return oldNote.Id;
		}

		//usuwa notatkę z kosza o podanym indeksie
		private void DeleteOnceNoteInTrash(int index)
		{
			/*
			 * CEL:
			 *	Usuwa notatkę z kosza o podanym indeksie
			 */

			//przesukaj wszystkie notatki
			foreach (var note in NotesList)
			{ 
				//jesli indeks notatki jest równy podanemu indeksowi to usuń ją
				if (note.Id == index)
				{
					NotesList.Remove(note);
					break;
				}
			}
		}

		private void DeleteOldestNotesInTrash()
		{ 
			//dopuki notatek jest wiecej lub równo niż limit to usuwaj
			while(GetNotesToTrash.Count >= TrashSize[SettingsTrashSizeIndex])
			{
				//usuń najstarszą
				DeleteOnceNoteInTrash(GetIndexOldestNoteInTrash());
			}
			IsDataChanged = true;
		}

		#endregion

		#region EDYTOWANIE NOTATKI

		public void SaveEditedNote()
		{
			/*
			 * CEL:
			 *	Zapisuje edytowaną notatkę
			 */

			//wyszukaj indeks wskazanej notatki
			int index = GetSelectedNoteIndex(SelectedNote, NotesList.ToList());
			//przenieś do kosza
			SelectedNote.ChangeData = DateTime.Now;
			//uaktualnij notatkę w liście
			//NotesList[index] = SelectedNote;
			NotesList.RemoveAt(index);
			NotesList.Add(SelectedNote);

			// Uaktualnij kafelek
			UpdateTile();

			//wprowadzono zmiany w liście
			IsDataChanged = true;
			//odświerz listy notatek
			NotifyPropertyChanged("GetNotesToNotebook");
		}

		#endregion

		////v1.1.0.2
		#region UDOSTĘPNIJ NOTATKĘ

		//
		//udostępnia notatkę za pomocą sms
		////v1.1.0.2
		public void ShareViaSms()
		{
			/*
			 * CEL:
			 * Udostępnia notatkę za pomocą sms
			 */

			//udostępnij
			using (ShareManagement sm = new ShareManagement())
			{
				sm.ShareViaSms(SelectedNote.Title, SelectedNote.Content);
			}
		}

		//
		//udostępnia notatkę za pomocą email
		////v1.1.0.2
		public void ShareViaEmail()
		{
			/*
			 * CEL:
			 * Udostępnia notatkę za pomocą email
			 */

			//udostępnij
			using (ShareManagement sm = new ShareManagement())
			{
				sm.ShareViaEmail(SelectedNote.Title, SelectedNote.Content);
			}
		}

		//
		//udostępnia notatkę jako status w sieci
		////v1.1.0.2
		public void ShareYourStatus()
		{
			/*
			 * CEL:
			 * Udostępnia notatkę jako status w sieci
			 */

			//udostępnij
			using (ShareManagement sm = new ShareManagement())
			{
				sm.ShareViaSocialNetwork(SelectedNote.Title, SelectedNote.Content);
			}
		}

		#endregion


		#region KAFELKI

		public bool TileExist()
		{
			bool tileExist = false;
			using (TileManager tm = new TileManager(SelectedNote))
			{
				tileExist = tm.TileExist();
			}

			return tileExist;
		}


		public void CreateTile()
		{
			using (TileManager tm = new TileManager(SelectedNote))
			{
				tm.CreateTile();
			}
		}


		public void UpdateTile()
		{
			using (TileManager tm = new TileManager(SelectedNote))
			{
				tm.UpdateTile();
			}
		}


		public void DeleteTile()
		{ 
			using(TileManager tm = new TileManager(SelectedNote))
			{
				tm.DeleteTile();
			}
		}

		#endregion

		//public void LoadTestData()
		//{
		//	NotesList.Add(new Note() {Title = "Zebranie", Content = "Zawiadomić wszystkich o zebraniu w klubie wędkarskim", CreationData = DateTime.Now, ChangeData = DateTime.Now, InCloud = false, InTrash = false});
		//	NotesList.Add(new Note() { Title = "Zrobić sprawozdanie", Content = "Sprawozdanie wykonać na podstawie danych z laboratorium", CreationData = DateTime.Now, ChangeData = DateTime.Now, InCloud = false, InTrash = false });
		//	NotesList.Add(new Note() { Title = "Ocenić pracowników", Content = "Ocena pracowników na poniedziałek", CreationData = DateTime.Now, ChangeData = DateTime.Now, InCloud = false, InTrash = false });
		//	NotesList.Add(new Note() { Title = "Lista zakupów", Content = "Wykonać nową listę zakupów", CreationData = new DateTime(2013, 4, 12), ChangeData = new DateTime(2013, 5, 21), InCloud = false, InTrash = false });
		//	NotesList.Add(new Note() { Title = "Kod doskonały", Content = "Książa dostępna w księgarni Helion", CreationData = new DateTime(2013, 5, 30), ChangeData = new DateTime(2013, 6, 30), InCloud = false, InTrash = false });
		//	NotesList.Add(new Note() { Title = "Wycieczka do Karpacza!", Content = "Zabrać aparat, polar, buty...", CreationData = new DateTime(2013, 2, 23), ChangeData = new DateTime(2013, 3, 23), InCloud = false, InTrash = false });
		//	NotesList.Add(new Note() { Title = "Wizyta u dentysty", Content = "Huta, za tydzień", CreationData = new DateTime(2013, 2, 23), ChangeData = DateTime.Now, InCloud = false, InTrash = true });
		//	NotesList.Add(new Note() { Title = "Nalewka z pigwy", Content = "Pigwa, wódka, cukier", CreationData = new DateTime(2013, 2, 23), ChangeData = DateTime.Now, InCloud = false, InTrash = true });
		//	NotesList.Add(new Note() { Title = "Kupić wino", Content = "Nie wiecej niż 30 zl.", CreationData = DateTime.Now, ChangeData = DateTime.Now, InCloud = false, InTrash = false });
			
		//	IsDataLoaded = true;
		//}

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

		#region TESTER

		public long DataFileSize { get; set; }

		public int NotebookCount { get { return GetNotesToNotebook.Count(); } }
		public int TrashCount { get { return GetNotesToTrash.Count(); } }

		public long GetDataFileSize()
		{
			long size = 0;

			string fileName = DATA_FILE;

			using (var isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
			{
				using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
				{
					if (myIsolatedStorage.FileExists(fileName))
					{
						using (IsolatedStorageFileStream stream = myIsolatedStorage.OpenFile(fileName, FileMode.Open))
						{
							size = stream.Length-157;
						}
					}
				}
			}

			return size;
		}

		public void AddTestNotes()
		{
			for (int i = 0; i < 10; i++)
			{
				NotesList.Add(
					new Note()
					{
						Title = "ToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJe",
						Content = "ToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJestTestowaNotatkaToJe"
					}
					);
			}

			DataFileSize = GetDataFileSize();

			NotifyPropertyChanged("GetActiveNotes");
			NotifyPropertyChanged("NotebookCount");
			NotifyPropertyChanged("TrashCount");
			NotifyPropertyChanged("DataFileSize");
			IsDataChanged = true;
			SaveData();
		}

		public int GetSaveTestTime { get; set; }

		public void SaveTest()
		{
			int count = 100;
			int testTime = 0;

			for (int i = 0; i < count; i++)
			{
				IsDataChanged = true;
				DateTime start = DateTime.Now;
				SaveData();
				DateTime stop = DateTime.Now;
				testTime += (int)((stop - start).TotalMilliseconds);
			}

			GetSaveTestTime = testTime / count;
			NotifyPropertyChanged("GetSaveTestTime");
		}

		public int GetReadTestTime { get; set; }

		public void ReadTest()
		{
			int count = 100;
			int testTime = 0;

			for (int i = 0; i < count; i++)
			{
				IsDataLoaded = false;
				NotesList.Clear();
				DateTime start = DateTime.Now;
				LoadData();
				DateTime stop = DateTime.Now;
				testTime += (int)((stop - start).TotalMilliseconds);
			}

			GetReadTestTime = testTime / count;
			NotifyPropertyChanged("GetReadTestTime");
		}

		#endregion
	}
}
