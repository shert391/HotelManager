using DevExpress.Mvvm;
using HotelManager.MVVM.Models.Builders;
using HotelManager.MVVM.Models.DataContract;
using HotelManager.MVVM.Models.Others;
using HotelManager.MVVM.Utils;
using System.Windows.Input;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

public class PeopleCreatorDialogViewModel : AbstractDialogInteractiveViewModel, IConfigurable<PeopleCreatorDialogViewModel.Configuration>
{
    public class Configuration : BindableBase
    {
        public bool IsStateEditing { get; init; } = false;
        public Action<People>? OnSuccessfulValidation { get; init; }
        public People? InitInputDefault;
    }
    
    public ICommand ValidateInputCommand { get; }
    public ICommand CancelCommand { get; }
    public Configuration? Config { get; private set; }
    
    private readonly ValidatorBase _validatorBase = new(DefaultValidatorConfigBuilder.Create().AddShowErrorMessageBox().Build());

    public People NewPeople { get; private set; } = new();

    public PeopleCreatorDialogViewModel()
    {
        CancelCommand = new DelegateCommand(() => DialogHostController.BackViewModel(1));
        ValidateInputCommand = new DelegateCommand(ValidateInput);
    }

    private void ValidateInput()
    {
        if (!_validatorBase.DefaultValidate(NewPeople, typeof(PeopleValidator)))
            return;

        Config?.OnSuccessfulValidation?.Invoke(NewPeople);
    }
    
    public void Configurate(Configuration configuration)
    {
        Config = configuration;
        
        if(configuration.InitInputDefault is not null)
            NewPeople = configuration.InitInputDefault;
    }
}