using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Rocketnote.Pages
{
	public partial class TestPage : PhoneApplicationPage
	{
		public TestPage()
		{
			InitializeComponent();
			this.Loaded += new RoutedEventHandler(TestPage_Loaded);
			//btnTest.IsEnabled = false;
		}

		private void TestPage_Loaded(object sender, RoutedEventArgs e)
		{
			//throw new NotImplementedException();
			//this.btnTest.IsEnabled = false;
		}

		private void btnTest_Click(object sender, EventArgs e)
		{

		}
	}
}