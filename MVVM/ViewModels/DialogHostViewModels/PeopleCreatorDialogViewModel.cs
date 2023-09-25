using DevExpress.Mvvm;
using HotelManager.MVVM.Models.Builders;
using HotelManager.MVVM.Models.DataContract;
using HotelManager.MVVM.Models.Others;
using HotelManager.MVVM.Utils;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

public class PeopleCreatorDialogViewModel : AbstractDialogManagerViewModel, IConfigurable<ObservableCollection<People>>
{
    private ValidatorBase _validatorBase = new(DefaultValidatorConfigBuilder.Create().AddShowErrorMessageBox().Build());
    public ICommand AddPeopleCommand { get; }
    public ICommand CancelCommand { get; }

    public ObservableCollection<People>? PeoplesCollection { get; private set; }

    public People NewPeople { get; } = new();

    public PeopleCreatorDialogViewModel()
    {
        CancelCommand = new DelegateCommand(() => DialogHostController.BackViewModel(1));
        AddPeopleCommand = new DelegateCommand(AddPeople);
    }

    private void AddPeople()
    {
        if (!_validatorBase.DefaultValidate(NewPeople, typeof(PeopleValidator)))
            return;

        PeoplesCollection?.Add(NewPeople);

        DialogHostController.ShowMessageBoxInformation("Жилец успешно добавлен!", () => DialogHostController.BackViewModel(2));
    }

    public void Configurate(ObservableCollection<People> peoplesCollection) => PeoplesCollection = peoplesCollection;
}