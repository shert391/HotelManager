using DevExpress.Mvvm;
using HotelManager.MVVM.Models.DataContract;
using HotelManager.MVVM.Utils;
using HotelManager.MVVM.ViewModels.DialogHostViewModels;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;
using DevExpress.Mvvm.Native;
using ReserveRoomDialogViewModelConfiguration =
    HotelManager.MVVM.ViewModels.DialogHostViewModels.ReserveCreatorDialogViewModel.Configuration;

namespace HotelManager.MVVM.ViewModels.PageViewModels;

public enum TypeViewRooms
{
    [Description("Все")] All,

    [Description("Свободные")] Free,

    [Description("Занятые")] Busy,
}

public class SystemPageViewModel : AbstractAppManagerViewModel
{
    public ICommand AddRoomCommand { get; }

    public ICommand FindRoomCommand { get; }

    public ICommand EditRoomCommand { get; }

    public ICommand ShowRoomsCommand { get; }

    public ICommand DeleteRoomCommand { get; }

    public ICommand ReserveRoomCommand { get; }
    
    public ICommand EditReservationRoomCommand { get; }

    public ICommand GenerateRoomsCommand { get; }


    public int? NumberRoomTargetFind { get; set; }
    public TypeViewRooms TypeViewRooms { get; set; }


    public SystemPageViewModel()
    {
        GenerateRoomsCommand = new DelegateCommand(() => TestService.GenerateTestRooms(30, 100, 3000, 100000));

        ReserveRoomCommand = new DelegateCommand<IRoom>(ReserveRoom);
        EditReservationRoomCommand = new DelegateCommand<IRoom>(EditReservationRoom);
        AddRoomCommand = new DelegateCommand(DialogHostController.Show<RoomCreatorDialogViewModel>);
        EditRoomCommand = new DelegateCommand<IRoom>(DialogHostController.Show<RoomEditorDialogViewModel, IRoom>);
        
        FindRoomCommand = new DelegateCommand(ShowRooms);
        ShowRoomsCommand = new DelegateCommand(ShowRooms);
        DeleteRoomCommand = new DelegateCommand<int>(DeleteRoom);
    }

    private void ReserveRoom(IRoom roomViewModel)
    {
        DialogHostController.Show<ReserveCreatorDialogViewModel, ReserveRoomDialogViewModelConfiguration>(new ReserveRoomDialogViewModelConfiguration
        {
            IsStateEditing = false,
            MaxPeoples = roomViewModel.MaxPeoples,
            TargetRoomIndex = roomViewModel.Number
        });
    }
    
    private void EditReservationRoom(IRoom roomViewModel)
    {
        DialogHostController.Show<ReserveCreatorDialogViewModel, ReserveRoomDialogViewModelConfiguration>(new ReserveRoomDialogViewModelConfiguration
        {
            IsStateEditing = true,
            MaxPeoples = roomViewModel.MaxPeoples,
            TargetRoomIndex = roomViewModel.Number,
        });
    }
    
    private void ShowRooms()
    {
        var rooms = RoomService.GetRooms();

        switch (TypeViewRooms)
        {
            case TypeViewRooms.All:
                break;
            case TypeViewRooms.Free:
                rooms = rooms.Where(room => !room.IsReservation).ToReadOnlyCollection();
                break;
            case TypeViewRooms.Busy:
                rooms = rooms.Where(room => room.IsReservation).ToReadOnlyCollection();
                break;
        }
        
        Rooms = NumberRoomTargetFind is null
            ? rooms
            : rooms.Where(room => room.Number == NumberRoomTargetFind).ToReadOnlyCollection();
    }

    private void DeleteRoom(int roomNumber)
    {
        DialogHostController.ShowMessageBoxConfirmation(
            () => RoomService.DeleteRoom(roomNumber),
            $"Вы точно хотите удалить комнату({roomNumber})?");
    }

    protected override void OnRoomCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) => ShowRooms();
}