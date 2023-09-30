using DevExpress.Mvvm;

namespace DataContract.ViewModelsDto;

public class PeopleViewModel : BindableBase
{
    public int Age { get; set; }
    public string FullName { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public string NumberPassport { get; set; } = "";
    public string SeriesPassport { get; set; } = "";
    public string ResidenceAddress { get; set; } = "";

    public PeopleViewModel Clone() => (PeopleViewModel)MemberwiseClone();
}