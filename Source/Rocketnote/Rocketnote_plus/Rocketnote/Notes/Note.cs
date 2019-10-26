using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Rocketnote.Notes
{
	public class Note
	{
		// Zarządzanie kategoriami
		private CategoryManagement _category = CategoryManagement.Instance;

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

		// Znacznik określający czy notatka jest przypięta do menu start
		public bool IsPinnedToStart { get; set; }

		// Kategoria notatki
		public int Category {get; set;}

		// Zwraca nazwę kategori
		public string CategoryName
		{
			get { return _category.GetCategoryName(Category); }
		}

		// Zwraca kolor kategori
		public SolidColorBrush CategoryColor
		{
			get { return _category.GetCategoryColor(Category); }
		}

		// Znacznik określający czy ustawiony jest alarm
		public bool IsAlarmSet { get; set; }

		// Data alarmu dla notatki
		public DateTime AlarmData { get; set; }

		//
		// Konstruktor
		//
		public Note() { }

	}
}
