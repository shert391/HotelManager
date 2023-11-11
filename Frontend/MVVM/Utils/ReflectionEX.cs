using System.Reflection;

namespace HotelManager.MVVM.Utils;

internal static class ReflectionEX
{
    /// <summary>
    /// С помощью рефлексии извлекает значение первого найденного свойства в <b>runtimeObject</b> по наличию в нём подстроки <b>fieldName</b>
    /// </summary>
    /// <param name="fieldName">Имя поля в <b>runtimeObject</b></param>
    public static TResult GetValueByNameProperty<TResult>(string propertyName, object runtimeObject)
    {
        foreach (PropertyInfo propertyInfo in runtimeObject.GetType().GetProperties())
            if(propertyInfo.Name.Contains(propertyName))
                return (TResult)propertyInfo.GetValue(runtimeObject)!;

        throw new Exception("GetValueByNameProperty failed!");
    }
}

