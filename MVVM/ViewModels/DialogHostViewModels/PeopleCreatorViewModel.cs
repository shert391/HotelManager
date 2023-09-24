using DevExpress.Mvvm;
using System.Windows.Input;
using HotelManager.MVVM.Utils;
using HotelManager.MVVM.Models.Builders;
using HotelManager.MVVM.Models.DataContract;
using HotelManager.MVVM.Models.Others;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

public class PeopleCreatorViewModel : IConfigurable<ICommand<People>>, IDialogViewModel
{
    private ValidatorBase _validatorBase = new(DefaultValidatorConfigBuilder.Create().AddShowErrorMessageBox().Build());
    public ICommand ValidateCommand { get; }
    public ICommand CancelCommand { get; }

    private ICommand<People>? _addCommand;

    public string FullName { get; set; } = "";
    public int Age { get; set; }

    public PeopleCreatorViewModel()
    {
        CancelCommand = new DelegateCommand(DialogHostController.ShowPreview);
        ValidateCommand = new DelegateCommand(Validate);
    }

    private void Validate()
    {
        var newPeople = new People() { FullName = FullName, Age = Age };

        if (!_validatorBase.DefaultValidate(newPeople, typeof(PeopleValidator)))
            return;

        DialogHostController.ShowPreview();
        _addCommand?.Execute(newPeople);
    }

    public void Configurate(ICommand<People> addCommand) => _addCommand = addCommand;
}