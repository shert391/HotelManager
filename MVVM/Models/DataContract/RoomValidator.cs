using FluentValidation;
using HotelManager.InitApp;

namespace HotelManager.MVVM.Models.DataContract;

public class RoomValidator : AbstractValidator<Room>
{
    public readonly int MinNumber = 1;
    public readonly int MaxNumber = App.GetRoomSetting<int>(nameof(MaxNumber));

    public readonly decimal MinPrice = App.GetRoomSetting<decimal>(nameof(MinPrice));
    public readonly decimal MaxPrice = App.GetRoomSetting<decimal>(nameof(MaxPrice));
    public RoomValidator()
    {
        RuleFor(room => room.Number)
            .InclusiveBetween(MinNumber, MaxNumber)
            .NotEmpty();

        RuleFor(room => room.Price)
            .InclusiveBetween(MinPrice, MaxPrice)
            .NotEmpty();

        RuleFor(room => room.Type).IsInEnum();
    }
}