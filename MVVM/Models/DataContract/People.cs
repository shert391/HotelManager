using DevExpress.Mvvm;

namespace HotelManager.MVVM.Models.DataContract;

public interface IPeople
{
    public int? Age { get; }
    public string? FullName { get; }
    public string? PhoneNumber { get; }
    public string? NumberPassport { get; }
    public string? SeriesPassport { get; }
    public string? ResidenceAddress { get; }
    public People Clone();
}

public class People : BindableBase, IPeople
{
    public int? Age { get; set; }
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? NumberPassport { get; set; }
    public string? SeriesPassport { get; set; }
    public string? ResidenceAddress { get; set; }
    public People Clone() => (People)MemberwiseClone();
}

