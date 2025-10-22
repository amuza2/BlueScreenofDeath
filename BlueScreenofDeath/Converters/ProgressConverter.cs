using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace BlueScreenofDeath.Converters;

public class ProgressConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double progress)
        {
            // Convert percentage to actual width (500 * progress/100)
            return 500 * (progress / 100);
        }
        return 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}