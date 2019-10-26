using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Rocketnote.Notes;

namespace Rocketnote.Notes
{
	public class Note
	{
		//// Zarządzanie kategoriami
		//private CategoryManagement _category = CategoryManagement.Instance;

		// Id notatki
		public int Id { get; set; }

		// Tytuł notatki
		public string Title { get; set; }

		// Treść notatki
		public string Content { get; set; }

		// Data utworzenia notatki
		public DateTime CreationData { get; set; }

		// Data edycji notatki - ustawiana jest przez konstruktor
		public DateTime ChangeData { get; set; }

		// Data usunięcia - ustawiane przez konstruktor
		public DateTime DeleteData { get; set; }

		// Znacznik określający czy notatka znajduje się w koszu
		public bool InTrash { get; set; }

        // Znacznik określający czy notatka ma wysoki priorytet
        public bool IsHighPriority { get; set; }

		//
		// Konstruktor
		//
		public Note() { }


		//public int Id { get; set; }
		//public string Title { get; set; }
		//public string Content { get; set; }
		//private DateTime _creationData;
		//public DateTime CreationData
		//{
		//	get { return _creationData; }
		//	set { if (_creationData != value) _creationData = value; }
		//}
		//private DateTime _changeData;
		//public DateTime ChangeData
		//{
		//	get { return _changeData; }
		//	set { if (_changeData != value) _changeData = value; }
		//}
		//private DateTime _deleteData;
		//public DateTime DeleteData
		//{
		//	get { return _deleteData; }
		//	set { if (_deleteData != value) _deleteData = value; }
		//}

		//public bool InCloud { get; set; }
		//public bool InTrash { get; set; }

		//public NoteCategory Category {get; set;}
		////public Note() { CreationData.; }
	}
}
