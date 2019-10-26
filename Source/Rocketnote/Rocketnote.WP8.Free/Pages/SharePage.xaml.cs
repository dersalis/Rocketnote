using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace Rocketnote.Pages
{
	public partial class SharePage : PhoneApplicationPage
	{
		public SharePage()
		{
			InitializeComponent();
		}

		#region ZDARZENIA

		//
		//zdarzenie naciśnięcia udostępnij notatkę jako status w sieci społecznościowej
		//
		private void btnShareStatus_Tap(object sender, System.Windows.Input.GestureEventArgs e)
		{
			/*
			 * CEL:
			 * Udostępnia notatkę jako status w sieci społecznościowej
			 */

			//udostępnij status
			RnModelView.Instance.ShareYourStatus();
            if (NavigationService.CanGoBack) NavigationService.GoBack();
		}

		//
		//zdarzenie naciśnięcia udostępnij notatkę jako wiadomość email
		//
		private void btnShareEmail_Tap(object sender, System.Windows.Input.GestureEventArgs e)
		{
			/*
			 * CEL:
			 * Udostępnia notatkę jako wiadomość email
			 */

			//udostępnij wiadomość Email
			RnModelView.Instance.ShareViaEmail();
            if (NavigationService.CanGoBack) NavigationService.GoBack();
		}

		//
		//zdarzenie naciśnięcia udostępnij notatkę jako wiadomość sms
		//
		private void btnShareSms_Tap(object sender, System.Windows.Input.GestureEventArgs e)
		{
			/*
			 * CEL:
			 * Udostępnia notatkę jako wiadomość sms
			 */

			//udostępnij wiadomość sms
			RnModelView.Instance.ShareViaSms();
            if (NavigationService.CanGoBack) NavigationService.GoBack();
		}

		#endregion
	}
}