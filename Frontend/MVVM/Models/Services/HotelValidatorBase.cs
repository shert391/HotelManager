using FluentValidation;
using HotelManager.MVVM.Models.Extensions;
using HotelManager.MVVM.Utils;

namespace HotelManager.MVVM.Models.Services;

public class HotelValidatorBase : AbstractHotelService
{
    public static bool DefaultValidate<TType>(TType verifiable, Type validatorType)
    {
        var validator = Activator.CreateInstance(validatorType) as AbstractValidator<TType> ??
                        throw new Exception("Validator Type fail!");
        var error = validator.Validate<TType>(verifiable);
        if (error is null) return true;
        DialogHostController.ShowMessageBox(error);
        return false;
    }
}