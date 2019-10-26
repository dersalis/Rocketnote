using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rocketnote.Resources;
using Microsoft.Phone.Tasks;

namespace Rocketnote.Notes
{
	class ShareManagement : IDisposable
	{
		//
		//udostępnia notakę za pomocą wiadomości sms
		//
		public void ShareViaSms(string title, string note)
		{
			/*
			 * CEL:
			 * Tworzy wiadomość sms z notatki i wysyła ją
			 * 
			 * PARAMETRY WEJŚCIOWE:
			 * title:string - tytuł notatki
			 * note:string - treść notatki 
			 */

			//zadanie 
			var smsTask = new SmsComposeTask();
			//treść wiadomości
			smsTask.Body = CreateTextMessage(title, note);
			//wyślij wiadomość
			smsTask.Show();
		}

		//
		//udostępnia notakę za pomocą wiadomości email
		//
		public void ShareViaEmail(string title, string note)
		{
			/*
			 * CEL:
			 * Tworzy wiadomość email z notatki i wysyła ją
			 * 
			 * PARAMETRY WEJŚCIOWE:
			 * title:string - tytuł notatki
			 * note:string - treść notatki 
			 */

			//zadanie
			var emailTask = new EmailComposeTask();
			//tytuł
			emailTask.Subject = title;
			//treść wiadomości
			emailTask.Body = CreateEmailBody(note);
			//wyślij wiadomość
			emailTask.Show();
		}

		//
		//udostępnia notakę do sieci społecznościowej
		//
		public void ShareViaSocialNetwork(string title, string note)
		{
			/*
			 * CEL:
			 * Tworzy wiadomość społecznościową z notatki i wysyła ją
			 * 
			 * PARAMETRY WEJŚCIOWE:
			 * title:string - tytuł notatki
			 * note:string - treść notatki 
			 */

			//zadanie
			var snTask = new ShareStatusTask();
			//treść wiadomości
			snTask.Status = CreateTextMessage(title, note);
			//wyślij wiadomość
			snTask.Show();
		}

		//
		//tworzy miadomość tekstową sms lub dla sieci społecznościowej
		//
		private string CreateTextMessage(string title, string note)
		{
			/*
			 * CEL:
			 * Tworzy wiadomość tekstową z połączenia tytułu oraz treści notatki
			 * 
			 * PARAMETRY WEJŚCIOWE:
			 * title:string - tytuł notatki
			 * note:string - treść notatki
			 * 
			 * WARTOŚĆ ZWRACANA:
			 * string - treść wiadomości 
			 */

			//utwórz wiadomość
			string textMessage = string.Format("{0}\n{1}", title, note);
			//zwróć wiadomość
			return textMessage;
		}

		//
		//tworzy treść wiadomości email
		//
		private string CreateEmailBody(string note)
		{
			/*
			 * CEL:
			 * Tworzy treść wiadomości email z połączenia treści notatki oraz informacji
			 * o programie
			 * 
			 * PARAMETRY WEJŚCIOWE:
			 * note:string - treść notatki
			 * 
			 * WARTOŚĆ ZWRACANA:
			 * string - treść email
			 */

			//utwórz wiadomość
			string emailBody = string.Format("{0}\n\n{1}", note, AppResources.TextCreatedBy);
			//zwróć wiadomość
			return emailBody;
		}

		public void Dispose()
		{
			//throw new NotImplementedException();
		}
	}
}
