using System.ComponentModel;

namespace HotelManager.MVVM.Models.Extensions;

public static class GetEnumDescriptionExtension
{
    public static string GetDescription(this Enum enumValue)
    {
        var field = enumValue.GetType().GetField(enumValue.ToString());
        if (field is null)
            goto BAD;
        if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            return attribute.Description;

        BAD:
        return "Don't find Description";
    }
}

