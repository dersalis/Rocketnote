using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace Rocketnote.ValueConverters 
{
	public class NumberToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			Visibility visible = Visibility.Collapsed;

			if ((int)value == 0) visible = Visibility.Visible;
			
			return visible;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			int number = 1;
			if ((Visibility)value == Visibility.Visible) return 0;

			return number;
		}
	}
}
