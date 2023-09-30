using System.ComponentModel;
using DevExpress.Mvvm;
using HotelManager.MVVM.Utils;
using HotelManager.MVVM.ViewModels.DialogHostViewModels;
using System.Windows.Input;
using DataContract.Extensions;
using DataContract.ViewModelsDto;

namespace HotelManager.MVVM.ViewModels.PageViewModels;

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

    public ICommand FindRoomCommand { get; }

    public ICommand EditRoomCommand { get; }

    public ICommand ShowRoomsCommand { get; }

    public ICommand DeleteRoomCommand { get; }

    public ICommand ReserveRoomCommand { get; }
    
    public ICommand EditReservationRoomCommand { get; }

    public ICommand GenerateRoomsCommand { get; }


    public int? NumberRoomTargetFind { get; set; }
    public TypeViewRooms TypeViewRooms { get; set; } = TypeViewRooms.All;


    public SystemPageViewModel()
    {
        GenerateRoomsCommand = new DelegateCommand(() => TestService.GenerateTestRooms());

        ReserveRoomCommand = new DelegateCommand<RoomViewModel>(ReserveRoom);
        EditReservationRoomCommand = new DelegateCommand<RoomViewModel>(EditReservationRoom);
        AddRoomCommand = new DelegateCommand(DialogHostController.Show<RoomCreatorDialogViewModel>);
        EditRoomCommand = new DelegateCommand<RoomViewModel>(roomViewModel =>
        {
            var viewModel = DialogHostController.ShowPull<RoomEditorDialogViewModel>();
            viewModel.NewRoomDto = roomViewModel.Clone();
        });

        RoomsView.Filter = FilteringRooms;
        FindRoomCommand = new DelegateCommand(RoomsView.Refresh);
        ShowRoomsCommand = new DelegateCommand(RoomsView.Refresh);
        DeleteRoomCommand = new DelegateCommand<int>(DeleteRoom);
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
    
    private void EditReservationRoom(RoomViewModel targetRoomDto) // TODO: тут ужас конечно, но пок так
    {
        var viewModel = DialogHostController.ShowPull<ReserveCreatorDialogViewModel>();
        viewModel.TargetRoomDto = targetRoomDto;
        viewModel.NewPeoples.AddRange(ReservationService.GetReservationInfo(reservation => reservation.Peoples, targetRoomDto.Number));
        viewModel.EndData = ReservationService.GetReservationInfo(reservation => reservation.EndData, targetRoomDto.Number);
        viewModel.CurrentViewState = DialogViewModelState.Edit;
    }

    private void DeleteRoom(int roomNumber)
    {
        DialogHostController.ShowMessageBoxConfirmation(
            () => RoomService.DeleteRoom(roomNumber),
            $"Вы точно хотите удалить комнату({roomNumber})?");
    }
}