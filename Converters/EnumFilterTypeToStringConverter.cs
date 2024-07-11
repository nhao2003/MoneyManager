using System.Globalization;
using MoneyManager.Enums;

namespace MoneyManager.Converters;

public class EnumFilterTypeToStringConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not FilterType filterType) return value?.ToString() ?? string.Empty;
        return filterType switch
        {
            FilterType.All => "Tất cả",
            FilterType.Income => "Thu",
            FilterType.Expense => "Chi",
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string stringValue) return FilterType.All;
        return stringValue switch
        {
            "Tất cả" => FilterType.All,
            "Thu" => FilterType.Income,
            "Chi" => FilterType.Expense,
            _ => FilterType.All
        };
    }
}