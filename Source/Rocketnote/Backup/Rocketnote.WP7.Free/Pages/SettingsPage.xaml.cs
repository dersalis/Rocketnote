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
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using Rocketnote.Resources;

namespace Rocketnote.Pages
{
	public partial class SettingsPage : PhoneApplicationPage
	{
		public SettingsPage()
		{
			InitializeComponent();

			this.DataContext = RnModelView.Instance;
		}

		//resetowanie listy notatek
		private void btnResetNotesList_Tap(object sender, System.Windows.Input.GestureEventArgs e)
		{
			RnModelView.Instance.ResetNotesList();
		}

		private void btnResetRocketnote_Tap(object sender, System.Windows.Input.GestureEventArgs e)
		{

		}

		private void pivSettings_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (pivSettings.SelectedIndex == 1) ApplicationBar = CreateAboutAppBar();
			else ApplicationBar = null;

		}

		private ApplicationBar CreateAboutAppBar()
		{
			
			ApplicationBar aboutAppBar = new ApplicationBar();
			aboutAppBar.Mode = ApplicationBarMode.Default;
			aboutAppBar.Opacity = 1.0;
			aboutAppBar.IsVisible = true;
			aboutAppBar.IsMenuEnabled = true;
            //aboutAppBar.ForegroundColor = Colors.White;
            //aboutAppBar.BackgroundColor = (Color)Application.Current.Resources["PhoneAccentColor"];

			ApplicationBarIconButton btnReviewMe = new ApplicationBarIconButton();
			btnReviewMe.IconUri = new Uri("/Images/reviewMe.png", UriKind.Relative);
			btnReviewMe.Text = AppResources.AppBarRateMe;
			btnReviewMe.Click += new EventHandler(btnReviewMe_click);
			aboutAppBar.Buttons.Add(btnReviewMe);

			ApplicationBarIconButton btnLikeMe = new ApplicationBarIconButton();
			btnLikeMe.IconUri = new Uri("/Images/likeMe.png", UriKind.Relative);
			btnLikeMe.Text = AppResources.AppBarLikeMe;
			btnLikeMe.Click += new EventHandler(btnLikeMe_click);
			aboutAppBar.Buttons.Add(btnLikeMe);

			return aboutAppBar;
		}

		private void btnLikeMe_click(object sender, EventArgs e)
		{
			WebBrowserTask browser = new WebBrowserTask();
			browser.Uri = new Uri(@"https://www.facebook.com/rocketnote");
			//browser.URL = "https://www.facebook.com/rocketnote";
			browser.Show();
		}

		private void btnReviewMe_click(object sender, EventArgs e)
		{
			MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();

			marketplaceReviewTask.Show();

		}

		private void txtSendEmail_Tap(object sender, System.Windows.Input.GestureEventArgs e)
		{
			EmailComposeTask emailComposeTask = new EmailComposeTask();

			emailComposeTask.Subject = AppResources.TextEmailSubiect;
			//emailComposeTask.Body = "message body";
            emailComposeTask.To = "mobileapps@aturex.pl";
			//emailComposeTask.Cc = "cc@example.com";
			//emailComposeTask.Bcc = "bcc@example.com";

			emailComposeTask.Show();
		}

        private void tswTheme_Checked(object sender, RoutedEventArgs e)
        {
            tswTheme.Content = AppResources.TextDarkTheme;
        }

        private void tswTheme_Unchecked(object sender, RoutedEventArgs e)
        {
            tswTheme.Content = AppResources.TextLightTheme;
        }
	}
}