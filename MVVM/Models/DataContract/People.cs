using DevExpress.Mvvm;
using System.ComponentModel;

namespace HotelManager.MVVM.Models.DataContract;
public class People : BindableBase
{
    public int? Age { get; set; }
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? NumberPassport { get; set; }
    public string? SeriesPassport { get; set; }
    public string? ResidenceAddress { get; set; }
}

