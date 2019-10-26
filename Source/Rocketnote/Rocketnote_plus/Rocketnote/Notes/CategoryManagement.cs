using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Rocketnote.Resources;

namespace Rocketnote.Notes
{
	public class CategoryManagement
	{
		//lista kategori
		public List<NoteCategory> CategoryList;
		//plik kategori

		//
		//konstruktor
		//
		public CategoryManagement()
		{
			//inicjalizuj listę kategori
			CategoryList = new List<NoteCategory>();

			//dodaj kategorie
			CreateCategoryList();
		}

		public SolidColorBrush GetCategoryColor(int index)
		{
			return CategoryList[index].Color;
		}

		public string GetCategoryName(int index)
		{
			return CategoryList[index].Name;
		}

		private void CreateCategoryList()
		{
			//dodaj kategorie
			CategoryList.Add(new NoteCategory { Color = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)), Name = AppResources.TextCategory0 });
			CategoryList.Add(new NoteCategory { Color = new SolidColorBrush(Color.FromArgb(255, 255, 170, 14)), Name = AppResources.TextCategory1 });
			CategoryList.Add(new NoteCategory { Color = new SolidColorBrush(Color.FromArgb(255, 137, 201, 7)), Name = AppResources.TextCategory2 });
			CategoryList.Add(new NoteCategory { Color = new SolidColorBrush(Color.FromArgb(255, 202, 92, 243)), Name = AppResources.TextCategory3 });
			CategoryList.Add(new NoteCategory { Color = new SolidColorBrush(Color.FromArgb(255, 57, 137, 205)), Name = AppResources.TextCategory4 });
			CategoryList.Add(new NoteCategory { Color = new SolidColorBrush(Color.FromArgb(255, 209, 14, 58)), Name = AppResources.TextCategory5 });
			CategoryList.Add(new NoteCategory { Color = new SolidColorBrush(Color.FromArgb(255, 75, 102, 101)), Name = AppResources.TextCategory6 });
		}

		#region SINGLETON

		private static CategoryManagement _instance = null;
		public static CategoryManagement Instance
		{
			get
			{
				if (_instance == null) _instance = new CategoryManagement();
				return _instance;
			}
		}

		#endregion
	}
}
