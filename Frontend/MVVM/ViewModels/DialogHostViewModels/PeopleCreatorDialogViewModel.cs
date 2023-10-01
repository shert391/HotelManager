using DevExpress.Mvvm;
using HotelManager.MVVM.Utils;
using System.Windows.Input;
using DataContract.ViewModelsDto;
using DataContract.ViewModelsDto.Validators;
using HotelManager.MVVM.Models.Services;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

public class PeopleCreatorDialogViewModel : AbstractDialogViewModel
{
    public Action<PeopleViewModel>? OnSuccessPeopleValidated;
    public PeopleViewModel NewPeople { get; set; } = new();
    public ICommand ValidateInputCommand { get; }

    public PeopleCreatorDialogViewModel()
    {
        CancelCommand = new DelegateCommand(() => DialogHostController.BackViewModel(1));
        ValidateInputCommand = new DelegateCommand(ValidateInput);
    }

    private void ValidateInput()
    {
        if (!HotelValidatorBase.DefaultValidate(NewPeople, typeof(PeopleViewModelValidator)))
            return;

        OnSuccessPeopleValidated?.Invoke(NewPeople);
    }
}