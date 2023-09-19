using HotelManager.MVVM.Models.DataContract;
using HotelManager.MVVM.Models.Extensions;
using System.Globalization;
using System.Windows.Data;

namespace HotelManager.MVVM.Views.Xamls.Converters;

public class EnumToIntConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (int)value;
    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
}