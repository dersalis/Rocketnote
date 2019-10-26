using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.IsolatedStorage;

namespace Rocketnote.Notes
{
	public class SettingsManagement :IDisposable
	{
		//ustawienia aplikacji - zapis/odczyt
		////v1.0.1.2
		IsolatedStorageSettings _appSettings;

		//
		//konstruktor
		//
		public SettingsManagement()
		{
			//ustawienia
			_appSettings = IsolatedStorageSettings.ApplicationSettings;	
		}

		//
		//Odczytuje wartość z ustawień
		////v1.0.1.2
		public int LoadSortIndex(string settingName)
		{
			/*
			 * CEL:
			 * Odczytuje wartość z ustawień
			 * 
			 * WARTOŚĆ WEJŚCIOWA:
			 * settingName:string - nazwa ustawienia
			 * 
			 * WARTOŚĆ ZWRACANA:
			 * string - wartość odczytana
			 */

			//ustaw wartość początkową - pusta
			int settValue = 0;

			//jeśli ustawienia istnieją to je odczytaj
			if (_appSettings.Contains(settingName))
				settValue = (int)_appSettings[settingName];
			//zwróć
			return settValue;
		}

		//
		//Zapisuje wartość do ustawień
		////v1.0.1.2
		public void SaveSortIndex(string settingName, int settingValue)
		{
			/*
			 * CEL:
			 * Zapisuje wartość do ustawień
			 * 
			 * WARTOŚĆ WEJŚCIOWA:
			 * settingName:string - nazwa ustawienia
			 * settingValue:string - wartość ustawień
			 */

			//zapisz ustawienia 
			//jeśli zmienna ustawień istnieje to zapisz wartość
			if (_appSettings.Contains(settingName))
				_appSettings[settingName] = settingValue;
			//jeśli zmienna ustawień nie istnieje to ją utwórz i zapisz wartość
			else _appSettings.Add(settingName, settingValue);
		}

		public void Dispose()
		{
			//throw new NotImplementedException();
		}
	}
}
