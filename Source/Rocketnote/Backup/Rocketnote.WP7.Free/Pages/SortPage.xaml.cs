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
using Rocketnote.Resources;

namespace Rocketnote.Pages
{
	public partial class SortPage : PhoneApplicationPage
	{
		string[] SortType = { AppResources.TextSortByCreationDate, AppResources.TextSortByChangeDate, AppResources.TextSortAlphabetically};

		public SortPage()
		{
			InitializeComponent();
			DataContext = RnModelView.Instance;
			//lstSortNotes.ItemsSource = SortType;
		}

		private void lstSortNotes_Tap(object sender, System.Windows.Input.GestureEventArgs e)
		{
            if(NavigationService.CanGoBack) NavigationService.GoBack();
		}
	}
}