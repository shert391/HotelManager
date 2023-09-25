using System.Collections;
using HotelManager.MVVM.Models.Extensions;
using System.Globalization;
using System.Windows.Data;

namespace HotelManager.MVVM.Views.Xamls.Converters;

public class EnumConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (targetType == typeof(string))
        {
            foreach (Enum enumValue in Enum.GetValues(value.GetType()))
                if (enumValue.Equals(value))
                    return enumValue.GetDescription();
        }
        else if (targetType == typeof(IEnumerable))
        {
            return (from Enum enumValue in Enum.GetValues(value.GetType()) select enumValue.GetDescription()).ToList();   
        }
        else if(targetType == typeof(int))
        {
            return (int)value;
        }
        
        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        switch (value)
        {
            case string:
            {
                foreach (Enum enumValue in Enum.GetValues(targetType))
                    if (enumValue.GetDescription() == value.ToString())
                        return enumValue;
                break;
            }
        }

        return value;
    }
}