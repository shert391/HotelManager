using HotelManager.MVVM.Models.Services.RoomService;

namespace HotelManager.MVVM.Models.Builders;

public class RoomServiceValidatorConfigBuilder
{
    private RoomServiceValidatorConfig _config = new();

    public static RoomServiceValidatorConfigBuilder Create() => new();

    public RoomServiceValidatorConfigBuilder AddActionOnSuccess(Action actionOnSuccess)
    {
        _config.OnSuccess += actionOnSuccess;
        return this;
    }
    public RoomServiceValidatorConfigBuilder AddActionOnError(Action<string> actionOnError)
    {
        _config.OnError = actionOnError;
        return this;
    }

    public RoomServiceValidatorConfigBuilder AddGenerateException()
    {
        _config.IsGenerateException = true;
        return this;
    }

    public RoomServiceValidatorConfig Build() => _config;
}