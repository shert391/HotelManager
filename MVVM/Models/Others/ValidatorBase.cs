using System.CodeDom;
using FluentValidation;
using HotelManager.MVVM.Models.Builders;
using HotelManager.MVVM.Models.Configs;
using HotelManager.MVVM.Models.Extensions;
using HotelManager.MVVM.Utils;

namespace HotelManager.MVVM.Models.Others;

public class ValidatorBase : IConfigurable<IDefaultValidatorConfig>
{
    protected IDefaultValidatorConfig ValidatorConfig = DefaultValidatorConfigBuilder.Create().Build();

    public ValidatorBase(IDefaultValidatorConfig defaultValidatorConfig) => ValidatorConfig = defaultValidatorConfig;

    public ValidatorBase()
    {
    }

    protected void ErrorHandler(string error)
    {
        ValidatorConfig.OnError?.Invoke(error);
        if (ValidatorConfig.IsGenerateException)
            throw new Exception(error);
    }

    public bool DefaultValidate<TType>(TType verifiable, Type validatorType)
    {
        var validator = Activator.CreateInstance(validatorType) as AbstractValidator<TType> ??
                        throw new Exception("Validator Type fail!");
        var error = validator.Validate<TType>(verifiable);
        if (error is null) return true;
        ErrorHandler(error);
        return false;
    }

    public void Configurate(IDefaultValidatorConfig validatorConfig) => ValidatorConfig = validatorConfig;
}