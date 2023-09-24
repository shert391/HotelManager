using HotelManager.MVVM.Models.Configs;
using HotelManager.MVVM.Utils;

namespace HotelManager.MVVM.Models.Builders;

public class DefaultValidatorConfigBuilder
{
    private class DefaultValidatorConfig : IConfiguration, IDefaultValidatorConfig
    {
        public Action? OnSuccess { get; set; }
        public Action<string>? OnError { get; set; }
        public bool IsGenerateException { get; set; }
    }

    private readonly DefaultValidatorConfig _validatorConfig = new();

    public static DefaultValidatorConfigBuilder Create() => new();

    public DefaultValidatorConfigBuilder AddActionOnSuccess(Action actionOnSuccess)
    {
        _validatorConfig.OnSuccess += actionOnSuccess;
        return this;
    }
    public DefaultValidatorConfigBuilder AddActionOnError(Action<string> actionOnError)
    {
        _validatorConfig.OnError = actionOnError;
        return this;
    }

    public DefaultValidatorConfigBuilder AddGenerateException()
    {
        _validatorConfig.IsGenerateException = true;
        return this;
    }

    public DefaultValidatorConfigBuilder AddShowErrorMessageBox(bool isClose = false)
    {
        _validatorConfig.OnError = error => DialogHostController.ShowMessageBoxInformation(error, isClose);
        return this;
    }
    
    public DefaultValidatorConfigBuilder AddShowMessageBox(string message, bool isClose = false)
    {
        _validatorConfig.OnSuccess = () => DialogHostController.ShowMessageBoxInformation(message, isClose);
        return this;
    }

    public DefaultValidatorConfigBuilder AddShowMessageBoxSuccessError(string messageSuccess, 
        bool isCloseSuccess = false, 
        bool isCloseError = false)
    {
        AddShowErrorMessageBox(isCloseError).AddShowMessageBox(messageSuccess, isCloseSuccess);
        return this;
    }

    public IDefaultValidatorConfig Build() => _validatorConfig;
}