using System.ComponentModel;
using HotelManager.MVVM.Models.CustomAttributes;

namespace HotelManager.MVVM.Models.Extensions;

public static class GetEnumDescriptionExtension
{
    private static T GetAttributeFromField<T>(Type typeTarget, Type typeAttribute, string fieldName) where T : Attribute
    {
        var field = typeTarget.GetField(fieldName) ?? throw new ArgumentException("Field is not found!");
        if (Attribute.GetCustomAttribute(field, typeof(T)) is not T attribute)
            throw new Exception("Attribute is not found!");
        return attribute;
    }
    
    public static string GetDescription(this Enum enumValue)
    { 
        return GetAttributeFromField<DescriptionAttribute>(enumValue.GetType(), 
            typeof(DescriptionAttribute), 
            enumValue.ToString()).Description;
    }
    
    public static int GetMaxPeople(this Enum enumValue)
    {
        return GetAttributeFromField<MaxPeopleAttribute>(enumValue.GetType(),
            typeof(MaxPeopleAttribute), 
            enumValue.ToString()).MaxPeoples;
    }
}

