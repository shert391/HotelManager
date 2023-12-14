﻿using DataContract.Extensions;
using DevExpress.Mvvm;
using HotelManager.MVVM.Utils;
using HotelManager.MVVM.ViewModels.DialogHostViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using DataContract.DTO.Messages;
using DataContract.DTO.ViewModels;

namespace HotelManager.MVVM.ViewModels.PageViewModels;

/// <summary>
/// Важно заполнять значения TypeViewRooms в тандеме с порядком RoomState у RoomViewModel - для синхронизации фильтра
/// </summary>
public enum TypeViewRooms
{
    [Description("Занятые")] Busy,
    [Description("Свободные")] Free,
    [Description("Пора выселять")] NeedPaid,

    [Description("Все")] All,
}

public class SystemPageViewModel : AbstractRoomManagerViewModel
{
    public ICommand AddRoomCommand { get; }

    public ICommand PayRoomCommand { get; }

    public ICommand ClearAllCommand { get; }

    public ICommand FindRoomCommand { get; }

    public ICommand EditRoomCommand { get; }

    public ICommand ShowRoomsCommand { get; }

    public ICommand DeleteRoomCommand { get; }

    public ICommand ReserveRoomCommand { get; }

    public ICommand GenerateRoomsCommand { get; }

    public ICommand ShowPayHistoryCommand { get; }

    public ICommand EditReservationRoomCommand { get; }

    public int? NumberRoomTargetFind { get; set; }

    public TypeViewRooms TypeViewRooms { get; set; } = TypeViewRooms.All;


    public SystemPageViewModel()
    {
        RoomsView.Filter = FilteringRooms;

        EditRoomCommand = new DelegateCommand<RoomViewModel>(roomViewModel =>
        {
            var viewModel = DialogHostController.ShowPull<RoomEditorDialogViewModel>();
            viewModel.NewRoomDto = roomViewModel.Clone();
        });

        ClearAllCommand = new DelegateCommand(ClearAll);
        DeleteRoomCommand = new DelegateCommand<int>(DeleteRoom);
        FindRoomCommand = new DelegateCommand(RoomsView.Refresh);
        ShowRoomsCommand = new DelegateCommand(RoomsView.Refresh);
        ReserveRoomCommand = new DelegateCommand<RoomViewModel>(ReserveRoom);
        EditReservationRoomCommand = new DelegateCommand<RoomViewModel>(EditReservationRoom);
        AddRoomCommand = new DelegateCommand(DialogHostController.Show<RoomCreatorDialogViewModel>);
        GenerateRoomsCommand = new DelegateCommand(DialogHostController.Show<SettingsGeneratorViewModel>);
        PayRoomCommand = new DelegateCommand<NeedPaymentMessage>(DialogHostController.Show<PayRoomViewModel, NeedPaymentMessage>);

        ShowPayHistoryCommand = new DelegateCommand(() => DialogHostController
            .Show<PayHistoryViewModel, ObservableCollection<PayInformationViewModel>>(FinanceService.GetPayHistory()));
    }

    private void ClearAll()
    {
        DialogHostController.ShowMessageBoxConfirmation(RoomService.ClearAll, "Вы точно хотите удалить все комнаты?");
    }

    private bool FilteringRooms(object obj)
    {
        if (obj is not RoomViewModel roomViewModel)
            throw new AggregateException();

        if ((int)TypeViewRooms != (int)roomViewModel.CurrentState && TypeViewRooms != TypeViewRooms.All)
            return false;

        if (NumberRoomTargetFind is null) return true;

        return roomViewModel.Number == NumberRoomTargetFind;
    }

    private void ReserveRoom(RoomViewModel targetRoomDto)
    {
        var viewModel = DialogHostController.ShowPull<ReserveCreatorDialogViewModel>();
        viewModel.TargetRoomDto = targetRoomDto;
    }

    private void EditReservationRoom(RoomViewModel targetRoomDto)
    {
        var viewModel = DialogHostController.ShowPull<ReserveCreatorDialogViewModel>();
        viewModel.TargetRoomDto = targetRoomDto;
        viewModel.NewPeoples.AddRange(
            ReservationService.GetReservationInfo(reservation => reservation.Peoples, targetRoomDto.Number));
        viewModel.EndData =
            ReservationService.GetReservationInfo(reservation => reservation.EndData, targetRoomDto.Number);
        viewModel.CurrentViewState = DialogViewModelState.Edit;
    }

    private void DeleteRoom(int roomNumber)
    {
        DialogHostController.ShowMessageBoxConfirmation(
            () => RoomService.DeleteRoom(roomNumber),
            $"Вы точно хотите удалить комнату({roomNumber})?");
    }
}