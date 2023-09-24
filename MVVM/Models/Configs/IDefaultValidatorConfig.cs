namespace HotelManager.MVVM.Models.Configs;

public interface IDefaultValidatorConfig
{
    public Action? OnSuccess { get; }
    public Action<string>? OnError { get; }
    public bool IsGenerateException { get; }
}