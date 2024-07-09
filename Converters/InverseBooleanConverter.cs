using System.Globalization;

namespace MoneyManager.Converters;

public class InverseBooleanConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolean)
        {
            return !boolean;
        }

        return value is null;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolean)
        {
            return !boolean;
        }

        return value is null;
    }
    
}