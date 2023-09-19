using HotelManager.MVVM.Models.DataContract;
using HotelManager.MVVM.Models.Extensions;
using System.Globalization;
using System.Windows.Data;

namespace HotelManager.MVVM.Views.Xamls.Converters;

public class RoomTypeToTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not RoomType)
            throw new ArgumentException("value is not RoomType");

        if (targetType == typeof(string))
        {
            foreach (RoomType enumValue in Enum.GetValues(typeof(RoomType)))
                if (enumValue.Equals(value))
                    return enumValue.GetDescription();

            return "Didn't found description";
        }

        return (from RoomType enumValue in Enum.GetValues(typeof(RoomType)) select enumValue.GetDescription()).ToList();
    }

    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
            return null;

        foreach (RoomType enumValue in Enum.GetValues(typeof(RoomType)))
            if (enumValue.GetDescription() == value.ToString())
                return enumValue;
        
        throw new ArgumentException("failed");
    }
}