using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;
using System.Xml;
using Microsoft.Phone.Controls;
using Rocketnote.Notes;

namespace Rocketnote.ViewModels
{
	public class FileManagement
	{
		private List<Note> tempData;

		public FileManagement()
		{
			tempData = new List<Note>();
		}

		public void SaveToXml(string fileName, List<Note> noteList)
		{
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			xmlWriterSettings.Indent = true;

			using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
			{
				using (IsolatedStorageFileStream stream = myIsolatedStorage.OpenFile(fileName, FileMode.Create))
				{
					XmlSerializer serializer = new XmlSerializer(typeof(List<Note>));
					using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
					{
						serializer.Serialize(xmlWriter, noteList);
					}
				}
			}
		}

		public List<Note> OpenFromXml(string fileName)
		{
			try
			{
				using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
				{
					if (myIsolatedStorage.FileExists(fileName))
					{
						using (IsolatedStorageFileStream stream = myIsolatedStorage.OpenFile(fileName, FileMode.Open))
						{
							XmlSerializer serializer = new XmlSerializer(typeof(List<Note>));
							tempData = ((List<Note>)serializer.Deserialize(stream));
						}
					}
					else
					{
						CustomMessageBox msg = new CustomMessageBox()
						{
							Title = "Dane",
							Content = "Brak pliku danych",
							LeftButtonContent = "Zamknij"
						};
						msg.Show();
					}
				}
			}
			catch
			{
				//add some code here
			}

			return tempData;
		}
	}
}
