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
	public partial class TesterPage : PhoneApplicationPage
	{
		public TesterPage()
		{
			InitializeComponent();
			DataContext = RnModelView.Instance;

			RnModelView.Instance.DataFileSize = RnModelView.Instance.GetDataFileSize();
		}

		private void btnAddTestNotes_Click(object sender, RoutedEventArgs e)
		{
			RnModelView.Instance.AddTestNotes();
		}

		private void btnSaveTest_Click(object sender, RoutedEventArgs e)
		{
			RnModelView.Instance.SaveTest();
		}

		private void btnReadTest_Click(object sender, RoutedEventArgs e)
		{
			RnModelView.Instance.ReadTest();
		}
	}
}