using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Phone.Shell;
using System.Windows.Media;

namespace Rocketnote.Notes
{
	public class TileManager : IDisposable
	{
		Note _note = null;

		public TileManager(Note note)
		{
			_note = note;
		}

		public bool TileExist()
		{
			//kafelek nie istnieje
			bool exist = false;

			string tileParameter = _note.Id.ToString();
            ShellTile tile = CheckIfTileExist(tileParameter);
			if (tile != null) exist = true;
			
			return exist;
		}

		public void CreateTile()
		{ 
			string tileParameter = _note.Id.ToString();
            ShellTile tile = CheckIfTileExist(tileParameter);
			if (tile == null)
			{
				FlipTileData secondaryTile = new FlipTileData() 
				{
					WideBackBackgroundImage = new Uri("/Images/secTileBackground.png", UriKind.Relative),
					Title = "Rocketnote+",

					WideBackContent = _note.Content,
					BackContent = _note.Content,
					BackTitle = _note.Title,
					
				};

				//IconicTileData secondaryTile = new IconicTileData()
				//{
				//	BackgroundColor = (_note.CategoryColor).Color,
				//	Title = "Rocketnote+",
				//	WideContent1 = _note.Title,
				//	WideContent2 = _note.Content
				//};

				//StandardTileData secondaryTile = new StandardTileData
				//{
				//	Title = "Rocketnote",
				//	BackTitle = _note.Title,
				//	BackgroundImage = new Uri("/Images/secTileBackground.png", UriKind.Relative),
				//	BackBackgroundImage = new Uri("/Images/secTileBackground.png", UriKind.Relative),
				//	//Count = 2,
				//	BackContent = _note.Content
				//};
				//Utwórz kafelek
				ShellTile.Create(new Uri("/Pages/ViewNotePage.xaml?selectedItem=" + tileParameter, UriKind.Relative), secondaryTile, true);
				
			}
		}


		public void UpdateTile()
		{
			string tileParameter = _note.Id.ToString();
			ShellTile tile = CheckIfTileExist(tileParameter);
			if (tile != null)
			{
				StandardTileData secondaryTile = new StandardTileData
				{
					Title = "Rocketnote",
					BackTitle = _note.Title,
					BackgroundImage = new Uri("/Images/secTileBackground.png", UriKind.Relative),
					BackBackgroundImage = new Uri("/Images/secTileBackground.png", UriKind.Relative),
					//Count = 2,
					BackContent = _note.Content
				};
				// Uaktualnij kafelek
				tile.Update(secondaryTile);
			}
		}


		public void DeleteTile()
		{ 
			string tileParameter = _note.Id.ToString();
			ShellTile tile = CheckIfTileExist(tileParameter);
			if (tile != null)
			{
				tile.Delete();
			}
		}

		private ShellTile CheckIfTileExist(string tileUri)
		{
			ShellTile shellTile = ShellTile.ActiveTiles.FirstOrDefault(
					tile => tile.NavigationUri.ToString().Contains(tileUri));
			return shellTile;
		}

		public void Dispose()
		{
			//throw new NotImplementedException();
		}
	}
}
