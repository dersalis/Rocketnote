using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Threading;

namespace Rocketnote.ValueConverters
{
	public class DateToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (targetType == typeof(string) && value.GetType() == typeof(DateTime))
			{
				//sprawdz datę
				//jeśli data jest dzisiejsza to zwróć tylko godzinę
				//w przeciwnym przypadku zwróć tylko datę
				if (((DateTime)value).ToShortDateString() == DateTime.Now.ToShortDateString())
					//return ((DateTime)value).ToShortTimeString();
					return ((DateTime)value).ToString("T", Thread.CurrentThread.CurrentCulture);
				else
					//return ((DateTime)value).ToShortDateString();
					return ((DateTime)value).ToString("D", Thread.CurrentThread.CurrentCulture);
			}
			//jeśli nie wprowadzamy zmian to zwróć wartość
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value.GetType() == typeof(string) && targetType == typeof(DateTime))
			{
				DateTime newDate;
				if (DateTime.TryParse((string)value, out newDate))
					return newDate;
			}
			//jeśli nie wprowadzamy zmian to zwróć wartość
			return value;
		}
	}
}
