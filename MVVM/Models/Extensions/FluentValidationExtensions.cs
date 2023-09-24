using FluentValidation;

namespace HotelManager.MVVM.Models.Extensions;

public static class FluentValidationExtensions
{
    public static string? Validate<T>(this AbstractValidator<T> abstractValidator, T targetValidate)
    {
        var result = abstractValidator.Validate(targetValidate);
        return result.IsValid ? null : result.Errors.First().ErrorMessage;
    }
}