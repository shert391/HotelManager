using HotelManager.MVVM.Utils;

namespace HotelManager.MVVM.Models.Builders;

public interface IRoomServiceValidatorConfig
{
    public Action? OnSuccess { get; }
    public Action<string>? OnError { get; }
    public bool IsGenerateException { get; }
}

public class RoomServiceValidatorConfigBuilder
{
    private class RoomServiceValidatorConfig : IConfiguration, IRoomServiceValidatorConfig
    {
        public Action? OnSuccess { get; set; }
        public bool IsGenerateException { get; set; }
        public Action<string>? OnError { get; set; }
    }

    private readonly RoomServiceValidatorConfig _config = new();

    public static RoomServiceValidatorConfigBuilder CreateDefault() => new();

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

    public IRoomServiceValidatorConfig Build() => _config;
}