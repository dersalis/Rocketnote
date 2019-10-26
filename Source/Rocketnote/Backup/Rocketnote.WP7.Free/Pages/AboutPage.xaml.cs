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

using Microsoft.Phone.Tasks;
using Rocketnote.Resources;
using Microsoft.Phone.Shell;

namespace Rocketnote.Pages
{
    public partial class AboutPage : PhoneApplicationPage
    {
        public AboutPage()
        {
            InitializeComponent();

            ApplicationBar = CreateAboutAppBar();
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

            //ApplicationBarIconButton btnLikeMe = new ApplicationBarIconButton();
            //btnLikeMe.IconUri = new Uri("/Images/likeMe.png", UriKind.Relative);
            //btnLikeMe.Text = AppResources.AppBarLikeMe;
            //btnLikeMe.Click += new EventHandler(btnLikeMe_click);
            //aboutAppBar.Buttons.Add(btnLikeMe);

            ApplicationBarIconButton btnStore = new ApplicationBarIconButton();
            btnStore.IconUri = new Uri("/Images/store.png", UriKind.Relative);
            btnStore.Text = AppResources.AppBarStore;
            btnStore.Click += new EventHandler(btnStore_click);
            aboutAppBar.Buttons.Add(btnStore);

            ApplicationBarIconButton btnContact = new ApplicationBarIconButton();
            btnContact.IconUri = new Uri("/Images/email.png", UriKind.Relative);
            btnContact.Text = AppResources.TextEmail;
            btnContact.Click += new EventHandler(btnContact_click);
            aboutAppBar.Buttons.Add(btnContact);

            return aboutAppBar;
        }

        private void btnContact_click(object sender, EventArgs e)
        {
            EmailComposeTask emailComposeTask = new EmailComposeTask();

            emailComposeTask.Subject = AppResources.TextEmailSubiect;
            //emailComposeTask.Body = "message body";
            emailComposeTask.To = "mobileapps@aturex.pl";
            //emailComposeTask.Cc = "cc@example.com";
            //emailComposeTask.Bcc = "bcc@example.com";

            emailComposeTask.Show();
        }

        private void btnStore_click(object sender, EventArgs e)
        {
            // Przenieś do sklepu
            MarketplaceSearchTask marketplaceSearchTask = new MarketplaceSearchTask();
            marketplaceSearchTask.SearchTerms = "Damian Ruta";
            marketplaceSearchTask.Show();
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

        private void btnFacebook_Click(object sender, RoutedEventArgs e)
        {
            var web = new WebBrowserTask();
            web.Uri = new Uri("https://www.facebook.com/rocketnote");
            web.Show();
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
    }
}