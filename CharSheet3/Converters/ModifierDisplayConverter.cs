using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace CharSheet3.Converters;

public class ModifierDisplayConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int intValue)
        {
            if (intValue > 0)
                return $"+{intValue}";
            if (intValue == 0)
                return "±0";
            return intValue.ToString();
        }
        if (value is null)
            return "±0"; // Default display for null values
        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string strValue)
        {
            if (strValue.StartsWith("+") && int.TryParse(strValue[1..], out int positiveValue))
                return positiveValue;
            if (strValue.StartsWith("±") && int.TryParse(strValue[2..], out int zeroValue))
                return 0;
            if (int.TryParse(strValue, out int parsedValue))
                return parsedValue;
        }
        return 0; // Default return value for unrecognized formats
    }
}