namespace HotelManager.MVVM.Utils;
public interface IConfigurable<TOption>
{
    public void Configure(TOption option);
}

