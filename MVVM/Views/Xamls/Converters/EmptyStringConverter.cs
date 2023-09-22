using System.Globalization;
using System.Windows.Data;

namespace HotelManager.MVVM.Views.Xamls.Converters;
public class EmptyStringConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
            return null;

        return string.IsNullOrEmpty(value.ToString()) ? parameter : value;
    }

    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
            return null;

        return string.IsNullOrEmpty(value.ToString()) ? null : int.Parse(value.ToString()!);
    }
}
