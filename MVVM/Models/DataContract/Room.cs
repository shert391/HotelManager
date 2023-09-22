using DevExpress.Mvvm;
using FluentValidation;
using HotelManager.InitApp;
using System.ComponentModel;

namespace HotelManager.MVVM.Models.DataContract;

public enum RoomType
{
    [Description("Люкс")]
    Luxury,

    [Description("Двухместный")]
    Double,

    [Description("Одноместный")]
    Single,

    [Description("Полулюкс")]
    SemiLuxury,

    [Description("Двухместный с раскладным диваном")]
    DoubleConvertibleSofa,
}

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
            .WithMessage($"Номер должен быть в диапазоне от {MinNumber} до {MaxNumber}")
            .NotEmpty()
            .WithMessage($"Задайте номер комнаты!");

        RuleFor(room => room.Price)
            .InclusiveBetween(MinPrice, MaxPrice)
            .WithMessage($"Диапазон суточной цены: [{MinPrice};{MaxPrice}]")
            .NotEmpty()
            .WithMessage($"Укажите ежесуточный ценник!");

        RuleFor(room => room.Type).IsInEnum().WithMessage($"Тип комнаты задан неправильно!");
    }
}

public class Room : BindableBase
{
    public int Number { get; set; }

    public decimal Price { get; set; }

    public RoomType Type { get; set; }

    public Room Clone() => new() { Number = Number, Price = Price, Type = Type };
}
