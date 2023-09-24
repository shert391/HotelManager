using FluentValidation;

namespace HotelManager.MVVM.Models.DataContract;

public class ReservationValidator : AbstractValidator<Reservation>
{
    public ReservationValidator()
    {
        RuleFor(reservation => reservation.Peoples).NotNull().NotEmpty().WithMessage("Добавьте жильцов!"); // TODO: Разобраться с валидацией DataTime
    }
}