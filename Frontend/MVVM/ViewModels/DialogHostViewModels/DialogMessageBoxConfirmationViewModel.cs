﻿using DevExpress.Mvvm;
using HotelManager.MVVM.Utils;
using System.Windows.Input;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

public class DialogMessageBoxConfirmationViewModel : AbstractDialogMessageBoxViewModel
{
    public ICommand AcceptCommand { get; }
    
    public DialogMessageBoxConfirmationViewModel(Action onAgreement, string message) : base(message)
    {
        AcceptCommand = new DelegateCommand(() => {
            DialogHostController.BackViewModel();
            onAgreement();
        });

        CancelCommand = new DelegateCommand(() => DialogHostController.BackViewModel());
    }
}