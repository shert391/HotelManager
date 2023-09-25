using FluentValidation;

namespace HotelManager.MVVM.Models.DataContract;

public class ReservationValidator : AbstractValidator<Reservation>
{
    public ReservationValidator()
    {
        RuleFor(reservation => reservation.EndData).NotNull().NotEmpty().Must(IsDataValid).WithMessage("Дата выселения введена неправильно!");
        RuleFor(reservation => reservation.Peoples).NotNull().NotEmpty().WithMessage("Добавьте хотя бы одного жильца!");
    }
    private bool IsDataValid(DateTime date) => date > DateTime.Now;
}