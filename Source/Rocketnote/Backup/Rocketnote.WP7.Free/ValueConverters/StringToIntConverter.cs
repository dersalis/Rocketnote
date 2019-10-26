using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Rocketnote.ValueConverters
{
	public class StringToIntConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			//throw new NotImplementedException();
			if (value.GetType() == typeof(string) && targetType == typeof(int)) return (int)value;
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			//throw new NotImplementedException();
			if (value.GetType() == typeof(int) && targetType == typeof(string)) value.ToString();
			return value;
		}
	}
}
