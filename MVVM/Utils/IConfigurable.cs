namespace HotelManager.MVVM.Utils;
public interface IConfiguration { }

public interface IConfigurable<T>
{
    public void Configurate(T configuration);
}

